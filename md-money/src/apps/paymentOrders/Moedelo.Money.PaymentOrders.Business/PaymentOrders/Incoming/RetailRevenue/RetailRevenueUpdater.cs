using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Incoming.RetailRevenue;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Incoming.RetailRevenue
{
    [InjectAsSingleton(typeof(IRetailRevenueUpdater))]
    internal class RetailRevenueUpdater : IRetailRevenueUpdater
    {
        private readonly IPaymentOrderUpdater paymentOrderUpdater;

        public RetailRevenueUpdater(
            IPaymentOrderUpdater paymentOrderUpdater)
        {
            this.paymentOrderUpdater = paymentOrderUpdater;
        }

        public Task UpdateAsync(PaymentOrderSaveRequest request)
        {
            request.PaymentOrder.SaleDate = RetailRevenueMapper.MapSaleDate(request.PaymentOrder.SaleDate, request.PaymentOrder.Date);
            return paymentOrderUpdater.UpdateAsync(request);
        }
    }
}
