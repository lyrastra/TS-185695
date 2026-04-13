using System.Collections.Generic;
using System.Threading.Tasks;

using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.OutSystemsIntegrationV2.Dto;

namespace Moedelo.OutSystemsIntegrationV2.Client.CheckChangeEgrul
{
    public interface ICheckChangeEgrulClient : IDI
    {
        Task<List<string>> CheckAsync(CheckChangeEgrulRequestDto request);
    }
}
