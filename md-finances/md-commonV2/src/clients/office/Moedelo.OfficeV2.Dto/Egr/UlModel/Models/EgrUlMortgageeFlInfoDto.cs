using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о залогодержателе - ФЛ
    /// </summary>
    public class EgrUlMortgageeFlInfoDto
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        public EgrUlGrnDateInfoDto Grn { get; set; }
        
        /// <summary>
        /// Сведения о ФИО и (при наличии) ИНН ФЛ
        /// </summary>
        public EgrUlFlInfoDto FlInfo { get; set; }

        /// <summary>
        /// Сведения о ФИО и (при наличии) ИНН ФЛ
        /// </summary>
        public EgrUlFlBirthInfoDto FlBirthInfo { get; set; }

        /// <summary>
        /// Сведения о документе, удостоверяющем личность
        /// </summary>
        public EgrUlIdentityDocumentInfoDto FlIdentityDocument { get; set; }

        /// <summary>
        /// Сведения об адресе места жительства в Российской Федерации
        /// </summary>
        public EgrUlAddressDto HomeAddressInRf { get; set; }

        /// <summary>
        /// Сведения об адресе места жительства в Российской Федерации
        /// </summary>
        public EgrUlAddressDto HomeAddressOutsideRf { get; set; }

        /// <summary>
        /// Сведения о нотариальном удостоверении договора залога
        /// </summary>
        public EgrUlNotarizationPledgeAgreementInfoDto NotarizationPledgeAgreement { get; set; }
    }
}