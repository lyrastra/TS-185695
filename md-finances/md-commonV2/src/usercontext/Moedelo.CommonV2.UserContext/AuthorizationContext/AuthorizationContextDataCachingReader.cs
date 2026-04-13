using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Authorization.Client.Abstractions;
using Moedelo.Authorization.Dto;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CommonV2.UserContext.AuthorizationContext;

[InjectAsSingleton(typeof(IAuthorizationContextDataCachingReader))]
public sealed class AuthorizationContextDataCachingReader : IAuthorizationContextDataCachingReader
{
    /// <summary>
    /// Корректный контекст можно хранить достаточно долгий срок.
    /// Причины:
    /// 1. скорее всего он будет востребован в ближайшее время
    /// 2. он редко протухает
    /// 3. кэш будет инвалидироваться явным образом
    /// Этот кэш должен инвалидироваться явным образом в каждом из случаев:
    /// - изменение тарифа фирмы (сейчас привязан к ITariffRoleCache::InvalidateAsync(firmId)
    /// - изменение прав в ролях (этого нет, как и не было до этого. Не реализовано)
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
        cacheKeyPrefix = settingRepository.GetRequired("AuthorizationContextDataCacheRedisKeyPrefix");
    }

    public Task<IAuthorizationContextData> GetAsync(int firmId, int userId)
    {
        return cache.IsAvailable()
            ? GetCachedAsync(firmId, userId)
            : GetFromApiWithoutCachingAsync(firmId, userId);
    }
        
    private static readonly IAuthorizationContextData emptyAuthorizationContext =
        new AuthorizationContextData(
            new AuthorizationContextDto
            {
                RoleId = AuthorizationContextDto.InvalidRoleId,
                RoleRules = Array.Empty<AccessRule>(),
                TariffRules = Array.Empty<AccessRule>(),
            });

    public IAuthorizationContextData GetEmpty()
    {
        return emptyAuthorizationContext;
    }

    public async Task InvalidateCacheAsync(int firmId)
    {
        if (!cache.IsAvailable())
        {
            return;
        }

        var mapCacheKey = GetFirmCachedUsersListKey(firmId);
        var userIdsAsStrings = await cache.GetAllValuesOfSetAsync(mapCacheKey).ConfigureAwait(false);

        if (userIdsAsStrings?.Any() == true)
        {
            var keysToDelete = userIdsAsStrings
                .Select(userId => GetCacheKey(firmId, userId))
                .ToList();
            keysToDelete.Add(mapCacheKey);

            await cache.DeleteKeysAsync(keysToDelete).ConfigureAwait(false);
        }
    }

    public Task InvalidateCacheAsync(int firmId, int userId)
    {
        if (userId <= 0)
        {
            return InvalidateCacheAsync(firmId);
        }

        return cache.IsAvailable()
            ? Task.WhenAll(
                cache.DeleteValueFromSetAsync(GetFirmCachedUsersListKey(firmId), userId),
                cache.DeleteKeyAsync(GetCacheKey(firmId, userId)))
            : Task.CompletedTask;
    }

    public Task InvalidateCacheAsync(IReadOnlyCollection<int> firmIds, int userId)
    {
        var keys = firmIds
            .Select(firmId => GetCacheKey(firmId, userId))
            .ToList();

        return cache.DeleteKeysAsync(keys);
    }

    private string GetCacheKey(int firmId, int userId)
    {
        return $"{CacheKeyPrefix}:{firmId}:{userId}";
    }

    private string GetCacheKey(int firmId, string userIdKey)
    {
        return $"{CacheKeyPrefix}:{firmId}:{userIdKey}";
    }

    private string GetFirmCachedUsersListKey(int firmId)
    {
        return $"{CacheKeyPrefix}:{firmId}:users";
    }

    private async Task<IAuthorizationContextData> GetCachedAsync(int firmId, int userId)
    {
        var cacheKey = GetCacheKey(firmId, userId);
        var context = await cache.GetValueByKeyAsync<AuthorizationContextDto>(cacheKey).ConfigureAwait(false);

        if (context != null)
        {
            return new AuthorizationContextData(context);
        }

        context = await apiClient.GetAsync(firmId, userId).ConfigureAwait(false);

        if (CanBeCached(context))
        {
            var usersListKey = GetFirmCachedUsersListKey(firmId);
            await cache
                .AddValueToSetAsync(usersListKey, userId)
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

    private async Task<IAuthorizationContextData> GetFromApiWithoutCachingAsync(int firmId, int userId)
    {
        var dto = await apiClient.GetAsync(firmId, userId).ConfigureAwait(false);

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
        public IReadOnlyCollection<AccessRule> UserRules => dto.RoleRules;
        public IReadOnlyCollection<AccessRule> TariffRules => dto.TariffRules;
    }
}