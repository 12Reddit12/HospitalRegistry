using BLL;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit.Sdk;
using System.Collections.Generic;
using DAL;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class TestsHospital
    {
        [TestMethod]
        public void TestMakeDoctor()
        {
            // Arrange
            var service = new DoctorService();
            var listsched = new List<Schedule>();
            
            var sched = new Schedule
            {
                Hour = 1,
                Minute = 1,
            };
            listsched.Add(sched);
            var newdoctor = new Doctor
            {
                Name = "Valera",
                Surname = "Molodoy",
                Specialization = "Kolit",
            };
            newdoctor.Schedule = listsched;
            // Act
            service.AddDoctor("Valera", "Molodoy", "Kolit");
            var read = service.GetDoctors()[0] as Doctor;
            service.SaveDoctors();
            // Assert
            Assert.AreEqual(newdoctor.Name, read.Name);
            Assert.AreEqual(newdoctor.Surname, read.Surname);
            Assert.AreEqual(newdoctor.Specialization, read.Specialization);
            Assert.IsNotNull(read.Schedule);
            Assert.AreEqual(newdoctor.ToString(), read.ToString());
        }
        [TestMethod]
        public void TestMakePatient()
        {
            // Arrange
            var service = new PatientService();
            var card = new List<string> {"adawdwadaw" };
            var newdoctor = new Doctor
            {
                Name = "Ddwaawdawd",
                Surname = "Ddwadawaw",
                Specialization = "Kolit",
            };
            var newpat = new Patient
            {
                Name = "Valera",
                Surname = "Molodoy",

            };
           
            // Act
            service.AddPatient("Valera", "Molodoy");
           
            var read = service.GetPatients()[0] as Patient;
            service.SavePatients();

            // Assert
            Assert.AreEqual(newpat.Name, read.Name);
            Assert.AreEqual(newpat.Surname, read.Surname);
            Assert.AreEqual(newpat.doctor, read.doctor);
            Assert.IsNotNull(read.Card);
            Assert.AreEqual(newpat.ToString(), read.ToString());
            newpat.Card = card;
            newpat.doctor = newdoctor;
        }
        [TestMethod]
        public void TestFindPatient()
        {
            // Arrange
            var service = new PatientService();
            // Act
            service.AddPatient("Valera", "Molodoy");
            var read = service.GetPatients()[0] as Patient;
            var obj = service.FindPatient("Valera", "Molodoy");
            // Assert
            Assert.AreEqual(obj.Name, "Valera");
        }
        [TestMethod]
        public void TestEditPatient()
        {
            // Arrange
            var service = new PatientService();
            // Act
            service.AddPatient("Valera", "Molodoy");
            service.EditPatient(0, "Valer4ik", "Maloy");
            // Assert
            Assert.AreEqual(service.GetPatients()[0].Name, "Valer4ik");
        }

        [TestMethod]
        public void TestEditDoctor()
        {
            // Arrange
            var service = new DoctorService();
            // Act
            service.AddDoctor("Valera", "Molodoy","Tancyet");
            service.EditDoctor(0, "Valer4ik", "Maloy","Netancyet");
            // Assert
            Assert.AreEqual(service.GetDoctors()[0].Name, "Valer4ik");
        }
        [TestMethod]
        public void TestRemovePatient()
        {
            // Arrange
            var service = new PatientService();
            // Act
            service.AddPatient("Valera", "Molodoy");
            service.RemovePatient(0);
            // Assert
            Assert.AreEqual(service.FindPatient("Valеra","Molodoy"),null);
        }
        [TestMethod]
        public void TestRemoveDoctor()
        {
            // Arrange
            var service = new DoctorService();
            // Act
            service.AddDoctor("Valera", "Molodoy","Supervar4");
            service.RemoveDoctor(0);
            // Assert
            Assert.AreEqual(service.GetDoctor(0), service.GetDoctor(0));
        }
        [TestMethod]
        public void TestMakeSchedule()
        {
            // Arrange
            var service = new DoctorService();
            var newdoctor = new Doctor
            {
                Name = "Valera",
                Surname = "Molodoy",
                Specialization = "Kolit",
            };
            // Act
            service.AddDoctor("Valera", "Molodoy", "Kolit");
            var read = service.GetDoctors()[0] as Doctor;
            service.AddScheduleDoctor(0, 1, 1);
            // Assert
            Assert.AreEqual(newdoctor.Name, read.Name);
            Assert.AreEqual(newdoctor.Surname, read.Surname);
            Assert.AreEqual(newdoctor.Specialization, read.Specialization);
            Assert.IsNotNull(read.Schedule);
            Assert.AreEqual(newdoctor.ToString(), read.ToString());
            Assert.AreEqual(read.Schedule[0].Hour, 1);
            Assert.AreEqual(read.Schedule[0].Minute, 1);
        }

        [TestMethod]
        public void TestMakeSchedule11()
        {
            // Arrange
           
            var schedule = new Schedule
            {
                Hour = 1,
                Minute = 1,
            };
            // Act
            schedule.client = new Patient { Name = "Valera", Surname = "Aaaaa", };
            // Assert
            Assert.AreEqual(schedule.ToString(), schedule.ToString());
        }
        [TestMethod]
        public void TestAddPatientToSchedule()
        {
            // Arrange
            var service = new DoctorService();
            var serpat = new PatientService();
            var newdoctor = new Doctor
            {
                Name = "Valera",
                Surname = "Molodoy",
                Specialization = "Kolit",
            };
            // Act
            service.AddDoctor("Valera", "Molodoy", "Kolit");
            var read = service.GetDoctors()[0] as Doctor;
            service.AddScheduleDoctor(0, 1, 1);
            serpat.AddPatient("Valera", "Stariy");
            serpat.AddPatientToDoctor(0, 0, 0);
            // Assert
            Assert.AreEqual(newdoctor.Name, read.Name);
            Assert.AreEqual(newdoctor.Surname, read.Surname);
            Assert.AreEqual(newdoctor.Specialization, read.Specialization);
            Assert.IsNotNull(read.Schedule);
            Assert.AreEqual(newdoctor.ToString(), read.ToString());
            Assert.AreEqual(read.Schedule[0].Hour, 1);
            Assert.AreEqual(read.Schedule[0].Minute, 1);
            Assert.AreEqual(read.Schedule[0].client, serpat.GetPatient(0));

        }
        [TestMethod]
        public void TestRemovePatientFromSchedule()
        {
            // Arrange
            var service = new DoctorService();
            var serpat = new PatientService();

            // Act
            service.AddDoctor("Valera", "Molodoy", "Kolit");
            var read = service.GetDoctors()[0] as Doctor;
            service.AddScheduleDoctor(0, 1, 1);
            serpat.AddPatient("Valera", "Stariy");
            serpat.AddPatientToDoctor(0, 0, 0);
            serpat.RemovePatientFromDoctor(0, 0, 0);
            // Assert

            Assert.AreEqual(read.Schedule[0].client, null);
            
        }

        [TestMethod]
        public void TestAddECardPatient()
        {
            // Arrange
            var serpat = new PatientService();

            // Act
            serpat.AddPatient("Valera", "Stariy");
            serpat.AddToECardPatient(0, "Pokyshal gre4ki");
            // Assert

            Assert.AreEqual(serpat.GetPatients()[0].Card[0], serpat.GetECardPatient(0)[0]);

        }
        [TestMethod()]
        public void AddPatientsExThrowTest1()
        {
            var service = new PatientService();
            Assert.ThrowsException<ExThrow>(() => service.AddPatient(null, "Hello"), "Name can`t be null");
        }

       
        [TestMethod()]
        public void AddPatientsExThrowTest2()
        {
            var service = new PatientService();
            Assert.ThrowsException<ExThrow>(() => service.AddPatient("Privet", null), "Name can`t be null");
        }

        [TestMethod()]
        public void RemovePatientsExThrowTest1()
        {
            var service = new PatientService();
            Assert.ThrowsException<ExThrow>(() => service.RemovePatient(123), "ID out of range");
        }

        [TestMethod()]
        public void EditPatientsExThrowTest1()
        {
            var service = new PatientService();
            service.AddPatient("Valera", "Mitozheymeembegat");
            Assert.ThrowsException<ExThrow>(() => service.EditPatient(123,"Valera","Amineymeem"), "ID out of range");
        }

        [TestMethod()]
        public void EditPatientsExThrowTest2()
        {
            var service = new PatientService();
            service.AddPatient("Valera", "Mitozheymeembegat");
            Assert.ThrowsException<ExThrow>(() => service.EditPatient(0, null, "Amineymeem"), "Name can`t be null");
        }

        [TestMethod()]
        public void EditPatientsExThrowTest3()
        {
            var service = new PatientService();
            service.AddPatient("Valera", "Mitozheymeembegat");
            Assert.ThrowsException<ExThrow>(() => service.EditPatient(0, "Valera", null), "Surname can`t be null");
        }
        [TestMethod()]
        public void EditDoctorsExThrowTest1()
        {
            var service = new DoctorService();
            service.AddDoctor("Valera", "Mitozheymeembegat","Drug");
            Assert.ThrowsException<ExThrow>(() => service.EditDoctor(123, "Valera", "Amineymeem","Drugbad"), "ID out of range");
        }

        [TestMethod()]
        public void EditDoctorsExThrowTest2()
        {
            var service = new DoctorService();
            service.AddDoctor("Valera", "Mitozheymeembegat", "Drug");
            Assert.ThrowsException<ExThrow>(() => service.EditDoctor(0, null, "Amineymeem", "Drugbad"), "Name can`t be null");
        }

        [TestMethod()]
        public void EditDoctorsExThrowTest3()
        {
            var service = new DoctorService();
            service.AddDoctor("Valera", "Mitozheymeembegat", "Drug");
            Assert.ThrowsException<ExThrow>(() => service.EditDoctor(0, "Valera", null, "Drugbad"), "Surname can`t be null");
        }
        [TestMethod()]
        public void EditDoctorsExThrowTest4()
        {
            var service = new DoctorService();
            service.AddDoctor("Valera", "Mitozheymeembegat", "Drug");
            Assert.ThrowsException<ExThrow>(() => service.EditDoctor(0, "Valera", "Tancyet", null), "Specialization can`t be null");
        }

        [TestMethod()]
        public void GetPatientsExThrowTest1()
        {
            var service = new PatientService();
            service.AddPatient("Valera", "Mitozheymeembegat");
            Assert.ThrowsException<ExThrow>(() => service.GetPatient(123), "Index out of range");
        }

        [TestMethod()]
        public void GetDoctorExThrowTest1()
        {
            var service = new DoctorService();
            service.AddDoctor("Valera", "Mitozheymeembegat","Aaa");
            Assert.ThrowsException<ExThrow>(() => service.GetDoctor(123), "Index out of range");
        }

        [TestMethod()]
        public void AddPatientsToDoctorExThrowTest1()
        {
            var service = new PatientService();
            service.AddPatient("Valera", "Mitozheymeembegat");
            Assert.ThrowsException<ExThrow>(() => service.AddPatientToDoctor(123,0,0), "Index out of range");
        }

        [TestMethod()]
        public void AddPatientsToDoctorExThrowTest2()
        {
            var service = new PatientService();
            var s = new DoctorService();
            service.AddPatient("Valera", "Mitozheymeembegat");
            Assert.ThrowsException<ExThrow>(() => service.AddPatientToDoctor(0, 123, 0), "Index out of range");
        }

        [TestMethod()]
        public void AddPatientsToDoctorExThrowTest3()
        {
            var service = new PatientService();
            var s = new DoctorService();
            service.AddPatient("Valera", "Mitozheymeembegat");
            Assert.ThrowsException<ExThrow>(() => service.AddPatientToDoctor(0, 0, 123), "Index out of range");
        }

        [TestMethod()]
        public void AddPatientsToDoctorExThrowTest44()
        {
            var service = new PatientService();
            var s = new DoctorService();
            service.AddPatient("Valera", "Mitozheymeembegat");
            s.AddDoctor("A", "B", "C");
            s.AddScheduleDoctor(0, 0, 0);
            s.GetDoctors()[0].Schedule[0].client = new Patient();
            Assert.ThrowsException<ExThrow>(() => service.AddPatientToDoctor(0, 0, 0), "Schedule is busy");
        }

        [TestMethod()]
        public void RemovePatientsFromDoctorExThrowTest1()
        {
            var service = new PatientService();
            var s = new DoctorService();
            service.AddPatient("Valera", "Mitozheymeembegat");
            Assert.ThrowsException<ExThrow>(() => service.RemovePatientFromDoctor(123, 0, 0), "Index out of range");
        }

        [TestMethod()]
        public void RemovePatientsFromDoctorExThrowTest2()
        {
            var service = new PatientService();
            var s = new DoctorService();
            service.AddPatient("Valera", "Mitozheymeembegat");
            Assert.ThrowsException<ExThrow>(() => service.RemovePatientFromDoctor(0, 123, 0), "Index out of range");
        }

        [TestMethod()]
        public void RemovePatientsFromDoctorExThrowTest3()
        {
            var service = new PatientService();
            var s = new DoctorService();
            service.AddPatient("Valera", "Mitozheymeembegat");
            Assert.ThrowsException<ExThrow>(() => service.RemovePatientFromDoctor(0, 0, 123), "Index out of range");
        }
        
        [TestMethod()]
        public void RemovePatientsFromDoctorExThrowTest45()
        {
            var service1 = new PatientService();
            var s1 = new DoctorService();
            service1.AddPatient("Valera", "Mitozheymeembegat");
            s1.AddDoctor("A", "B", "C");
            s1.AddScheduleDoctor(0, 1, 1);
            Assert.ThrowsException<ExThrow>(() => service1.RemovePatientFromDoctor(0, 0, 0), "Schedule is not busy");
        }

        [TestMethod()]
        public void AddDoctorExThrowTest1()
        {
            var service = new DoctorService();
            Assert.ThrowsException<ExThrow>(() => service.AddDoctor(null,"Aaa","Top"), "Name can`t be null");
        }

        [TestMethod()]
        public void AddDoctorExThrowTest2()
        {
            var service = new DoctorService();
            Assert.ThrowsException<ExThrow>(() => service.AddDoctor("Bbbb", null, "Top"), "Surname can`t be null");
        }

        [TestMethod()]
        public void AddDoctorExThrowTest3()
        {
            var service = new DoctorService();
            Assert.ThrowsException<ExThrow>(() => service.AddDoctor("Bbbb", "Aaa", null), "Specialization can`t be null");
        }

        [TestMethod()]
        public void RemoveDoctorExThrowTest1()
        {
            var service = new DoctorService();
            service.AddDoctor("Bbbb", "Aaa", "Ccc");
            Assert.ThrowsException<ExThrow>(() => service.RemoveDoctor(123), "ID out of range");
        }

        [TestMethod()]
        public void RemoveDoctorExThrowTest2()
        {
            var service = new DoctorService();
            service.AddDoctor("Bbbb", "Aaa", "Ccc");
            Assert.ThrowsException<ExThrow>(() => service.RemoveDoctor(123), "ID out of range");
        }

        [TestMethod()]
        public void AddScheduleDoctorExThrowTest1()
        {
            var service = new DoctorService();
            service.AddDoctor("Bbbb", "Aaa", "Ccc");
            Assert.ThrowsException<ExThrow>(() => service.AddScheduleDoctor(123,1,1), "Index out of range");
        }
        
        [TestMethod()]
        public void AddScheduleDoctorExThrowTest2()
        {
            var service = new DoctorService();
            service.AddDoctor("Bbbb", "Aaa", "Ccc");
            Assert.ThrowsException<ExThrow>(() => service.AddScheduleDoctor(0, 123, 1), "Hours out of range");
        }
        [TestMethod()]
        public void AddScheduleDoctorExThrowTest3()
        {
            var service = new DoctorService();
            service.AddDoctor("Bbbb", "Aaa", "Ccc");
            Assert.ThrowsException<ExThrow>(() => service.AddScheduleDoctor(0, 1, 123), "Minutes out of range");
        }

        [TestMethod()]
        public void AddScheduleDoctorExThrowTest46()
        {
            var service = new DoctorService();
            var service1 = new PatientService();
            service.AddDoctor("Bbbb", "Aaa", "Ccc");
            service.AddScheduleDoctor(0, 12, 12);
            Assert.ThrowsException<ExThrow>(() => service.AddScheduleDoctor(0, 12, 12), "Schedule is already existed");
        }

        [TestMethod()]
        public void Removeshecudeldoctortest1()
        {
            var service = new DoctorService();
            var service1 = new PatientService();
            service.AddDoctor("Bbbb", "Aaa", "Ccc");
            Assert.ThrowsException<ExThrow>(() => service.RemoveScheduleDoctor(123,0), "Index out of range");
        }

        [TestMethod()]
        public void Removeshecudeldoctortest2()
        {
            var service = new DoctorService();
            var service1 = new PatientService();
            service.AddDoctor("Bbbb", "Aaa", "Ccc");
            Assert.ThrowsException<ExThrow>(() => service.RemoveScheduleDoctor(0, 123), "Index out of range");
            
        }

        [TestMethod()]
        public void Removeshecudeldoctortest33()
        {
            var service = new DoctorService();
            var service1 = new PatientService();
            service.AddDoctor("Bbbb", "Aaa", "Ccc");
            service.AddScheduleDoctor(0, 12, 12);
            service.RemoveScheduleDoctor(0, 0);

        }

        [TestMethod()]
        public void EditScheduleDoctorTest1()
        {
            var service = new DoctorService();
            var service1 = new PatientService();
            service.AddDoctor("Bbbb", "Aaa", "Ccc");
            service.AddScheduleDoctor(0, 12, 12);
            Assert.ThrowsException<ExThrow>(() => service.EditScheduleDoctor(123, 0,1,1), "Index out of range");

        }
        [TestMethod()]
        public void EditScheduleDoctorTest2()
        {
            var service = new DoctorService();
            var service1 = new PatientService();
            service.AddDoctor("Bbbb", "Aaa", "Ccc");
            service.AddScheduleDoctor(0, 12, 12);
            Assert.ThrowsException<ExThrow>(() => service.EditScheduleDoctor(0, 123, 1, 1), "Index out of range");

        }
        [TestMethod()]
        public void EditScheduleDoctorTest3()
        {
            var service = new DoctorService();
            var service1 = new PatientService();
            service.AddDoctor("Bbbb", "Aaa", "Ccc");
            service.AddScheduleDoctor(0, 12, 12);
            Assert.ThrowsException<ExThrow>(() => service.EditScheduleDoctor(0, 0, 123, 1), "Hours out of range");

        }

        [TestMethod()]
        public void EditScheduleDoctorTest4()
        {
            var service = new DoctorService();
            var service1 = new PatientService();
            service.AddDoctor("Bbbb", "Aaa", "Ccc");
            service.AddScheduleDoctor(0, 12, 12);
            service.EditScheduleDoctor(0, 0, 1, 1);
            Assert.ThrowsException<ExThrow>(() => service.EditScheduleDoctor(0, 0, 1, 123), "Minutes out of range");

        }

        [TestMethod()]
        public void TryCatchDoctorTest()
        {
            var service = new DoctorService();
            var service1 = new PatientService();
            
            service.AddDoctor("Bbbb", "Aaa", "Ccc");
            service.AddScheduleDoctor(0, 12, 12);
            service.EditScheduleDoctor(0, 0, 1, 1);
            Assert.ThrowsException<ExThrow>(() => service.EditScheduleDoctor(0, 0, 1, 123), "Minutes out of range");

        }
    }
}
