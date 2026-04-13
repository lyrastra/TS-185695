using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Accounts.Abstractions.Dto;
using Moedelo.Accounts.Abstractions.Dto.UserFirmData;

namespace Moedelo.Accounts.Abstractions.Interfaces
{
    public interface IUserFirmDataApiClient
    {
        Task<List<FirmNameInfoDto>> GetFirmNameListByUserIdAsync(int userId);

        Task CreateAsync(int firmId, int userId);

        Task<List<UserFirmDataDto>> GetByFirmIdAsync(int firmId);

        Task<IReadOnlyCollection<UserFirmDataDto>> GetByFirmIdsAsync(
            IReadOnlyCollection<int> firmIds,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<UserFirmDataDto>> GetByUserIdsAsync(
            IReadOnlyCollection<int> userIds,
            CancellationToken cancellationToken = default);

        public Task<List<UserFirmDataDto>> GetByUserAndFirmIdsAsync(
            int userId,
            IReadOnlyCollection<int> firmIds,
            CancellationToken cancellationToken = default);
    }
}