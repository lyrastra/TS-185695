using System.Threading.Tasks;
using Moedelo.Common.AccessRules.Models;

namespace Moedelo.Common.AccessRules.Abstractions
{
    internal interface ITariffsAndRolesCache
    {
        Task<TariffsAndRoles> GetTariffsAndRolesAsync();
        
        void Invalidate();
    }
}