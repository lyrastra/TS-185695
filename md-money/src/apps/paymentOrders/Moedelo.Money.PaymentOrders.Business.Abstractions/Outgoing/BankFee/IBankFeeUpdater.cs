using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.BankFee
{
    public interface IBankFeeUpdater
    {
        Task UpdateAsync(PaymentOrderSaveRequest request);
    }
}