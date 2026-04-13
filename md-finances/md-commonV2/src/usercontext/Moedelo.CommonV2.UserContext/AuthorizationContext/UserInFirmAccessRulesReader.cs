using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.CommonV2.UserContext.AuthorizationContext;

[InjectAsSingleton(typeof(IUserInFirmAccessRulesReader))]
public class UserInFirmAccessRulesReader : IUserInFirmAccessRulesReader
{
    private readonly IAuthorizationContextDataCachingReader dataReader;

    public UserInFirmAccessRulesReader(IAuthorizationContextDataCachingReader dataReader)
    {
        this.dataReader = dataReader;
    }

    public async Task<ISet<AccessRule>> GetAsync(int firmId, int userId)
    {
        var data = await dataReader.GetAsync(firmId, userId).ConfigureAwait(false);

        return new HashSet<AccessRule>(data.UserRules);
    }
}