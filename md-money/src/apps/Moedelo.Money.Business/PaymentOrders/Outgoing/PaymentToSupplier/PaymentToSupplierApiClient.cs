using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier
{
    [InjectAsSingleton(typeof(PaymentToSupplierApiClient))]
    internal sealed class PaymentToSupplierApiClient
    {
        private const string path = "Outgoing/PaymentToSupplier";

        private readonly IPaymentOrderApiClient apiClient;

        public PaymentToSupplierApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<PaymentToSupplierResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<PaymentToSupplierDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return PaymentToSupplierMapper.MapToResponse(dto);
        }

        public async Task<IReadOnlyCollection<PaymentToSupplierResponse>> GetByBaseIdsAsync(
            IReadOnlyCollection<long> documentBaseIds)
        {
            var dtos = await apiClient.PostAsync<IReadOnlyCollection<long>, PaymentToSupplierDto[]>(
                $"{path}/ByBaseIds", documentBaseIds);
            return dtos.Select(PaymentToSupplierMapper.MapToResponse).ToArray();
        }

        public Task CreateAsync(PaymentToSupplierSaveRequest request)
        {
            var dto = PaymentToSupplierMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(PaymentToSupplierSaveRequest request)
        {
            var dto = PaymentToSupplierMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
