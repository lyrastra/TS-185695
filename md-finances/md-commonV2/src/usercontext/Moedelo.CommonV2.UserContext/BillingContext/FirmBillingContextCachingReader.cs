using Moedelo.AccountV2.Client.Account;
using Moedelo.BillingV2.Client.FirmBillingState;
using Moedelo.BillingV2.Dto.FirmBillingState;
using Moedelo.Common.Enums.Enums.Account;
using Moedelo.CommonV2.UserContext.Domain.BillingContext;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.CommonV2.UserContext.BillingContext;

[InjectAsSingleton(typeof(IFirmBillingContextCachingReader))]
public sealed class FirmBillingContextCachingReader : IFirmBillingContextCachingReader
{
    private readonly TimeSpan validDataCacheLifeTime = TimeSpan.FromDays(1);
    private const string CacheKeyPrefix = "BillingActualStateCache";

    private readonly IFirmBillingStateApiClient apiClient;
    private readonly IAuthorizationRedisDbExecutor cache;
    private readonly IAccountApiClient accountApiClient;

    public FirmBillingContextCachingReader(
        IFirmBillingStateApiClient apiClient,
        IAuthorizationRedisDbExecutor cache,
        IAccountApiClient accountApiClient)
    {
        this.apiClient = apiClient;
        this.cache = cache;
        this.accountApiClient = accountApiClient;
    }

    public Task<IFirmBillingContextData> GetAsync(int firmId, int userId)
    {
        return cache.IsAvailable()
            ? GetCachedAsync(firmId, userId)
            : GetFromApiWithoutCachingAsync(firmId, userId);
    }

    public async Task InvalidateAsync(int firmId)
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
                .Select(userId => GetCacheKey(firmId, userId))
                .ToList();
            keysToDelete.Add(mapCacheKey);

            await cache.DeleteKeysAsync(keysToDelete).ConfigureAwait(false);
        }
    }

    public Task InvalidateAsync(int firmId, int userId)
    {
        return cache.IsAvailable()
            ? Task.WhenAll(
                cache.DeleteValueFromSetAsync(GetFirmCachedUsersListKey(firmId), userId),
                cache.DeleteKeyAsync(GetCacheKey(firmId, userId)))
            : Task.CompletedTask;
    }

    public Task InvalidateAsync(IReadOnlyCollection<int> firmIds, int userId)
    {
        var keys = firmIds
            .Select(firmId => GetCacheKey(firmId, userId))
            .ToList();

        return cache.DeleteKeysAsync(keys);
    }

    private static string GetCacheKey(int firmId, int userId)
    {
        return $"{CacheKeyPrefix}:{firmId}:{userId}";
    }

    private static string GetFirmCachedUsersListKey(int firmId)
    {
        return $"{CacheKeyPrefix}:{firmId}:users";
    }

    private async Task<IFirmBillingContextData> GetCachedAsync(int firmId, int userId)
    {
        var cacheKey = GetCacheKey(firmId, userId);
        var context = await cache.GetValueByKeyAsync<FirmBillingStateDto>(cacheKey).ConfigureAwait(false);

        if (context != null)
        {
            return new FirmBillingContextData(context);
        }

        context = await apiClient.GetActualAsync(firmId).ConfigureAwait(false);
        var account = await accountApiClient.GetAccountByUserIdAsync(userId).ConfigureAwait(false);
        if (account != null && account.Type == AccountType.ProfOutsource)
        {
            context.PaidStatus = FirmBillingStateDto.FirmPaidStatus.Paid;
            context.ValidUntil = context.ValidUntil.AddDays(30);
        }

        if (context.PaidStatus != FirmBillingStateDto.FirmPaidStatus.NoOnePayment)
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

        return new FirmBillingContextData(context);
    }

    private async Task<IFirmBillingContextData> GetFromApiWithoutCachingAsync(int firmId, int userId)
    {
        var dto = await apiClient.GetActualAsync(firmId).ConfigureAwait(false);
        var account = await accountApiClient.GetAccountByUserIdAsync(userId).ConfigureAwait(false);
        if (account != null && account.Type == AccountType.ProfOutsource)
        {
            dto.PaidStatus = FirmBillingStateDto.FirmPaidStatus.Paid;
            dto.ValidUntil = dto.ValidUntil.AddDays(30);
        }

        return new FirmBillingContextData(dto);
    }

    private class FirmBillingContextData : IFirmBillingContextData
    {
        private readonly FirmBillingStateDto dto;

        public FirmBillingContextData(FirmBillingStateDto dto)
        {
            this.dto = dto;
        }

        public int FirmId => dto.FirmId;
        public bool IsTrial => dto.PaidStatus == FirmBillingStateDto.FirmPaidStatus.Trial;
        public bool IsPaid => dto.PaidStatus == FirmBillingStateDto.FirmPaidStatus.Paid;
        public bool IsExpired => dto.PaidStatus == FirmBillingStateDto.FirmPaidStatus.Expired;
        public string ProductPlatform => dto.ProductPlatform;
        public DateTime ValidUntil => dto.ValidUntil;
        public string ProductGroup => dto.ProductGroups.FirstOrDefault();
        public bool IsTrialCard => dto.IsTrialCard;
        public int TariffId => dto.TariffIds.FirstOrDefault();
        public string TariffName => dto.TariffName;

        private static readonly int[] registrationTariffIds =
        {
            4 /*Tariff.IpRegistration = 4*/,
            10 /*Tariff.OooRegistration = 10*/,
            79 /*MasterOfRegistration = 79*/
        };

        // NOTE: можно перевести проверку на проверку прав тарифов (добавить соответствующие права IpRegistrationTariff и OooRegistrationTariff нужным тарифам)
        // в идеале избавиться от этого поля: вынести в IUserContextExtensions или по месту использования (вроде только два места)
        public bool IsIpOrOooRegistrationTariff => registrationTariffIds.Contains(TariffId);
    }
}