using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class PatientService
    {
        public List<Patient> Patients { get; private set; } = new List<Patient>();
        Serialize<Patient> serialize;
        public static PatientService instanse;
        public PatientService()
        {
            instanse = this;
            serialize = new Serialize<Patient>("Patients");
            try { Patients = serialize.Load().ToList(); }
            catch { serialize.Save(Patients.ToArray()); }
        }
        public void AddPatient(string name, string surname)
        {
            if (name == null)
                throw new ExThrow("Name can`t be null");
            if (surname == null)
                throw new ExThrow("Surname can`t be null");
            Patients.Add(new Patient { Name = name, Surname = surname });
        }
        public void RemovePatient(int id)
        {
            if (id < 0 || id >= Patients.Count)
                throw new ExThrow("ID out of range");
            Patients.RemoveAt(id);
        }
        public void EditPatient(int id, string name, string surname)
        {
            if (id < 0 || id >= Patients.Count)
                throw new ExThrow("Index out of range");
            if (name == null)
                throw new ExThrow("Name can`t be null");
            if (surname == null)
                throw new ExThrow("Surname can`t be null");
            Patients[id].Name = name;
            Patients[id].Surname = surname;
        }

        public Patient GetPatient(int id)
        {
            if (id < 0 || id >= Patients.Count)
                throw new ExThrow("Index out of range");
            return Patients[id];
        }
        public Patient FindPatient(string name,string surname)
        {
            foreach (var item in Patients)
            {

                if (item.Name == name && item.Surname == surname)
                {
                    return item;
                }
            }
            return null;
        }
        public List<Patient> GetPatients()
        {
            return Patients;
        }
        public void AddPatientToDoctor(int id,int id2,int id3)
        {
            if (id < 0 || id >= Patients.Count)
                throw new ExThrow("Index out of range");
            if (id2 < 0 || id2 >= DoctorService.instanse.Doctors.Count)
                throw new ExThrow("Index out of range");
            if (id3 < 0 || id3 >= DoctorService.instanse.Doctors[id2].Schedule.Count)
                throw new ExThrow("Index out of range");
            if (DoctorService.instanse.Doctors[id2].Schedule[id3].client != null)
                throw new ExThrow("Schedule is busy");
            DoctorService.instanse.Doctors[id2].Schedule[id3].client = Patients[id];
            AddToECardPatient(id, $"Writed to {DoctorService.instanse.Doctors[id2].Name} {DoctorService.instanse.Doctors[id2].Surname} to {DoctorService.instanse.Doctors[id2].Specialization}");
        }

        public void RemovePatientFromDoctor(int id, int id2, int id3)
        {
            if (id < 0 || id >= Patients.Count)
                throw new ExThrow("Index out of range");
            if (id2 < 0 || id2 >= DoctorService.instanse.Doctors.Count)
                throw new ExThrow("Index out of range");
            if (id3 < 0 || id3 >= DoctorService.instanse.Doctors[id2].Schedule.Count)
                throw new ExThrow("Index out of range");
            if (DoctorService.instanse.Doctors[id2].Schedule[id3].client == null)
                throw new ExThrow("Schedule is not busy");
            DoctorService.instanse.Doctors[id2].Schedule[id3].client = null;
            AddToECardPatient(id, $"Out from {DoctorService.instanse.Doctors[id2].Name} {DoctorService.instanse.Doctors[id2].Surname} to {DoctorService.instanse.Doctors[id2].Specialization}");
        }

        public void AddToECardPatient(int id,string action)
        {
            Patients[id].Card.Add(action);
        }

        public List<string> GetECardPatient(int id)
        {
            return Patients[id].Card;
        }

        public void SavePatients() => serialize.Save(Patients.ToArray());
    }
}
