using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyBankFee;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyBankFee
{
    [InjectAsSingleton(typeof(ICurrencyBankFeeCreator))]
    class CurrencyBankFeeCreator : ICurrencyBankFeeCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly CurrencyBankFeeApiClient apiClient;
        private readonly CurrencyBankFeeEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;

        public CurrencyBankFeeCreator(
            IBaseDocumentCreator baseDocumentCreator,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            CurrencyBankFeeEventWriter writer,
            CurrencyBankFeeApiClient apiClient)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.writer = writer;
            this.apiClient = apiClient;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(CurrencyBankFeeSaveRequest request)
        {
            var baseDocumentCreateRequest = CurrencyBankFeeMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                CurrencyBankFeeMapper.MapToCustomTaxPostingsOverwriteRequest(request));

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}