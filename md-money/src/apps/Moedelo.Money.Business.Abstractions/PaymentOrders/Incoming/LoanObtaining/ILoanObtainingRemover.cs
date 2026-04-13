using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanObtaining
{
    public interface ILoanObtainingRemover
    {
        Task DeleteAsync(long documentBaseId, long? newDocumentBaseId = null);
    }
}