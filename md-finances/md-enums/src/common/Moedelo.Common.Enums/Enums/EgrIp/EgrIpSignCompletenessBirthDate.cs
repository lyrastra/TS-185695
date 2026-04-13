using System.Xml.Serialization;

namespace Moedelo.Common.Enums.Enums.EgrIp
{
    /// <summary>
    /// Признак полноты представляемой даты рождения физического лица
    /// </summary>
    [XmlType(AnonymousType = true)]
    public enum EgrIpSignCompletenessBirthDate
    {
        /// <summary>
        /// только год (сведения о месяце и дне в указанном месяце отсутствуют)
        /// </summary>
        [XmlEnum("1")]
        OnlyYear = 1,

        /// <summary>
        /// только месяц и код (сведения о дне в указанном месяце отсутствуют)
        /// </summary>
        [XmlEnum("2")]
        OnlyYearAndMonth = 2,

        /// <summary>
        /// полная дата
        /// </summary>
        [XmlEnum("3")]
        FullDate = 3
    }
}