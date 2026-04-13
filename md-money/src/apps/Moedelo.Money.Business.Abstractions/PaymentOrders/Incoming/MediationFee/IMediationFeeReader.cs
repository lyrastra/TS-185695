using Moedelo.Money.Domain.PaymentOrders.Incoming.MediationFee;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.MediationFee
{
    public interface IMediationFeeReader
    {
        Task<MediationFeeResponse> GetByBaseIdAsync(long baseId);
    }
}
