using System.Xml.Serialization;

namespace Moedelo.Common.Enums.Enums.EgrIp
{
    /// <summary>
    /// Наименование классификатора, по которому введены сведения об организационно-правовой форме: ОКОПФ, КОПФ
    /// </summary>
    [XmlType(AnonymousType = true)]
    public enum EgrUlOpfType
    {
        [XmlEnum("ОКОПФ")]
        OKOPF,

        [XmlEnum("КОПФ")]
        KOPF
    }
}