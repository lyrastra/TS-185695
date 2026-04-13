using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using System.Threading.Tasks;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundToSettlementAccount
{
    [InjectAsSingleton(typeof(IRefundToSettlementAccountCreator))]
    internal sealed class RefundToSettlementAccountCreator : IRefundToSettlementAccountCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly RefundToSettlementAccountApiClient apiClient;
        private readonly RefundToSettlementAccountEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly RefundToSettlementAccountCustomAccPostingsSaver customAccPostingsSaver;
        private readonly RefundToSettlementAccountProvideInAccountingFixer provideInAccountingFixer;

        public RefundToSettlementAccountCreator(
            IBaseDocumentCreator baseDocumentCreator,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            RefundToSettlementAccountApiClient apiClient,
            RefundToSettlementAccountEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            RefundToSettlementAccountCustomAccPostingsSaver customAccPostingsSaver,
            RefundToSettlementAccountProvideInAccountingFixer provideInAccountingFixer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.apiClient = apiClient;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.customAccPostingsSaver = customAccPostingsSaver;
            this.provideInAccountingFixer = provideInAccountingFixer;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(RefundToSettlementAccountSaveRequest request)
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

        private Task<long> CreateBaseDocumentAsync(RefundToSettlementAccountSaveRequest request)
        {
            var baseDocument = new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
            return baseDocumentCreator.CreateForPaymentOrderAsync(baseDocument);
        }

        private CustomTaxPostingsOverwriteRequest MapOverwriteRequest(RefundToSettlementAccountSaveRequest request)
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