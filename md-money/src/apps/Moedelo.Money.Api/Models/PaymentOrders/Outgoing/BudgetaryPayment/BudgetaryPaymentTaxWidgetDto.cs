using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryPaymentTaxWidgetDto
    {
        /// <summary>
        /// Год
        /// </summary>
        [RequiredValue]
        public int Year { get; set; }
        
        /// <summary>
        /// Бюджетный период
        /// </summary>
        public int? Quarter { get; set; }
        
        /// <summary>
        /// Идентификатор КБК
        /// </summary>
        [RequiredValue]
        [IdIntValue]
        public int KbkId { get; set; }
        
        [RequiredValue]
        public BudgetaryAccountCodes BudgetaryAccountCode { get; set; }
    }
}
