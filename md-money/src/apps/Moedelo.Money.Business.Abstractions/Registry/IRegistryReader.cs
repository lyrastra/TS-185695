using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.Domain.Registry;
using Moedelo.Money.Domain.SelfCostPayments;

namespace Moedelo.Money.Business.Abstractions.Registry
{
    public interface IRegistryReader
    {
        Task<RegistryResponse> GetAsync(RegistryQuery query);

        Task<IReadOnlyCollection<RegistryOperation>> GetOutgoingPaymentsForTaxWidgetsAsync(DateTime startDate, DateTime endDate);
        
        Task<IReadOnlyCollection<SelfCostPayment>> GetSelfCostSettlementAccountPaymentsAsync(SelfCostPaymentRequest request);

        Task<IReadOnlyCollection<SelfCostPayment>> GetSelfCostCashPaymentsAsync(SelfCostPaymentRequest request);
    }
}
