using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    [InjectAsSingleton(typeof(IRefundFromAccountablePersonReader))]
    internal sealed class RefundFromAccountablePersonReader : IRefundFromAccountablePersonReader
    {
        private readonly IPaymentOrderApiClient apiClient;
        private readonly RefundFromAccountablePersonLinksGetter linksGetter;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public RefundFromAccountablePersonReader(
            IPaymentOrderApiClient apiClient,
            RefundFromAccountablePersonLinksGetter linksGetter,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.linksGetter = linksGetter;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<RefundFromAccountablePersonResponse> GetByBaseIdAsync(long baseId)
        {
            var dto = await apiClient.GetAsync<RefundFromAccountablePersonDto>($"Incoming/RefundFromAccountablePerson/{baseId}").ConfigureAwait(false);
            var response = RefundFromAccountablePersonMapper.MapToResponse(dto);
            var links = await linksGetter.GetAsync(baseId).ConfigureAwait(false);
            response.AdvanceStatement = links.AdvanceStatement;
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}
