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
        
        ObservableCollection<PatientAppointment> OpenList;
        DataTable Appointments_DT;
        public string ClerkName { get; set; }


        public FrontDeskView(/*string ClerkName*/)
        {

            OpenList = new ObservableCollection<PatientAppointment>();

            InitializeComponent();

            this.ClerkName = ClerkName;

            // set data source for DataGrids by querying from view model

            #region Setting up DataTable to bind with appointments table
            var Appointments = Query.GetAppointments();

            Appointments_DT = new DataTable();

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

                NewDR["ChartNumber"] = Patient.PatientInfo.ChartNumber;
                NewDR["PatientName"] = Patient.PatientInfo.Name;
                NewDR["DoctorName"] = Patient.Slot.DoctorInfo.Name;

                NewDR["Specialty"] = Patient.Slot.DoctorInfo.Specialty;

                NewDR["NoShowUps"] = Patient.PatientInfo.NoShowUps.ToString();
                NewDR["PatientStatus"] = Patient.PatientStatus;
                NewDR["Slot"] = Patient.Date.ToString("G");
                NewDR["DelayedBy"] = random.Next(0, 40);
            }
            #endregion
            this.AppointmentsGrid.DataContext = Appointments_DT.DefaultView;
            this.PatientSchedulingGrid.ItemsSource = Query.GetToBeScheduledPatients();

            this.OpenListGrid.ItemsSource = OpenList;

            // query clinic names
            var ClinicItems = Query.GetClinicNames();
            // query specialty names
            var SpecItems = Query.GetSpecialties();

            // Set combobox filters item source
            this.ClinicFilterAppointments.ItemsSource = ClinicItems;
            this.SpecialityFilterAppointments.ItemsSource = SpecItems;

            this.ClinicFilterOpenList.ItemsSource = ClinicItems;
            this.SpecialityFilterOpenList.ItemsSource = SpecItems;            
        }
        
        // Details button click even handler
        // Opens new window with information regarding patient
    
        private void DetailsView_Click(object sender, RoutedEventArgs e)
        {       
            PatientAppointment SelectedPatient = ((FrameworkElement)sender).DataContext as PatientAppointment;

            // creating and displaying a new scheduling window based on the object selected
            Window SchedulingWindow = new PatientScheduling(SelectedPatient);

            this.Hide();
            SchedulingWindow.ShowDialog();
            this.Show();
        }

        private void ShowUpsOnly_Checked(object sender, RoutedEventArgs e)
        {
            
            AppointmentsGrid.ItemsSource = Appointments_DT.Select("[DelayedBy] > 0");            
        }

        private void ShowUpsOnly_Unchecked(object sender, RoutedEventArgs e)
        {
            AppointmentsGrid.ItemsSource = Appointments_DT.Rows;
        }

        private void AddAppointmentToOpenList(object sender, RoutedEventArgs e)
        {
            PatientAppointment Patient = ((FrameworkElement)sender).DataContext as PatientAppointment;

            AddToOpenList PopupWindow = new AddToOpenList(Patient);
            PopupWindow.ShowDialog();
        }

        private void RemoveFromOpenList(object sender, RoutedEventArgs e)
        {
            PatientAppointment ToRemove = ((FrameworkElement)sender).DataContext as PatientAppointment;
            
            foreach(PatientAppointment appointment in OpenList)
            {
                if (ToRemove.PatientInfo.ChartNumber == appointment.PatientInfo.ChartNumber)
                {
                    OpenList.Remove(appointment);
                    break;
                }
            }
        }

        private void SpecialityFilterAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Changed = (sender as ComboBox);

            if ((string)Changed.SelectedItem != "Any")
            {
                AppointmentsGrid.Items.Filter = new Predicate<object>(item => ((Appointment)item).Speciality == (string)Changed.SelectedItem);
            }
            else
            {
                AppointmentsGrid.Items.Filter = null;
            }
        }

        private void SpecialityFilterOpenList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Changed = (sender as ComboBox);

            if ((string)Changed.SelectedItem != "Any")
            {
                OpenListGrid.Items.Filter = new Predicate<object>(item => ((Appointment)item).Speciality == (string)Changed.SelectedItem);
            }
            else
            {
                OpenListGrid.Items.Filter = null;
            }
        }

        private void ClinicFilterOpenList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Changed = (sender as ComboBox);

            if ((string)Changed.SelectedItem != "Any")
            {
                OpenListGrid.Items.Filter = new Predicate<object>(item => ((Appointment)item).ClinicName == (string)Changed.SelectedItem);
            }
            else
            {
                OpenListGrid.Items.Filter = null;
            }
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
