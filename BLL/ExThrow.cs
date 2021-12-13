using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ExThrow: Exception
    {
        private string message;
       

        public ExThrow(string message)
        {
            this.message = message;
        }

        public override string Message => message;
    }
}
