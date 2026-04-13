using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Deduction
{
    [InjectAsSingleton(typeof(IDeductionCreator))]
    internal sealed class DeductionCreator : IDeductionCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly DeductionApiClient apiClient;
        private readonly DeductionEventWriter writer;
        private readonly DeductionCustomAccPostingsSaver customAccPostingsSaver;
        private readonly DeductionProvideInAccountingFixer provideInAccountingFixer;

        public DeductionCreator(
            IBaseDocumentCreator baseDocumentCreator,
            DeductionApiClient apiClient,
            DeductionEventWriter writer,
            DeductionCustomAccPostingsSaver customAccPostingsSaver,
            DeductionProvideInAccountingFixer provideInAccountingFixer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customAccPostingsSaver = customAccPostingsSaver;
            this.provideInAccountingFixer = provideInAccountingFixer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(DeductionSaveRequest request)
        {
            request.DocumentBaseId = await CreateBaseDocumentAsync(request);
            await provideInAccountingFixer.FixAsync(request);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            await ProvideCustomPostingsAsync(request);

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        private Task<long> CreateBaseDocumentAsync(DeductionSaveRequest request)
        {
            var baseDocument = new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
            return baseDocumentCreator.CreateForPaymentOrderAsync(baseDocument);
        }

        private async Task ProvideCustomPostingsAsync(DeductionSaveRequest request)
        {
            if (request.IsPaid == false)
            {
                return;
            }

            await customAccPostingsSaver.OverwriteAsync(request);
        }
    }
}