using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier
{
    [InjectAsSingleton(typeof(IPaymentToSupplierReader))]
    class PaymentToSupplierReader : IPaymentToSupplierReader
    {
        private readonly PaymentToSupplierApiClient apiClient;
        private readonly IKontragentsReader kontragentsReader;
        private readonly PaymentToSupplierLinksGetter linksGetter;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public PaymentToSupplierReader(
            PaymentToSupplierApiClient apiClient,
            IKontragentsReader kontragentsReader,
            PaymentToSupplierLinksGetter linksGetter,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.kontragentsReader = kontragentsReader;
            this.linksGetter = linksGetter;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<PaymentToSupplierResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId);
            if (response.Kontragent != null)
            {
                var kontraget = await kontragentsReader.GetByIdAsync(response.Kontragent.Id);
                response.Kontragent.Form = (int?)kontraget?.Form;
            }
            var documents = await linksGetter.GetAsync(documentBaseId);
            response.Contract = documents.Contract;
            response.Documents = documents.Documents;
            response.Invoices = documents.Invoices;
            response.ReserveSum = documents.ReserveSum;
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }

        public async Task<IReadOnlyCollection<PaymentToSupplierResponse>> GetByBaseIdsAsync(
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

            return operationResponses
                .Select(x => EnrichOperationResponse(x, kontragentsMap, documentsByPaymentMap))
                .ToArray();
        }

        private PaymentToSupplierResponse EnrichOperationResponse(
            PaymentToSupplierResponse operationResponse,
            IReadOnlyDictionary<int, Kontragent> kontragentsMap,
            IReadOnlyDictionary<long, PaymentToSupplierLinks> documentsByPaymentMap)
        {
            if (operationResponse.Kontragent != null)
            {
                var kontraget = kontragentsMap.GetValueOrDefault(operationResponse.Kontragent.Id);
                operationResponse.Kontragent.Form = (int?)kontraget?.Form;
            }

            var documents = documentsByPaymentMap.GetValueOrDefault(operationResponse.DocumentBaseId);
            operationResponse.Contract = documents.Contract;
            operationResponse.Documents = documents.Documents;
            operationResponse.Invoices = documents.Invoices;
            operationResponse.ReserveSum = documents.ReserveSum;
            operationResponse.IsReadOnly = paymentOrderAccessor.IsReadOnly(operationResponse);

            return operationResponse;
        }
    }
}
