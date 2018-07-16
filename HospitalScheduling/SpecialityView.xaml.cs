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
        public List<Doc> doctors = new List<Doc>()
        {
            new Doc("John", "", "", "","ACCU"),
            new Doc("Shoaib", "", "", "","Medical"),
            new Doc("Ahsan", "", "","", "Medical"),
            new Doc("Haris", "", "", "", "PT"),
            new Doc("Tom", "", "", "", "Chiro"),
        };


        public bool SPECIALITYvsCLINIC = true ;

        DateTime currentDate = System.DateTime.Now;


        public DataTable Calender_DataTable = new DataTable();
       

        public ObservableCollection<SpecialityList> specialityListBox = new ObservableCollection<SpecialityList>();
        public ObservableCollection<ClinicList> clinicsListBox = new ObservableCollection<ClinicList>();


        public SpecialityView()
        {
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
            if (Calender_DataTable.Columns.Contains(header))
                Calender_DataTable.Columns.Remove(header);

            // Refresh context of dataGrid
            dg_dayView.DataContext = null;
            dg_dayView.DataContext = Calender_DataTable.DefaultView;
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
                    addDataGridColumn( ((CheckBox)sender).Content.ToString()  );
                else
                    addDataGridRow( ((CheckBox)sender).Content.ToString() );

                /*
                foreach (DataGridTextColumn Column in dg_dayView.Columns)
                {
                    if (Column.Header == ((CheckBox)sender).Content)
                    {
                        Column.Visibility = Visibility.Visible;
                        return;
                    }
                }
                */
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

                /*
                foreach (DataGridTextColumn Column in dg_dayView.Columns)
                {
                    if (Column.Header == ((CheckBox)sender).Content)
                    {
                        Column.Visibility = Visibility.Hidden;
                        return;
                    }
                }*/
            }

            
        }

        private void CheckBox_Checked_ShowSpeciality(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Unchecked_HideSpeciality(object sender, RoutedEventArgs e)
        {

        }
    }


    public class DataGridHeaderStruct : INotifyPropertyChanged
    {
        public bool checkBox { get; set; }
        public string headerText { get; set; }

        public DataGridHeaderStruct(bool _checkbox, string _text)
        {
            checkBox = _checkbox;
            headerText = _text;
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
    /* Data -- Class 
    public partial class Clinic
    {
        public string name;
        public string address;
        public string phone;
        public string email;

        public string Name { get; set; }

        public Clinic(string name, string address, string phone, string email)
        {
            this.Name = name;
            this.address = address;
            this.email = email;
            this.phone = phone;

        }
    };
    */

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
}
