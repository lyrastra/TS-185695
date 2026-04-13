using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent
{
    public interface IIncomeFromCommissionAgentUpdater
    {
        Task<PaymentOrderSaveResponse> UpdateAsync(IncomeFromCommissionAgentSaveRequest saveRequest);
    }
}