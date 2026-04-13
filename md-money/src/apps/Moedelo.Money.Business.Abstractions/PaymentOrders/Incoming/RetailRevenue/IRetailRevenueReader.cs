using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RetailRevenue
{
    public interface IRetailRevenueReader
    {
        Task<RetailRevenueResponse> GetByBaseIdAsync(long baseId);
    }
}
