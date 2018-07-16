using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace TestDesign {

    public class Doctor
    {
        public string Name { get; set; }
        public string Specialty { get; set; }       // 1 doctor = 1 specialty?          specialty = enum?
    }

    public class Clinic
    {
        public string Name { get; set; }
    }

    public class ScheduledDoctor
    {
        public Clinic ClinicInfo { get; set; }
        public Doctor DoctorInfo { get; set; }

    }
}

namespace SchedulingSystem
{

    public static class Data
    {

        public static ObservableCollection<string> GetSpecialities()
        {
            return new ObservableCollection<string>() { "Medical", "PT", "ACCU", "Cardiology", "Chiro" };
        }

        public static ObservableCollection<string> GetClinicNames()
        {
            return new ObservableCollection<string>() { "Gun Hill", "Jamaica", "Islamabad Diagnostic Center" };
        }

        public static List<string> GetTimeStrings(int MorningTime, int EveningTime)
        {
            List<string> dayView_Time = new List<string>();
            int currHour = MorningTime;
            int currMin = 0;
            string suffix = "AM";

            while (true)
            {
                if (currHour == 12 && currMin == 0)
                    suffix = "PM";
                
                string TimeString = "";

                if (currHour < 10)
                    TimeString += "0";

                TimeString += currHour.ToString() + ":";

                if (currMin < 10)
                    TimeString += "0";

                TimeString += currMin.ToString() + " " + suffix;
                dayView_Time.Add(TimeString);

                if (suffix == "PM" && currHour == EveningTime)
                    break;

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
            return dayView_Time;
        }

        //public static ObservableCollection<Doctor> GetDoctors() {
            
         
        //}
    }

    public class Doctor
    {
        public string Name { get; set; }
    }

    public class PatientToBeScheduled
    {
        public PatientToBeScheduled()
        {
            Insurance = new ObservableCollection<InsuranceRecord>();
            VisitHistory = new ObservableCollection<Visit>();
        }
        public string ChartNumber { get; set; }
        public string PatientName { get; set; }
        public string AccidentDate { get; set; }
        public string InsuranceName { get; set; }
        public string CaseType { get; set; }
        public string CaseStatus { get; set; }
        public string AttorneyName { get; set; }
        public string PatientStatus { get; set; }
        public bool OldNew { get; set; }


        public ObservableCollection<InsuranceRecord> Insurance;
        public ObservableCollection<Visit> VisitHistory;
    }

    public class InsuranceRecord {

        public InsuranceRecord() {
        }
        public string Speciality { get; set; }
        public int VisitsRemaining { get; set; }
    }

    public class Appointment
    {
        public Appointment() {

        }
        public string ChartNumber   { get; set; }
        public string PatientName   { get; set; }
        public string DoctorName    { get; set; }
        public string Speciality    { get; set; }
        public int    NoShowUps     { get; set; }
        public string PatientStatus { get; set; }
        public string SlotTime      { get; set; }
        public int DelayedBy     { get; set; }
        public int PatientsAhead { get; set; }
        public string ClinicName { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public PatientToBeScheduled SchedulingInfo { get; set; }

        
        //public static ObservableCollection<Appointment> GetAppointments()
        //{
        //    #region Query Appointment data from database
        //    return new ObservableCollection<Appointment>();
        //    #endregion
        //}
    }

    public class Visit
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public int Duration { get; set; }
        public string Speciality { get; set; }  // should be ENUM type
        public string DoctorName { get; set; }      // should be reference to existing doctor object
        public string ClinicName { get; set; }      // should be reference to existing clinic object
    }

    public class Event
    {
        public Clinic Clinicc { get; set; }
        public Doc Doctor { get; set; }
        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set; }

        List<Appointment> appointments;
        
        Event()
        {

        }

    }

    public class Calender
    {
        public DateTime startDate;
        public DateTime endDate;

        Dictionary<DateTime, List<Event>> eventsDictionary = new Dictionary<DateTime, List<Event>>();

        public Calender()
        {
            //startDate = new DateTime(1990, 1, 1);
            //endDate = new DateTime(2050, 12, 31);

            // one Year for validation purpose
            startDate = new DateTime(2018, 1, 1);
            endDate = new DateTime(2018, 12, 31);

            initalizeCalender();
        }
        public Calender(DateTime _start , DateTime _end )
        {
            startDate = _start;
            endDate = _end;

            initalizeCalender();
        }

        private void initalizeCalender()
        {
            double daysCount = (endDate - startDate).TotalDays;

            for (double i = 0; i < daysCount; ++i)
                eventsDictionary.Add(startDate.AddDays(i), new List<Event>());
            
        }

        public void AddEvent(DateTime date , Event newEvent)
        {
            eventsDictionary[date].Add(newEvent);
        }
    }

}
