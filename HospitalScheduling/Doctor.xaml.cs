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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchedulingSystem
{
    /* classes for dummy data
     * added on 5th junly, 2018 11:30 AM
    */
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
        public class GridData
        {
            public string time { get; set; }
            public string c1 { get; set; }
            public string c2 { get; set; }
            public string c3 { get; set; }
            public string c4 { get; set; }
            public string c5 { get; set; }


        }
        public MainWindow()
        {
            InitializeComponent();
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

            AddClinic.Items.Add(Global.Clinic_list[8].name);
            AddClinic.Items.Add(Global.Clinic_list[9].name);
            AddClinic.Items.Add(Global.Clinic_list[10].name);


            GridData d1 = new GridData();
            GridData d2 = new GridData();
            GridData d3 = new GridData();
            GridData d4 = new GridData();
            GridData d5 = new GridData();
            GridData d6 = new GridData();
            GridData d7 = new GridData();
            GridData d8 = new GridData();
            GridData d9 = new GridData();
            GridData d10 = new GridData();
            GridData d11 = new GridData();


            d1.time = "7:00 AM"; d1.c1 = "Available";
            d2.time = "8:00 AM"; d2.c5 = "Available";
            d3.time = "9:00 AM"; d3.c4 = "Available";
            d4.time = "10:00 AM"; d4.c5 = "Available";
            d5.time = "11:00 AM"; d5.c2 = "Available";
            d6.time = "12:00 PM"; d6.c3 = "Available";
            d7.time = "01:00 PM"; d7.c1 = "Available";
            d8.time = "02:00 PM"; d8.c3 = "Available";
            d9.time = "03:00 PM"; d9.c5 = "Available";
            d10.time = "04:00 PM"; d10.c4 = "Available";
            d11.time = "05:00 PM"; d11.c1 = "Available";



            WeeklyGrid.Items.Add(d1);
            WeeklyGrid.Items.Add(d2);
            WeeklyGrid.Items.Add(d3);
            WeeklyGrid.Items.Add(d4);
            WeeklyGrid.Items.Add(d5);
            WeeklyGrid.Items.Add(d6);
            WeeklyGrid.Items.Add(d7);
            WeeklyGrid.Items.Add(d8);
            WeeklyGrid.Items.Add(d9);
            WeeklyGrid.Items.Add(d10);
            WeeklyGrid.Items.Add(d11);
            WeeklyGrid.IsReadOnly = true;

            // WeeklyGrid.ClipboardCopyMode = DataGridClipboardCopyMode.ExcludeHeade

        }

        public void DoubleClickHandler(object sender, MouseButtonEventArgs e)
        {


            return;
        }

        public void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {

            ContextMenu cm = new ContextMenu();
            cm.Items.Add("Copy");
            cm.Items.Add("Cut");
            cm.Items.Add("Paste");

        }

        public void MenuItemClick(object sender, System.EventArgs e)
        {
            RoutedEventArgs args = e as RoutedEventArgs;
            MenuItem item = args.OriginalSource as MenuItem;
            string header = item.Header.ToString();
            if (header == "Copy")
            {
                int cell_count = WeeklyGrid.SelectedCells.Count();
                Clipboard.Clear();

                var cell = WeeklyGrid.SelectedCells[0];
                Clipboard.SetDataObject(cell);





            }
            else if (header == "Paste")
            {
                WeeklyGrid.CurrentCell = WeeklyGrid.SelectedCells[0];

            }
            else if (header == "Cut")
            {

            }
        }

        private void OpenList_Click(object sender, RoutedEventArgs e)
        {
            Doctor_openList DocWindow = new Doctor_openList();
            this.Hide();
            DocWindow.ShowDialog();
            this.Show();

        }

        private void DoctorReport_Click(object sender, RoutedEventArgs e)
        {
            ReportUnavailability ReportUnav = new ReportUnavailability();
            this.Hide();
            ReportUnav.ShowDialog();
            this.Show();
        }

        private void AddClinic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AddClinic.SelectedIndex >= 0)
            {

                int index = ClinicListBox.Items.IndexOf(AddClinic.SelectedValue.ToString());
                if (index < 0)
                {
                    ClinicListBox.Items.Add(AddClinic.SelectedValue.ToString());
                    AddClinic.SelectedIndex = -1;
                }


            }
        }

        private void AddClinic_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void DoctorRemove_Click(object sender, RoutedEventArgs e)
        {
            if (ClinicListBox.SelectedIndex >= 0)
            {
                ClinicListBox.Items.RemoveAt(ClinicListBox.SelectedIndex);
            }
        }

        private void DoctorTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ClinicListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ClinicListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void WeeklyGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGridRow_MouseEnter(object sender, MouseEventArgs e)
        {

        }
    }
}
