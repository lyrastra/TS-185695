using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyBankFee;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee
{
    public interface ICurrencyBankFeeValidator
    {
        Task ValidateAsync(CurrencyBankFeeSaveRequest request);
    }
}