using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    [InjectAsSingleton(typeof(PaymentToAccountablePersonApiClient))]
    internal sealed class PaymentToAccountablePersonApiClient
    {
        private const string path = "Outgoing/PaymentToAccountablePerson";

        private readonly IPaymentOrderApiClient apiClient;

        public PaymentToAccountablePersonApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<PaymentToAccountablePersonResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<PaymentToAccountablePersonDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return PaymentToAccountablePersonMapper.MapToResponse(dto);
        }

        public Task CreateAsync(PaymentToAccountablePersonSaveRequest request)
        {
            var dto = PaymentToAccountablePersonMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task CreateMissingAsync(PaymentToAccountablePersonWithMissingEmployeeSaveRequest request)
        {
            var dto = PaymentToAccountablePersonMapper.MapToDto(request);
            return apiClient.CreateAsync($"{path}/WithMissingEmployee", dto);
        }

        public Task UpdateAsync(PaymentToAccountablePersonSaveRequest request)
        {
            var dto = PaymentToAccountablePersonMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
