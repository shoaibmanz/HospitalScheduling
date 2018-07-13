using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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
        Clinic clinic;
        Doc doctor;
        DateTime startTime, endTime;
        List<Appointment> appointments;
        
        Event()
        {

        }
    }

    public class Calender
    {
        public DateTime startDate;
        public DateTime endDate;

        Dictionary<DateTime, List<int>> eventsDictionary;

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
                eventsDictionary.Add(startDate.AddDays(i), new List<int>());



        }
    }

}
