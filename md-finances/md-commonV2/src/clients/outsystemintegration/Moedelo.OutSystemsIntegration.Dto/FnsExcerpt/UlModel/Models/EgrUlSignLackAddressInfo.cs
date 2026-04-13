using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Enums;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о недостоверности адреса или отсутствии связи с ЮЛ по указанному адресу
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlSignLackAddressInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Сведения о решении суда, на основании которого адрес признан недостоверным
        /// </summary>
        [XmlElement("РешСудНедАдр")]
        public EgrUlCourtDecisionInfo EgrUlCourtDecision { get; set; }

        /// <summary>
        /// Признак невозможности взаимодействия с юридическим лицом по содержащемуся в ЕГРЮЛ адресу
        /// </summary>
        [XmlAttribute("ПризнОтсутАдресЮЛ")]
        public EgrUlSignLackAddress EgrUlSignLackAddress { get; set; }
    }
}
