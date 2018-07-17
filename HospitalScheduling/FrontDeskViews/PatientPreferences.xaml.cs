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
using TestDesign;

namespace SchedulingSystem
{
    /// <summary>
    /// Interaction logic for PatientPreferences.xaml
    /// </summary>
    public partial class PatientPreferences : Window
    {
        public PatientAppointment CurrentPatient { get; set; }
        public PatientPreferences(PatientAppointment CurrentPatient)
        {

            InitializeComponent();
            this.DataContext = CurrentPatient;
            this.CurrentPatient = CurrentPatient;

            
            this.ComboBox_Clinic.ItemsSource = Query.GetClinicNames();
            this.ComboBox_Doctor.ItemsSource = Query.GetDoctorNames();
            this.ComboBox_Speciality.ItemsSource = Query.GetSpecialties();
        }

        private void Suggest_Click(object sender, RoutedEventArgs e)
        {
            PossibleAppointments OptionsWindow = new PossibleAppointments();
            this.Hide();
            OptionsWindow.ShowDialog();
            this.Close();
        }
    }
}
