using System;
using System.Security.Claims;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Http;

namespace Moedelo.CommonV2.Auth.Domain;

[InjectAsSingleton(typeof(IUserFirmContextInitializer))]
public sealed class ClaimsUserFirmContextInitializer : IUserFirmContextInitializer
{
    private readonly IDIResolver dIResolver;

    public ClaimsUserFirmContextInitializer(IDIResolver dIResolver)
    {
        this.dIResolver = dIResolver;
    }

    public void InitializeContext(AuthenticationInfo authenticationInfo)
    {
        if (authenticationInfo == null || (authenticationInfo.UserId <= 0 && authenticationInfo.FirmId <= 0))
        {
            return;
        }

        var httpContext = dIResolver.GetInstance<IHttpEnviroment>().CurrentContext
                          ?? throw new ArgumentNullException(
                              "HttpContext",
                              $"Не удалось получить текущий HttpContext в {nameof(ClaimsUserFirmContextInitializer)}");

        var claims = new Claim[]
        {
            new Claim(MoedeloClaimsTypes.FirmId, authenticationInfo.FirmId.ToString()),
            new Claim(MoedeloClaimsTypes.UserId, authenticationInfo.UserId.ToString()),
            new Claim(MoedeloClaimsTypes.ClientId, authenticationInfo.ClientId?.ToString() ?? "")
        };

        httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
    }
}