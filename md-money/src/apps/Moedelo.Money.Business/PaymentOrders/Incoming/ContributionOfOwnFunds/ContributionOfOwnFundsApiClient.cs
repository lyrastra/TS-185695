using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionOfOwnFunds;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionOfOwnFunds
{
    [InjectAsSingleton(typeof(ContributionOfOwnFundsApiClient))]
    internal sealed class ContributionOfOwnFundsApiClient
    {
        private const string path = "Incoming/ContributionOfOwnFunds";

        private readonly IPaymentOrderApiClient apiClient;

        public ContributionOfOwnFundsApiClient(
            IPaymentOrderApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<ContributionOfOwnFundsResponse> GetAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<ContributionOfOwnFundsDto>($"{path}/{documentBaseId}").ConfigureAwait(false);
            return ContributionOfOwnFundsMapper.MapToResponse(dto);
        }

        public Task CreateAsync(ContributionOfOwnFundsSaveRequest request)
        {
            var dto = ContributionOfOwnFundsMapper.MapToDto(request);
            return apiClient.CreateAsync(path, dto);
        }

        public Task UpdateAsync(ContributionOfOwnFundsSaveRequest request)
        {
            var dto = ContributionOfOwnFundsMapper.MapToDto(request);
            return apiClient.UpdateAsync($"{path}/{request.DocumentBaseId}", dto);
        }

        public Task DeleteAsync(long documentBaseId)
        {
            return apiClient.DeleteAsync($"{path}/{documentBaseId}");
        }
    }
}
