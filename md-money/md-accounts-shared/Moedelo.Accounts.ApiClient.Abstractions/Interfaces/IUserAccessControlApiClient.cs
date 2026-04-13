using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.Types;

namespace Moedelo.Accounts.Abstractions
{
    public interface IUserAccessControlApiClient
    {
        Task<ISet<AccessRule>> GetExplicitUserRulesAsync(UserId userId);
    }
}