using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о представительствах юридического лица
    /// </summary>
    public class EgrUlAgencyInfoDto
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном филиале
        /// </summary>
        public EgrUlGrnDateInfoDto Grn { get; set; }

        /// <summary>
        /// Сведения о наименовании представительства
        /// </summary>
        public EgrUlAffiliateNameInfoDto Name { get; set; }

        /// <summary>
        /// Адрес (место расположения) на территории Российской Федерации
        /// </summary>
        public EgrUlAddressDto AddressRF { get; set; }

        /// <summary>
        /// Адрес (место расположения) за пределами территории Российской Федерации
        /// </summary>
        public EgrUlForeignAddressDto AddressForeign { get; set; }

        /// <summary>
        /// Сведения об учете в налоговом органе по месту нахождения представительства
        /// </summary>
        public EgrUlTaxRegAffiliateInfoDto TaxRegAffiliate { get; set; }
    }
}