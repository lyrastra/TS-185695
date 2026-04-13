using Moedelo.Common.Enums.Enums.EgrIp;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения об обременении доли участника, внесенные в ЕГРЮЛ
    /// </summary>
    public class EgrUlFounderTypeInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Код вида учредителя
        /// </summary>
        public EgrUlFounderType FounderType { get; set; }

        /// <summary>
        /// Наименование муниципального образования
        /// </summary>
        public string MunicipalityName { get; set; }

        /// <summary>
        /// Код субъекта Российской Федерации, который является учредителем (участником) юридического лица или 
        /// на территории которого находится муниципальное образование, которое является учредителем (участником) юридического лица
        /// </summary>
        public string RegionCode { get; set; }

        /// <summary>
        /// Наименование субъекта Российской Федерации
        /// </summary>
        public string SubjectName { get; set; }
    }
}