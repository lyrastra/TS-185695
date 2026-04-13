using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Morphers;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.OutSystemsIntegrationV2.Dto.Morpher;

namespace Moedelo.OutSystemsIntegrationV2.Client.Morpher
{
    public interface IMorpherClient : IDI
    {
        Task<CasesDto> GetCasesAsync(string query, MorpherFlag? flag = null);
    }
}