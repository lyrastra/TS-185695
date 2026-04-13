using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [InjectAsSingleton(typeof(IPaymentToNaturalPersonsCreator))]
    class PaymentToNaturalPersonsCreator : IPaymentToNaturalPersonsCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly PaymentToNaturalPersonsApiClient apiClient;
        private readonly PaymentToNaturalPersonsEventWriter writer;

        public PaymentToNaturalPersonsCreator(
            IBaseDocumentCreator baseDocumentCreator,
            PaymentToNaturalPersonsApiClient apiClient,
            PaymentToNaturalPersonsEventWriter writer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(PaymentToNaturalPersonsSaveRequest request)
        {
            var baseDocumentCreateRequest = PaymentToNaturalPersonsMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}