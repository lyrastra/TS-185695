using System.Xml.Serialization;

namespace Moedelo.Common.Enums.Enums.EgrIp
{
    [XmlType(AnonymousType = true)]
    public enum EgrIpGender
    {
        [XmlEnum("1")]
        Male,

        [XmlEnum("2")]
        Female
    }
}