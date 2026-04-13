using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Client.UserContext;
using Moedelo.AccountV2.Dto.UserContext;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;
using Moedelo.CommonV2.UserContext.Domain.ExtraData;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.CommonV2.UserContext;

[InjectAsSingleton(typeof(IUserContextExtraDataLoader))]
public class UserContextExtraDataLoader : IUserContextExtraDataLoader
{
    private readonly IUserContextNetCoreApiClient userContextApiClient;
    private readonly IRoleReader roleReader;

    public UserContextExtraDataLoader(
        IUserContextNetCoreApiClient userContextApiClient,
        IRoleReader roleReader)
    {
        this.userContextApiClient = userContextApiClient;
        this.roleReader = roleReader;
    }

    public async Task<IUserContextExtraData> GetAsync(int firmId, int userId, int roleId)
    {
        // эти данные сложно закэшировать, потому что пока сложно собрать и отследить все события,
        // которые должны их инвалидировать
        var infoDto = await userContextApiClient
            .GetAuthorizedUserInfoAsync(userId, firmId, CancellationToken.None)
            .ConfigureAwait(false);

        if (infoDto == null)
        {
            return null;
        }

        var role = await roleReader.GetAsync(roleId).ConfigureAwait(false);
            
        return new UserContextExtraDataFromAccount(infoDto, role);
    }

    public IUserContextExtraData GetUnauthorized()
    {
        return new UserContextExtraDataFromAccount(
            new UserContextBasicInfoDto(),
            null /*empty RoleInfo*/);
    }

    private class UserContextExtraDataFromAccount : IUserContextExtraData
    {
        private readonly UserContextBasicInfoDto commonInfo;
        private readonly RoleInfo roleInfo;

        internal UserContextExtraDataFromAccount(
            UserContextBasicInfoDto commonInfo,
            RoleInfo roleInfo)
        {
            this.commonInfo = commonInfo;
            this.roleInfo = roleInfo;
        }

        public string Login => commonInfo.Login;
        public string UserName => commonInfo.UserName;
        public string OrganizationName => commonInfo.OrganizationName;
        public bool IsInternal => commonInfo.IsInternal;
        public string Inn => commonInfo.Inn;
        public bool IsOoo => commonInfo.IsOoo;
        public bool IsEmployerMode => commonInfo.IsEmployerMode;
        public DateTime? FirmRegistrationDate => commonInfo.FirmRegistrationDate;
        public string RoleCode => roleInfo?.RoleCode;
        public string RoleName => roleInfo?.Name;
    }
}