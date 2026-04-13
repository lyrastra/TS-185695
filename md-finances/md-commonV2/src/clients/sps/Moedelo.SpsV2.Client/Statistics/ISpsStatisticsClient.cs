using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.SpsV2.Dto.Statistics;

namespace Moedelo.SpsV2.Client.Statistics
{
    public interface ISpsStatisticsClient
    {
        Task<List<StatDataByLoginDto>> GetStatLastSearchAsync(StatRequestByLoginsData request);
        Task<List<StatDataByUserIdDto>> GetStatLastViewAsync(StatRequestByUserIdsData request);
        Task<List<StatDataByUserIdsDto>> GetGroupedStatLastViewByIdsListAsync(GroupedStatRequestByUserIds request);
    }
}
