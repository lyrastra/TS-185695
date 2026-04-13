using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.MediationFee;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.MediationFee
{
    public interface IMediationFeeCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(MediationFeeSaveRequest saveRequest);
    }
}