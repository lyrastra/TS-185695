using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Enums
{
    /// <summary>
    /// Вид обременения
    /// </summary>
    [XmlType(AnonymousType = true)]
    public enum EgrUlFounderType
    {
        /// <summary>
        /// если учредителем (участником) является  Российская Федерация
        /// </summary>
        [XmlEnum("1")]
        RussianFederation,

        /// <summary>
        /// если учредителем (участником) является субъект Российской организации
        /// </summary>
        [XmlEnum("2")]
        Subject,

        /// <summary>
        /// если учредителем (участником) является муниципальное образование
        /// </summary>
        [XmlEnum("3")]
        Municipality
    }
}
