using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PurseOperations;
using Moedelo.Money.Business.PurseOperations.ApiClient;
using Moedelo.Money.Enums;
using Moedelo.Money.PurseOperations.Dto.PurseOperations;

namespace Moedelo.Money.Business.PurseOperations
{
    [InjectAsSingleton(typeof(IPurseOperationGetter))]
    internal sealed class PurseOperationGetter : IPurseOperationGetter
    {
        private readonly IPurseOperationApiClient apiClient;

        public PurseOperationGetter(
            IPurseOperationApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<OperationType> GetOperationTypeAsync(long documentBaseId)
        {
            var dto = await apiClient.GetAsync<OperationTypeDto>($"PurseOperations/{documentBaseId}/OperationType").ConfigureAwait(false);
            return dto.OperationType;
        }
    }
}
