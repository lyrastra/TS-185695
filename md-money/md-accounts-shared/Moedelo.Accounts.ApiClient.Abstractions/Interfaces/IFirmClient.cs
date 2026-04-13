using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Accounts.Abstractions.Dto;
using Moedelo.Accounts.Abstractions.Dto.Firm;

namespace Moedelo.Accounts.Abstractions.Interfaces
{
    public interface IFirmClient
    {
        Task<int> GetLegalUserId(int firmId);
        
        Task<bool> GetIsInternal(int firmId);

        Task<int> CreateAsync(FirmDto firm);

        Task<List<FirmLeadMarkDto>> GetFirmLeadMarksAsync(IReadOnlyCollection<int> firmIds);
        
        Task<List<FirmDto>> GetFirmsAsync(IReadOnlyCollection<int> firmIds);

        Task<bool> IsDeletedAsync(int firmId);
        
        Task<int?> GetTargetFirmIdAsync(int firmId);

        Task<IReadOnlyCollection<FirmIdLegalUserIdDto>> GetByLegalUsersAsync(
            IReadOnlyCollection<int> userIds,
            CancellationToken cancellationToken = default);

        Task<List<FirmDto>> GetByInnsAsync(IReadOnlyCollection<string> inns, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<int>> FilterOutInternalAsync(
            IReadOnlyCollection<int> firmIds,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyDictionary<int, bool>> GetFlagsIsDeletedForFirmIdsAsync(
            IReadOnlyCollection<int> firmIds,
            CancellationToken cancellationToken = default);
    }
}