using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Enums
{
    /// <summary>
    /// Признак невозможности взаимодействия с юридическим лицом по содержащемуся в ЕГРЮЛ адресу
    /// </summary>
    [XmlType(AnonymousType = true)]
    public enum EgrUlSignLackAddress
    {
        /// <summary>
        /// связь с юридическим лицом по указанному в ЕГРЮЛ адресу отсутствует
        /// </summary>
        [XmlEnum("1")]
        CommunicationByThisAddressAbsent = 1,

        /// <summary>
        /// адрес, указанный юридическим лицом при государственной регистрации, не существует
        /// </summary>
        [XmlEnum("2")]
        AddressNotExist = 2,

        /// <summary>
        /// сведения об адресе являются недостоверными на основании решения суда
        /// </summary>
        [XmlEnum("3")]
        AddressInvalidOnBasisOfCourtDecision = 3
    }
}
