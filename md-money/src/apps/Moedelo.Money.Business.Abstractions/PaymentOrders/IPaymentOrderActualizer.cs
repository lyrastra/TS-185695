using Moedelo.Money.Domain.PaymentOrders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders
{
    public interface IPaymentOrderActualizer
    {
        Task ActualizeAsync(IReadOnlyCollection<ActualizeRequestItem> items);
    }
}