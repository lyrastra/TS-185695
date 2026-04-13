using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.AccrualOfInterest;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.AccrualOfInterest
{
    [InjectAsSingleton(typeof(IAccrualOfInterestCreator))]
    internal sealed class AccrualOfInterestCreator : IAccrualOfInterestCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly AccrualOfInterestApiClient apiClient;
        private readonly AccrualOfInterestEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;

        public AccrualOfInterestCreator(
            IBaseDocumentCreator baseDocumentUpdater,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            AccrualOfInterestApiClient apiClient,
            AccrualOfInterestEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver)
        {
            this.baseDocumentCreator = baseDocumentUpdater;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.apiClient = apiClient;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(AccrualOfInterestSaveRequest request)
        {
            request.TaxationSystemType ??= await taxationSystemTypeReader.GetDefaultByYearAsync(request.Date.Year);

            var baseDocumentCreateRequest = AccrualOfInterestMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                AccrualOfInterestMapper.MapToCustomTaxPostingsOverwriteRequest(request));

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}