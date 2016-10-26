using System.IO;
using System.Xml.Serialization;

namespace Serialization
{
    public class XmlDataContractSerializerTester<T> : SerializationTester<T, XmlSerializer>
    {
        public XmlDataContractSerializerTester(
            XmlSerializer serializer, bool showResult = false) : base(serializer, showResult)
        { }

        internal override T Deserialization(Stream stream)
        {
            return (T)serializer.Deserialize(stream);
        }

        internal override void Serialization(T data, Stream stream)
        {
            serializer.Serialize(stream, data);
        }
    }
}