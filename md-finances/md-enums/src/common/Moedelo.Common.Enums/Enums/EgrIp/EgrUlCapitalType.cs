using System.Xml.Serialization;

namespace Moedelo.Common.Enums.Enums.EgrIp
{
    public enum EgrUlCapitalType
    {
        [XmlEnum("УСТАВНЫЙ КАПИТАЛ")]
        AuthorizedCapital,

        [XmlEnum("СКЛАДОЧНЫЙ КАПИТАЛ")]
        PooledCapital,

        [XmlEnum("УСТАВНЫЙ ФОНД")]
        StatutoryFund,

        [XmlEnum("ПАЕВЫЕ ВЗНОСЫ")]
        ShareContribution,

        [XmlEnum("ПАЕВОЙ ФОНД")]
        ShareFund,
    }
}