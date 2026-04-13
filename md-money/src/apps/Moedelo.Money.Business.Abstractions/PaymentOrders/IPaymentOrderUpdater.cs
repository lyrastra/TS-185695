using Moedelo.Money.Domain.PaymentOrders;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders
{
    public interface IPaymentOrderUpdater<TSaveRequest, TSaveResponse>
    {
        Task<TSaveResponse> UpdateAsync(TSaveRequest saveRequest);
    }
}
