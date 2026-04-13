using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BankFee;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BankFee
{
    public interface IBankFeeUpdater
    {
        Task<PaymentOrderSaveResponse> UpdateAsync(BankFeeSaveRequest saveRequest);
    }
}