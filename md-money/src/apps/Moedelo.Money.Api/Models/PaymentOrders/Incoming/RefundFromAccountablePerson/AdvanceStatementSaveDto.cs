using Moedelo.Infrastructure.AspNetCore.Validation;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    public class AdvanceStatementSaveDto
    {
        /// <summary>
        /// Идентификатор авансового отчета
        /// </summary>
        [IdLongValue]
        public long? DocumentBaseId { get; set; }
    }
}
