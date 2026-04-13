using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.FinancialAssistance
{
    public interface IFinancialAssistanceRemover
    {
        Task DeleteAsync(long documentBaseId, long? newDocumentBaseId = null);
    }
}