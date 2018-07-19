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

namespace HospitalScheduling
{
    /// <summary>
    /// Interaction logic for AssignDoctor.xaml
    /// </summary>
    public partial class AssignDoctor : Window
    {
        public AssignDoctor()
        {
            InitializeComponent();
        }

        public AssignDoctor(string clinic , string speciality )
        {

            InitializeComponent();

            txt_Clinic.Text = clinic;
            txt_Speciality.Text = speciality;
        }
    }
}
