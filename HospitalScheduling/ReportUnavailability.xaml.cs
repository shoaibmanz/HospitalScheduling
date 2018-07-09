using System;
using System.Collections.Generic;
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
    /// Interaction logic for ReportUnavailability.xaml
    /// </summary>
    public partial class ReportUnavailability : Window
    {
        public ReportUnavailability()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            this.Close();

        }

        private void DoesNotRepeat_Checked(object sender, RoutedEventArgs e)//11:50,7/5/2018
        {
            if (DoesNotRepeat.IsChecked == true)
            {
                UnavDailyRadio.IsEnabled = false;
                UnavWeeklyRadio.IsEnabled = false;
                UnavMonthlyRadio.IsEnabled = false;
                UnavDate1.IsEnabled = false;
                UnavRepeatRadio.IsEnabled = false;
                UnavEndsonDate.IsEnabled = false;
                UnavRepeatForTextbox.IsEnabled = false;
                UnavEndsonRadio.IsEnabled = false;
                DailyStack.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (DoesNotRepeat.IsChecked == false)
            {
                UnavDailyRadio.IsEnabled = true;
                UnavWeeklyRadio.IsEnabled = true;
                UnavMonthlyRadio.IsEnabled = true;
                UnavDate1.IsEnabled = true;
                UnavRepeatRadio.IsEnabled = true;
                UnavEndsonDate.IsEnabled = true;
                UnavRepeatForTextbox.IsEnabled = true;
                UnavEndsonRadio.IsEnabled = true;
                if (UnavWeeklyRadio.IsChecked == true)
                {
                    DailyStack.Visibility = System.Windows.Visibility.Visible;
                }
                else if (UnavMonthlyRadio.IsChecked == true)
                {
                    DailyStack.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void UnavWeeklyRadio_Checked(object sender, RoutedEventArgs e)//12:07,7/5/2018
        {
            if (UnavWeeklyRadio.IsChecked == true)
            {
                DailyStack.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                DailyStack.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void UnavMonthlyRadio_Checked(object sender, RoutedEventArgs e)//12:08,7/5/2018
        {
            if (UnavMonthlyRadio.IsChecked == true)
            {
                DailyStack.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                DailyStack.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)//12:10, 7/5/2018
        {
            if (UnavAllDayCheck.IsChecked == true)
            {
                UnavStartTimeTextBox.IsEnabled = false;
                UnavEndTimeTextBox.IsEnabled = false;
            }
            else if (UnavAllDayCheck.IsChecked == false)
            {
                UnavStartTimeTextBox.IsEnabled = true;
                UnavEndTimeTextBox.IsEnabled = true;
            }
        }

        private void UnavDailyRadio_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
