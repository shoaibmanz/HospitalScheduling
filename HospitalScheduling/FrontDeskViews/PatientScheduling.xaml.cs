﻿using System;
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
    /// Interaction logic for PatientScheduling.xaml
    /// </summary>
    public partial class PatientScheduling : Window
    {

        public PatientAppointment CurrentPatient { get; set; }
        public PatientScheduling(PatientAppointment CurrentPatient)
        {
            InitializeComponent();
            this.CurrentPatient = CurrentPatient;
            this.DataContext = this.CurrentPatient;


            if (Query.IsOldPatient(CurrentPatient)) {
                ReturningPatient.IsChecked = true;                
            }
            else {
                NewPatient.IsChecked = true;
            }

            InsuranceInfo.ItemsSource = CurrentPatient.PatientInfo.InsuranceInfo;
        }


        private void CurrApointments_Click(object sender, RoutedEventArgs e)
        {
            PatientCalender CalandarWindow = new PatientCalender(this.CurrentPatient);
            this.Hide();
            CalandarWindow.ShowDialog();
            this.Show();
        }

        private void VisitDetails_Click(object sender, RoutedEventArgs e)
        {
            VisitHistory VisitHistoryWindow = new VisitHistory(CurrentPatient);

            VisitHistoryWindow.ShowDialog();
        }

        private void AddToOpenList_Click(object sender, RoutedEventArgs e)
        {
            AddToOpenList OpenListWindow = new AddToOpenList(CurrentPatient);

            OpenListWindow.ShowDialog();
            this.Close();
        }

        private void FindBestAppointment_Click(object sender, RoutedEventArgs e)
        {
            PatientPreferences PatientPreferencesWindow = new PatientPreferences(CurrentPatient);

            PatientPreferencesWindow.ShowDialog();
            this.Close();

        }

        private void ManualAppointment_Click(object sender, RoutedEventArgs e)
        {
            ManualAppointment CalandarWindow = new ManualAppointment(CurrentPatient);
            this.Hide();
            CalandarWindow.ShowDialog();
            this.Show();
        }

        private void AppointmentConfirmed(object sender, RoutedEventArgs e)
        {
            PopupNotification PopupWindow = new PopupNotification();
            this.Hide();
            PopupWindow.ShowDialog();
            this.Close();
        }
    }

    public class ComboBoxtoVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            ComboBoxItem argument = (ComboBoxItem)value;
            if ((string)argument.Content == "Follow Up") {
                return Visibility.Visible;
            }
            else {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new object();
        }
    }
}
