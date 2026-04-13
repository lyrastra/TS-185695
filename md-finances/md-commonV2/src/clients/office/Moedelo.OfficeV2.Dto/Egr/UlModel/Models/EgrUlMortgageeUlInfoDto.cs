using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о залогодержателе - ЮЛ
    /// </summary>
    public class EgrUlMortgageeUlInfoDto
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        public EgrUlGrnDateInfoDto Grn { get; set; }

        /// <summary>
        /// Сведения о наименовании и (при наличии) ОГРН и ИНН ЮЛ
        /// </summary>
        public EgrUlBaseInfoDto UlInfo { get; set; }

        /// <summary>
        /// Сведения о регистрации в стране происхождения
        /// </summary>
        public EgrUlForeignRegUlInfoDto ForeignRegInfo { get; set; }

        /// <summary>
        /// Сведения о нотариальном удостоверении договора залога
        /// </summary>
        public EgrUlNotarizationPledgeAgreementInfoDto NotarizationPledgeAgreement { get; set; }
    }
}