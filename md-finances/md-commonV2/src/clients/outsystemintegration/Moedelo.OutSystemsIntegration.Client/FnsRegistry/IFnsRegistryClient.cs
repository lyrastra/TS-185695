using System.Collections.Generic;
using System.Threading.Tasks;

using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.OutSystemsIntegrationV2.Dto.Registry;

namespace Moedelo.OutSystemsIntegrationV2.Client.FnsRegistry
{
    public interface IFnsRegistryClient : IDI
    {
        Task<bool> CheckEgrulExclusionAsync(CheckEgrulExclusionInRegistryRequestDto request);
        
        Task<bool> CheckFigureheadAsync(CheckFigureheadInRegistryRequestDto request);
        
        Task<bool> CheckFictitiousAddressAsync(CheckFictitiousAddressRequestDto request);

        Task<CheckBlockedAccountsResponseDto> CheckBlockedAccountsAsync(CheckBlockedAccountsRequestDto request);
    }
}