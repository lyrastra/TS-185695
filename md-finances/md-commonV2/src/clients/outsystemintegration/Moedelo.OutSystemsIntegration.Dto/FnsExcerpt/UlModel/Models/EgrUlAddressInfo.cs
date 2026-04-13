using System.Collections.Generic;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения об адресе (месте нахождения)
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlAddressInfo
    {
        /// <summary>
        /// Адрес (место нахождения) юридического лица
        /// </summary>
        [XmlElement("АдресРФ")]
        public EgrUlAddress Address { get; set; }

        /// <summary>
        /// Сведения о недостоверности адреса или отсутствии связи с ЮЛ по указанному адресу
        /// </summary>
        [XmlElement("СведОтсутАдресЮЛ")]
        public List<EgrUlSignLackAddressInfo> InfoAboutUnreliabilityAddress { get; set; }
    }
}
