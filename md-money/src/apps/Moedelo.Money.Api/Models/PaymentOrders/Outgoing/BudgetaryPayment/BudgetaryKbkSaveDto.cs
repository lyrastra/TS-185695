using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using System.ComponentModel;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryKbkSaveDto
    {
        /// <summary>
        /// Идентификатор КБК
        /// </summary>
        [IdIntValue]
        [DefaultValue(null)]
        public int? Id { get; set; }

        /// <summary>
        /// Номер КБК (104)
        /// </summary>
        [Kbk]
        [ValidateXss]
        [RequiredValue]
        public string Number { get; set; }
    }
}
