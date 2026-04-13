using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionToAuthorizedCapital
{
    [InjectAsSingleton(typeof(ContributionToAuthorizedCapitalApiClient))]
    internal sealed class ContributionToAuthorizedCapitalApiClient
    {
        private const string path = "Incoming/ContributionToAuthorizedCapital";

        private readonly IPaymentOrderApiClient apiClient;

        public ContributionToAuthorizedCapitalApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<ContributionToAuthorizedCapitalResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<ContributionToAuthorizedCapitalDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return ContributionToAuthorizedCapitalMapper.MapToResponse(dto);
        }

        public Task CreateAsync(ContributionToAuthorizedCapitalSaveRequest request)
        {
            var dto = ContributionToAuthorizedCapitalMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(ContributionToAuthorizedCapitalSaveRequest request)
        {
            var dto = ContributionToAuthorizedCapitalMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
