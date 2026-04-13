using System.Collections.Generic;
using System.Threading.Tasks;

using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.OutSystemsIntegrationV2.Dto.StateContract;

namespace Moedelo.OutSystemsIntegrationV2.Client.StateContract
{
    public interface IStateContractClient : IDI
    {
        Task<StateContractResponseDto> GetContractsAsync(StateContractsRequestDto request);
    }
}