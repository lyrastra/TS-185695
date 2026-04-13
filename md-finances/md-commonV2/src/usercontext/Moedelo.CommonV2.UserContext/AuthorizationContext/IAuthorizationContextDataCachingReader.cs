using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;

namespace Moedelo.CommonV2.UserContext.AuthorizationContext;

public interface IAuthorizationContextDataCachingReader
{
    Task<IAuthorizationContextData> GetAsync(int firmId, int userId);
    IAuthorizationContextData GetEmpty();
    Task InvalidateCacheAsync(int firmId);
    Task InvalidateCacheAsync(int firmId, int userId);
    Task InvalidateCacheAsync(IReadOnlyCollection<int> firmIds, int userId);
}