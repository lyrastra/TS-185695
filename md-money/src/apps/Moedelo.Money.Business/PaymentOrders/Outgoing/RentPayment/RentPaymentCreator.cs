using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RentPayment
{
    [InjectAsSingleton(typeof(IRentPaymentCreator))]
    internal sealed class RentPaymentCreator : IRentPaymentCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly RentPaymentApiClient apiClient;
        private readonly RentPeriodsReader rentPeriodsReader;
        private readonly RentPaymentEventWriter writer;

        public RentPaymentCreator(
            IBaseDocumentCreator baseDocumentCreator,
            RentPaymentApiClient apiClient,
            RentPeriodsReader rentPeriodsReader,
            RentPaymentEventWriter writer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.rentPeriodsReader = rentPeriodsReader;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(RentPaymentSaveRequest request)
        {
            var baseDocumentCreateRequest = RentPaymentMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);

            request.RentPeriods = await rentPeriodsReader.GetAsync(request.RentPeriods);
            await writer.WriteCreatedEventAsync(request);


            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}
