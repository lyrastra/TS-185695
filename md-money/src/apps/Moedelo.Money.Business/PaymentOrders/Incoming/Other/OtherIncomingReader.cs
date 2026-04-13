using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.Other;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Domain.PaymentOrders.Incoming.Other;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Moedelo.Money.Business.Kontragents;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.Other
{
    [InjectAsSingleton(typeof(IOtherIncomingReader))]
    internal sealed class OtherIncomingReader : IOtherIncomingReader
    {
        private readonly OtherIncomingApiClient apiClient;
        private readonly IKontragentsReader kontragentsReader;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly OtherIncomingLinksGetter linksGetter;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public OtherIncomingReader(
            OtherIncomingApiClient apiClient,
            IKontragentsReader kontragentsReader,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            OtherIncomingLinksGetter linksGetter,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.kontragentsReader = kontragentsReader;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.linksGetter = linksGetter;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<OtherIncomingResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId);
            if (response.Contractor?.Type == ContractorType.Kontragent)
            {
                var kontragent = await kontragentsReader.GetByIdAsync(response.Contractor.Id);
                response.Contractor.Form = (int?)kontragent?.Form;
            }
            response.TaxationSystemType ??= await taxationSystemTypeReader.GetDefaultByYearAsync(response.Date.Year);
            var documents = await linksGetter.GetAsync(documentBaseId);
            response.Bills = documents.Bills;
            response.Contract = documents.Contract;
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }

        public async Task<IReadOnlyCollection<OtherIncomingResponse>> GetByBaseIdsAsync(
            IReadOnlyCollection<long> documentBaseIds)
        {
            var operationResponses = await apiClient.GetByBaseIdsAsync(documentBaseIds);

            var kontragentIds = operationResponses
                .Where(x => x.Contractor?.Type == ContractorType.Kontragent && x.Contractor != null)
                .Select(x => x.Contractor.Id)
                .Distinct()
                .ToArray();
            var kontragents = await kontragentsReader.GetByIdsAsync(kontragentIds);
            var kontragentsMap = kontragents.ToDictionary(x => x.Id);

            var documentsByPaymentMap = await linksGetter.GetAsync(documentBaseIds);

            var defaultTaxSystemsMap = new Dictionary<int, TaxationSystemType?>();
            foreach (var operationResponse in operationResponses)
            {
                if (operationResponse.Contractor?.Type == ContractorType.Kontragent &&
                    operationResponse.Contractor != null)
                {
                    var kontraget = kontragentsMap.GetValueOrDefault(operationResponse.Contractor.Id);
                    operationResponse.Contractor.Form = (int?)kontraget?.Form;
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
                operationResponse.IsReadOnly = paymentOrderAccessor.IsReadOnly(operationResponse);
            }

            return operationResponses;
        }
    }
}
