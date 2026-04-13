using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    [InjectAsSingleton(typeof(ICurrencyPaymentFromCustomerReader))]
    internal sealed class CurrencyPaymentFromCustomerReader : ICurrencyPaymentFromCustomerReader
    {
        private readonly CurrencyPaymentFromCustomerApiClient apiClient;
        private readonly IKontragentsReader kontragentsReader;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly CurrencyPaymentFromCustomerLinksGetter linksGetter;
        private readonly IPaymentOrderAccessor accessor;

        public CurrencyPaymentFromCustomerReader(
            CurrencyPaymentFromCustomerApiClient apiClient,
            IKontragentsReader kontragentsReader,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            CurrencyPaymentFromCustomerLinksGetter linksGetter,
            IPaymentOrderAccessor accessor)
        {
            this.apiClient = apiClient;
            this.kontragentsReader = kontragentsReader;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.linksGetter = linksGetter;
            this.accessor = accessor;
        }
        
        public async Task<CurrencyPaymentFromCustomerResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId);
            if (response.Kontragent != null)
            {
                var kontragent = await kontragentsReader.GetByIdAsync(response.Kontragent.Id);
                response.Kontragent.Form = (int?)kontragent?.Form;
            }
            if (response.TaxationSystemType == null)
            {
                response.TaxationSystemType = await taxationSystemTypeReader.GetDefaultByYearAsync(response.Date.Year);
            }
            var documents = await linksGetter.GetAsync(documentBaseId);
            response.Contract = documents.Contract;
            response.IsReadOnly = accessor.IsReadOnly(response);
            response.Documents = documents.Documents;
            return response;
        }
    }
}