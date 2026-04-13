using System.Xml.Serialization;

namespace Moedelo.Common.Enums.Enums.EgrIp
{
    [XmlType(AnonymousType = true)]
    public enum EgrIpCitizenshipType
    {
        [XmlEnum("1")]
        CitizenRF,

        [XmlEnum("2")]
        CitizenForeign,

        [XmlEnum("3")]
        Stateless
    }
}