using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Enums
{
    /// <summary>
    /// Состояние юридического лица после завершения реорганизации
    /// </summary>
    [XmlType(AnonymousType = true)]
    public enum EgrUlStatuAfterReorg
    {
        /// <summary>
        /// ПРЕКРАТИТ ДЕЯТЕЛЬНОСТЬ ПОСЛЕ РЕОРГАНИЗАЦИИ
        /// </summary>
        [XmlEnum("ПРЕКРАТИТ ДЕЯТЕЛЬНОСТЬ ПОСЛЕ РЕОРГАНИЗАЦИИ")]
        StopActivity,

        /// <summary>
        /// ПРОДОЛЖИТ ДЕЯТЕЛЬНОСТЬ ПОСЛЕ РЕОРГАНИЗАЦИИ
        /// </summary>
        [XmlEnum("ПРОДОЛЖИТ ДЕЯТЕЛЬНОСТЬ ПОСЛЕ РЕОРГАНИЗАЦИИ")]
        ContinuingActivity 
    }
}
