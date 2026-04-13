using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.Common
{
    public class ChargePaymentInfo
    {
        [Display(Name = "Начисление")]
        public string Name { get; set; }

        [Display(Name = "К выплате")]
        public MoneySum Sum { get; set; }
    }
}
