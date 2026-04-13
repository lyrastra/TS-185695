using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Incoming.RetailRevenue
{
    public interface IRetailRevenueCreator
    {
        Task<CreateResponse> CreateAsync(PaymentOrderSaveRequest request);
    }
}