using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BankFee;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BankFee;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BankFee
{
    [InjectAsSingleton(typeof(IBankFeeCreator))]
    class BankFeeCreator : IBankFeeCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly BankFeeApiClient apiClient;
        private readonly BankFeeEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;

        public BankFeeCreator(
            IBaseDocumentCreator baseDocumentCreator,
            ITaxationSystemTypeReader taxationSystemTypeReader,
            BankFeeApiClient apiClient,
            BankFeeEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(BankFeeSaveRequest request)
        {
            if (request.TaxationSystemType == null)
            {
                request.TaxationSystemType = await taxationSystemTypeReader.GetDefaultByYearAsync(request.Date.Year);
            }

            var baseDocumentCreateRequest = BankFeeMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(BankFeeMapper.MapToCustomTaxPostingsOverwriteRequest(request));

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}