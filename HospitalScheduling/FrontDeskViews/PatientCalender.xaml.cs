using System;
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

namespace SchedulingSystem
{
    /// <summary>
    /// Interaction logic for PatientCalender.xaml
    /// </summary>
    public partial class PatientCalender : Window
    {
        public DataTable PatientTable;
        public PatientCalender(PatientToBeScheduled Patient)
        {
            InitializeComponent();
            
            PatientTable = new DataTable();
            PatientTable.Columns.Add("Time", typeof(string));

            PatientTable.Columns.Add(Patient.PatientName, typeof(string));

            var TimeStrings = Data.GetTimeStrings(9, 2);

            foreach (string tStr in TimeStrings)
            {
                DataRow dRow = PatientTable.NewRow();
                dRow["Time"] = tStr;
                PatientTable.Rows.Add(dRow);
            }

            dg_dayView.DataContext = PatientTable.DefaultView;
        }
        
    }
}
