using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public interface IPaymentToNaturalPersonsRemover
    {
        Task DeleteAsync(long documentBaseId, long? newDocumentBaseId = null);
    }
}