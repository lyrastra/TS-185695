using Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue;
using System.Threading.Tasks;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RetailRevenue
{
    public interface IRetailRevenueEventWriter
    {
        Task WriteCreatedEventAsync(RetailRevenueSaveRequest request);
        Task WriteUpdatedEventAsync(RetailRevenueSaveRequest request);
        Task WriteUpdateAfterAccountingAtatementCreatedEventAsync(RetailRevenueAfterAccountingAtatementCreatedUpdateRequest request);
        Task WriteProvideRequiredEventAsync(RetailRevenueResponse response);
        Task WriteDeletedEventAsync(RetailRevenueResponse response, long? newDocumentBaseId);
        Task WriteDeletedEventAsync(long documentBaseId);
    }
}