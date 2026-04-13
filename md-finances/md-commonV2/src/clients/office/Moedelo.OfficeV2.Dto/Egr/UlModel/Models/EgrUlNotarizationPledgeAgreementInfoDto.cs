using System;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о нотариальном удостоверении договора залога
    /// </summary>
    public class EgrUlNotarizationPledgeAgreementInfoDto
    {
        /// <summary>
        /// ФИО и (при наличии) ИНН нотариуса, удостоверившего договор залога
        /// </summary>
        public EgrUlFlInfoDto Notary { get; set; }

        /// <summary>
        /// Номер договора залога
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата договора залога
        /// </summary>
        public DateTime Date { get; set; }
    }
}