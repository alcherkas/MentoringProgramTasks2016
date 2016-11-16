using System;
using System.IO;
using System.Xml.Serialization;

namespace Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlSerializer s = new XmlSerializer(typeof(Catalog));
            var tester = new XmlDataContractSerializerTester<Catalog>(s, true);
            var streamReader = new StreamReader("books.xml");
            var catalog = tester.Deserialization(streamReader.BaseStream);

            tester.SerializeAndDeserialize(catalog);

            Console.ReadKey();
        }
    }
}
