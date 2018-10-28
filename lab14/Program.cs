using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;

namespace lab14
{
    class Program
    {
        static void Main(string[] args)
        {
            Flower cactus = new Flower("Cactus", "Green");
            Bouqet bouqet = new Bouqet() { new Flower("Rose", "Red"), new Flower("Gladiolus", "Yellow") };
            
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            SoapFormatter soapFormatter = new SoapFormatter();
            XmlSerializer xmlFormatter = new XmlSerializer(typeof(Bouqet));
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Bouqet));


            Serializer.Serialize(binaryFormatter, "bouqet.dat", bouqet);
            Serializer.Deserialize(binaryFormatter, "bouqet.dat");

            Serializer.SerializeSoap(soapFormatter, "cactus.soap", cactus);
            Serializer.DeserializeSoap(soapFormatter, "cactus.soap");

            using (FileStream fs = new FileStream("bouqet.xml", FileMode.OpenOrCreate))
            {
                xmlFormatter.Serialize(fs, bouqet);
                Console.WriteLine("Букет сериализован");
            }

            using (FileStream fs = new FileStream("bouqet.xml", FileMode.OpenOrCreate))
            {
                Bouqet newBouqet = (Bouqet)xmlFormatter.Deserialize(fs);

                Console.WriteLine("Букет десериализован\n");
                Console.WriteLine(newBouqet.ToString());
            }
                       
 
            using (FileStream fs = new FileStream("bouqet.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, bouqet);
                Console.WriteLine("Букет сериализован");
            }
 
            using (FileStream fs = new FileStream("bouqet.json", FileMode.OpenOrCreate))
            {
                Bouqet newBouqet = (Bouqet)jsonFormatter.ReadObject(fs);

                Console.WriteLine("Букет десериализован\n");
                Console.WriteLine(newBouqet.ToString());
            }


        }
    }
}
