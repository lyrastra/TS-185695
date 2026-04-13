using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.BackofficeV2.Dto.RegionalPartnerInfo;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BackofficeV2.Client.RegionalPartnerInfo
{
    public interface IRegionalPartnerInfoApiClient : IDI
    {
        Task<RegionalPartnerInfoDto> GetAsync(int id);

        Task<IReadOnlyCollection<RegionalPartnerInfoDto>> GetByIdsAsync(IReadOnlyCollection<int> ids);

        Task<RegionalPartnerInfoDto> GetByUserIdAsync(int id);

        Task<List<RegionalPartnerInfoDto>> GetByUserIdsAsync(IReadOnlyCollection<int> ids);

        Task<RegionalPartnerInfoDto> GetByProfOutsourceIdAsync(int id);

        Task<int> GetRegionalPartnerIdByUtmSourceAsync(string utmSource);

        Task<IReadOnlyCollection<RegionalPartnerInfoDto>> GetByExternalPartnerCredentialIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<bool> IsDeletedAsync(int id, CancellationToken cancellationToken = default);
    }
}
