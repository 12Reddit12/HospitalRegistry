using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    [Serializable]
    public class Doctor
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Specialization { get; set; }
        public List<Schedule> Schedule { get; set; } = new List<Schedule>();
        public override string ToString() => "Doctor: " + Name + " " + Surname + " Specialization: " + Specialization;

    }
}
