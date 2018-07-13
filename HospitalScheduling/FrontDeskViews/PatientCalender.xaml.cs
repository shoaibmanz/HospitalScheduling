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
using System.Windows.Shapes;

namespace SchedulingSystem
{
    /// <summary>
    /// Interaction logic for PatientCalender.xaml
    /// </summary>
    public partial class PatientCalender : Window
    {
        public PatientCalender(PatientToBeScheduled Patient)
        {
            InitializeComponent();

            DataGridTemplateColumn PatientNameColumn = new DataGridTemplateColumn();
            PatientNameColumn.Header = Patient.PatientName;


            dg_dayView.Columns.Add(PatientNameColumn);

            List<string> dayView_Time = new List<string>();
            int currHour = 12;
            int currMin = 0;
            string suffix = "AM";

            while (true)
            {
                if (currHour == 12 && currMin == 0)
                    suffix = "PM";
                else if (suffix == "PM" && currHour == 2)
                    break;

                if (currHour < 10)
                    dayView_Time.Add("0" + Convert.ToString(currHour) + ":" + currMin.ToString() + suffix);
                else
                    dayView_Time.Add(Convert.ToString(currHour) + ":" + currMin.ToString() + suffix);

                currMin += 15;
                if (currMin == 60)
                {
                    if (currHour == 12)
                        currHour = 1;
                    else
                        currHour += 1;

                    currMin = 0;
                }
            }

            Console.WriteLine(dayView_Time.Count);
        }
    }
}
