using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using System.Threading.Tasks;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierEventWriter
    {
        Task WriteCreatedEventAsync(PaymentToSupplierSaveRequest request);
        Task WriteUpdatedEventAsync(PaymentToSupplierSaveRequest request);
        Task WriteProvideRequiredEventAsync(PaymentToSupplierResponse response);
        Task WriteDeletedEventAsync(PaymentToSupplierResponse response, long? newDocumentBaseId);
        Task WriteDeletedEventAsync(long documentBaseId);
        /// <summary> Установка значения резерва (без проведения операции) </summary>
        Task WriteSetReserveEventAsync(SetReserveRequest request);
    }
}
