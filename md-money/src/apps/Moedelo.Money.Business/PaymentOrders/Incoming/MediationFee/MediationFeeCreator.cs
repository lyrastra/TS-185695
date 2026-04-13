using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.MediationFee;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.MediationFee;
using System.Threading.Tasks;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.MediationFee
{
    [InjectAsSingleton(typeof(IMediationFeeCreator))]
    class MediationFeeCreator : IMediationFeeCreator
    {
        private readonly IMediationFeeValidator validator;
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly MediationFeeApiClient apiClient;
        private readonly MediationFeeEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;

        public MediationFeeCreator(
            IMediationFeeValidator validator,
            IBaseDocumentCreator baseDocumentCreator,
            MediationFeeApiClient apiClient,
            MediationFeeEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver)
        {
            this.validator = validator;
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(MediationFeeSaveRequest request)
        {
            if (request.OutsourceState != OutsourceState.ConfirmInvalid)
            {
                await validator.ValidateAsync(request);
            }

            var baseDocumentCreateRequest = MediationFeeMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                MediationFeeMapper.MapToCustomTaxPostingsOverwriteRequest(request));

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}