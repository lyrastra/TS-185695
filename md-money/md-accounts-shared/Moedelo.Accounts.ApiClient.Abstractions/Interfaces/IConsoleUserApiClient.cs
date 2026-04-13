using System.Threading.Tasks;
using Moedelo.Accounts.Abstractions.Dto;

namespace Moedelo.Accounts.Abstractions.Interfaces
{
    public interface IConsoleUserApiClient
    {
        Task<UserDto> GetOrCreateByLoginAsync(string login);
    }
}