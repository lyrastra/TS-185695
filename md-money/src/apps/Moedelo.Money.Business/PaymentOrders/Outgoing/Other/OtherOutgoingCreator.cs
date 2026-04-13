using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Other;
using System.Threading.Tasks;
using Moedelo.Money.Domain.TaxPostings;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Other
{
    [InjectAsSingleton(typeof(IOtherOutgoingCreator))]
    internal sealed class OtherOutgoingCreator : IOtherOutgoingCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly OtherOutgoingApiClient apiClient;
        private readonly OtherOutgoingEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly OtherOutgoingCustomAccPostingsSaver customAccPostingsSaver;
        private readonly OtherOutgoingProvideInAccountingFixer provideInAccountingFixer;

        public OtherOutgoingCreator(
            IBaseDocumentCreator baseDocumentCreator,
            OtherOutgoingApiClient apiClient,
            OtherOutgoingEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            OtherOutgoingCustomAccPostingsSaver customAccPostingsSaver,
            OtherOutgoingProvideInAccountingFixer provideInAccountingFixer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.customAccPostingsSaver = customAccPostingsSaver;
            this.provideInAccountingFixer = provideInAccountingFixer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(OtherOutgoingSaveRequest request)
        {
            request.DocumentBaseId = await CreateBaseDocumentAsync(request);
            await provideInAccountingFixer.FixAsync(request);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            await ProvideCustomPostingsAsync(request);

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        private Task<long> CreateBaseDocumentAsync(OtherOutgoingSaveRequest request)
        {
            var baseDocument = new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
            return baseDocumentCreator.CreateForPaymentOrderAsync(baseDocument);
        }

        private async Task ProvideCustomPostingsAsync(OtherOutgoingSaveRequest request)
        {
            if (request.IsPaid == false)
            {
                return;
            }

            // кастомные проводки
            await Task.WhenAll(
                customTaxPostingsSaver.OverwriteAsync(MapOverwriteRequest(request)),
                customAccPostingsSaver.OverwriteAsync(request));
        }

        private CustomTaxPostingsOverwriteRequest MapOverwriteRequest(OtherOutgoingSaveRequest request)
        {
            return new CustomTaxPostingsOverwriteRequest
            {
                DocumentBaseId = request.DocumentBaseId,
                DocumentDate = request.Date,
                DocumentNumber = request.Number,
                Description = request.Description,
                TaxationSystemType = null,
                Postings = request.TaxPostings
            };
        }
    }
}