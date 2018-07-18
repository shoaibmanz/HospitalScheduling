using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for FrontDeskView.xaml
    /// </summary>
    public partial class FrontDeskView : Window
    {
        
        public static DataTable OpenList { get; set; }
        public static DataTable Appointments_DT { get; set; }
        public string ClerkName { get; set; }


        public FrontDeskView(/*string ClerkName*/)
        {


            InitializeComponent();

            this.ClerkName = ClerkName;

            // set data source for DataGrids by querying from view model
            #region Setting up DataTable to bind with appointments table
            var Appointments = Query.GetAppointments();

            Appointments_DT = new DataTable();

            Appointments_DT.Columns.Add("ID", typeof(int));
            Appointments_DT.Columns.Add("ChartNumber", typeof(string));
            Appointments_DT.Columns.Add("PatientName", typeof(string));
            Appointments_DT.Columns.Add("DoctorName", typeof(string));
            Appointments_DT.Columns.Add("Specialty", typeof(string));
            Appointments_DT.Columns.Add("NoShowUps", typeof(string));
            Appointments_DT.Columns.Add("PatientStatus", typeof(string));
            Appointments_DT.Columns.Add("Slot", typeof(string));
            Appointments_DT.Columns.Add("DelayedBy", typeof(string));

            Random random = new Random();
            foreach (PatientAppointment Patient in Appointments)
            {
                DataRow NewDR = Appointments_DT.NewRow();

                NewDR["ID"] = Patient.ID;
                NewDR["ChartNumber"] = Patient.PatientInfo.ChartNumber;
                NewDR["PatientName"] = Patient.PatientInfo.Name;
                NewDR["DoctorName"] = Patient.Slot.DoctorInfo.Name;

                NewDR["Specialty"] = Patient.Slot.DoctorInfo.Specialty;

                NewDR["NoShowUps"] = Patient.PatientInfo.NoShowUps.ToString();
                NewDR["PatientStatus"] = Patient.PatientStatus;
                NewDR["Slot"] = Patient.Date.ToString("hh:mm tt");
                NewDR["DelayedBy"] = random.Next(0, 40);

                Appointments_DT.Rows.Add(NewDR);
            }
            this.AppointmentsGrid.DataContext = Appointments_DT.DefaultView;
            #endregion

            // Populate patient scheduling grid with data
            this.PatientSchedulingGrid.ItemsSource = Query.GetToBeScheduledPatients();

            #region Setting up DataTable to bind with OpenList table

            OpenList = new DataTable();

            OpenList.Columns.Add("ID", typeof(int));
            OpenList.Columns.Add("ChartNumber", typeof(string));
            OpenList.Columns.Add("PatientName", typeof(string));
            OpenList.Columns.Add("DoctorName", typeof(string));
            OpenList.Columns.Add("Specialty", typeof(string));
            OpenList.Columns.Add("PatientStatus", typeof(string));
            OpenList.Columns.Add("CheckedInTime", typeof(string));
            OpenList.Columns.Add("WaitTime", typeof(string));
            OpenList.Columns.Add("PatientsAhead", typeof(string));
            
            this.OpenListGrid.DataContext = OpenList.DefaultView;

            #endregion

            // query clinic names
            var ClinicItems = Query.GetClinicNames();

            // query specialty names
            var SpecItems = Query.GetSpecialties();

            // query doctor names
            var DocItems = Query.GetDoctorNames();

            // Set combobox filters item source
            this.ClinicFilterAppointments.ItemsSource = ClinicItems;
            this.SpecialityFilterAppointments.ItemsSource = SpecItems;
            this.DoctorFilterAppointments.ItemsSource = DocItems;

            this.ClinicFilterOpenList.ItemsSource = ClinicItems;
            this.SpecialityFilterOpenList.ItemsSource = SpecItems;
            this.DoctorFilterOpenList.ItemsSource = DocItems;
        }

        // Details button click even handler
        // Opens new window with information regarding patient  
      
        // For Scheduling Grid
        private void DetailsViewSchedulingGrid_Click(object sender, RoutedEventArgs e)
        {   
            
            PatientAppointment SelectedPatient = ((FrameworkElement)sender).DataContext as PatientAppointment;

            // creating and displaying a new scheduling window based on the object selected
            Window SchedulingWindow = new PatientScheduling(SelectedPatient);

            this.Hide();
            SchedulingWindow.ShowDialog();
            this.RefreshGrids();
            this.Show();
        }
     
        // For Appointments Grid
        private void DetailsViewAppointmentsGrid_Click(object sender, RoutedEventArgs e)
        {

            DataRow Selected = (((FrameworkElement)sender).DataContext as DataRowView).Row;

            PatientAppointment SelectedPatient = Query.GetAppointment(Convert.ToInt32(Selected["ID"]));

            // creating and displaying a new scheduling window based on the object selected
            Window SchedulingWindow = new PatientScheduling(SelectedPatient);

            this.Hide();
            SchedulingWindow.ShowDialog();
            this.RefreshGrids();
            this.Show();
        }
        
        private void ShowUpsOnly_Checked(object sender, RoutedEventArgs e)
        {
            DataView dv = AppointmentsGrid.ItemsSource as DataView;
            dv.RowFilter = "[DelayedBy] > 0";
        }

        private void ShowUpsOnly_Unchecked(object sender, RoutedEventArgs e)
        {
            DataView dv = AppointmentsGrid.ItemsSource as DataView;
            dv.RowFilter = null;
        }

        private void AddAppointmentToOpenList(object sender, RoutedEventArgs e)
        {
            
            DataRow Selected = (((FrameworkElement)sender).DataContext as DataRowView).Row;

            PatientAppointment SelectedPatient = Query.GetAppointment(Convert.ToInt32(Selected["ID"]));

            AddToOpenList PopupWindow = new AddToOpenList(SelectedPatient);
            PopupWindow.ShowDialog();

            RefreshGrids();
        }

        private void RemoveFromOpenList(object sender, RoutedEventArgs e)
        {
            DataRow Selected = (((FrameworkElement)sender).DataContext as DataRowView).Row;

            int index = OpenList.Rows.IndexOf(Selected);
            OpenList.Rows.RemoveAt(index);

            RefreshGrids();
        }

        private void SpecialityFilterAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Changed = (sender as ComboBox);

            if ((string)Changed.SelectedItem != null)
            {
                DataView dv = AppointmentsGrid.ItemsSource as DataView;
                dv.RowFilter = String.Format("[Specialty] = '{0}'", (string)Changed.SelectedItem);
            }
        }

        private void SpecialityFilterOpenList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Changed = (sender as ComboBox);

            if ((string)Changed.SelectedItem != null)
            {
                DataView dv = OpenListGrid.ItemsSource as DataView;
                dv.RowFilter = String.Format("Specialty = '{0}'", (string)Changed.SelectedItem);
            }
        }

        private void ClinicFilterOpenList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Clinic does not exist in current grid!!

            //ComboBox Changed = (sender as ComboBox);

            //if ((string)Changed.SelectedItem != null)
            //{
            //    DataView dv = OpenListGrid.ItemsSource as DataView;
            //    dv.RowFilter = String.Format("Clinic = '{0}'", (string)Changed.SelectedItem);
            //}
        }

        private void DoctorFilterOpenList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Changed = (sender as ComboBox);

            if ((string)Changed.SelectedItem != null)
            {
                DataView dv = OpenListGrid.ItemsSource as DataView;
                dv.RowFilter = String.Format("DoctorName = '{0}'", (string)Changed.SelectedItem);
            }
        }

        private void DoctorFilterAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Changed = (sender as ComboBox);

            if ((string)Changed.SelectedItem != null)
            {
                DataView dv = AppointmentsGrid.ItemsSource as DataView;
                dv.RowFilter = String.Format("DoctorName = '{0}'", (string)Changed.SelectedItem);
            }
        }

        private void RefreshGrids()
        {
            this.AppointmentsGrid.DataContext = null;
            this.AppointmentsGrid.DataContext = Appointments_DT.DefaultView;

            this.PatientSchedulingGrid.DataContext = null;
            this.PatientSchedulingGrid.ItemsSource = Query.GetToBeScheduledPatients();

            this.OpenListGrid.DataContext = null;
            this.OpenListGrid.DataContext = OpenList.DefaultView;
        }
        private void ClinicFilterAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }


    public class CheckBoxToVisiblityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (value is bool && (bool)value == true)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }       
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object();
        }
    }

    public class RowBackGroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int && (int)value == 0)
            {
                return Colors.LightGreen;
            }
            return Colors.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object();
        }
    }
}
