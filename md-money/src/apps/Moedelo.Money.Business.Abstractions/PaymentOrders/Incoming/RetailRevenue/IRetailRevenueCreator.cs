using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RetailRevenue
{
    public interface IRetailRevenueCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(RetailRevenueSaveRequest saveRequest);
    }
}