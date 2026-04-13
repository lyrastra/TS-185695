using Moedelo.Money.Domain.PurseOperations.Outgoing.PaymentSystemFee;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PurseOperations.Outgoing.PaymentSystemFee
{
    public interface IPaymentSystemFeeReader
    {
        Task<PaymentSystemFeeResponse> GetByBaseIdAsync(long baseId);
    }
}
