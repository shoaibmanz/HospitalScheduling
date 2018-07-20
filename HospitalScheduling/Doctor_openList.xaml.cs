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
    /// Interaction logic for Doctor_openList.xaml
    /// </summary>
    public partial class Doctor_openList : Window
    {

        ObservableCollection<Appointment> OpenList;
        public Doctor_openList()
        {
            InitializeComponent();

            OpenList = new ObservableCollection<Appointment>();

            OpenList.Add(new Appointment()
            {
                ChartNumber = "6434233",
                PatientName = "Kurt",
                DoctorName = "Dr. Adam",
                Speciality = "Medical",
                NoShowUps = 2,
                PatientStatus = "Checked in",
                SlotTime = "09:00 AM",
                DelayedBy = 45,
                PatientsAhead = 0
            });

            OpenList.Add(new Appointment()
            {
                ChartNumber = "6434453",
                PatientName = "David",
                DoctorName = "Any",
                Speciality = "Medical",
                NoShowUps = 2,
                PatientStatus = "Checked in",
                SlotTime = "09:45 AM",
                DelayedBy = 15,
                PatientsAhead = 1
            });

            OpenList.Add(new Appointment()
            {
                ChartNumber = "6453433",
                PatientName = "John",
                DoctorName = "Dr. Azeem",
                Speciality = "PT",
                NoShowUps = 2,
                PatientStatus = "Checked in",
                SlotTime = "09:30 AM",
                DelayedBy = 30,
                PatientsAhead = 1
            });

            OpenList.Add(new Appointment()
            {
                ChartNumber = "343234233",
                PatientName = "Richard",
                DoctorName = "Dr. Hattab",
                Speciality = "ACCU",
                NoShowUps = 2,
                PatientStatus = "Offline",
                SlotTime = "08:30 AM",
                DelayedBy = 90,
                PatientsAhead = 10
            });

            this.OpenListGrid.ItemsSource = OpenList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RemoveFromOpenList(object sender, RoutedEventArgs e)
        {
            Appointment ToRemove = ((FrameworkElement)sender).DataContext as Appointment;

            foreach (Appointment appointment in OpenList)
            {
                if (ToRemove.ChartNumber == appointment.ChartNumber)
                {
                    OpenList.Remove(appointment);
                    break;
                }
            }
        }
    }
}
