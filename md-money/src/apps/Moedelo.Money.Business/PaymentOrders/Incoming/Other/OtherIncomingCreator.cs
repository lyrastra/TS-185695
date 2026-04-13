using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.Other;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.Other;
using System.Threading.Tasks;
using Moedelo.Money.Domain.TaxPostings;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.Other
{
    [InjectAsSingleton(typeof(IOtherIncomingCreator))]
    internal sealed class OtherIncomingCreator : IOtherIncomingCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly OtherIncomingApiClient apiClient;
        private readonly OtherIncomingEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly OtherIncomingCustomAccPostingsSaver customAccPostingsSaver;
        private readonly OtherIncomingProvideInAccountingFixer provideInAccountingFixer;

        public OtherIncomingCreator(
            IBaseDocumentCreator baseDocumentCreator,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            OtherIncomingApiClient apiClient,
            OtherIncomingEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            OtherIncomingCustomAccPostingsSaver customAccPostingsSaver,
            OtherIncomingProvideInAccountingFixer provideInAccountingFixer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.apiClient = apiClient;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.customAccPostingsSaver = customAccPostingsSaver;
            this.provideInAccountingFixer = provideInAccountingFixer;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(OtherIncomingSaveRequest request)
        {
            request.TaxationSystemType ??= await taxationSystemTypeReader.GetDefaultByYearAsync(request.Date.Year);

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

        private Task<long> CreateBaseDocumentAsync(OtherIncomingSaveRequest request)
        {
            var baseDocument = new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
            return baseDocumentCreator.CreateForPaymentOrderAsync(baseDocument);
        }

        private CustomTaxPostingsOverwriteRequest MapOverwriteRequest(OtherIncomingSaveRequest request)
        {
            return new CustomTaxPostingsOverwriteRequest
            {
                DocumentBaseId = request.DocumentBaseId,
                DocumentDate = request.Date,
                DocumentNumber = request.Number,
                Description = request.Description,
                TaxationSystemType = request.TaxationSystemType,
                Postings = request.TaxPostings
            };
        }
    }
}