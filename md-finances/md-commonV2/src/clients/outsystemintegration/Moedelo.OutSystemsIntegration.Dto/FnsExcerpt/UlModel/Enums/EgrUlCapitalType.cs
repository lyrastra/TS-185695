using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Enums
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
