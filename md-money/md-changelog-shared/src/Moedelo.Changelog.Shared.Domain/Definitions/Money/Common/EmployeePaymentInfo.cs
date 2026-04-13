using System.ComponentModel.DataAnnotations;

namespace Moedelo.Changelog.Shared.Domain.Definitions.Money.Common
{
    public class EmployeePaymentInfo
    {
        // сохраняется на всякий случай, можно и не сохранять
        public int EmployeeId { get; set; }

        [Display(Name = "ФИО")]
        public string Name { get; set; }

        [Display(Name = "Начисление")]
        public ChargePaymentInfo[] ChargePayments { get; set; }
    }
}
