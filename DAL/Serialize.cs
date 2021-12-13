using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DAL
{
    public class Serialize<T> where T : class
    {
        private string fileName;
        BinaryFormatter binformat = new BinaryFormatter();
        public Serialize(string fileName)
        {
            this.fileName = fileName;
        }

        public virtual void Save(T[] data)
        {
            using (FileStream fstream = new FileStream(fileName + ".dat", FileMode.Create))
            {
                if (data != null)
                    binformat.Serialize(fstream, data);
            }
        }

        public virtual T[] Load()
        {

            using (FileStream fstream = new FileStream(fileName + ".dat", FileMode.Open))
            {
                return binformat.Deserialize(fstream) as T[];
            }
        }
    }
}
