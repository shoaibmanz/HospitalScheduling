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
    /// Interaction logic for VisitHistory.xaml
    /// </summary>
    public partial class VisitHistory : Window
    {

        public PatientAppointment CurrentPatient;
        public DataTable HistoryDT;
        public VisitHistory(PatientAppointment CurrentPatient)
        {
            InitializeComponent();
            this.CurrentPatient = CurrentPatient;
            this.DataContext = CurrentPatient;

            var Appointments = Query.GetAppointmentsByChartNumber(CurrentPatient.PatientInfo.ChartNumber);

            HistoryDT = new DataTable();

            HistoryDT.Columns.Add("Date", typeof(string));
            HistoryDT.Columns.Add("Time", typeof(string));
            HistoryDT.Columns.Add("Duration", typeof(string));
            HistoryDT.Columns.Add("Specialty", typeof(string));
            HistoryDT.Columns.Add("Doctor", typeof(string));
            HistoryDT.Columns.Add("Clinic", typeof(string));

            foreach (var Appointment in Appointments)
            {
                DataRow dRow = HistoryDT.NewRow();

                dRow["Date"] = Appointment.Date.ToString("MM/dd/yyyy");
                dRow["Time"] = Appointment.Date.ToString("HH:mm");
                dRow["Duration"] = Appointment.Duration.ToString();
                dRow["Specialty"] = Appointment.Slot.DoctorInfo.Specialty;
                dRow["Doctor"] = Appointment.Slot.DoctorInfo.Name;
                dRow["Clinic"] = Appointment.Slot.ClinicInfo.Name;

                HistoryDT.Rows.Add(dRow);
            }

            HistoryTable.ItemsSource = HistoryDT.DefaultView;
            ComboBox_Clinic.ItemsSource = Query.GetClinicNames();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {

            this.Close();

        }

        private void ComboBox_Clinic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataView dv = HistoryTable.ItemsSource as DataView;
            dv.RowFilter = String.Format("[Clinic] = '{0}'", (string)ComboBox_Clinic.SelectedItem);
        }
    }
}
