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
    public partial class PatientCalender : Window
    {
        private DataTable PatientTable;
        private List<PatientAppointment> patientAppointments;
        private List<SpecialtyColor> Specialties;
        public PatientAppointment CurrentPatient { get; set; }
        public DateTime CurrentDate { get; set; }
        

        public PatientCalender(PatientAppointment CurrentPatient)
        {
            InitializeComponent();
            this.DataContext = this;
            this.CurrentDate = CurrentPatient.Date;
            this.CurrentPatient = CurrentPatient;

            PatientTable = new DataTable();
            PatientTable.Columns.Add("Time", typeof(string));
            PatientTable.Columns.Add("Schedule", typeof(string));

            DateTime now = DateTime.Now;
            DateTime CurrentTime = new DateTime(now.Year, now.Month, now.Day, 9, 0, 0);

            do
            {
                DataRow dRow = PatientTable.NewRow();

                dRow["Time"] = CurrentTime.ToString("hh:mm tt");

                PatientTable.Rows.Add(dRow);

                CurrentTime = CurrentTime.AddMinutes(15);
            } while (CurrentTime.Hour != 14);

            patientAppointments = Query.GetAppointmentsByChartNumber(CurrentPatient.PatientInfo.ChartNumber);


            Color[] colors = { Colors.Red, Colors.Pink, Colors.LightBlue, Colors.GreenYellow, Colors.ForestGreen, Colors.Yellow, Colors.LightCyan, Colors.LightGray };

            int i = 0;
            Specialties = Query.GetSpecialties().Select(Spec => new SpecialtyColor(Spec, colors[i++])).ToList();
            this.cb_DaySpeciality.ItemsSource = Specialties;

            dg_dayView.DataContext = PatientTable.DefaultView;
        }
        
        // Adds an item in the specialty list box upon combobox selection
        private void cb_DaySpeciality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {         
            ComboBox SpecBox = (sender as ComboBox);

            if (SpecBox.SelectedItem != null)
            {
                // do not allow the same item to be selected twice
                foreach (SpecialtyColor item in lb_SelectedSpec.Items)
                {
                    if (item.Specialty == SpecBox.SelectedItem.ToString())
                        return;
                }

                // add the new specialty to the list box
                lb_SelectedSpec.Items.Add(Specialties.Find(Item => Item.Specialty == SpecBox.SelectedItem.ToString()));

                // populate the grid with this specialty
                PopulateWithSpecialty(SpecBox.SelectedItem.ToString());

                dg_dayView.DataContext = null;
                dg_dayView.DataContext = PatientTable.DefaultView;
            }
        }
        private void PopulateWithSpecialty(string Specialty)
        {
            // find all appointments belonging to this specialty and add into the table
            foreach (PatientAppointment Appointment in patientAppointments)
            {
                if (Appointment.Date.ToString("MMMM dd, yyyy") == CurrentDate.ToString("MMMM dd, yyyy") && Appointment.Slot.DoctorInfo.Specialty == Specialty)
                {
                    // this appointment should be filled in
                    // find the slot time it should occupy
                    for (int i = 0; i < PatientTable.Rows.Count; ++i)
                    {
                        if ((string)PatientTable.Rows[i][0] == Appointment.Date.ToString("hh:mm tt"))
                        {
                            PatientTable.Rows[i]["Schedule"] = Appointment.Slot.DoctorInfo.Name + " (" + Appointment.Slot.ClinicInfo.Name + ")";
                            break;
                        }
                    }
                }
            }
        }

        // Removes an item from the specialty list box upon right click
        private void MenuItem_Remove_Click(object sender, RoutedEventArgs e)
        {
            
            string ToRemove = (sender as MenuItem).DataContext.ToString();

            for (int i = 0; i < lb_SelectedSpec.Items.Count; ++i)
            {
                if (ToRemove == ((SpecialtyColor)lb_SelectedSpec.Items[i]).Specialty)
                {
                    lb_SelectedSpec.Items.RemoveAt(i);
                    break;
                }
            }
        }

        private void dg_dayView_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock Text = new TextBlock();
            Text.HorizontalAlignment = HorizontalAlignment.Center;
            Text.Text = "Patient " + CurrentPatient.PatientInfo.Name;

            dg_dayView.Columns[1].Header = Text;
        }

        private void btn_prevDay_Clicked(object sender, RoutedEventArgs e)
        {
            CurrentDate = CurrentDate.AddDays(-1);
            DateText.Text = CurrentDate.ToString("MMMM dd, yyyy");

            for (int i = 0; i < PatientTable.Rows.Count; ++i)
            {
                var row = PatientTable.Rows[i];
                row["Schedule"] = "";
            }

            for (int i = 0; i < lb_SelectedSpec.Items.Count; ++i)
            {
                string specialty = ((SpecialtyColor)lb_SelectedSpec.Items[i]).Specialty;

                PopulateWithSpecialty(specialty);
            }
            dg_dayView.DataContext = null;
            dg_dayView.DataContext = PatientTable.DefaultView;
        }

        private void btn_nextDay_Clicked(object sender, RoutedEventArgs e)
        {
            CurrentDate = CurrentDate.AddDays(1);
            DateText.Text = CurrentDate.ToString("MMMM dd, yyyy");

            // clearing out all previous fields in the table
            for (int i = 0; i < PatientTable.Rows.Count; ++i)
            {
                var row = PatientTable.Rows[i];
                row["Schedule"] = "";
            }

            for (int i = 0; i < lb_SelectedSpec.Items.Count; ++i)
            {
                string specialty = ((SpecialtyColor)lb_SelectedSpec.Items[i]).Specialty;

                PopulateWithSpecialty(specialty);
            }

            dg_dayView.DataContext = null;
            dg_dayView.DataContext = PatientTable.DefaultView;
        }
    }
    public class SpecialtyColor
    {
        public string Specialty { get; set; }
        public Brush Color { get; set; }
        public SpecialtyColor(string Specialty, Color color)
        {
            this.Specialty = Specialty;
            this.Color = new SolidColorBrush(color);
        }

        public override string ToString()
        {
            return Specialty;
        }
    }
    
}
