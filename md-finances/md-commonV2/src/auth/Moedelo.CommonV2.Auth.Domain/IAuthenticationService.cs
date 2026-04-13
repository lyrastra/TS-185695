using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CommonV2.Auth.Domain;

public interface IAuthenticationService : IDI
{
    Task<AuthenticationInfo> AuthenticateAsync(CancellationToken cancellationToken);
}