using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.MediationFee
{
    public interface IMediationFeeProvider
    {
        Task ProvideAsync(long documentBaseId);
    }
}