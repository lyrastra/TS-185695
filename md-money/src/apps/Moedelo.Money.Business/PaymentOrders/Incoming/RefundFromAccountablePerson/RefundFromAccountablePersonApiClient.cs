using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    [InjectAsSingleton(typeof(RefundFromAccountablePersonApiClient))]
    internal sealed class RefundFromAccountablePersonApiClient
    {
        private const string path = "Incoming/RefundFromAccountablePerson";

        private readonly IPaymentOrderApiClient apiClient;

        public RefundFromAccountablePersonApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<RefundFromAccountablePersonResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<RefundFromAccountablePersonDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return RefundFromAccountablePersonMapper.MapToResponse(dto);
        }

        public Task CreateAsync(RefundFromAccountablePersonSaveRequest request)
        {
            var dto = RefundFromAccountablePersonMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(RefundFromAccountablePersonSaveRequest request)
        {
            var dto = RefundFromAccountablePersonMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
