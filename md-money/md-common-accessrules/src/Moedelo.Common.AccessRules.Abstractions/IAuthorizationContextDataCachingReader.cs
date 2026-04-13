using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.AccessRules.Abstractions.Models;
using Moedelo.Common.Types;

namespace Moedelo.Common.AccessRules.Abstractions
{
    public interface IAuthorizationContextDataCachingReader
    {
        Task<IAuthorizationContextData> GetAsync(FirmId firmId, UserId userId);
        IAuthorizationContextData GetEmpty();
        Task InvalidateCacheAsync(FirmId firmId);
        Task InvalidateCacheAsync(FirmId firmId, UserId userId);
        Task InvalidateCacheAsync(FirmId firmId, IEnumerable<UserId> userIds);
    }
}