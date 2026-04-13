using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.Common
{
    public sealed class ContractorInfo
    {
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "ИНН")]
        public string Inn { get; set; }

        [Display(Name = "КПП")]
        public string Kpp { get; set; }

        [Display(Name = "Расчетный счет")]
        public string SettlementAccount { get; set; }

        [Display(Name = "Название банка")]
        public string BankName { get; set; }

        [Display(Name = "БИК банка")]
        public string BankBik { get; set; }

        [Display(Name = "Кор.счет банка")]
        public string BankCorrespondentAccount { get; set; }
    }
}
