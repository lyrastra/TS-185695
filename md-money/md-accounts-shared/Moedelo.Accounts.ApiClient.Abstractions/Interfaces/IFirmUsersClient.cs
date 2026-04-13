using System.Threading;
using System.Threading.Tasks;
using Moedelo.Accounts.Abstractions.Dto.FirmUsers;

namespace Moedelo.Accounts.Abstractions.Interfaces;

public interface IFirmUsersClient
{
    Task<UsersWithAccessToFirmDto> GetUsersHavingAccessToFirmAsync(int firmId, int userId, int companyId, CancellationToken ct);
}