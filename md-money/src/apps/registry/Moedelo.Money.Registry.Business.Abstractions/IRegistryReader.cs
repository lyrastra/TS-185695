using System;
using System.Collections.Generic;
using Moedelo.Money.Registry.Domain.Models;
using System.Threading.Tasks;
using Moedelo.Money.Registry.Domain.Models.SelfCostPayments;

namespace Moedelo.Money.Registry.Business.Abstractions
{
    public interface IRegistryReader
    {
        Task<OperationsResult> GetAsync(RegistryQuery query);

        Task<List<MoneyOperation>> GetOutgoingPaymentsForTaxWidgetsAsync(DateTime startDate, DateTime endDate);
        
        Task<IReadOnlyList<SelfCostPayment>> GetSelfCostPaymentsAsync(SelfCostPaymentRequest request);
    }
}
