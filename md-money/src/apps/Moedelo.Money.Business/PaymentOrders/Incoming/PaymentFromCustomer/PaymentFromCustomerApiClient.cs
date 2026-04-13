using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton(typeof(PaymentFromCustomerApiClient))]
    internal sealed class PaymentFromCustomerApiClient
    {
        private const string path = "Incoming/PaymentFromCustomer";

        private readonly IPaymentOrderApiClient apiClient;

        public PaymentFromCustomerApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<PaymentFromCustomerResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<PaymentFromCustomerDto>($"{path}/{documentBaseId}");
            return PaymentFromCustomerMapper.MapToResponse(dto);
        }

        public async Task<IReadOnlyCollection<PaymentFromCustomerResponse>> GetByBaseIdsAsync(
            IReadOnlyCollection<long> documentBaseIds)
        {
            var dtos = await apiClient.PostAsync<IReadOnlyCollection<long>, PaymentFromCustomerDto[]>(
                $"{path}/ByBaseIds", documentBaseIds);
            return dtos.Select(PaymentFromCustomerMapper.MapToResponse).ToArray();
        }

        public Task CreateAsync(PaymentFromCustomerSaveRequest request)
        {
            var dto = PaymentFromCustomerMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(PaymentFromCustomerSaveRequest request)
        {
            var dto = PaymentFromCustomerMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
