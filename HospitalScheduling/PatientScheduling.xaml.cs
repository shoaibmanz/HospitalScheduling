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
    /// Interaction logic for PatientScheduling.xaml
    /// </summary>
    public partial class PatientScheduling : Window
    {
        public PatientScheduling()
        {
            InitializeComponent();
        }

        private void CurrApointments_Click(object sender, RoutedEventArgs e)
        {

        }

        private void VisitDetails_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class ComboBoxtoVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            ComboBoxItem argument = (ComboBoxItem)value;
            if ((string)argument.Content == "Follow Up") {
                return Visibility.Visible;
            }
            else {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object();
        }
    }
}
