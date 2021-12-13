using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    [Serializable]
    public class Patient
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Card { get; set; } = new List<string>();
        public Doctor doctor { get; set; }
        public override string ToString() => "Patient: " + Name + " " + Surname;

    }
}
