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

namespace SchedulingSystem
{
    /// <summary>
    /// Interaction logic for PossibleAppointments.xaml
    /// </summary>
    public partial class PossibleAppointments : Window
    {
        public PossibleAppointments()
        {
            InitializeComponent();
        }

        private void AppointmentSelected_Click(object sender, RoutedEventArgs e)
        {
            PopupNotification PopupWindow = new PopupNotification();

            this.Hide();
            PopupWindow.ShowDialog();
            this.Close();
        }
    }
}
