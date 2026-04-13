using Moedelo.Money.Domain.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent
{
    public interface IIncomeFromCommissionAgentReader
    {
        Task<IncomeFromCommissionAgentResponse> GetByBaseIdAsync(long baseId);
    }
}
