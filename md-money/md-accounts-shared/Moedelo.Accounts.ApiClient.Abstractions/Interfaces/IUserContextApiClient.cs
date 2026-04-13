using System.Threading.Tasks;
using Moedelo.Accounts.Abstractions.Dto;
using Moedelo.Common.Types;

namespace Moedelo.Accounts.Abstractions.Interfaces
{
    public interface IUserContextApiClient
    {
        Task<UserCommonContextWithoutRulesDto> GetAsync(UserId userId, FirmId firmId);
    }
}