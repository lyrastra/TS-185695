using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Common.AccessRules.Abstractions.Models;
using Moedelo.Common.AccessRules.External.Authorization;
using Moedelo.Common.AccessRules.Infrastructure;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.AccessRules
{
    [InjectAsSingleton(typeof(IAuthorizationContextDataCachingReader))]
    internal sealed class AuthorizationContextDataCachingReader : IAuthorizationContextDataCachingReader
    {
        /// <summary>
        /// корректный контекст можно хранить достаточно долгий срок
        /// причины:
        /// 1. скорее всего он будет востребован в ближайшее время
        /// 2. он редко протухает
        /// 3. кэш будет инвалидироваться явным образом
        /// этот кэш должен инвалидироваться явным образом в каждом из случаев:
        /// - изменение тарифа фирмы (сейчас привязан к ITariffRoleCache::InvalidateAsync(firmId)
        /// - изменение прав в ролях (этого нет, как и не было до этого. пока не делаем)
        /// - изменение роли пользователя в фирме (сейчас привязан к ITariffRoleCache::InvalidateAsync(firmId, firmId)
        /// </summary>
        private readonly TimeSpan validDataCacheLifeTime = TimeSpan.FromDays(1);

        private readonly IAuthorizationContextApiClient apiClient;
        private readonly IAuthorizationRedisDbExecutor cache;
        private readonly SettingValue cacheKeyPrefix;
        private string CacheKeyPrefix => cacheKeyPrefix.Value;

        public AuthorizationContextDataCachingReader(
            ISettingRepository settingRepository,
            IAuthorizationContextApiClient apiClient,
            IAuthorizationRedisDbExecutor cache)
        {
            this.apiClient = apiClient;
            this.cache = cache;
            cacheKeyPrefix = settingRepository.Get("AuthorizationContextDataCacheRedisKeyPrefix");
        }

        public async Task<IAuthorizationContextData> GetAsync(FirmId firmId, UserId userId)
        {
            return cache.IsAvailable()
                ? await GetCachedAsync(firmId, userId).ConfigureAwait(false)
                : await GetFromApiWithoutCachingAsync(firmId, userId).ConfigureAwait(false);
        }
        
        private static readonly IAuthorizationContextData emptyAuthorizationContext =
            new AuthorizationContextData(
                new AuthorizationContextDto
                {
                    FirmId = (int)FirmId.Unidentified,
                    UserId = (int)UserId.Unidentified,
                    RoleId = (int)RoleId.Unidentified,
                    RoleRules = Array.Empty<AccessRule>(),
                    TariffRules = Array.Empty<AccessRule>()
                });

        public IAuthorizationContextData GetEmpty()
        {
            return emptyAuthorizationContext;
        }

        public async Task InvalidateCacheAsync(FirmId firmId)
        {
            if (!cache.IsAvailable())
            {
                return;
            }

            var mapCacheKey = GetFirmCachedUsersListKey(firmId);
            var userIds = await cache.GetAllValuesOfSetAsync<int>(mapCacheKey).ConfigureAwait(false);

            if (userIds?.Any() == true)
            {
                var keysToDelete = userIds
                    .Select(userId => GetCacheKey(firmId, new UserId(userId)))
                    .ToList();
                keysToDelete.Add(mapCacheKey);

                await cache.DeleteKeysAsync(keysToDelete).ConfigureAwait(false);
            }
        }

        public Task InvalidateCacheAsync(FirmId firmId, UserId userId)
        {
            return cache.IsAvailable()
                ? Task.WhenAll(
                    cache.DeleteValueFromSetAsync<int>(GetFirmCachedUsersListKey(firmId), (int)userId),
                    cache.DeleteKeyAsync(GetCacheKey(firmId, userId)))
                : Task.CompletedTask;
        }

        public Task InvalidateCacheAsync(FirmId firmId, IEnumerable<UserId> userIds)
        {
            var fullKeys = userIds.Select(userId => GetCacheKey(firmId, userId)).ToArray();

            return cache.IsAvailable()
                ? cache.DeleteKeysAsync(fullKeys)
                : Task.CompletedTask;
        }
        
        private string GetCacheKey(FirmId firmId, UserId userId)
        {
            return $"{CacheKeyPrefix}:{firmId.ToString()}:{userId.ToString()}";
        }

        private string GetFirmCachedUsersListKey(FirmId firmId)
        {
            return $"{CacheKeyPrefix}:{firmId.ToString()}:users";
        }

        private async Task<IAuthorizationContextData> GetCachedAsync(FirmId firmId, UserId userId)
        {
            var cacheKey = GetCacheKey(firmId, userId);
            var context = await cache.GetValueByKeyAsync<AuthorizationContextDto>(cacheKey).ConfigureAwait(false);

            if (context != null)
            {
                return new AuthorizationContextData(context);
            }

            context = await apiClient.GetAsync((int)firmId, (int)userId).ConfigureAwait(false);

            if (CanBeCached(context))
            {
                var usersListKey = GetFirmCachedUsersListKey(firmId);
                await cache
                    .AddValueToSetAsync<int>(usersListKey, (int)userId)
                    .ConfigureAwait(false);
                await cache
                    .ExpireForKeyAsync(usersListKey, validDataCacheLifeTime)
                    .ConfigureAwait(false);
                await cache
                    .SetValueForKeyAsync(cacheKey, context, validDataCacheLifeTime)
                    .ConfigureAwait(false);
            }

            return new AuthorizationContextData(context);
        }
        
        private static bool CanBeCached(AuthorizationContextDto context)
        {
            return context != null
                   && context.RoleId != AuthorizationContextDto.InvalidRoleId
                   && context.RoleRules?.Any() == true;
        }

        private async Task<AuthorizationContextData> GetFromApiWithoutCachingAsync(FirmId firmId, UserId userId)
        {
            var dto = await apiClient.GetAsync((int)firmId, (int)userId).ConfigureAwait(false);

            return new AuthorizationContextData(dto);
        }

        private class AuthorizationContextData : IAuthorizationContextData
        {
            private readonly AuthorizationContextDto dto;

            public AuthorizationContextData(AuthorizationContextDto dto)
            {
                this.dto = dto;
            }

            public int FirmId => dto.FirmId;
            public int UserId => dto.UserId;
            public int RoleId => dto.RoleId;
            public IReadOnlyCollection<AccessRule> RoleRules => dto.RoleRules;
            public IReadOnlyCollection<AccessRule> TariffRules => dto.TariffRules;
        }
    }
}