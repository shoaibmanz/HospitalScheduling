using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;

namespace TestDesign {

    public static class Query
    { 
        public static List<string> GetDoctorNames()
        {
            List<string> names = Doctor.Data.Select(Doc => Doc.Name).ToList();
            return names;
        }

        public static List<string> GetClinicNames()
        {
            List<string> names = Clinic.Data.Select(Clinic => Clinic.Name).ToList();
            return names;
        }

        public static List<string> GetSpecialties()
        {
            return Doctor.Specialties;
        }

        public static List<PatientAppointment> GetToBeScheduledPatients()
        {
            List<PatientAppointment> Patients = new List<PatientAppointment>();

            foreach (PatientAppointment P in PatientAppointment.Data)
            {
                if (P.ToBeScheduled)
                {
                    Patients.Add(P);
                }
            }

            return Patients;
        }

        public static List<PatientAppointment> GetAppointments()
        {

            List<PatientAppointment> Patients = new List<PatientAppointment>();

            foreach (PatientAppointment P in PatientAppointment.Data)
            {
                if (!P.ToBeScheduled)
                {
                    Patients.Add(P);
                }
            }

            return Patients;
        }

        public static bool IsOldPatient(PatientAppointment Patient)
        {
            int nAppointments = 0;
            foreach(PatientAppointment P in PatientAppointment.Data)
            {
                if (P.PatientInfo.ChartNumber == Patient.PatientInfo.ChartNumber)
                {
                    nAppointments++;
                }
            }

            return nAppointments > 1;
        }
        
        /// <summary>
        /// Fetches the appointment history for a given patient
        /// </summary>
        /// <param name="ChartNumber"> Unique identifier for the patient </param>
        /// <returns> History of appointments </returns>
        public static List<PatientAppointment> GetAppointments(string ChartNumber)
        {
            List<PatientAppointment> Appointments = new List<PatientAppointment>();

            foreach (PatientAppointment P in PatientAppointment.Data)
            {
                if (P.PatientInfo.ChartNumber == ChartNumber)
                {
                    Appointments.Add(P);
                }
            }

            return Appointments;
        }

        /// <summary>
        /// Searches for and returns an appointment given the appointment ID
        /// </summary>
        /// <param name="ID"> ID of the appointment to fetch </param>
        /// <returns> Appointment object of the required ID.. returns NULL if not found </returns>
        public static PatientAppointment GetAppointment(int ID)
        {
            foreach (PatientAppointment Appointment in PatientAppointment.Data)
            {
                if (Appointment.ID == ID)
                {
                    return Appointment;
                }
            }

            return null;
        }
    }

    public class Doctor
    {
        public Doctor(string Name, string Specialty)
        {
            this.Name = Name;
            this.Specialty = Specialty;
        }

        public string Name { get; set; }
        public string Specialty { get; set; }       // 1 doctor = 1 specialty?          specialty = enum?


        public static ObservableCollection<Doctor> Data { get; set; }

        public static List<string> Specialties { get; set; }

        static public void GenerateData()
        {

            Data = new ObservableCollection<Doctor>();
            Specialties = new List<string>() { "Medical", "Cardiologist", "ACCU", "Chiro", "PT", "Physician", "Neurologist"};

            string[] DoctorNames = { "Dr. John", "Dr. Amitabh", "Dr. Azeem", "Dr. Adam", "Dr. Khattak", "Dr. Hattab", "Dr. Waseem" };

            Random random = new Random();
            foreach (string name in DoctorNames)
            {
                Data.Add(new Doctor(name, Specialties[random.Next(0, Specialties.Count)]));
            }
        }
    }

    public class Clinic
    {
        public static ObservableCollection<Clinic> Data { get; set; }
        static public void GenerateData()
        {
            Data = new ObservableCollection<Clinic>();
            
            Data.Add(new Clinic()
            {
                Name = "Gun Hill"
            });

            Data.Add(new Clinic()
            {
                Name = "Jamaica"
            });

            Data.Add(new Clinic()
            {
                Name = "Islamabad Diagnostic Center"
            });
        }

        public string Name { get; set; }
    }

    public class ScheduledDoctor
    {

        ScheduledDoctor(Doctor doctor, Clinic clinic, DateTime from, DateTime to)
        {
            this.ClinicInfo = clinic;
            this.DoctorInfo = doctor;
            this.From = from;
            this.To = to;
        }

        public Doctor DoctorInfo { get; set; }
        public Clinic ClinicInfo { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public static ObservableCollection<ScheduledDoctor> Data { get; set; }
        public static void GenerateData()
        {
            Data = new ObservableCollection<ScheduledDoctor>();

            Random random = new Random();

            DateTime now = DateTime.Now.AddDays(-15);
            
            // creating 30 random entries
            for (int i = 0; i < 30; ++i)
            {
                int docIndex = random.Next(0, Doctor.Data.Count);
                int clinicIndex = random.Next(0, Clinic.Data.Count);

                Clinic _Clinic = Clinic.Data[clinicIndex];
                Doctor _Doctor = Doctor.Data[docIndex];

                Data.Add(new ScheduledDoctor(_Doctor, _Clinic, now, now.AddHours(6)));
                now = now.AddDays(1);
            }
        }
    }

    public class Patient
    {
        public Patient(string Name, string ChartNumber, string InsuranceName, string AttorneyName, int NoShowUps)
        {
            this.Name = Name;
            this.ChartNumber = ChartNumber;
            this.InsuranceName = InsuranceName;
            this.AttorneyName = AttorneyName;
            this.InsuranceInfo = new List<InsuranceRecord>();
            this.NoShowUps = NoShowUps;
        }

        public string Name { get; set; }
        public string ChartNumber { get; set; }
        public string InsuranceName { get; set; }
        public string AttorneyName { get; set; }
        public int NoShowUps { get; set; }
        public List<InsuranceRecord> InsuranceInfo { get; set; }


        public static ObservableCollection<Patient> Data;
        public static void GenerateData()
        {
            Data = new ObservableCollection<Patient>();
            string[] PatientNames = { "Ruben", "Mirana", "David", "Thomas", "Kurt", "John", "Richard" };

            Random random = new Random(DateTime.Now.Second);

            foreach (string name in PatientNames)
            {
                int ChartNumber = random.Next(0, 1000000);
                Patient P1 = new Patient(name, ChartNumber.ToString(), "XYZ", "JKL", random.Next(0, 5));

                P1.InsuranceInfo.Add(new InsuranceRecord("Medical", random.Next(0, 4)));
                P1.InsuranceInfo.Add(new InsuranceRecord("ACCU", random.Next(0, 4)));
                P1.InsuranceInfo.Add(new InsuranceRecord("Physician", random.Next(0, 4)));
                P1.InsuranceInfo.Add(new InsuranceRecord("Cardiologist", random.Next(0, 4)));

                Data.Add(P1);
            }

        }
    }

    public class InsuranceRecord
    {
        public InsuranceRecord(string Spec, int Visits)
        {
            this.Speciality = Spec;
            this.VisitsRemaining = Visits;
        }

        public string Speciality { get; set; }
        public int VisitsRemaining { get; set; }
    }

    public class PatientAppointment {

        public PatientAppointment(int ID, Patient PatientInfo, ScheduledDoctor slot, DateTime date, int duration, string status, string type, string PatientStatus, bool ToBeScheduled)
        {
            this.PatientInfo = PatientInfo;
            this.Slot = slot;
            this.Date = date;
            this.Duration = duration;
            this.CaseStatus = status;
            this.CaseType = type;
            this.PatientStatus = PatientStatus;
            this.ToBeScheduled = ToBeScheduled;
            this.ID = ID;
        }

        public int ID { get; set; }
        public Patient PatientInfo { get; set; }
        public ScheduledDoctor Slot { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public string CaseStatus { get; set; }
        public string CaseType { get; set; }
        public string PatientStatus { get; set; }
        public bool ToBeScheduled { get; set; }

        public static ObservableCollection<PatientAppointment> Data { get; set; }

        public static void GenerateData()
        {
            Data = new ObservableCollection<PatientAppointment>();
            string[] CaseStatusStrings = { "Open" };
            string[] CaseTypeStrings = { "NoFault" };
            string[] PatientStatusStrings = { "Walk in", "Follow Up", "Rescheduled" };


            Random random = new Random(DateTime.Now.Second);

            for (int i = 0; i < 30; ++i)
            {
                Patient P = Patient.Data[random.Next(0, Patient.Data.Count)];
                ScheduledDoctor Slot = ScheduledDoctor.Data[random.Next(0, ScheduledDoctor.Data.Count)];

                DateTime Date = Slot.From;
                int Duration = random.Next(0, 30);

                string CaseStatus = CaseStatusStrings[random.Next(0, CaseStatusStrings.Length)];
                string CaseType = CaseTypeStrings[random.Next(0, CaseTypeStrings.Length)];
                string PatientStatus = PatientStatusStrings[random.Next(0, PatientStatusStrings.Length)];

                bool ToBeScheduled = random.Next(0, 2) == 0 ? true : false;

                Data.Add(new PatientAppointment(random.Next(0, 10000000), P, Slot, Date, Duration, CaseStatus, CaseType, PatientStatus, ToBeScheduled));
            }
        }
    }
}


namespace SchedulingSystem
{

    /* classes for dummy data
* added on 5th junly, 2018 11:30 AM
*/
    public partial class Clinic
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Clinic(string name, string address, string phone, string email)
        {
            this.Name = name;
            this.Address = address;
            this.Email = email;
            this.Phone = phone;

        }
    };

    public class Doc
    {
        public string name { get; set; }
        public string address { get; set; }

        public string email { get; set; }
        public string phone { get; set; }

        public string Speciality { get; set; }

        public Doc(string name, string address, string email, string phone, string speciality)
        {
            this.address = address;
            this.email = email;
            this.name = name;
            this.phone = phone;
            this.Speciality = speciality;
        }
    };


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