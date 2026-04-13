using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanObtaining;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanObtaining;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.LoanObtaining
{
    [InjectAsSingleton(typeof(ILoanObtainingCreator))]
    internal sealed class LoanObtainingCreator : ILoanObtainingCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly LoanObtainingApiClient apiClient;
        private readonly LoanObtainingEventWriter writer;

        public LoanObtainingCreator(
            IBaseDocumentCreator baseDocumentCreator,
            LoanObtainingApiClient apiClient,
            LoanObtainingEventWriter writer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(LoanObtainingSaveRequest request)
        {
            var baseDocumentCreateRequest = LoanObtainingMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest).ConfigureAwait(false);

            await apiClient.CreateAsync(request).ConfigureAwait(false);
            await writer.WriteCreatedEventAsync(request).ConfigureAwait(false);

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}