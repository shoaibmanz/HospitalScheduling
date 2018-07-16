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

namespace SchedulingSystem
{
    /// <summary>
    /// Interaction logic for PatientCalender.xaml
    /// </summary>
    public partial class PatientCalender : Window
    {
        public DataTable PatientTable;
        public PatientCalender(PatientToBeScheduled Patient)
        {
            InitializeComponent();
            
            PatientTable = new DataTable();
            PatientTable.Columns.Add("Time", typeof(string));

            PatientTable.Columns.Add(Patient.PatientName, typeof(string));

            var TimeStrings = Data.GetTimeStrings(9, 2);

            foreach (string tStr in TimeStrings)
            {
                DataRow dRow = PatientTable.NewRow();
                dRow["Time"] = tStr;
                PatientTable.Rows.Add(dRow);
            }

            this.cb_DaySpeciality.ItemsSource = Data.GetSpecialities();

            dg_dayView.DataContext = PatientTable.DefaultView;
            
            
            
        }
        

        // Adds an item in the specialty list box upon combobox selection
        private void cb_DaySpeciality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {         
            ComboBox SpecBox = (sender as ComboBox);

            if (SpecBox.SelectedItem != null)
            {
                foreach (string item in lb_SelectedSpec.Items)
                {
                    if (item == (string)SpecBox.SelectedItem)
                        return;
                }

                lb_SelectedSpec.Items.Add(SpecBox.SelectedItem);
            }
        }

        // Removes an item from the specialty list box upon right click
        private void MenuItem_Remove_Click(object sender, RoutedEventArgs e)
        {
            
            string ToRemove = (string)(sender as MenuItem).DataContext;

            for (int i = 0; i < lb_SelectedSpec.Items.Count; ++i)
            {
                if (ToRemove == (string)lb_SelectedSpec.Items[i])
                {
                    lb_SelectedSpec.Items.RemoveAt(i);
                    break;
                }
            }

        }
    }
}
