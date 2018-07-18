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
using TestDesign;

namespace SchedulingSystem
{
    /// <summary>
    /// Interaction logic for PatientCalender.xaml
    /// </summary>
    public partial class PatientCalender : Window
    {
        public DataTable PatientTable;

        public List<PatientAppointment> patientAppointments;
        public PatientAppointment CurrentPatient;
        public PatientCalender(PatientAppointment CurrentPatient)
        {
            InitializeComponent();
            this.DataContext = CurrentPatient;
            this.CurrentPatient = CurrentPatient;

            PatientTable = new DataTable();
            PatientTable.Columns.Add("Time", typeof(string));
            PatientTable.Columns.Add("Schedule", typeof(string));

            DateTime now = DateTime.Now;
            DateTime CurrentTime = new DateTime(now.Year, now.Month, now.Day, 9, 0, 0);

            while (CurrentTime.Hour < 14)
            {
                DataRow dRow = PatientTable.NewRow();

                dRow["Time"] = CurrentTime.ToString("hh:mm tt");

                PatientTable.Rows.Add(dRow);

                CurrentTime = CurrentTime.AddMinutes(15);
            }


            patientAppointments = new List<PatientAppointment>();
            patientAppointments = Query.GetAppointments(CurrentPatient.PatientInfo.ChartNumber);

            this.cb_DaySpeciality.ItemsSource = Query.GetSpecialties();

            dg_dayView.DataContext = PatientTable.DefaultView;
        }
        

        // Adds an item in the specialty list box upon combobox selection
        private void cb_DaySpeciality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {         
            ComboBox SpecBox = (sender as ComboBox);

            if (SpecBox.SelectedItem != null)
            {
                // do not allow the same item to be selected twice
                foreach (string item in lb_SelectedSpec.Items)
                {
                    if (item == (string)SpecBox.SelectedItem)
                        return;
                }

                // add the new specialty to the list box
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

        private void dg_dayView_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock Text = new TextBlock();
            Text.HorizontalAlignment = HorizontalAlignment.Center;
            Text.Text = "Patient " + CurrentPatient.PatientInfo.Name;

            dg_dayView.Columns[1].Header = Text;
        }
    }
}
