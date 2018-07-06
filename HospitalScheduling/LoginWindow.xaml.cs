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
    public partial class Clinic
    {
        public string name;
        public string address;
        public string phone;
        public string email;

        public Clinic(string name, string address, string phone, string email)
        {
            this.name = name;
            this.address = address;
            this.email = email;
            this.phone = phone;

        }
    };

    public partial class Doc
    {
        public string name { get; set; }
        public string address { get; set; }

        public string email { get; set; }
        public string phone { get; set; }

        public Doc(string name, string address, string email, string phone)
        {
            this.address = address;
            this.email = email;
            this.name = name;
            this.phone = phone;
        }
    };

    static class Global
    {

        public static List<Clinic> Clinic_list;

        public static List<Doc> Docs;

        /*three arguments to send data between windows*/
        public static string arg1;
        public static string arg2;
        public static string arg3;

        //status bool
        public static bool status;
    }

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            RoleCombobox.Items.Add("Supervisor");
            RoleCombobox.Items.Add("Doctor");
            RoleCombobox.Items.Add("Front Desk");

            RoleCombobox.SelectedIndex = 0;

            Global.Clinic_list = new List<Clinic>();
            Global.Docs = new List<Doc>();
            Global.arg1 = Global.arg2 = Global.arg3 = "";
            Global.status = false;

            Global.Docs.Add(new Doc("John", "", "", ""));
            Global.Docs.Add(new Doc("Shoaib", "", "", ""));
            Global.Docs.Add(new Doc("Ahsan", "", "", ""));
            Global.Docs.Add(new Doc("Haris", "", "", ""));
            Global.Docs.Add(new Doc("Tom", "", "", ""));


            Global.Clinic_list.Add(new Clinic("clinic1", "Islamabad", "051-231256", "infoclinic1.com"));
            Global.Clinic_list.Add(new Clinic("clinic2", "Lahore", "051-231256", "info@clinic2.com"));
            Global.Clinic_list.Add(new Clinic("clinic3", "Karachi", "223233343", "info@clinic3.com"));
            Global.Clinic_list.Add(new Clinic("clinic4", "Peshawar", "2232434343", "info@clinic4.com"));
            Global.Clinic_list.Add(new Clinic("clinic5", "Multan", "22343323", "info@clinic5.com"));
            Global.Clinic_list.Add(new Clinic("clinic6", "Rawalpindi", "051-2563254", "info@clinic6.com"));
            Global.Clinic_list.Add(new Clinic("clinic7", "Rawalpindi", "051-2563254", "info@clinic6.com"));
            Global.Clinic_list.Add(new Clinic("clinic8", "Islamabad", "051-2563254", "info@clinic6.com"));

            Global.Clinic_list.Add(new Clinic("Jamaica", "Asia", "051-2563254", "info@clinic6.com"));
            Global.Clinic_list.Add(new Clinic("Gun Hill", "Islamabad", "051-2563254", "info@clinic6.com"));
            Global.Clinic_list.Add(new Clinic("Brazil", "Islamabad", "051-2563254", "info@clinic6.com"));

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
                    FrontDeskView NewWindow = new FrontDeskView(/*LoginBox.Text*/);
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
