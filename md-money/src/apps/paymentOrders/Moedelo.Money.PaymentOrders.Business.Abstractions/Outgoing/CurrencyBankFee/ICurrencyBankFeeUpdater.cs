using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.CurrencyBankFee
{
    public interface ICurrencyBankFeeUpdater
    {
        Task UpdateAsync(PaymentOrderSaveRequest request);
    }
}