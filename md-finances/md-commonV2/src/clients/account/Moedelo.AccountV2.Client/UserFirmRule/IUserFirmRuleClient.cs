using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountV2.Client.UserFirmRule
{
    public interface IUserFirmRuleClient : IDI
    {
        Task<List<AccessRule>> GetRulesAsync(int firmId, int userId);
    }
}