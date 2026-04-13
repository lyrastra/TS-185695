using Moedelo.Money.Domain.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent.Autocomplete
{
    public interface ICommissionAgentAutocompleteReader
    {
        Task<IReadOnlyCollection<CommissionAgentWithRequisites>> GetAsync(DateTime? date, string query, int count);
    }
}
