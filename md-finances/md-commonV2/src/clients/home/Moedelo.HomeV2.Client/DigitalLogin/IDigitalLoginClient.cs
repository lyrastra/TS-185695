using Moedelo.HomeV2.Dto.DigitalLogin;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.HomeV2.Client.DigitalLogin
{
    public interface IDigitalLoginClient : IDI
    {
        Task<List<DigitalLoginEmailDto>> GetEmailsByDigitalLoginAsync(string digitalLogin);
    }
}
