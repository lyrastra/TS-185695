using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions
{
    public interface IPaymentOrderUpdater
    {
        Task UpdateAsync(PaymentOrderSaveRequest request);
    }
}