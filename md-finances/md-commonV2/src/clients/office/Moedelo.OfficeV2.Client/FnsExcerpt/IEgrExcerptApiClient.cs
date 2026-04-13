using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.OfficeV2.Dto.Egr;
using Moedelo.OfficeV2.Dto.Egr.IpModel;
using Moedelo.OfficeV2.Dto.Egr.Search;
using Moedelo.OfficeV2.Dto.Egr.UlModel;

namespace Moedelo.OfficeV2.Client.FnsExcerpt
{
    public interface IEgrExcerptApiClient : IDI
    {
        Task<EgrUlResponceDto> GetUlExcerptAsync(GetExcerptByInnAndOgrnRequestDto request);

        Task<EgrUlResponceDto> GetUlExcerptAsync(int id);

        Task<EgrIpResponceDto> GetIpExcerptAsync(GetExcerptByInnAndOgrnRequestDto request);

        Task<EgrIpResponceDto> GetIpExcerptAsync(int id);

        Task<SearchEgrResponseDto> SearchAsync(SearchEgrRequestDto request);

        Task<SearchEgrResponseDto> SearchDebugAsync(SearchEgrRequestDto request);

        Task<List<QueryStatResponseDto>> GetQueryStatAsync(QueryStatRequestDto request);
    }
}