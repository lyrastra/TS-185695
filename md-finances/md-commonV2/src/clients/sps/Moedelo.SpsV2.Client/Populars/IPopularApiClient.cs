using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.SpsV2.Dto.Populars;

namespace Moedelo.SpsV2.Client.Populars
{
    public interface IPopularApiClient
    {
        Task<List<PopularDto>> GetPopularList(GetPopularListRequestDto request);
    }
}