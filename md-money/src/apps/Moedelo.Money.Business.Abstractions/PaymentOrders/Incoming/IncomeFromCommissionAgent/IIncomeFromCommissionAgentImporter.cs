using Moedelo.Money.Domain.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent
{
    public interface IIncomeFromCommissionAgentImporter
    {
        Task ImportAsync(IncomeFromCommissionAgentImportRequest request);
    }
}