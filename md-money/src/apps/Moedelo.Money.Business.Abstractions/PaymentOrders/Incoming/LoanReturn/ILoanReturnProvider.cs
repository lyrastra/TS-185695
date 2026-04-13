using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanReturn
{
    public interface ILoanReturnProvider
    {
        Task ProvideAsync(long paymentBaseId);
    }
}