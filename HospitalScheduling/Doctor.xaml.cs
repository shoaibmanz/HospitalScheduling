using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public string Speciality { get; set; }

        public Doc(string name, string address, string email, string phone, string speciality)
        {
            this.address = address;
            this.email = email;
            this.name = name;
            this.phone = phone;
            this.Speciality = speciality;
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
        public class GridData : INotifyPropertyChanged
        {
            public string time { get; set; }


            private string _c1;
            public string c1
            {
                get
                {
                    return _c1;
                }
                set
                {
                    if (this._c1 != value)
                    {
                        this._c1 = value;
                        this.NotifyPropertyChanged("c1");
                    }
                }
            }

            private string _c2;

            public string c2
            {
                get
                {
                    return _c2;
                }
                set
                {
                    if (this._c2 != value)
                    {
                        this._c2 = value;
                        this.NotifyPropertyChanged("c2");
                    }
                }
            }
            private string _c3 { get; set; }
            public string c3
            {
                get
                {
                    return _c3;
                }
                set
                {
                    if (this._c3 != value)
                    {
                        this._c3 = value;
                        this.NotifyPropertyChanged("c3");
                    }
                }
            }
            public string _c4 { get; set; }
            public string c4
            {
                get
                {
                    return _c4;
                }
                set
                {
                    if (this._c4 != value)
                    {
                        this._c4 = value;
                        this.NotifyPropertyChanged("c4");
                    }
                }
            }
            public string _c5 { get; set; }
            public string c5
            {
                get
                {
                    return _c5;
                }
                set
                {
                    if (this._c5 != value)
                    {
                        this._c5 = value;
                        this.NotifyPropertyChanged("c5");
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public void NotifyPropertyChanged(string propName)
            {
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        ObservableCollection<GridData> AppointmentInfo;

        public MainWindow()
        {
            InitializeComponent();
            Global.Clinic_list = new List<Clinic>();
            Global.Docs = new List<Doc>();
            Global.arg1 = Global.arg2 = Global.arg3 = "";
            Global.status = false;

            Global.Docs.Add(new Doc("John", "", "", "","ACCU"));
            Global.Docs.Add(new Doc("Shoaib", "", "", "","Medical"));
            Global.Docs.Add(new Doc("Ahsan", "", "","", "Medical"));
            Global.Docs.Add(new Doc("Haris", "", "", "", "PT"));
            Global.Docs.Add(new Doc("Tom", "", "", "", "Chiro"));


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


            AppointmentInfo = new ObservableCollection<GridData>();

            AppointmentInfo.Add(new GridData() {time = "7:00 AM", c1 = "Available"
            });
            AppointmentInfo.Add(new GridData()
            {
                time = "8:00 AM",
                c5 = "Available"
            });
            AppointmentInfo.Add(new GridData()
            {
                time = "9:00 AM",
                c1 = "Available"
            });
            AppointmentInfo.Add(new GridData()
            {
                time = "10:00 AM",
                c1 = "Available"
            });

            AppointmentInfo.Add(new GridData()
            {
                time = "11:00 AM",
                c5 = "Available"
            });
            AppointmentInfo.Add(new GridData()
            {
                time = "12:00 PM",
                c3 = "Available"
            });
            AppointmentInfo.Add(new GridData()
            {
                time = "1:00 PM",
                c1 = "Available"
            });
            AppointmentInfo.Add(new GridData()
            {
                time = "2:00 PM",
                c5 = "Available"
            });
            AppointmentInfo.Add(new GridData()
            {
                time = "3:00 PM",
                c4 = "Available"
            });
            AppointmentInfo.Add(new GridData()
            {
                time = "4:00 PM",
                c2= "Available"
            });
            AppointmentInfo.Add(new GridData()
            {
                time = "5:00 PM",
                c1 = "Available"
            });
            WeeklyGrid.ItemsSource = AppointmentInfo;
           

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

        public void MenuItemClick(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = e as RoutedEventArgs;
            MenuItem item = args.OriginalSource as MenuItem;
            string header = item.Header.ToString();

            //WeeklyGrid.CurrentCell
            if (header == "Copy" || header=="Cut")
            {
                int i = WeeklyGrid.CurrentCell.Column.DisplayIndex;
                switch (i)
                {
                    case 1:
                        Global.arg1= ((GridData)WeeklyGrid.CurrentCell.Item).c1  ;
                        break;
                    case 2:
                        Global.arg1 = ((GridData)WeeklyGrid.CurrentCell.Item).c2;
                        break;

                    case 3:
                        Global.arg1 = ((GridData)WeeklyGrid.CurrentCell.Item).c3;
                        break;
                    case 4:
                        Global.arg1 = ((GridData)WeeklyGrid.CurrentCell.Item).c4;
                        break;
                    case 5:
                        Global.arg1 = ((GridData)WeeklyGrid.CurrentCell.Item).c5;
                        break;
                }
                if (header == "Cut")
                {
                    ((GridData)WeeklyGrid.CurrentCell.Item).c1 = "";
                    ((GridData)WeeklyGrid.CurrentCell.Item).c2 = "";
                    ((GridData)WeeklyGrid.CurrentCell.Item).c3 = "";
                    ((GridData)WeeklyGrid.CurrentCell.Item).c4 = "";
                    ((GridData)WeeklyGrid.CurrentCell.Item).c5 = "";
                }

            }
            else if (header == "Paste")
            {

             
                int i = WeeklyGrid.CurrentCell.Column.DisplayIndex;

                switch (i)
                {
                    case 1:
                        ((GridData)WeeklyGrid.CurrentCell.Item).c1 = Global.arg1;
                        break;
                    case 2:
                        ((GridData)WeeklyGrid.CurrentCell.Item).c2 = Global.arg1;
                        break;

                    case 3:
                        ((GridData)WeeklyGrid.CurrentCell.Item).c3 = Global.arg1;
                        break;
                    case 4:
                        ((GridData)WeeklyGrid.CurrentCell.Item).c4 = Global.arg1;
                        break;
                    case 5:
                        ((GridData)WeeklyGrid.CurrentCell.Item).c5 = Global.arg1;
                        break;
                }

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


        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           
        }
    }
}
