using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.AccessRules.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.AccessRules
{
    [InjectAsSingleton(typeof(IAccessRulesCache))]
    internal sealed class AccessRulesCache : IAccessRulesCache
    {
        private readonly ITariffsAndRolesCache tariffsAndRolesCache;
        
        private readonly HashSet<int> finallyMissedRoleIds = new HashSet<int>(new int[0]);
        private readonly object cacheMissRoleRetryLock = new object();
        
        private readonly HashSet<int> finallyMissedTariffIds = new HashSet<int>(new int[0]);
        private readonly object cacheMissTariffRetryLock = new object();
        
        public AccessRulesCache(ITariffsAndRolesCache tariffsAndRolesCache)
        {
            this.tariffsAndRolesCache = tariffsAndRolesCache;
        }

        public async Task<HashSet<AccessRule>> GetRoleRulesAsync(int roleId)
        {
            if (roleId <= 0)
            {
                return new HashSet<AccessRule>();
            }
            
            var tariffsAndRoles = await tariffsAndRolesCache.GetTariffsAndRolesAsync().ConfigureAwait(false);

            lock (cacheMissRoleRetryLock)
            {
                finallyMissedRoleIds.RemoveWhere(id => tariffsAndRoles.Roles.ContainsKey(id));
                
                if (finallyMissedRoleIds.Contains(roleId))
                {
                    return new HashSet<AccessRule>();
                }
            }

            if (tariffsAndRoles.Roles.TryGetValue(roleId, out var role))
            {
                return new HashSet<AccessRule>(role.AccessRules); // return copy
            }
            
            tariffsAndRolesCache.Invalidate();
            tariffsAndRoles = await tariffsAndRolesCache.GetTariffsAndRolesAsync().ConfigureAwait(false);
            
            if (tariffsAndRoles.Roles.TryGetValue(roleId, out role))
            {
                return new HashSet<AccessRule>(role.AccessRules); // return copy
            }
            
            // не удалось найти указанную сущность и после инвалидации кэша - помечаем её как окончательно отсутствующую
            // чтобы предотвратить бесконечную инвалидацию кэша по этому значению
            lock (cacheMissRoleRetryLock)
            {
                finallyMissedRoleIds.Add(roleId);
            }
            
            return new HashSet<AccessRule>();
        }

        public async Task<HashSet<AccessRule>> GetTariffRulesAsync(int tariffId)
        {
            if (tariffId <= 0)
            {
                return new HashSet<AccessRule>();
            }
            
            var tariffsAndRoles = await tariffsAndRolesCache.GetTariffsAndRolesAsync().ConfigureAwait(false);

            lock (cacheMissTariffRetryLock)
            {
                finallyMissedTariffIds.RemoveWhere(id => tariffsAndRoles.Tariffs.ContainsKey(id));
                
                if (finallyMissedTariffIds.Contains(tariffId))
                {
                    return new HashSet<AccessRule>();
                }
            }

            if (tariffsAndRoles.Tariffs.TryGetValue(tariffId, out var tariff))
            {
                return new HashSet<AccessRule>(tariff.AccessRules); // return copy
            }
            
            tariffsAndRolesCache.Invalidate();
            tariffsAndRoles = await tariffsAndRolesCache.GetTariffsAndRolesAsync().ConfigureAwait(false);
            
            if (tariffsAndRoles.Tariffs.TryGetValue(tariffId, out tariff))
            {
                return new HashSet<AccessRule>(tariff.AccessRules); // return copy
            }
            
            // не удалось найти указанную сущность и после инвалидации кэша - помечаем её как окончательно отсутствующую
            // чтобы предотвратить бесконечную инвалидацию кэша по этому значению
            lock (cacheMissTariffRetryLock)
            {
                finallyMissedTariffIds.Add(tariffId);
            }
            
            return new HashSet<AccessRule>();
        }

        public async Task<HashSet<AccessRule>> GetTariffsRulesAsync(IReadOnlyCollection<int> tariffIdList)
        {
            var tariffsRules = new HashSet<AccessRule>();
            var tariffsAndRoles = await tariffsAndRolesCache.GetTariffsAndRolesAsync().ConfigureAwait(false);

            lock (cacheMissTariffRetryLock)
            {
                finallyMissedTariffIds.RemoveWhere(id => tariffsAndRoles.Tariffs.ContainsKey(id));
                tariffIdList = tariffIdList.Where(id => id >= 0 && finallyMissedTariffIds.Contains(id) == false)
                    .ToHashSet();
            }

            var missedTariffIds = new List<int>();
            
            foreach (var tariffId in tariffIdList)
            {
                if (tariffsAndRoles.Tariffs.TryGetValue(tariffId, out var tariff))
                {
                    foreach (var tariffRule in tariff.AccessRules)
                    {
                        tariffsRules.Add(tariffRule);
                    }
                }
                else
                {
                    missedTariffIds.Add(tariffId);
                }
            }

            if (missedTariffIds.Count <= 0)
            {
                return tariffsRules;
            }
            
            tariffsAndRolesCache.Invalidate();
            tariffsAndRoles = await tariffsAndRolesCache.GetTariffsAndRolesAsync().ConfigureAwait(false);

            var addToFinallyMissedTariffIds = new List<int>();
            
            foreach (var tariffId in missedTariffIds)
            {
                if (tariffsAndRoles.Tariffs.TryGetValue(tariffId, out var tariff))
                {
                    foreach (var tariffRule in tariff.AccessRules)
                    {
                        tariffsRules.Add(tariffRule);
                    }
                }
                else
                {
                    addToFinallyMissedTariffIds.Add(tariffId);
                }
            }

            // не удалось найти указанную сущность и после инвалидации кэша - помечаем её как окончательно отсутствующую
            // чтобы предотвратить бесконечную инвалидацию кэша по этому значению
            lock (cacheMissTariffRetryLock)
            {
                foreach (var finallyMissedTariffId in addToFinallyMissedTariffIds)
                {
                    finallyMissedTariffIds.Add(finallyMissedTariffId);
                }
            }
            
            return tariffsRules;
        }
    }
}