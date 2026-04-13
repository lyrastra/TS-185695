using System;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Base
{
    /// <summary>
    /// Сведения об учете в налоговом органе по месту нахождения обособленного подразделения (филиала/представительства)
    /// </summary>
    public class EgrUlTaxRegAffiliateInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Сведения о налоговом органе по месту нахождения филиала/представительства
        /// </summary>
        public EgrUlTaxAuthorityInfoDto TaxAuthority { get; set; }

        /// <summary>
        /// КПП филиала/представительства
        /// </summary>
        public string Kpp { get; set; }

        /// <summary>
        /// Дата постановки на учет в налоговом органе
        /// </summary>
        public DateTime RegDate { get; set; }
    }
}