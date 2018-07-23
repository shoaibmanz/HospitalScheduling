using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using SchedulingSystem;
using System.Data;

namespace HospitalScheduling
{
    /// <summary>
    /// Interaction logic for SpecialityView.xaml
    /// </summary>
    /// 

    public partial class SpecialityView : Window
    {
        /* A list to hold all specialities */
        public List<string> specialities = new List<string>()
        {
            "Medical",
            "ACCU",
            "PT",
            "Chiro"
        };

        /* A list to hold all clinics */
        public List<Clinic> clinics = new List<Clinic>()
        {
            new Clinic ("Jamaica", "Asia", "051-2563254", "info@clinic6.com"),
            new Clinic("Gun Hill", "Islamabad", "051-2563254", "info@clinic6.com"),
            new Clinic("Brazil", "Islamabad", "051-2563254", "info@clinic6.com")
        };

        /* A list to hold all doctors*/
        public static List<Doc> doctors = new List<Doc>()
        {
            new Doc("John", "", "", "","ACCU"),
            new Doc("Shoaib", "", "", "","Medical"),
            new Doc("Ahsan", "", "","", "Medical"),
            new Doc("Haris", "", "", "", "PT"),
            new Doc("Tom", "", "", "", "Chiro"),
        };

        struct Clipboard
        {
            public bool valid;
            public string startDate, endDate;
            public List<string> Doctors;

            public Clipboard(bool _valid)
            {
                valid = _valid;
                startDate = endDate = "";
                Doctors = new List<string>();
            }
        }

        private Clipboard clipboard;

        public bool SPECIALITYvsCLINIC = true;

        DateTime currentDate = System.DateTime.Now;


        public DataTable Calender_DataTable = new DataTable();


        public ObservableCollection<SpecialityList> specialityListBox = new ObservableCollection<SpecialityList>();
        public ObservableCollection<ClinicList> clinicsListBox = new ObservableCollection<ClinicList>();


        public SpecialityView()
        {
            clipboard = new Clipboard(false);
            

            InitializeComponent();

            DataContext = this;

            /* Bind Speciality Drop Down To list of Specialities */
            cmb_SpecialityDropdown.ItemsSource = specialities;
            lb_SpecialityList.ItemsSource = specialityListBox;

            Calender_DataTable.Columns.Add("INDEX");


            // Add Clinics to Clinic List Box
            foreach (var item in clinics)
            {
                clinicsListBox.Add(new ClinicList(false, item));




            }




            dg_dayView.DataContext = Calender_DataTable.DefaultView;



            lb_clinicsListBox.ItemsSource = clinicsListBox;
        }

        private void cmb_SpecialityDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedSpec = "";
            if ((sender as ComboBox).SelectedItem != null)
            {
                selectedSpec = (sender as ComboBox).SelectedItem.ToString();

                Console.WriteLine("Selected : " + selectedSpec + "\n");

                // Check for duplicate specialities in listbox
                if (specialityListBox.Any(p => p.specialityName == selectedSpec) == false)
                {
                    specialityListBox.Insert(0, (new SpecialityList(true, selectedSpec)));

                    // -----------------------------
                    if (SPECIALITYvsCLINIC)
                        addDataGridRow(selectedSpec);
                    else
                        addDataGridColumn(selectedSpec);
                }


            }

            (sender as ComboBox).SelectedIndex = -1;
        }



        private void addDataGridColumn(string header)
        {
            if (Calender_DataTable.Columns.Contains(header) == false)
                Calender_DataTable.Columns.Add(header, typeof(string));

            // Refresh context of dataGrid
            dg_dayView.DataContext = null;
            dg_dayView.DataContext = Calender_DataTable.DefaultView;
        }

        private void removeDataGridColumn(string header)
        {
            if (Calender_DataTable.Columns.Contains(header))
                Calender_DataTable.Columns.Remove(header);

            // Refresh context of dataGrid
            dg_dayView.DataContext = null;
            dg_dayView.DataContext = Calender_DataTable.DefaultView;
        }

        private void addDataGridRow(string rowString)
        {
            DataRow dRow = Calender_DataTable.NewRow();
            dRow["INDEX"] = rowString;
            Calender_DataTable.Rows.Add(dRow);

            // Refresh context of dataGrid
            dg_dayView.DataContext = null;
            dg_dayView.DataContext = Calender_DataTable.DefaultView;
        }

        private void removeDataGridRow(string header)
        {
            for (int i = Calender_DataTable.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = Calender_DataTable.Rows[i];
                if (dr["INDEX"].ToString() == header)
                    dr.Delete();
            }

            // Refresh context of dataGrid
            dg_dayView.DataContext = null;
            dg_dayView.DataContext = Calender_DataTable.DefaultView;
        }


        private void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            DataGridCell cell = sender as DataGridCell;
            if (cell == null || cell.IsEditing )
                return;


            // Get Row and Column  https://blog.scottlogic.com/2008/12/02/wpf-datagrid-detecting-clicked-cell-and-row.html
            int rowIndex = dg_dayView.Items.IndexOf(dg_dayView.CurrentItem);


            //MessageBox.Show(Calender_DataTable.Rows[]["INDEX"].ToString());
            string clinic, speciality;

            if ( SPECIALITYvsCLINIC )
            {
                clinic = cell.Column.Header.ToString();
                speciality = Calender_DataTable.Rows[rowIndex]["INDEX"].ToString();
            }
            else
            {
                clinic = Calender_DataTable.Rows[rowIndex]["INDEX"].ToString();
                speciality = cell.Column.Header.ToString();
            }


            if (clinic != "INDEX" && speciality != "INDEX")
            {
                //---------------------------------------------------------------------------------------
                ObservableCollection<Doc> doctorsOfSpec = new ObservableCollection<Doc>();
                for (int i = 0; i < doctors.Count; ++i)
                    if (doctors[i].Speciality == speciality)
                        doctorsOfSpec.Add(doctors[i]);


                // Open Window ..
                AssignDoctor window_AssignDoctor = new AssignDoctor(clinic, speciality, doctorsOfSpec);
                window_AssignDoctor.ShowDialog();

                ObservableCollection<Doc> appointedDocs = window_AssignDoctor.AppointedDocs;
                if (appointedDocs == null)
                    appointedDocs = new ObservableCollection<Doc>();


                if ( window_AssignDoctor.AutomaticAppointment )
                {
                    if (appointedDocs.Count != 0 )
                        appointedDocs.Clear();

                    for (int i = 0; i < window_AssignDoctor.NumberOfDocs; ++i)
                        appointedDocs.Add(doctorsOfSpec[(new Random()).Next(0, doctorsOfSpec.Count)]);
                }

                string toWrite = window_AssignDoctor.StartTime.ToString("t") + " to " + window_AssignDoctor.EndTime.ToString("t") + "\n"; 


                for (int i = 0; i < appointedDocs.Count; ++i)
                    toWrite += "Dr." + appointedDocs[i].name + " ";

                Calender_DataTable.Rows[rowIndex][cell.Column.Header.ToString()] = toWrite;



            }
            //this.
            //MessageBox.Show(cell.Content.ToString());
        }

        public void MenuItemClick(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = e as RoutedEventArgs;
            MenuItem item = args.OriginalSource as MenuItem;
            string header = item.Header.ToString();

            //WeeklyGrid.CurrentCell
            if (header == "Copy" || header == "Cut")
            {
                int index = dg_dayView.SelectedIndex;
                string col = dg_dayView.CurrentCell.Column.Header.ToString();

                if (col != "INDEX" )
                { 
                    string ToCopy = Calender_DataTable.Rows[index][col].ToString();

                    if (ToCopy != "")
                    {
                        string[] chars = ToCopy.Split(' ','\n');

                        clipboard.startDate = chars[0] + " " +chars[1];
                        clipboard.endDate = chars[3] + " " + chars[4];
                        if (clipboard.Doctors.Count != 0)
                            clipboard.Doctors.Clear();

                        Console.WriteLine("Doctor: " + chars[5]);
                        for (int i = 4; i < chars.Count(); ++i)
                            clipboard.Doctors.Add(chars[i]);

                        clipboard.valid = true;

                        Console.WriteLine("Copied To Clipboard Success!");
                    }

                    
                }
                /*
                int i = WeeklyGrid.CurrentCell.Column.DisplayIndex;
                switch (i)
                {
                    case 1:
                        Global.arg1 = ((GridData)WeeklyGrid.CurrentCell.Item).c1;
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
                       } */

                if (header == "Cut")
                {
                    /*
                    ((GridData)WeeklyGrid.CurrentCell.Item).c1 = "";
                    ((GridData)WeeklyGrid.CurrentCell.Item).c2 = "";
                    ((GridData)WeeklyGrid.CurrentCell.Item).c3 = "";
                    ((GridData)WeeklyGrid.CurrentCell.Item).c4 = "";
                    ((GridData)WeeklyGrid.CurrentCell.Item).c5 = "";
                    */
                }

            }
            else if (header == "Paste")
            {
                int index = dg_dayView.SelectedIndex;
                string col = dg_dayView.CurrentCell.Column.Header.ToString();

                if (col != "INDEX")
                {

                    if (clipboard.valid)
                    {
                        string toWrite = clipboard.startDate + " to " + clipboard.endDate + "\n";

                        string spec;

                        if (SPECIALITYvsCLINIC)
                            spec = Calender_DataTable.Rows[index]["INDEX"].ToString();
                        else
                            spec = col;

                        for ( int i = 0; i < doctors.Count(); ++i )
                            if ( doctors[i].Speciality == spec )
                            {
                                toWrite += "Dr." + doctors[i].name + " ";
                                Console.WriteLine("Attack!!!!");
                                //if (i >= clipboard.Doctors.Count())
                                    //break;
                            }

                        Calender_DataTable.Rows[index][col] = toWrite; 
                    }


                }
                /*
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
            */

            }
            else if (header == "Cut")
            {

            }
        }


        private void btn_nextDay_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddDays(1);
            txt_date.Text = currentDate.ToString("MMMM dd, yyyy");
        }
        private void btn_prevDay_Click(object sender, RoutedEventArgs e)
        {
            currentDate = currentDate.AddDays(-1);
            txt_date.Text = currentDate.ToString("MMMM dd, yyyy");
        }

        private void btn_filterClinics_Click(object sender, RoutedEventArgs e)
        {
            if (txt_filterName.Text != "")
            {

                foreach (var item in clinics)
                {
                    if (item.Name.Contains(txt_filterName.Text) == false)
                    {
                        foreach (var item2 in clinicsListBox)
                            if (item.Name == item2.Name && item2.IS_ClinicCheckBoxEnabled == false)
                            {
                                clinicsListBox.Remove(item2);
                                break;
                            }

                    }

                }

                txt_filterName.Text = "";
            }
        }


        private void CheckBox_Checked_ShowClinic(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox)
            {
                // Add to DataTable
                if (SPECIALITYvsCLINIC)
                    addDataGridColumn(((CheckBox)sender).Content.ToString());
                else
                    addDataGridRow(((CheckBox)sender).Content.ToString());

            }


        }
        private void CheckBox_Unchecked_HideClinic(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox)
            {
                // Remove from DataTable
                if (SPECIALITYvsCLINIC)
                    removeDataGridColumn(((CheckBox)sender).Content.ToString());
                else
                    removeDataGridRow(((CheckBox)sender).Content.ToString());          
            }


        }

        private void CheckBox_Checked_ShowSpeciality(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox)
            {
                // Add to DataTable
                if (SPECIALITYvsCLINIC)
                    addDataGridRow(((CheckBox)sender).Content.ToString());
                else
                    addDataGridColumn(((CheckBox)sender).Content.ToString());

            }
        }

        private void CheckBox_Unchecked_HideSpeciality(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox)
            {
                // Remove from DataTable
                if (SPECIALITYvsCLINIC)
                    removeDataGridRow(((CheckBox)sender).Content.ToString());
                else
                    removeDataGridColumn(((CheckBox)sender).Content.ToString());
            }
        }

        private void ComboBox_View_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb.SelectedIndex == 0)
                SPECIALITYvsCLINIC = true;
            else
                SPECIALITYvsCLINIC = false;
        }

        private void LogOut_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindowLogin window = new MainWindowLogin();
            window.Show();
            this.Close();
        }
    }


    /*
    public class ComboBoxtoVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            bool argument = (bool)value;
            if (argument == true)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object();
        }
    }
    */

    public class SpecialityList : INotifyPropertyChanged
    {
        public bool isEnabled { get; set; }
        public string specialityName { get; set; }

        public SpecialityList(bool _isEnabled, string _Name)
        {
            isEnabled = _isEnabled;
            specialityName = _Name;
        }

        public SpecialityList()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }

    
    public class ClinicList : INotifyPropertyChanged
    {
        private bool isEnabled = false;
        public bool IS_ClinicCheckBoxEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;
                NotifyPropertyChanged("IsEnabled Clinic Changed");
            }
        }


        Clinic clinic;

        public string Name
        {
            get { return clinic.Name; }
            set
            {
                clinic.Name = value;
                NotifyPropertyChanged("Clinic Name");
            }
        }

        public ClinicList(bool _isEnabled, Clinic _clinic)
        {
            isEnabled = _isEnabled;
            clinic = _clinic;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
 
/*
    class BoolToVisibleOrHidden : IValueConverter
    {
        #region Constructors
        /// <summary>
        /// The default constructor
        /// </summary>
        public BoolToVisibleOrHidden() { }
        #endregion

        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool bValue = (bool)value;
            if (bValue)
                return Visibility.Visible;
            else
                return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;

            if (visibility == Visibility.Visible)
                return true;
            else
                return false;
        }
        #endregion
    }
*/

}
