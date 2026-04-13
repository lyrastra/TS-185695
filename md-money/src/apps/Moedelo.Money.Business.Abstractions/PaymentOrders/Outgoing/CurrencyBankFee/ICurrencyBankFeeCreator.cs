using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyBankFee;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee
{
    public interface ICurrencyBankFeeCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(CurrencyBankFeeSaveRequest request);
    }
}