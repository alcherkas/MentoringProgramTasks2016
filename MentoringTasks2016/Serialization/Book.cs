using System;
using System.Xml.Serialization;

namespace Serialization
{
    [Serializable]
    public class Book
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlElement("isbn")]
        public string Isbn;
        [XmlElement("title")]
        public string Title;
        [XmlElement("author")]
        public string Author;
        [XmlElement("genre")]
        public Genre Genre;
        [XmlElement("publisher")]
        public string Publisher { get; set; }

        [XmlElement("publish_date")]
        public string PublishDateString
        {
            get { return PublishDate.ToString("yyyy-MM-dd"); }
            set { PublishDate = DateTime.ParseExact(value, "yyyy-MM-dd", null); }
        }

        [XmlIgnore]
        public DateTime PublishDate;

        [XmlElement("description")]
        public string Description;

        [XmlElement("registration_date")]
        public string RegistrationDateString
        {
            get { return RegistrationDate.ToString("yyyy-MM-dd"); }
            set { RegistrationDate = DateTime.ParseExact(value, "yyyy-MM-dd", null); }
        }

        [XmlIgnore]
        public DateTime RegistrationDate;
    }
}