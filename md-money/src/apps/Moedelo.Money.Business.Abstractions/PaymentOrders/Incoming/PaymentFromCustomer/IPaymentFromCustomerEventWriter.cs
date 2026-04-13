using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;
using System.Threading.Tasks;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer
{
    public interface IPaymentFromCustomerEventWriter
    {
        Task WriteCreatedEventAsync(PaymentFromCustomerSaveRequest request);
        Task WriteUpdatedEventAsync(PaymentFromCustomerSaveRequest request);
        Task WriteProvideRequiredEventAsync(PaymentFromCustomerResponse response);
        Task WriteDeletedEventAsync(PaymentFromCustomerResponse response, long? newDocumentBaseId);
        Task WriteDeletedEventAsync(long documentBaseId);
        /// <summary> Установка значения резерва (без проведения операции) </summary>
        Task WriteSetReserveEventAsync(SetReserveRequest request);
    }
}