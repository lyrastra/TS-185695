using Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RetailRevenue
{
    public interface IRetailRevenueImporter
    {
        Task ImportAsync(RetailRevenueImportRequest request);
    }
}