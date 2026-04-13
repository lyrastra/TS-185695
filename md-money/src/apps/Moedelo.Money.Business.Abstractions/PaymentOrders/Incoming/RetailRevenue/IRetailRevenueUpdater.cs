using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RetailRevenue
{
    public interface IRetailRevenueUpdater
    {
        Task<PaymentOrderSaveResponse> UpdateAsync(RetailRevenueSaveRequest saveRequest);
    }
}