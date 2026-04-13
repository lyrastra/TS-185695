using System.Xml.Serialization;

namespace Moedelo.Common.Enums.Enums.EgrIp
{
    /// <summary>
    /// Вид обременения
    /// </summary>
    [XmlType(AnonymousType = true)]
    public enum EgrUlEncumbranceType
    {
        [XmlEnum("ЗАЛОГ")]
        pledge,

        [XmlEnum("ИНОЕ ОБРЕМЕНЕНИЕ")]
        OtherEncumbrance
    }
}