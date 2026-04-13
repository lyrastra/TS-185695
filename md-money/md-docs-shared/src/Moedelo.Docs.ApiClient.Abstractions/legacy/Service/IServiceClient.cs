using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Service.Dto;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Service
{
    public interface IServiceClient
    {
        Task<List<ServiceDto>> GetByNamesAsync(FirmId firmId, UserId userId, IReadOnlyCollection<string> names);
    }
}