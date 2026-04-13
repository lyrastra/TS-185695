using Moedelo.Common.Types;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy
{
    public interface IPatentApiClient
    {
        Task<bool> IsAnyExists(FirmId firmId, UserId userId, int year);

        Task<PatentWithoutAdditionalDataDto[]> GetWithoutAdditionalDataAsync(FirmId firmId, UserId userId, int? year = null);

        Task<PatentWithoutAdditionalDataDto> GetWithoutAdditionalDataByIdAsync(FirmId firmId, UserId userId, long id);

        Task<IReadOnlyCollection<PatentWithoutAdditionalDataDto>> GetWithoutAdditionalDataByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> ids);
        
        Task TurnOffNotificationsByFirmIdAsync(FirmId firmId, UserId userId);
    }
}