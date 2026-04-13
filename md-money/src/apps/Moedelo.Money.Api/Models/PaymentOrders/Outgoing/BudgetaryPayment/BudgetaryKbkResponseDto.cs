using Moedelo.Infrastructure.AspNetCore.Validation;

namespace Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment
{
    public class BudgetaryKbkResponseDto
    {
        /// <summary>
        /// Идентификатор КБК
        /// </summary>
        [IdIntValue]
        public int? Id { get; set; }

        /// <summary>
        /// Номер КБК (104)
        /// </summary>
        public string Number { get; set; }
    }
}
