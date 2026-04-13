using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.SpsV2.Dto.News;

namespace Moedelo.SpsV2.Client.News
{
    public interface ISpsNewsClient
    {
        Task<List<DailyBuroNewsDto>> GetNewsAsync(string type, int take = 10, int skip = 0);

        Task<List<DailyBuroNewsDto>> GetDailyNewsAsync(GetBuroNewsRequestDto request);

        Task<List<MainBuroNewsDto>> GetMainNewsAsync(GetBuroNewsRequestDto request);

        Task<ChangeVoteResponseDto> ChangeVote(ChangeVoteRequestDto request);
    }
}
