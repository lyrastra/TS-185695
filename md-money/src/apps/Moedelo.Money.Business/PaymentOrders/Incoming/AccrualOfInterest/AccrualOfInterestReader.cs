using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Domain.PaymentOrders.Incoming.AccrualOfInterest;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.AccrualOfInterest
{
    [InjectAsSingleton(typeof(IAccrualOfInterestReader))]
    internal sealed class AccrualOfInterestReader : IAccrualOfInterestReader
    {
        private readonly AccrualOfInterestApiClient apiClient;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public AccrualOfInterestReader(
            AccrualOfInterestApiClient apiClient,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<AccrualOfInterestResponse> GetByBaseIdAsync(long documentBaseId)
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
