using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.FirmActivityCategory;

namespace Moedelo.RequisitesV2.Client.FirmActivityCategory
{
    public interface IFirmActivityCategoryClient : IDI
    {
        Task<FirmActivityCategoryDto> GetMainAsync(int firmId, int userId);
        Task SetMainAsync(int firmId, int userId, string code,
            CancellationToken cancellationToken);
        
        Task<List<FirmActivityCategoryDto>> GetMainAsync(IReadOnlyCollection<int> firmIds);
        
        Task<FirmActivityCategoryDto> GetOutdatedAsync(int firmId, int userId);
    }
}