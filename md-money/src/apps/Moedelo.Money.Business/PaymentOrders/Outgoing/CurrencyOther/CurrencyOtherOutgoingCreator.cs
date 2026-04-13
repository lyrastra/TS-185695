using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyOther;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyOther;
using System.Threading.Tasks;
using Moedelo.Money.Domain.TaxPostings;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyOther
{
    [InjectAsSingleton(typeof(ICurrencyOtherOutgoingCreator))]
    internal sealed class CurrencyOtherOutgoingCreator : ICurrencyOtherOutgoingCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly CurrencyOtherOutgoingApiClient apiClient;
        private readonly CurrencyOtherOutgoingEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly CurrencyOtherOutgoingCustomAccPostingsSaver customAccPostingsSaver;
        private readonly CurrencyOtherOutgoingProvideInAccountingFixer provideInAccountingFixer;

        public CurrencyOtherOutgoingCreator(
            IBaseDocumentCreator baseDocumentCreator,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            CurrencyOtherOutgoingCustomAccPostingsSaver customAccPostingsSaver,
            CurrencyOtherOutgoingApiClient apiClient,
            CurrencyOtherOutgoingEventWriter writer,
            CurrencyOtherOutgoingProvideInAccountingFixer provideInAccountingFixer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.apiClient = apiClient;
            this.writer = writer;
            this.provideInAccountingFixer = provideInAccountingFixer;
            this.customAccPostingsSaver = customAccPostingsSaver;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(CurrencyOtherOutgoingSaveRequest request)
        {
            request.DocumentBaseId = await CreateBaseDocumentAsync(request);
            await provideInAccountingFixer.FixAsync(request);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            // кастомные проводки
            await Task.WhenAll(
                customTaxPostingsSaver.OverwriteAsync(MapOverwriteRequest(request)),
                customAccPostingsSaver.OverwriteAsync(request));

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        private Task<long> CreateBaseDocumentAsync(CurrencyOtherOutgoingSaveRequest request)
        {
            var baseDocument = new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
            return baseDocumentCreator.CreateForPaymentOrderAsync(baseDocument);
        }

        private CustomTaxPostingsOverwriteRequest MapOverwriteRequest(CurrencyOtherOutgoingSaveRequest request)
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