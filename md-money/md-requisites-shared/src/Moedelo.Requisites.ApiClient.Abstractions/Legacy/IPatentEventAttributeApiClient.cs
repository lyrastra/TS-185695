using Moedelo.Common.Types;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy
{
    public interface IPatentEventAttributeApiClient
    {
        Task<PatentEventAttributeDto[]> GetByPatentIdsAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<long> patentIds);
    }
}