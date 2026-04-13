using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Enums
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
