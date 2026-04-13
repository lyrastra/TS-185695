using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.OauthToken.Client
{
    public interface IOauthTokenClient
    {
        Task<List<Guid>> GetTokenGuidListAsync(int userId, int clientId);

        Task<bool> AddTokenGuidAsync(Guid guid, int userId, int clientId, TimeSpan expire, int size);

        Task<bool> DeleteTokenGuidAsync(Guid guid, int userId, int clientId, bool temporary = false);

        Task<bool> DeleteAllTokenGuidAsync(int userId, int clientId = 0);

        Task<bool> SetTokenTemporaryGuidAsync(Guid guid, int userId, int clientId, TimeSpan timeSpan);

        Task<Guid?> GetTokenTemporaryGuidAsync(int userId, int clientId);

        Task DeleteAllTokenGuidsExceptCurrentSessionAsync(
            int userId,
            Guid currentSessionGuid,
            int? currentSessionClientId,
            IReadOnlyCollection<int> clientIds);

        /// <summary>
        /// Проверить, что указанный guid публичного oauth-токена существует в контексте указанного пользователя и клиента
        /// </summary>
        /// <param name="userId">идентификатор пользователя</param>
        /// <param name="clientId">идентификатор oauth2.0 клиента</param>
        /// <param name="tokenGuid">guid публиного oauth-токена</param>
        /// <returns></returns>
        Task<bool> IsPublicTokenGuidExistAsync(int userId, int clientId, Guid tokenGuid);
    }
}