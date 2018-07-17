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
    /// Interaction logic for AddToOpenList.xaml
    /// </summary>
    public partial class AddToOpenList : Window
    {
        public AddToOpenList(PatientAppointment CurrentPatient)
        {
            InitializeComponent();
            this.DataContext = CurrentPatient;

            cb_Clinic.ItemsSource = Query.GetClinicNames();
            cb_Doctor.ItemsSource = Query.GetDoctorNames();
            cb_Specialty.ItemsSource = Query.GetSpecialties();

        }
    }
}
