using Moedelo.Money.Domain.CashOrders;
using Moedelo.Money.Domain.CashOrders.Incoming.RetailRevenue;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.CashOrders.Incoming.RetailRevenue
{
    public interface IRetailRevenueUpdater
    {
        Task<CashOrderSaveResponse> UpdateAsync(RetailRevenueSaveRequest saveRequest);
    }
}