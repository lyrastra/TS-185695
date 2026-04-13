using System.Xml.Serialization;

namespace Moedelo.Common.Enums.Enums.EgrIp
{
    public enum EgrIpOkvedVersion
    {
        [XmlEnum("2001")]
        Version2001,

        [XmlEnum("2014")]
        Version2014
    }
}
