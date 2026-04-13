using Moedelo.Accounts.Abstractions.Dto;
using Moedelo.Accounts.Abstractions.Dto.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Accounts.Abstractions.Interfaces
{
    public interface IUsersApiClient
    {
        Task<IReadOnlyCollection<UserDto>> SearchAsync(SearchUserRequestDto requestDto);
    }
}