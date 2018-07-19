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
using SchedulingSystem;


namespace HospitalScheduling
{
    /// <summary>
    /// Interaction logic for AssignDoctor_Manual.xaml
    /// </summary>
    public partial class AssignDoctor_Manual : Window
    {

        ObservableCollection<Doc> doctorsLeft = new ObservableCollection<Doc>();
        public ObservableCollection<Doc> doctorsRight { get; set; }

        private int numberofDoctors;
        public AssignDoctor_Manual(ObservableCollection<Doc> doctors, int _NumberofDocs)
        {
            DataContext = this;

            doctorsRight = new ObservableCollection<Doc>();
            doctorsLeft = doctors;
            numberofDoctors = _NumberofDocs;

            InitializeComponent();

            txt_DocNumber.Text = _NumberofDocs.ToString();
            lb_doctorsOnLeft.ItemsSource = doctorsLeft;
            lb_doctorsOnRight.ItemsSource = doctorsRight;

        }

        private void Assign_Button_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;

            for ( int i = doctorsLeft.Count - 1 ; i >= 0; i-- )
            {
                ListBoxItem item = this.lb_doctorsOnLeft.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                CheckBox chk = FindFirstElementInVisualTree<CheckBox>(item);

                if (chk.IsChecked == true && count < numberofDoctors)
                {
                    
                    doctorsRight.Add(doctorsLeft[i]);
                    doctorsLeft.RemoveAt(i);

                    count++;
                }
            }
        }
        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = doctorsRight.Count - 1; i >= 0; i--)
            {
                ListBoxItem item = this.lb_doctorsOnRight.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                CheckBox chk = FindFirstElementInVisualTree<CheckBox>(item);

                if (chk.IsChecked == true)
                {
                    
                    doctorsLeft.Add(doctorsRight[i]);
                    doctorsRight.RemoveAt(i);

                }
            }
        }

        // Access Template item
        // http://www.geekchamp.com/tips/how-to-access-a-control-placed-inside-listbox-itemtemplate-in-wp7
        private T FindFirstElementInVisualTree<T>(DependencyObject parentElement) where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(parentElement);
            if (count == 0)
                return null;

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parentElement, i);

                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    var result = FindFirstElementInVisualTree<T>(child);
                    if (result != null)
                        return result;

                }
            }
            return null;
        }

        private void Done_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
