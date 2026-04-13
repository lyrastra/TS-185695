using System.Collections.Generic;
using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения о видах экономической деятельности по Общероссийскому классификатору видов экономической деятельности
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlOkvedInfo
    {
        /// <summary>
        /// Сведения об основном виде деятельности
        /// </summary>
        [XmlElement("СвОКВЭДОсн")]
        public EgrUlOkved OkvedMain { get; set; }

        /// <summary>
        /// Сведения о дополнительном виде деятельности
        /// </summary>
        [XmlElement("СвОКВЭДДоп")]
        public List<EgrUlOkved> OkvedOther { get; set; }
    }
}
