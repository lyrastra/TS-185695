using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RetailRevenue;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RetailRevenue
{
    [InjectAsSingleton(typeof(IRetailRevenueReader))]
    internal sealed class RetailRevenueReader : IRetailRevenueReader
    {
        private readonly RetailRevenueApiClient apiClient;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public RetailRevenueReader(
            RetailRevenueApiClient apiClient,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<RetailRevenueResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId);
            if (response.TaxationSystemType == null)
            {
                response.TaxationSystemType = await taxationSystemTypeReader.GetDefaultByYearAsync(response.Date.Year);
            }
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}
