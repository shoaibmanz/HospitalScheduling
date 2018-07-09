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
    /// Interaction logic for VisitHistory.xaml
    /// </summary>
    public partial class VisitHistory : Window
    {

        public PatientToBeScheduled CurrentPatient;
        public VisitHistory(PatientToBeScheduled CurrentPatient)
        {
            InitializeComponent();
            this.CurrentPatient = CurrentPatient;
            this.DataContext = CurrentPatient;

            HistoryTable.ItemsSource = CurrentPatient.VisitHistory;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {

            this.Close();

        }
    }
}
