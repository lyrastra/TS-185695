using System.Xml.Serialization;

namespace Moedelo.Common.Enums.Enums.EgrIp
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