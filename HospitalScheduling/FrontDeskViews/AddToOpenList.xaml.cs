﻿using System;
using System.Collections.Generic;
using System.Data;
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

        public PatientAppointment CurrentPatient { get; set; }
        public AddToOpenList(PatientAppointment CurrentPatient)
        {
            InitializeComponent();
            this.DataContext = CurrentPatient;
            this.CurrentPatient = CurrentPatient;

            cb_Clinic.ItemsSource = Query.GetClinicNames();
            cb_Doctor.ItemsSource = Query.GetDoctorNames();
            cb_Specialty.ItemsSource = Query.GetSpecialties();

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            Random random = new Random();
            

            DataRow dRow = FrontDeskView.OpenList.NewRow();

            dRow["ID"] = CurrentPatient.ID;
            dRow["ChartNumber"] = CurrentPatient.PatientInfo.ChartNumber;
            dRow["PatientName"] = CurrentPatient.PatientInfo.Name;
            dRow["DoctorName"] = CurrentPatient.Slot.DoctorInfo.Name;
            dRow["Specialty"] = CurrentPatient.Slot.DoctorInfo.Specialty;
            dRow["PatientStatus"] = CurrentPatient.PatientStatus;
            dRow["WaitTime"] = random.Next(0, 45);
            dRow["PatientsAhead"] = FrontDeskView.OpenList.Rows.Count == 0 ? "On Call" : FrontDeskView.OpenList.Rows.Count.ToString();

            FrontDeskView.OpenList.Rows.Add(dRow);

            this.Close();
        }
    }
}
