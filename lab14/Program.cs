using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

namespace lab14
{
    class Program
    {
        static void Main(string[] args)
        {
            Flower cactus = new Flower("Cactus", "Green");
            Bouqet bouqet = new Bouqet() { new Flower("Rose", "Red"), new Flower("Gladiolus", "Yellow"), new Flower("Rose", "White") };

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


            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("bouqet.xml");
            XmlElement xRoot = xmlDoc.DocumentElement;

            Console.WriteLine("--------------XPATH--------------");

            XmlNodeList childnodes = xRoot.SelectNodes("Flower/Color");
            foreach (XmlNode n in childnodes)
                Console.WriteLine(n.InnerText);

            Console.WriteLine("\nЦветок красного цвета");
            XmlNode redFlower = xRoot.SelectSingleNode("Flower[Color = 'Red']");
            Console.WriteLine(redFlower.InnerText);

            Console.WriteLine("--------------Linq to Xml--------------");

            XDocument xDoc = XDocument.Load("bouqet.xml");
            XElement xBouqet = xDoc.Element("ArrayOfFlower");

            //Console.WriteLine(xBouqet.Element("Flower").Value);

            foreach (var el in xBouqet.Elements("Flower"))
            {
                Console.WriteLine(el.Element("Color").Value);
            }



            Console.WriteLine("\nЦветы жёлтого цвета");
            var flowers = from xf in xDoc.Element("ArrayOfFlower").Elements("Flower")
                          where xf.Element("Color").Value == "Yellow"
                          select new Flower
                          {
                              Name = xf.Element("Name").Value,
                              Color = xf.Element("Color").Value
                          };

            foreach(var flower in flowers)
            {
                Console.WriteLine(flower.Name);
            }

        }
    }
}
