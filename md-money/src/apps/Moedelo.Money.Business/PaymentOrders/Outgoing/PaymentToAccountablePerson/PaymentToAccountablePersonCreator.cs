using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    [InjectAsSingleton(typeof(IPaymentToAccountablePersonCreator))]
    class PaymentToAccountablePersonCreator : IPaymentToAccountablePersonCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly PaymentToAccountablePersonApiClient apiClient;
        private readonly PaymentToAccountablePersonEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;

        public PaymentToAccountablePersonCreator(
            IBaseDocumentCreator baseDocumentCreator,
            PaymentToAccountablePersonApiClient apiClient,
            PaymentToAccountablePersonEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(PaymentToAccountablePersonSaveRequest request)
        {
            // hack: при сохранении авансового отчета для п/п создаются связанные проводки,
            // но статус остается "проведен вручную", поэтому при пересохранении проводки слетают
            // будем считать, что при наличии авансового отчета не может быть кастомных проводок
            // пока не знаю как лучше разрулить этот костыль
            if (request.AdvanceStatementBaseIds.Count > 0 &&
                request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand)
            {
                request.TaxPostings.ProvidePostingType = ProvidePostingType.Auto;
            }

            var baseDocumentCreateRequest = PaymentToAccountablePersonMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                PaymentToAccountablePersonMapper.MapToCustomTaxPostingsOverwriteRequest(request));

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        public async Task<PaymentOrderSaveResponse> CreateWithMissingEmployeeAsync(PaymentToAccountablePersonWithMissingEmployeeSaveRequest request)
        {
            var baseDocumentCreateRequest = PaymentToAccountablePersonMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateMissingAsync(request);
            await writer.WriteCreatedEventAsync(PaymentToAccountablePersonMapper.MapToSaveRequest(request));

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}