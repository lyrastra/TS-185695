using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Moedelo.Money.PaymentOrders.Business
{
    internal static class XmlHelper
    {
        public static string Serialize(object obj)
        {
            var serializer = new XmlSerializer(obj.GetType());
            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, obj);
                return stream.ToString();
            }
        }

        public static T Deserialize<T>(string xml) where T : new()
        {
            var serializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(xml))
            {
                var result = serializer.Deserialize(reader);
                return (T)result;
            }
        }
    }
}