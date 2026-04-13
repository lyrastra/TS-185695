using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentCreator))]
    class BudgetaryPaymentCreator : IBudgetaryPaymentCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly BudgetaryPaymentApiClient apiClient;
        private readonly IBudgetaryPaymentEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;

        public BudgetaryPaymentCreator(
            IBaseDocumentCreator baseDocumentCreator,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            BudgetaryPaymentApiClient apiClient,
            IBudgetaryPaymentEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(BudgetaryPaymentSaveRequest request)
        {
            if (request.TaxationSystemType == null)
            {
                request.TaxationSystemType = await taxationSystemTypeReader.GetDefaultByYearAsync(request.Date.Year);
            }

            var baseDocumentCreateRequest = BudgetaryPaymentMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                BudgetaryPaymentMapper.MapToCustomTaxPostingsOverwriteRequest(request));

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}