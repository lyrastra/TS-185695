using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.Common
{
    public class EmployeeChargePaymentInfo
    {
        public string Name { get; set; }

        [Display(Name = "ФИО")]
        public string EmployeeName { get; set; }

        [Display(Name = "Начисление")]
        public string Description { get; set; }

        [Display(Name = "К выплате")]
        public MoneySum Sum { get; set; }
    }
}
