using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using SchedulingSystem;

namespace HospitalScheduling
{
    /// <summary>
    /// Interaction logic for AssignDoctor.xaml
    /// </summary>

    


    public partial class AssignDoctor : Window
    {
        
        
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<DateTime> TimeList
        {
            get
            {
                return timeList;
            }
            set
            {
                timeList = value;
            }

        }
        public bool AutomaticAppointment { get; set; }
        public int NumberOfDocs { get; set; }
        public ObservableCollection<Doc> AppointedDocs { get; set; }


        private ObservableCollection<Doc> doctors;
        private List<DateTime> timeList = new List<DateTime>()
        {
            new DateTime(2018,7,17,09,00,00),
            new DateTime(2018,7,17,10,00,00),
            new DateTime(2018,7,17,11,00,00),
            new DateTime(2018,7,17,12,00,00),
            new DateTime(2018,7,17,13,00,00),
            new DateTime(2018,7,17,14,00,00),
            new DateTime(2018,7,17,15,00,00),
            new DateTime(2018,7,17,16,00,00),
        };

        public AssignDoctor()
        {
            InitializeComponent();
        }

        public AssignDoctor(string clinic , string speciality , ObservableCollection<Doc> _docs )
        {

            DataContext = this;
            AutomaticAppointment = true;
            btn_manualAppointment = new Button();
            doctors = _docs;
            NumberOfDocs = 1;
            
            InitializeComponent();


            cmb_monthlyDropdownRecurrence.Items[0] = "Monthly on day " + DateTime.Now.Day.ToString();
            cmb_monthlyDropdownRecurrence.Items[1] = "Monthly on " +  GetWeekOfMonth(DateTime.Now) + " " + DateTime.Now.DayOfWeek.ToString(); 

            txt_Clinic.Text = clinic;
            txt_Speciality.Text = speciality;

        }

        private void cbx_AutoAssign_Checked(object sender, RoutedEventArgs e)
        {
            btn_manualAppointment.IsEnabled = false;
        }

        private void cbx_AutoAssign_Unchecked(object sender, RoutedEventArgs e)
        {
            btn_manualAppointment.IsEnabled = true;
        }

        private void rbtn_Weekly_Checked(object sender, RoutedEventArgs e)
        {
            grid_MonthlyDropdown.Visibility = Visibility.Hidden;
            grid_WeeklyCheckboxes.Visibility = Visibility.Visible;
        }

        private void rbtn_Monthly_Checked(object sender, RoutedEventArgs e)
        {
            grid_MonthlyDropdown.Visibility = Visibility.Visible;
            grid_WeeklyCheckboxes.Visibility = Visibility.Hidden;
        }

        private void rbtn_Daily_Checked(object sender, RoutedEventArgs e)
        {
            grid_MonthlyDropdown.Visibility = Visibility.Hidden;
            grid_WeeklyCheckboxes.Visibility = Visibility.Hidden;
        }

        private void btn_manualAppointment_Click(object sender, RoutedEventArgs e)
        {
            

            AssignDoctor_Manual window = new AssignDoctor_Manual(doctors,NumberOfDocs );
            window.ShowDialog();

            // get appointed doctors
            AppointedDocs = window.doctorsRight;

        }

        public static int GetWeekOfMonth(DateTime date)
        {
            DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);

            while (date.Date.AddDays(1).DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(1);

            return (int)Math.Truncate((double)date.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
        }

        private void Confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
