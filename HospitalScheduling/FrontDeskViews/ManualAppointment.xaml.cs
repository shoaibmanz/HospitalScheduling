using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestDesign;

namespace SchedulingSystem
{
    /// <summary>
    /// Interaction logic for PatientCalender.xaml
    /// </summary>
    public partial class ManualAppointment : Window
    {
        private DataTable PatientTable;
        private List<PatientAppointment> patientAppointments;

        public PatientAppointment CurrentPatient { get; set; }
        public DateTime CurrentDate { get; set; }

        public Dictionary<string, List<PatientAppointment>> DocAppointments;

        public ManualAppointment(PatientAppointment CurrentPatient)
        {
            InitializeComponent();
            this.DataContext = this;
            this.CurrentDate = CurrentPatient.Date;
            this.CurrentPatient = CurrentPatient;

            DocAppointments = new Dictionary<string, List<PatientAppointment>>();


            #region Setup DataTable with time and patient schedule field

            PatientTable = new DataTable();
            PatientTable.Columns.Add("Time", typeof(string));
            PatientTable.Columns.Add("Patient " + CurrentPatient.PatientInfo.Name, typeof(string));

            DateTime now = DateTime.Now;
            DateTime CurrentTime = new DateTime(now.Year, now.Month, now.Day, 9, 0, 0);

            do
            {
                DataRow dRow = PatientTable.NewRow();

                dRow["Time"] = CurrentTime.ToString("hh:mm tt");

                PatientTable.Rows.Add(dRow);

                CurrentTime = CurrentTime.AddMinutes(15);
            } while (CurrentTime.Hour != 14);

            // bind to DataGrid
            dg_dayView.DataContext = PatientTable.DefaultView;

            #endregion

            // populate patient appointments list
            patientAppointments = Query.GetAppointmentsByChartNumber(CurrentPatient.PatientInfo.ChartNumber);

            this.cb_DayClinic.ItemsSource = Query.GetClinicNames();
            this.cb_DayDoctor.ItemsSource = Query.GetDoctorNames();
            this.cb_DaySpec.ItemsSource = Query.GetSpecialties();
        }

        private void cb_DaySpec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // Adds an item in the clinic list box upon combobox selection
        private void cb_DayClinic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox SpecBox = (sender as ComboBox);

            if (SpecBox.SelectedItem != null)
            {
                // do not allow the same item to be selected twice
                foreach (string item in lb_SelectedClinic.Items)
                {
                    if (item == (string)SpecBox.SelectedItem)
                        return;
                }

                // add the new  to the list box
                lb_SelectedClinic.Items.Add(SpecBox.SelectedItem);

                // populate the grid with this specialty
                PopulateWithClinic((string)SpecBox.SelectedItem);

                dg_dayView.DataContext = null;
                dg_dayView.DataContext = PatientTable.DefaultView;
            }
        }

        private void PopulateWithClinic(string ClinicName)
        {
            // find all appointments belonging to this specialty and add into the table
            foreach (PatientAppointment Appointment in patientAppointments)
            {
                if (Appointment.Date.ToString("MMMM dd, yyyy") == CurrentDate.ToString("MMMM dd, yyyy") && Appointment.Slot.ClinicInfo.Name == ClinicName)
                {
                    // this appointment should be filled in
                    // find the slot time it should occupy
                    for (int i = 0; i < PatientTable.Rows.Count; ++i)
                    {
                        if ((string)PatientTable.Rows[i]["Time"] == Appointment.Date.ToString("hh:mm tt"))
                        {
                            PatientTable.Rows[i][1] = Appointment.Slot.DoctorInfo.Name + " (" + Appointment.Slot.ClinicInfo.Name + ")";
                            break;
                        }
                    }
                }
            }

            for (int i = 2; i < PatientTable.Columns.Count; ++i)
            {

            }

        }

        // Removes an item from the specialty list box upon right click
        private void ClinicMenuItem_Remove_Click(object sender, RoutedEventArgs e)
        {

            string ToRemove = (string)(sender as MenuItem).DataContext;

            for (int i = 0; i < lb_SelectedClinic.Items.Count; ++i)
            {
                if (ToRemove == (string)lb_SelectedClinic.Items[i])
                {
                    lb_SelectedClinic.Items.RemoveAt(i);
                    break;
                }
            }
        }

        // Removes an item from the doctor list box upon right click
        private void MenuItemDoctor_Remove_Click(object sender, RoutedEventArgs e)
        {

            string ToRemove = (string)(sender as MenuItem).DataContext;

            for (int i = 0; i < lb_SelectedDoctor.Items.Count; ++i)
            {
                if (ToRemove == (string)lb_SelectedDoctor.Items[i])
                {
                    lb_SelectedDoctor.Items.RemoveAt(i);
                    PatientTable.Columns.Remove(ToRemove);
                    break;
                }
            }
            PatientTable.AcceptChanges();

            dg_dayView.DataContext = null;
            dg_dayView.DataContext = PatientTable.DefaultView;
        }

        private void cb_DayDoctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox SpecBox = (sender as ComboBox);

            if (SpecBox.SelectedItem != null)
            {
                // do not allow the same item to be selected twice
                foreach (string item in lb_SelectedDoctor.Items)
                {
                    if (item == (string)SpecBox.SelectedItem)
                        return;
                }

                // add the new  to the list box
                lb_SelectedDoctor.Items.Add(SpecBox.SelectedItem);

                // populate the grid with this specialty
                PopulateWithDoctor((string)SpecBox.SelectedItem);

                dg_dayView.DataContext = null;
                dg_dayView.DataContext = PatientTable.DefaultView;
            }
        }

        void PopulateWithDoctor(string DocName)
        {
            // fetch a list containing every appointment this doctor has
            var Appointments = Query.GetAppointmentsByDoctor(DocName);

            DocAppointments.Add(DocName, Appointments);

            PatientTable.Columns.Add(DocName, typeof(string));

            foreach (var Appointment in Appointments)
            {

                if (Appointment.Date == CurrentDate)
                {
                    for (int i = 0; i < PatientTable.Rows.Count; ++i)
                    {
                        DataRow dRow = PatientTable.Rows[i];

                        if (Appointment.Date.ToString("hh:mm tt") == (string)dRow["Time"])
                        {
                            dRow[DocName] = Appointment.PatientInfo.Name;
                            break;
                        }
                    }
                }
            }
                
            dg_dayView.DataContext = null;
            dg_dayView.DataContext = PatientTable.DefaultView;
        }

        private void dg_dayView_Loaded(object sender, RoutedEventArgs e)
        {
            //TextBlock Text = new TextBlock();
            //Text.HorizontalAlignment = HorizontalAlignment.Center;
            //Text.Text = "Patient " + CurrentPatient.PatientInfo.Name;

            //dg_dayView.Columns[1].Header = Text;
        }

        private void btn_prevDay_Clicked(object sender, RoutedEventArgs e)
        {
            CurrentDate = CurrentDate.AddDays(-1);
            DateText.Text = CurrentDate.ToString("MMMM dd, yyyy");

            for (int i = 0; i < PatientTable.Rows.Count; ++i)
            {
                var row = PatientTable.Rows[i];
                row["Patient " + CurrentPatient.PatientInfo.Name] = "";
            }

            //for (int i = 0; i < lb_SelectedSpec.Items.Count; ++i)
            //{
            //    string specialty = (string)lb_SelectedSpec.Items[i];

            //    PopulateWithSpecialty(specialty);
            //}
            //dg_dayView.DataContext = null;
            //dg_dayView.DataContext = PatientTable.DefaultView;
        }

        private void btn_nextDay_Clicked(object sender, RoutedEventArgs e)
        {
            CurrentDate = CurrentDate.AddDays(1);
            DateText.Text = CurrentDate.ToString("MMMM dd, yyyy");

            for (int i = 0; i < PatientTable.Rows.Count; ++i)
            {
                var row = PatientTable.Rows[i];
                row["Patient " + CurrentPatient.PatientInfo.Name] = "";
            }

            //for (int i = 0; i < lb_SelectedSpec.Items.Count; ++i)
            //{
            //    string specialty = (string)lb_SelectedSpec.Items[i];

            //    PopulateWithSpecialty(specialty);
            //}
            //dg_dayView.DataContext = null;
            //dg_dayView.DataContext = PatientTable.DefaultView;
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
