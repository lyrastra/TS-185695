using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyOther;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyOther;
using System.Threading.Tasks;
using Moedelo.Money.Domain.TaxPostings;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyOther
{
    [InjectAsSingleton(typeof(ICurrencyOtherIncomingCreator))]
    internal sealed class CurrencyOtherIncomingCreator : ICurrencyOtherIncomingCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly CurrencyOtherIncomingApiClient apiClient;
        private readonly CurrencyOtherIncomingEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly CurrencyOtherIncomingCustomAccPostingsSaver customAccPostingsSaver;
        private readonly CurrencyOtherIncomingProvideInAccountingFixer provideInAccountingFixer;

        public CurrencyOtherIncomingCreator(
            IBaseDocumentCreator baseDocumentCreator,
            CurrencyOtherIncomingApiClient apiClient,
            CurrencyOtherIncomingEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            CurrencyOtherIncomingCustomAccPostingsSaver customAccPostingsSaver,
            CurrencyOtherIncomingProvideInAccountingFixer provideInAccountingFixer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.customAccPostingsSaver = customAccPostingsSaver;
            this.provideInAccountingFixer = provideInAccountingFixer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(CurrencyOtherIncomingSaveRequest request)
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

        private Task<long> CreateBaseDocumentAsync(CurrencyOtherIncomingSaveRequest request)
        {
            var baseDocument = new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
            return baseDocumentCreator.CreateForPaymentOrderAsync(baseDocument);
        }

        private CustomTaxPostingsOverwriteRequest MapOverwriteRequest(CurrencyOtherIncomingSaveRequest request)
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