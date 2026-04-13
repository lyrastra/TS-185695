using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Incoming.RetailRevenue;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Incoming.RetailRevenue
{
    [InjectAsSingleton(typeof(IRetailRevenueCreator))]
    internal class RetailRevenueCreator : IRetailRevenueCreator
    {
        private readonly IPaymentOrderCreator paymentOrderCreator;

        public RetailRevenueCreator(
            IPaymentOrderCreator paymentOrderCreator)
        {
            this.paymentOrderCreator = paymentOrderCreator;
        }

        public Task<CreateResponse> CreateAsync(PaymentOrderSaveRequest request)
        {
            request.PaymentOrder.SaleDate = RetailRevenueMapper.MapSaleDate(request.PaymentOrder.SaleDate, request.PaymentOrder.Date);
            return paymentOrderCreator.CreateAsync(request);
        }
    }
}
