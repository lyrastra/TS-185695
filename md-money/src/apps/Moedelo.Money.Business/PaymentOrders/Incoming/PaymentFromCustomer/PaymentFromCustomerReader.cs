using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton(typeof(IPaymentFromCustomerReader))]
    internal sealed class PaymentFromCustomerReader : IPaymentFromCustomerReader
    {
        private readonly PaymentFromCustomerApiClient apiClient;
        private readonly IKontragentsReader kontragentsReader;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly PaymentFromCustomerLinksGetter linksGetter;
        private readonly IPaymentFromCustomerAccessor paymentFromCustomerAccessor;

        public PaymentFromCustomerReader(
            PaymentFromCustomerApiClient apiClient,
            IKontragentsReader kontragentsReader,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            PaymentFromCustomerLinksGetter linksGetter,
            IPaymentFromCustomerAccessor paymentFromCustomerAccessor)
        {
            this.apiClient = apiClient;
            this.kontragentsReader = kontragentsReader;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.linksGetter = linksGetter;
            this.paymentFromCustomerAccessor = paymentFromCustomerAccessor;
        }

        public async Task<PaymentFromCustomerResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId);
            if (response.Kontragent != null)
            {
                var kontraget = await kontragentsReader.GetByIdAsync(response.Kontragent.Id);
                response.Kontragent.Form = (int?)kontraget?.Form;
            }
            if (response.TaxationSystemType == null)
            {
                response.TaxationSystemType = await taxationSystemTypeReader.GetDefaultByYearAsync(response.Date.Year);
            }
            var documents = await linksGetter.GetAsync(documentBaseId);
            response.Bills = documents.Bills;
            response.Contract = documents.Contract;
            response.Documents = documents.Documents;
            response.Invoices = documents.Invoices;
            response.ReserveSum = documents.ReserveSum;
            response.IsReadOnly = paymentFromCustomerAccessor.IsReadOnly(response);
            return response;
        }

        public async Task<IReadOnlyCollection<PaymentFromCustomerResponse>> GetByBaseIdsAsync(
            IReadOnlyCollection<long> documentBaseIds)
        {
            var operationResponses = await apiClient.GetByBaseIdsAsync(documentBaseIds);

            var kontragentIds = operationResponses
                .Where(x => x.Kontragent != null)
                .Select(x => x.Kontragent.Id)
                .Distinct()
                .ToArray();
            var kontragents = await kontragentsReader.GetByIdsAsync(kontragentIds);
            var kontragentsMap = kontragents.ToDictionary(x => x.Id);

            var documentsByPaymentMap = await linksGetter.GetAsync(documentBaseIds);

            var defaultTaxSystemsMap = new Dictionary<int, TaxationSystemType?>();
            foreach (var operationResponse in operationResponses)
            {
                if (operationResponse.Kontragent != null)
                {
                    var kontraget = kontragentsMap.GetValueOrDefault(operationResponse.Kontragent.Id);
                    operationResponse.Kontragent.Form = (int?)kontraget?.Form;
                }

                if (operationResponse.TaxationSystemType == null)
                {
                    if (defaultTaxSystemsMap.TryGetValue(operationResponse.Date.Year, out var defaultTaxSystem) == false)
                    {
                        defaultTaxSystem = await taxationSystemTypeReader.GetDefaultByYearAsync(operationResponse.Date.Year);
                        defaultTaxSystemsMap.Add(operationResponse.Date.Year, defaultTaxSystem);
                    }
                    operationResponse.TaxationSystemType = defaultTaxSystem;
                }

                var documents = documentsByPaymentMap.GetValueOrDefault(operationResponse.DocumentBaseId);
                operationResponse.Bills = documents.Bills;
                operationResponse.Contract = documents.Contract;
                operationResponse.Documents = documents.Documents;
                operationResponse.Invoices = documents.Invoices;
                operationResponse.ReserveSum = documents.ReserveSum;
                operationResponse.IsReadOnly = paymentFromCustomerAccessor.IsReadOnly(operationResponse);
            }

            return operationResponses;
        }
    }
}
