using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Enums
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
