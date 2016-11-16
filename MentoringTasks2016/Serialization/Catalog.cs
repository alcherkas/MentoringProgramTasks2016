using System;
using System.Xml.Serialization;

namespace Serialization
{
    [Serializable]
    [XmlRoot("catalog", Namespace = "http://library.by/catalog")]
    public class Catalog
    {
        [XmlAttribute("date")]
        public string DateString
        {
            get { return Date.ToString("yyyy-MM-dd"); }
            set { Date = DateTime.ParseExact(value, "yyyy-MM-dd", null); }
        }

        [XmlIgnore]
        public DateTime Date;

        [XmlElement("book")]
        public Book[] Books;
    }
}