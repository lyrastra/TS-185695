using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Authorization.Client.Abstractions;
using Moedelo.Authorization.Dto;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.CommonV2.UserContext;

[InjectAsSingleton(typeof(IRoleReader))]
internal sealed class RoleReader : IRoleReader
{
    private static readonly TimeSpan DataValidityPeriod = TimeSpan.FromHours(1); 

    private readonly IRoleApiClient rolesApiClient;
    private volatile RolesCacheData roles;

    public RoleReader(IRoleApiClient rolesApiClient)
    {
        this.rolesApiClient = rolesApiClient;
    }

    public async Task<RoleInfo> GetAsync(int roleId)
    {
        CheckForInvalidateCache();
        var cacheData = await GetCacheDataAsync().ConfigureAwait(false);

        return cacheData.Map.TryGetValue(roleId, out var role) ? role : null;
    }

    public async Task<IReadOnlyCollection<RoleInfo>> GetAllAsync()
    {
        CheckForInvalidateCache();
        var cacheData = await GetCacheDataAsync().ConfigureAwait(false);

        return cacheData.List;
    }

    private async Task<RolesCacheData> GetCacheDataAsync()
    {
        var localRefToRoles = this.roles;
        try
        {
            if (localRefToRoles == null)
            {
                var dto = await rolesApiClient.GetRolesAsync().ConfigureAwait(false);
                localRefToRoles = MapToDomain(dto);

                this.roles = localRefToRoles;

                return localRefToRoles;
            }

            return localRefToRoles;
        }
        catch
        {
            ClearCache();
            throw;
        }
    }

    private static RolesCacheData MapToDomain(IEnumerable<RoleDto> dto)
    {
        var roles = dto
            .Select(r => new RoleInfo
            {
                Id = r.Id,
                Name = r.Name,
                RoleCode = r.RoleCode,
                AccessRules = new HashSet<AccessRule>(r.AccessRules.Cast<AccessRule>())
            });

        var validUntil = DateTime.Now.Add(DataValidityPeriod);

        return new(roles, validUntil);
    }

    private void CheckForInvalidateCache()
    {
        var localRefToRoles = this.roles;
            
        if (localRefToRoles != null && localRefToRoles.ValidUntil < DateTime.Now)
        {
            ClearCache();
        }
    }

    private void ClearCache()
    {
        this.roles = null;
    }
}