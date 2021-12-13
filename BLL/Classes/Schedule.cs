using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    [Serializable]
    public class Schedule
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public Patient client { get; set; }
        public override string ToString()
        {
            return "Time: " + Hour + ":" + Minute;
        }
    }
}
