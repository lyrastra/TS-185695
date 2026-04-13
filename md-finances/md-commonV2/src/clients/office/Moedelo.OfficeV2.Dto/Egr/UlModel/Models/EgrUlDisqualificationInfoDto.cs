using System;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о дисквалификации
    /// </summary>
    public class EgrUlDisqualificationInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Дата начала дисквалификации
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// Дата окончания дисквалификации
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Дата вынесения судебным органом постановления о дисквалификации
        /// </summary>
        public DateTime DecisionDate { get; set; }
    }
}