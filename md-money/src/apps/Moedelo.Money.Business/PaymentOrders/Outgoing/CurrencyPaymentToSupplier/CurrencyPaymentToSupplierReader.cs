using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    [InjectAsSingleton(typeof(ICurrencyPaymentToSupplierReader))]
    class CurrencyPaymentToSupplierReader : ICurrencyPaymentToSupplierReader
    {
        private readonly CurrencyPaymentToSupplierApiClient apiClient;
        private readonly IKontragentsReader kontragentsReader;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;
        private readonly CurrencyPaymentToSupplierLinksGetter linksGetter;

        public CurrencyPaymentToSupplierReader(
            CurrencyPaymentToSupplierApiClient apiClient,
            IKontragentsReader kontragentsReader,
            IPaymentOrderAccessor paymentOrderAccessor, 
            CurrencyPaymentToSupplierLinksGetter linksGetter)
        {
            this.apiClient = apiClient;
            this.kontragentsReader = kontragentsReader;
            this.paymentOrderAccessor = paymentOrderAccessor;
            this.linksGetter = linksGetter;
        }

        public async Task<CurrencyPaymentToSupplierResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId);

            var kontragentTask = SetKontragentFormAsync(response);
            var linksTask = linksGetter.GetAsync(documentBaseId);

            await Task.WhenAll(
                kontragentTask, 
                linksTask);
            
            var links = linksTask.Result;
            response.Contract = links.Contract;
            response.Documents = links.Documents;
            
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            
            return response;
        }

        private async Task SetKontragentFormAsync(CurrencyPaymentToSupplierResponse response)
        {
            if(response.Kontragent == null)
            {
                return;
            }
            var kontragent = await kontragentsReader.GetByIdAsync(response.Kontragent.Id).ConfigureAwait(false);
            response.Kontragent.Form = (int?)kontragent.Form;
        }
    }
}
