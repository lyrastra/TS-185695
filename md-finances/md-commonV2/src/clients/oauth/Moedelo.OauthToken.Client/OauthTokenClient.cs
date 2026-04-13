using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;

namespace Moedelo.OauthToken.Client
{
    [InjectAsSingleton(typeof(IOauthTokenClient))]
    public class OauthTokenClient(ITokenRedisDbExecuter tokenRedisDbExecutor) : IOauthTokenClient
    {
        public async Task<List<Guid>> GetTokenGuidListAsync(int userId, int clientId)
        {
            var guidList = await tokenRedisDbExecutor.GetValueListByKeyAsync(GetGuidListKey(userId, clientId)).ConfigureAwait(false);

            return guidList?.Select(Guid.Parse).ToList();
        }

        public async Task<bool> AddTokenGuidAsync(Guid guid, int userId, int clientId, TimeSpan expire, int size)
        {
            var guidListKey = GetGuidListKey(userId, clientId);
            var result = await tokenRedisDbExecutor
                .LpushAndLtrimInListForKeyAsync(guidListKey, guid.ToString(), size)
                .ConfigureAwait(false);
            var exp = await tokenRedisDbExecutor
                .ExpireForKeyAsync(guidListKey, expire)
                .ConfigureAwait(false);

            return result && exp;
        }

        public Task<bool> DeleteTokenGuidAsync(Guid guid, int userId, int clientId, bool temporary)
        {
            var rKey = GetGuidListKey(userId, clientId, temporary);
            if (temporary)
            {
                return tokenRedisDbExecutor.DeleteKeyAsync(rKey);
            }

            return tokenRedisDbExecutor.DeleteValueInListForKeyAsync(rKey, guid.ToString());
        }

        public Task<bool> DeleteAllTokenGuidAsync(int userId, int clientId)
        {
            return tokenRedisDbExecutor.DeleteKeyAsync(GetGuidListKey(userId, clientId));
        }

        public Task<bool> SetTokenTemporaryGuidAsync(Guid guid, int userId, int clientId, TimeSpan timeSpan)
        {
            return tokenRedisDbExecutor.SetValueForKeyAsync(GetGuidListKey(userId, clientId, true), guid.ToString(), timeSpan);
        }

        public async Task<Guid?> GetTokenTemporaryGuidAsync(int userId, int clientId)
        {
            var guid = await tokenRedisDbExecutor.GetValueByKeyAsync(GetGuidListKey(userId, clientId, true)).ConfigureAwait(false);

            return (Guid?) (string.IsNullOrEmpty(guid) ? (ValueType) null : new Guid(guid));
        }

        public async Task DeleteAllTokenGuidsExceptCurrentSessionAsync(
            int userId,
            Guid currentSessionGuid,
            int? currentSessionClientId,
            IReadOnlyCollection<int> clientIds)
        {
            // удаляем все токены из наборов по clientId, отличных от указанного как текущий
            foreach (var clientId in clientIds.Where(value => value != currentSessionClientId))
            {
                await DeleteAllTokenGuidAsync(userId, clientId).ConfigureAwait(false);
            }

            // удаляем все токены кроме указанного из набора по указанному clientId
            if (currentSessionClientId.HasValue)
            {
                var clientId = currentSessionClientId.Value;
                var guidList = await GetTokenGuidListAsync(userId, clientId).ConfigureAwait(false);

                foreach (var guid in guidList)
                {
                    if (guid != currentSessionGuid)
                    {
                        await DeleteTokenGuidAsync(guid, userId, clientId, false).ConfigureAwait(false);
                    }
                }
            }
        }

        public async Task<bool> IsPublicTokenGuidExistAsync(int userId, int clientId, Guid tokenGuid)
        {
            var key = GetGuidListKey(userId, clientId);
            var value = tokenGuid.ToString();
        
            var index = await tokenRedisDbExecutor.GetValueIndexInListAsync(key, value).ConfigureAwait(false);

            return index.HasValue;
        }

        private static string GetGuidListKey(int userId, int clientId, bool temporary = false)
        {
            return $"user:{userId}:client:{clientId}{(temporary ? ":temporary" : string.Empty)}";
        }
    }
}