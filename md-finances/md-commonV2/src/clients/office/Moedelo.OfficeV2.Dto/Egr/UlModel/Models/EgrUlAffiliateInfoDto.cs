using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о филиалах юридического лица
    /// </summary>
    public class EgrUlAffiliateInfoDto
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном филиале
        /// </summary>
        public EgrUlGrnDateInfoDto Grn { get; set; }

        /// <summary>
        /// Сведения о наименовании филиала
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
        /// Сведения об учете в налоговом органе по месту нахождения филиала
        /// </summary>
        public EgrUlTaxRegAffiliateInfoDto TaxRegAffiliate { get; set; }
    }
}