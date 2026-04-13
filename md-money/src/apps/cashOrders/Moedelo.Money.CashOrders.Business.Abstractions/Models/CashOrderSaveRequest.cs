using Moedelo.Money.CashOrders.Domain.Models;
using Moedelo.Money.Common.Domain.Models;
using System.Collections.Generic;

namespace Moedelo.Money.CashOrders.Business.Abstractions.Models
{
    public class CashOrderSaveRequest
    {
        public long DocumentBaseId { get; set; }

        public CashOrder CashOrder { get; set; }

        public IReadOnlyCollection<UnifiedBudgetarySubPayment> UnifiedBudgetarySubPayments { get; set; }
    }
}
