using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp20
{
    class Program
    {
        static void Serialize(string fileName, Object obj)
        {
            using (var stream = System.IO.File.Create(fileName))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
                serializer.Serialize(stream, obj);
            }
        }

        static Object DeSerialize(string fileName, Object obj)
        {
            using (var stream = System.IO.File.OpenText(fileName))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
                return serializer.Deserialize(stream);
            }
        }

        static void Main(string[] args)
        {
            string[] names = { "Maccabi", "Jalgiris" };
            int[] scores = { 1000, 300 };

            //Saving the array in file
            Serialize("names.xml", names);
            //Reading from a file
            string[] name1 = new string[0];
            name1 = (string []) DeSerialize("names.xml", name1);
        }
    }
}
