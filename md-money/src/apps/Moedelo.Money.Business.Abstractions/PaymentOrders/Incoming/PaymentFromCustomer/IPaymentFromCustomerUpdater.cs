using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;
using System.Threading.Tasks;
using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerUpdater
    {
        Task<PaymentOrderSaveResponse> UpdateAsync(PaymentFromCustomerSaveRequest saveRequest);

        Task SetReserveAsync(SetReserveRequest request);
    }
}