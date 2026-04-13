using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.IpModel.Enums
{
    /// <summary>
    /// Наименование классификатора, по которому введены сведения об организационно-правовой форме: ОКОПФ, КОПФ
    /// </summary>
    [XmlType(AnonymousType = true)]
    public enum EgrIpType
    {
        [XmlEnum("1")]
        Ip,

        [XmlEnum("2")]
        PeasantFarm
    }
}
