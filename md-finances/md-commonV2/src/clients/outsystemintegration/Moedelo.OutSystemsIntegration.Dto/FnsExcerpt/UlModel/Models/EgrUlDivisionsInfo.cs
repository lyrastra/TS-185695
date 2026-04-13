using System.Collections.Generic;
using System.Xml.Serialization;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения об обособленных подразделениях юридического лица
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlDivisionsInfo
    {
        /// <summary>
        /// Сведения о филиалах юридического лица
        /// </summary>
        [XmlElement("СвФилиал")]
        public List<EgrUlAffiliateInfo> Affiliate { get; set; }

        /// <summary>
        /// Сведения о представительствах юридического лица
        /// </summary>
        [XmlElement("СвПредстав")]
        public List<EgrUlAgencyInfo> Agency { get; set; }
    }
}
