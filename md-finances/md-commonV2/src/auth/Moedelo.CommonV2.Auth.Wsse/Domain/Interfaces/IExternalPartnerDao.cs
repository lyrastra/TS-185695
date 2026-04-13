using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CommonV2.Auth.Wsse.Domain.Models;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CommonV2.Auth.Wsse.Domain.Interfaces
{
    public interface IExternalPartnerDao : IDI
    {
        Task<List<ExternalPartnerCredentialDbResult>> GetAsync();
    }
}
