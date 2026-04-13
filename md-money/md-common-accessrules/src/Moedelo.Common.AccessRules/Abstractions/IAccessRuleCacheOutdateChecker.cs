using System.Threading.Tasks;

namespace Moedelo.Common.AccessRules.Abstractions
{
    internal interface IAccessRuleCacheOutdateChecker
    {
        Task<bool> IsOutdatedAsync();

        void OnCacheRefreshed();
    }
}