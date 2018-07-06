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

            PatientList.Add(new PatientToBeScheduled()
            {
                ChartNumber = "1224124",
                PatientName = "Ruben",
                AccidentDate = "11-02-2017",
                InsuranceName = "XYZ",
                CaseType = "NoFault",
                CaseStatus = "Open",
                AttorneyName = "JKL",
                PatientStatus = "Walk in",
            });

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
            });

            this.PatientSchedulingGrid.ItemsSource = PatientList;
            

            DataGridTemplateColumn ButtonColumn = new DataGridTemplateColumn();

            ButtonColumn = (DataGridTemplateColumn)this.Resources["ButtonColumn"];
            
            this.PatientSchedulingGrid.Columns.Add(ButtonColumn);
        }
        
        public string ClerkName { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class PatientToBeScheduled
    {
        public PatientToBeScheduled() {

        }

        public string ChartNumber { get; set; }

        public string PatientName { get; set; }

        public string AccidentDate { get; set; }
        
        public string InsuranceName { get; set; }

        public string CaseType { get; set; }

        public string CaseStatus { get; set; }

        public string AttorneyName { get; set; }

        public string PatientStatus { get; set; }
    }
}
