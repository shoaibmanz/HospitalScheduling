using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SchedulingSystem
{
    /// <summary>
    /// Interaction logic for FrontDeskView.xaml
    /// </summary>
    public partial class FrontDeskView : Window
    {

        ObservableCollection<PatientToBeScheduled> PatientList;
        ObservableCollection<Appointment> Appointments;

        public FrontDeskView(/*string ClerkName*/)
        {
            PatientList = new ObservableCollection<PatientToBeScheduled>();
            Appointments = new ObservableCollection<Appointment>();

            InitializeComponent();

            this.ClerkName = ClerkName;


            #region Creating Dummy Data

            PatientToBeScheduled P1 = new PatientToBeScheduled()
            {
                ChartNumber = "1224124",
                PatientName = "Ruben",
                AccidentDate = "11-02-2017",
                InsuranceName = "XYZ",
                CaseType = "NoFault",
                CaseStatus = "Open",
                AttorneyName = "JKL",
                PatientStatus = "Walk in",
                OldNew = true
            };

            Appointments.Add(new Appointment()
            {
                ChartNumber = "12321423",
                PatientName = "Ruben",
                DoctorName = "Dr. Adam",
                Speciality = "Medical",
                NoShowUps = 0,
                PatientStatus = "Checked in",
                SlotTime = "10:45 AM",
                DelayedBy = 0,
                SchedulingInfo = P1

            });

            Appointments.Add(new Appointment()
            {
                ChartNumber = "64321423",
                PatientName = "Marina",
                DoctorName = "Dr. Adam",
                Speciality = "Medical",
                NoShowUps = 2,
                PatientStatus = "Checked in",
                SlotTime = "09:00 AM",
                DelayedBy = 15,
                SchedulingInfo = P1
            }); 

            Appointments.Add(new Appointment()
            {
                ChartNumber = "65321423",
                PatientName = "David",
                DoctorName = "Dr. Azeem",
                Speciality = "PT",
                NoShowUps = 1,
                PatientStatus = "Offline",
                SlotTime = "09:30 AM",
                DelayedBy = 0,
                SchedulingInfo = P1
            });

            Appointments.Add(new Appointment()
            {
                ChartNumber = "12321423",
                PatientName = "Thomas",
                DoctorName = "Dr. Hattab",
                Speciality = "ACCU",
                NoShowUps = 2,
                PatientStatus = "Offline",
                SlotTime = "08:30 AM",
                DelayedBy = 45,
                SchedulingInfo = P1

            });


            P1.Insurance.Add(new InsuranceRecord()
            {
                Speciality = "Medical",
                VisitsRemaining = 3
            });

            P1.Insurance.Add(new InsuranceRecord()
            {
                Speciality = "ACCU",
                VisitsRemaining = 0
            });

            P1.Insurance.Add(new InsuranceRecord()
            {
                Speciality = "Physician",
                VisitsRemaining = 1
            });

            P1.VisitHistory.Add(new Visit()
            {
                Date = "12-01-2017",
                Time = "9:00am",
                Duration = 15,
                Speciality = "Medical",
                DoctorName = "Dr. Adam",
                ClinicName = "Gun Hill",
            });

            P1.VisitHistory.Add(new Visit()
            {
                Date = "12-06-2016",
                Time = "11:00am",
                Duration = 30,
                Speciality = "Medical",
                DoctorName = "Dr. Steele",
                ClinicName = "Jamaica",
            });
            P1.VisitHistory.Add(new Visit()
            {
                Date = "12-9-2017",
                Time = "3:00pm",
                Duration = 15,
                Speciality = "Medical",
                DoctorName = "Dr. Adam",
                ClinicName = "Gun Hill",
            });
            P1.VisitHistory.Add(new Visit()
            {
                Date = "12-01-2018",
                Time = "12:00am",
                Duration = 15,
                Speciality = "Medical",
                DoctorName = "Dr. Steele",
                ClinicName = "ABC",
            });

            PatientList.Add(P1);


            PatientList.Add(new PatientToBeScheduled()
            {
                ChartNumber = "424124",
                PatientName = "Marina",
                AccidentDate = "12-14-2017",
                InsuranceName = "XYZ",
                CaseType = "NoFault",
                CaseStatus = "Open",
                AttorneyName = "JKL",
                PatientStatus = "Follow Up",
                OldNew = true
            });


            PatientList.Add(new PatientToBeScheduled()
            {
                ChartNumber = "231432",
                PatientName = "Shoaib",
                AccidentDate = "12-06-2017",
                InsuranceName = "XYZ",
                CaseType = "NoFault",
                CaseStatus = "Open",
                AttorneyName = "JKL",
                PatientStatus = "Rescheduled",
                OldNew = false
            });
            #endregion

            this.AppointmentsGrid.ItemsSource = Appointments;
            this.PatientSchedulingGrid.ItemsSource = PatientList;
        }
        

        public string ClerkName { get; set; }

        private void DetailsView_Click(object sender, RoutedEventArgs e)
        {
            PatientToBeScheduled Patient = new PatientToBeScheduled();

            // collect the patient object the user has selected
            if (((FrameworkElement)sender).DataContext is PatientToBeScheduled)
            {
                Patient = ((FrameworkElement)sender).DataContext as PatientToBeScheduled;
            }
            else if (((FrameworkElement)sender).DataContext is Appointment)
            {
                Patient = (((FrameworkElement)sender).DataContext as Appointment).SchedulingInfo;
            } 

            // creating and displaying a new scheduling window based on the object
            Window SchedulingWindow = new PatientScheduling(Patient);

            this.Hide();
            SchedulingWindow.ShowDialog();
            this.Show();
        }

        private void ShowUpsOnly_Checked(object sender, RoutedEventArgs e)
        {
            AppointmentsGrid.Items.Filter = new Predicate<object>(item => ((Appointment)item).DelayedBy > 0);
        }

        private void ShowUpsOnly_Unchecked(object sender, RoutedEventArgs e)
        {
            AppointmentsGrid.Items.Filter = null;
        }

        private void AddAppointmentToOpenList(object sender, RoutedEventArgs e)
        {
            Appointment Patient = ((FrameworkElement)sender).DataContext as Appointment;

            PatientToBeScheduled ToAdd = new PatientToBeScheduled();
            ToAdd.PatientName = Patient.PatientName;
            ToAdd.OldNew = false;

            AddToOpenList PopupWindow = new AddToOpenList(ToAdd);
            PopupWindow.ShowDialog();

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
