using Moedelo.Money.Domain.PaymentOrders.Incoming.MediationFee;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.MediationFee
{
    public interface IMediationFeeValidator
    {
        Task ValidateAsync(MediationFeeSaveRequest request);
    }
}
