using Moedelo.Money.Domain.PaymentOrders.Incoming.FinancialAssistance;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.FinancialAssistance
{
    public interface IFinancialAssistanceImporter
    {
        Task ImportAsync(FinancialAssistanceImportRequest request);
    }
}