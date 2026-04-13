using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundFromAccountablePerson;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    [InjectAsSingleton(typeof(IRefundFromAccountablePersonCreator))]
    internal sealed class RefundFromAccountablePersonCreator : IRefundFromAccountablePersonCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly RefundFromAccountablePersonApiClient apiClient;
        private readonly RefundFromAccountablePersonEventWriter writer;

        public RefundFromAccountablePersonCreator(
            IBaseDocumentCreator baseDocumentCreator,
            RefundFromAccountablePersonApiClient apiClient,
            RefundFromAccountablePersonEventWriter writer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(RefundFromAccountablePersonSaveRequest request)
        {
            var baseDocumentCreateRequest = RefundFromAccountablePersonMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}