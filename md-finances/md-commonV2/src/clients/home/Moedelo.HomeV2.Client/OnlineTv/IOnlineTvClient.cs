using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.OnlineTv;
using Moedelo.HomeV2.Dto.OnlineTv;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.HomeV2.Client.OnlineTv
{
    public interface IOnlineTvClient : IDI
    {
        /// <summary> Get list of last {count} onlineTv events </summary>
        /// <param name="count">list size</param>
        /// <returns>List of onlineTv evetns</returns>
        Task<List<OnlineTvArchiveDto>> GetOnlineTvArchive(int count);

        Task<string> GetArchiveVideoByEventId(int id);

        Task<OnlineTvArchiveDto> GetArchiveEventById(int id);

        Task<List<OnlineTvArchiveDto>> GetArchivesByType(OnlineTvCategoryType type);

        Task<List<WebinarCategoryDto>> GetWebinarCategoriesByType(OnlineTvCategoryType type);

        Task<OnlineTvEventResponseDto> GetEventByIdAsync(int id);

        Task<List<OnlineTvEventResponseDto>> GetEventsByListIdsAsync(List<int> ids);

        Task<List<OnlineTvEventResponseDto>> GetEventsByTimeIntervalAsync(DateTime startDateTime, DateTime endDateTime);

        Task<List<OnlineTvRegisteredPeopleResponseDto>> GetRegistredPeopleByEventIdAsync(int id);

        Task<OnlineTvArchiveDto> GetArchiveEventByIdAsync(int id);

        Task<List<OnlineTvArchiveDto>> GetArchivesByTypeAsync(OnlineTvCategoryType type);

        Task<string> GetArchiveVideoByEventIdAsync(int id);

        Task<List<WebinarCategoryDto>> GetWebinarCategoriesByTypeAsync(OnlineTvCategoryType type);
    }
}