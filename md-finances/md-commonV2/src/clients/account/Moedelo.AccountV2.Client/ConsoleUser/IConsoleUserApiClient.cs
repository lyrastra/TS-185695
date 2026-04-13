using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.User;

namespace Moedelo.AccountV2.Client.ConsoleUser;

public interface IConsoleUserApiClient
{
    Task<UserDto> GetOrCreateByLoginAsync(string login, CancellationToken cancellationToken = default);
}