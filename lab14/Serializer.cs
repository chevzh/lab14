using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

using System.Text;
using System.Threading.Tasks;

namespace lab14
{
    static class Serializer
    {
        public static void Serialize(IFormatter formatter, string path, object bouqet)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, bouqet);
                Console.WriteLine("Букет сериализован");
            }
        }


        public static void Deserialize(IFormatter formatter, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                Bouqet bouqet = (Bouqet)formatter.Deserialize(fs);

                Console.WriteLine("Букет десериализован\n");
                Console.WriteLine(bouqet.ToString());               
            }
        }

        public static void SerializeSoap(IFormatter formatter, string path, object flower)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, flower);
                Console.WriteLine("Цветок сериализован");
            }
        }


        public static void DeserializeSoap(IFormatter formatter, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                Flower flower = (Flower)formatter.Deserialize(fs);

                Console.WriteLine("Цветок десериализован");
                Console.WriteLine(flower.ToString());
            }
        }


    }
}
