using System.Threading.Tasks;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Incoming.RetailRevenue
{
    public interface IRetailRevenueUpdater
    {
        Task UpdateAsync(PaymentOrderSaveRequest request);
    }
}