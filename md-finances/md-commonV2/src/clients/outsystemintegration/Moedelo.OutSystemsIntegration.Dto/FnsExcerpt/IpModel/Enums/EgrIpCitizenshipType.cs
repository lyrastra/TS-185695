using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Enums
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
