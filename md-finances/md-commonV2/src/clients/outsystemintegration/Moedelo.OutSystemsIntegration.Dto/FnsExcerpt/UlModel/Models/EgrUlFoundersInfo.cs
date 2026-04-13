using System.Collections.Generic;
using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения об учредителях (участниках) юридического лица
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlFoundersInfo
    {
        /// <summary>
        /// Сведения об учредителе (участнике) - российском юридическом лице
        /// </summary>
        [XmlElement("УчрЮЛРос")]
        public List<EgrUlFounderUlRfInfo> FounderUlRf { get; set; }

        /// <summary>
        /// Сведения об учредителе (участнике) - российском юридическом лице
        /// </summary>
        [XmlElement("УчрЮЛИн")]
        public List<EgrUlFounderUlForeignInfo> FounderUlForeign { get; set; }

        /// <summary>
        /// Сведения об учредителе (участнике) - физическом лице
        /// </summary>
        [XmlElement("УчрФЛ")]
        public List<EgrUlFounderFlInfo> FounderFl { get; set; }

        /// <summary>
        /// Сведения об учредителе (участнике) - Российской Федерации, субъекте Российской Федерации, муниципальном образовании
        /// </summary>
        [XmlElement("УчрРФСубМО")]
        public List<EgrUlFounderStInfo> FounderSt { get; set; }

        /// <summary>
        /// Сведения о паевом инвестиционном фонде, в состав имущества которого включена доля в уставном капитале
        /// </summary>
        [XmlElement("УчрПИФ")]
        public List<EgrUlMutualFundInfo> FounderMf { get; set; }
    }
}
