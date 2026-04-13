using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.PaymentToNaturalPersons
{
    public interface IPaymentToNaturalPersonsRemover
    {
        Task DeleteAsync(long documentBaseId);
    }
}