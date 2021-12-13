using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BLL;

namespace PL
{
    public class Menu
    {
        DoctorService docserv = new DoctorService();
        PatientService patserv = new PatientService();
        public static string eSymBig(string name)
        {
            Console.WriteLine($"Enter the {name}");
            Regex regex = new Regex(@"[A-Z]{1}[a-z]+$");
            string temp = Convert.ToString(Console.ReadLine());
            while (!regex.IsMatch(temp))
            {
                Console.WriteLine("Error invalid input data,try again");
                temp = Convert.ToString(Console.ReadLine());
            }
            return temp;
        }

        public static string eAction()
        {
            Console.WriteLine($"Enter the action");
            string temp = Convert.ToString(Console.ReadLine());
            return temp;
        }

        public static int eNumInt(string name)
        {
            Console.WriteLine($"Enter the {name}");
            Regex regex = new Regex(@"^\d+$");
            string temp = Convert.ToString(Console.ReadLine());
            if (name == "Hour" && (int.Parse(temp) > 24) || name == "Hour" && (int.Parse(temp) < 0) || name == "Minute" && (int.Parse(temp) > 60) || name == "Minute" && (int.Parse(temp) < 0))
            {
                temp = "";
            }
            while (!regex.IsMatch(temp))
            {
                Console.WriteLine("Error invalid input data, try again");
                temp = Convert.ToString(Console.ReadLine());
                if (name == "Hour" && (int.Parse(temp) > 24) || name == "Hour" && (int.Parse(temp) < 0) || name == "Minute" && (int.Parse(temp) > 60) || name == "Minute" && (int.Parse(temp) < 0))
                {
                    temp = "";
                }
            }
            return int.Parse(temp);
        }

        public void Start()
        {
            
            while (true)
            {
                Console.WriteLine("Select Action:\n" +
                    "1.Doctor\n" +
                    "2.Patient\n" +
                    "3.Exit\n");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        Console.WriteLine("Select action:\n" +
                            "1.Add a Doctor\n" +
                            "2.Show list of Doctors\n" +
                            "3.Find Doctor\n" +
                            "4.Delete Doctor\n" +
                            "5.Add Schedule Doctor\n"+
                            "6.Show Schedule List With Doctors\n" +
                            "7.Find Doctor Schedule\n" +
                            "8.Edit Schedule Doctor\n" +
                            "9.Remove Schedule Doctor\n"+
                        "0.Back\n");

                        command = Console.ReadLine();
                        switch (command)
                        {
                            case "1":
                                
                                try
                                {
                                    docserv.AddDoctor(eSymBig("Name"), eSymBig("Surname"), eSymBig("Specialization"));
                                }
                                catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                                break;
                            case "2":
                                
                                try
                                {
                                    int id = 0;
                                    foreach (var value in docserv.GetDoctors())
                                    {
                                        Console.WriteLine($"ID:{id}  {value.ToString()}");
                                        id++;
                                    }
                                }
                                catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                                break;
                            case "3":
                                Console.WriteLine("Switch Method Find:\n"+
                                    "1.Find by ID\n"+
                                    "2.Find by Name and Surname\n" +
                                    "3.Back\n");
                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        try
                                        {
                                            Console.WriteLine(docserv.GetDoctor(eNumInt("ID Doctor")));
                                        }
                                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                                        break;
                                    case "2":
                                        string name, surname;
                                        try
                                        {
                                            name = eSymBig("Name");
                                            surname = eSymBig("Surname");
                                            int ids = 0;
                                            foreach (var item in docserv.GetDoctors())
                                            {
                                                if (item.Name == name && item.Surname == surname)
                                                {
                                                    Console.WriteLine("Finded that");
                                                    Console.WriteLine(item.ToString());
                                                    Console.WriteLine("Doctor Schedule");
                                                    foreach (var schedule in item.Schedule)
                                                    {
                                                        if (schedule.client != null)
                                                        {
                                                            Console.WriteLine($"ID:{ids} {schedule.Hour.ToString("00")}:{schedule.Minute.ToString("00")} Has a patient");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine($"ID:{ids} {schedule.Hour.ToString("00")}:{schedule.Minute.ToString("00")} No patients writed");
                                                        }
                                                        ids++;
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                                        break;
                                    case "3":
                                        break;
                                    default:
                                        break;
                                }

                                
                                break;
                            case "4":
                                
                                try
                                {
                                    docserv.RemoveDoctor(eNumInt("ID Doctor"));
                                }
                                catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                                break;
                            case "5":
                                
                                try
                                {
                                    docserv.AddScheduleDoctor(eNumInt("ID Doctor"), eNumInt("Hour"), eNumInt("Minute"));
                                }
                                catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                                break;
                            case "6":
                                
                                try
                                {
                                    int idd = 0;
                                    int ids = 0;
                                    foreach (var doc in docserv.GetDoctors())
                                    {
                                        Console.WriteLine($"ID:{idd}  {doc.ToString()}");
                                        Console.WriteLine($"Schedule List:\n");
                                        foreach (var schedule in doc.Schedule)
                                        {
                                            if (schedule.client != null)
                                            {
                                                Console.WriteLine($"ID:{ids} {schedule.Hour.ToString("00")}:{schedule.Minute.ToString("00")} Has a patient");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"ID:{ids} {schedule.Hour.ToString("00")}:{schedule.Minute.ToString("00")} No patients writed");
                                            }
                                            ids++;
                                        }
                                        idd++;
                                    }
                                }
                                catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                                break;
                            case "7":
                                try
                                {

                                    Doctor doc = docserv.GetDoctor(eNumInt("ID Doctor"));
                                    int hour = eNumInt("Hour");
                                    int ids = 0;
                                        Console.WriteLine($"Schedule Finded:\n");
                                        foreach (var schedule in doc.Schedule)
                                        {
                                        ids++;
                                        if (schedule.Hour != hour)
                                        {
                                            continue;
                                        }
                                        if (schedule.client != null)
                                            {
                                                Console.WriteLine($"ID:{ids} {schedule.Hour.ToString("00")}:{schedule.Minute.ToString("00")} Has a patient");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"ID:{ids} {schedule.Hour.ToString("00")}:{schedule.Minute.ToString("00")} No patients writed");
                                            }
                                            
                                        }
                                       
                                }
                                catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                                break;
                            case "8":
                               
                                try
                                {
                                    docserv.EditScheduleDoctor(eNumInt("ID Doctor"), eNumInt("ID Schedule"), eNumInt("Hour"), eNumInt("Minute"));
                                }
                                catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                                break;
                            case "9":
                                
                                try
                                {
                                    docserv.RemoveScheduleDoctor(eNumInt("ID Doctor"), eNumInt("ID Schedule"));
                                }
                                catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                                break;
                            case "0":
                               
                                break;
                            default:
                                Start();
                                Console.WriteLine("Incorrect data");
                                break;
                        }
                        break;
                    case "2":
                        Console.WriteLine("Select action:\n" +
                            "1.Add a Patient\n" +
                            "2.Show list of Patients\n" +
                            "3.Find Patient\n" +
                            "4.Delete Patient\n" +
                            "5.Write Patient to Doctor\n" +
                            "6.Delete Patient from Doctor\n"+
                            "7.Show Patient Card\n" +
                            "8.Add to Patient Card Action\n" +
                            "9.Back\n");
                        command = Console.ReadLine();
                        switch (command)
                        {
                            case "1":
                                try
                                {
                                    patserv.AddPatient(eSymBig("Name"), eSymBig("Surname"));
                                }
                                catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                                
                                break;
                            case "2":
                                try
                                {
                                    int id = 0;
                                    foreach (var value in patserv.GetPatients())
                                        Console.WriteLine($"ID:{id}  {value.ToString()}");
                                    id++;
                                }
                                catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                                break;
                            case "3":
                                Console.WriteLine("Switch Method Find:\n" +
                                    "1.Find by ID\n" +
                                    "2.Find by Name and Surname\n" +
                                    "3.Back\n");
                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        try
                                        {
                                            Console.WriteLine(patserv.GetPatient(eNumInt("ID")));
                                        }
                                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                                        
                                        break;
                                    case "2":

                                        try
                                        {
                                            Console.WriteLine(patserv.FindPatient(eSymBig("Name"), eSymBig("Surname")).ToString());
                                        }
                                        catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                                        break;
                                    case "3":
                                        break;
                                    default:
                                        break;
                                }
                                
                                break;
                            case "4":
                                
                                try
                                {
                                    patserv.RemovePatient(eNumInt("ID Patient"));
                                }
                                catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                                break;
                            case "5":
                                
                                try
                                {
                                    patserv.AddPatientToDoctor(eNumInt("ID Patient"), eNumInt("ID Doctor"), eNumInt("ID Schedule"));
                                    
                                }
                                catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                                break;
                            case "6":
                                
                                try
                                {
                                    patserv.RemovePatientFromDoctor(eNumInt("ID Patient"), eNumInt("ID Doctor"), eNumInt("ID Schedule"));
                                }
                                catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                                break;
                            case "7":
                                
                                try
                                {
                                    int idp = eNumInt("ID Patient");
                                    Patient pat = patserv.GetPatient(idp);
                                    Console.WriteLine($"E-Card patient {pat.Name} {pat.Surname}:\n");
                                    foreach (var item in pat.Card)
                                    {
                                        Console.WriteLine($"{item}\n");
                                    }
                                }
                                catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                                break;
                            case "8":
                                try
                                {
                                    patserv.AddToECardPatient(eNumInt("ID Patient"),eAction());
                                }
                                catch (Exception e) { Console.WriteLine(e.Message); Console.ReadKey(); }
                                break;
                            case "9":
                                break;
                            default:
                                Console.WriteLine("Incorrect data");
                                break;
                        }
                        break;
                    case "3":
                        docserv.SaveDoctors();
                        patserv.SavePatients();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Incorrect data");
                        break;
                }
            }
        }

    }
}
