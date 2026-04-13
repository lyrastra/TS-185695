using Moedelo.Money.Domain.PurseOperations.Incoming.PaymentFromCustomer;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PurseOperations.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerReader
    {
        Task<PaymentFromCustomerResponse> GetByBaseIdAsync(long baseId);
    }
}
