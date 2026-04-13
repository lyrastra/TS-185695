using System.Threading.Tasks;
using Moedelo.Money.Domain.CashOrders.Incoming.RetailRevenue;

namespace Moedelo.Money.Business.Abstractions.CashOrders.Incoming.RetailRevenue
{
    public interface IRetailRevenueReader
    {
        Task<RetailRevenueResponse> GetByBaseIdAsync(long baseId);
    }
}
