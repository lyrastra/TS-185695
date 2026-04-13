using System;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    public class EgrUlTerminationInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Способ прекращения юридического лица
        /// </summary>
        public EgrUlTerminationMethodInfoDto TerminationMethod { get; set; }

        /// <summary>
        /// Сведения о регистрирующем (налоговом) органе, внесшем запись о прекращении юридического лица
        /// </summary>
        public EgrUlRegisteringTaxAuthorityInfoDto RegisteringTaxAuthority { get; set; }

        /// <summary>
        /// Дата прекращения юридического лица
        /// </summary>
        public DateTime TerminationDate { get; set; }
    }
}