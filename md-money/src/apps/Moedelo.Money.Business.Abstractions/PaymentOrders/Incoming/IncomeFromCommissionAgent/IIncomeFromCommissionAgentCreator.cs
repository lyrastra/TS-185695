using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent
{
    public interface IIncomeFromCommissionAgentCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(IncomeFromCommissionAgentSaveRequest saveRequest);
    }
}