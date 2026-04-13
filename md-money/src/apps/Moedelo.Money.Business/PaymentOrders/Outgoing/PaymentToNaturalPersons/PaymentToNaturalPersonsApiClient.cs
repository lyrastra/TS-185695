using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [InjectAsSingleton(typeof(PaymentToNaturalPersonsApiClient))]
    internal sealed class PaymentToNaturalPersonsApiClient
    {
        private const string path = "Outgoing/PaymentToNaturalPersons";

        private readonly IPaymentOrderApiClient apiClient;

        public PaymentToNaturalPersonsApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<PaymentToNaturalPersonsResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<PaymentToNaturalPersonsDto>($"{path}/{documentBaseId}");
            return PaymentToNaturalPersonsMapper.MapToResponse(dto);
        }

        public async Task<IReadOnlyCollection<PaymentToNaturalPersonsResponse>> GetByBaseIdsAsync(
            IReadOnlyCollection<long> documentBaseIds)
        {
            var dtos = await apiClient.PostAsync<IReadOnlyCollection<long>, PaymentToNaturalPersonsDto[]>(
                $"{path}/ByBaseIds", documentBaseIds);
            return dtos.Select(PaymentToNaturalPersonsMapper.MapToResponse).ToArray();
        }

        public Task CreateAsync(PaymentToNaturalPersonsSaveRequest request)
        {
            if (request.OperationState == OperationState.MissingWorker)
            {
                var dto = PaymentToNaturalPersonsMapper.MapToMissingEmployeeDto(request);
                return apiClient.CreateAsync($"{path}/WithMissingEmployee", dto);
            }
            else
            {
                var dto = PaymentToNaturalPersonsMapper.MapToDto(request);
                return apiClient.CreateAsync(path, dto);
            }
        }

        public Task UpdateAsync(PaymentToNaturalPersonsSaveRequest request)
        {
            var dto = PaymentToNaturalPersonsMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
