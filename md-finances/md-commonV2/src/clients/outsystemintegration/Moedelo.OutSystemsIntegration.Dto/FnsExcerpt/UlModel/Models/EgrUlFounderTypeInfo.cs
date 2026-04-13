using System.Xml.Serialization;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base;
using Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Enums;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Models
{
    /// <summary>
    /// Сведения об обременении доли участника, внесенные в ЕГРЮЛ
    /// </summary>
    [XmlType(AnonymousType = true)]
    public class EgrUlFounderTypeInfo : EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// Код вида учредителя
        /// </summary>
        [XmlAttribute("КодУчрРФСубМО")]
        public EgrUlFounderType FounderType { get; set; }

        /// <summary>
        /// Наименование муниципального образования
        /// </summary>
        [XmlAttribute("НаимМО")]
        public string MunicipalityName { get; set; }

        /// <summary>
        /// Код субъекта Российской Федерации, который является учредителем (участником) юридического лица или 
        /// на территории которого находится муниципальное образование, которое является учредителем (участником) юридического лица
        /// </summary>
        [XmlAttribute("КодРегион")]
        public string RegionCode { get; set; }

        /// <summary>
        /// Наименование субъекта Российской Федерации
        /// </summary>
        [XmlAttribute("НаимРегион")]
        public string SubjectName { get; set; }
    }
}
