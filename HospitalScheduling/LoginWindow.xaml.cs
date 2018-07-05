using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchedulingSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            RoleCombobox.Items.Add("Supervisor");
            RoleCombobox.Items.Add("Doctor");
            RoleCombobox.Items.Add("Front Desk");

            RoleCombobox.SelectedIndex = 0;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // login button event handler

            // TODO: check login credentials from stored username/ passwords
            
            // check to see which role was selected to select next screen
            switch ((string)RoleCombobox.SelectedValue)
            {
                case "Supervisor":
                   
                    break;
                case "Doctor":

                    break;
                case "Front Desk":
                    FrontDeskView NewWindow = new FrontDeskView(LoginBox.Text);
                    NewWindow.Show();
                    this.Close();
                    break;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // cancel button pressed
            // close current window
            this.Close();
        }
    }
}
