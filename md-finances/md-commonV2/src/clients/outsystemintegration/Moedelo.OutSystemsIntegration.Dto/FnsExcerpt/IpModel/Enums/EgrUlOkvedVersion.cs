using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Enums
{
    public enum EgrIpOkvedVersion
    {
        [XmlEnum("2001")]
        Version2001,

        [XmlEnum("2014")]
        Version2014
    }
}
