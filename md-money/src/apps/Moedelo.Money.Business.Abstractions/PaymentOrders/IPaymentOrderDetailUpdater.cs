using Moedelo.Money.Domain.PaymentOrders;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders
{
    public interface IPaymentOrderDetailUpdater
    {
        Task UpdateAsync(ChangeIsPaidRequestItem item);
    }
}