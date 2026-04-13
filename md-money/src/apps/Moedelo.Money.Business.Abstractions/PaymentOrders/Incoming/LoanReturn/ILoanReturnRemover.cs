using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanReturn
{
    public interface ILoanReturnRemover
    {
        Task DeleteAsync(long documentBaseId, long? newDocumentBaseId = null);
    }
}