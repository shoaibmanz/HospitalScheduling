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

        public FrontDeskView(/*string ClerkName*/)
        {
            PatientList = new ObservableCollection<PatientToBeScheduled>();
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

            this.PatientSchedulingGrid.ItemsSource = PatientList;
        }
        

        public string ClerkName { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // collect the patient object the user has selected
            PatientToBeScheduled Patient = ((FrameworkElement)sender).DataContext as PatientToBeScheduled;

            // creating and displaying a new scheduling window based on the object
            Window SchedulingWindow = new PatientScheduling(Patient);

           
            this.Hide();
            SchedulingWindow.ShowDialog();
            this.Show();
        }

        private void ShowUpsOnly_Checked(object sender, RoutedEventArgs e)
        {

            //https://stackoverflow.com/questions/5843537/filtering-datagridview-without-changing-datasource
        }

        private void ShowUpsOnly_Unchecked(object sender, RoutedEventArgs e)
        {

            //https://stackoverflow.com/questions/5843537/filtering-datagridview-without-changing-datasource
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
}
