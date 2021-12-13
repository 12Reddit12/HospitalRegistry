using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class DoctorService
    {
        public List<Doctor> Doctors { get; private set; } = new List<Doctor>();
        Serialize<Doctor> serialize;
        public static DoctorService instanse;
        public DoctorService()
        {
            instanse = this;
            serialize = new Serialize<Doctor>("Doctors");
            try { Doctors = serialize.Load().ToList(); }
            catch { serialize.Save(Doctors.ToArray()); }
        }
        public void AddDoctor(string name, string surname,string specialization)
        {
            if (name == null)
                throw new ExThrow("Name can`t be null");
            if (surname == null)
                throw new ExThrow("Surname can`t be null");
            if (specialization == null)
                throw new ExThrow("Specialization can`t be null");
            Doctors.Add(new Doctor { Name = name,Surname = surname, Specialization = specialization });
        }

        public void RemoveDoctor(int id)
        {
            if (id < 0 || id >= Doctors.Count)
                throw new ExThrow("ID out of range");
                Doctors.RemoveAt(id);
        }

        public void EditDoctor(int id, string name, string surname, string specialization)
        {
            if (id < 0 || id >= Doctors.Count)
                throw new ExThrow("Index out of range");
            if (name == null)
                throw new ExThrow("Name can`t be null");
            if (surname == null )
                throw new ExThrow("Surname can`t be null");
            if (specialization == null)
                throw new ExThrow("Speciazliation can`t be null");
            Doctors[id].Name = name;
            Doctors[id].Surname = surname;
            Doctors[id].Specialization = specialization;
        }

        public Doctor GetDoctor(int id)
        {
            if (id < 0 || id >= Doctors.Count)
                throw new ExThrow("Index out of range");
            return Doctors[id];
        }
        public List<Doctor> GetDoctors()
        {
            return Doctors;
        }
        public void AddScheduleDoctor(int id,int h, int m)
        {
            if (id < 0 || id >= Doctors.Count)
                throw new ExThrow("Index out of range");
            if (h < 0 || h > 24)
                throw new ExThrow("Hours out of range");
            if (m < 0 || m > 60)
                throw new ExThrow("Minutes out of range");
            foreach (var item in Doctors[id].Schedule)
            {
                if (item.Hour == h && item.Minute == m)
                    throw new ExThrow("Schedule is already existed");
            }
                
            Doctors[id].Schedule.Add(new Schedule { Hour = h, Minute = m });
        }

        public void RemoveScheduleDoctor(int id, int id2)
        {
            if (id < 0 || id >= Doctors.Count)
                throw new ExThrow("Index out of range");
            if (id2 < 0 || id2 >= Doctors[id].Schedule.Count)
                throw new ExThrow("Index out of range");
            Doctors[id].Schedule.RemoveAt(id2);
        }
        public void EditScheduleDoctor(int id, int id2,int h, int m)
        {
            if (id < 0 || id >= Doctors.Count)
                throw new ExThrow("Index out of range");
            if (id2 < 0 || id2 >= Doctors[id].Schedule.Count)
                throw new ExThrow("Index out of range");
            if (h < 0 || h > 24)
                throw new ExThrow("Hours out of range");
            if (m < 0 || m > 60)
                throw new ExThrow("Minutes out of range");
            Doctors[id].Schedule[id2].Hour = h;
            Doctors[id].Schedule[id2].Minute = m;
        }
        public void SaveDoctors() => serialize.Save(Doctors.ToArray());
    }
}
