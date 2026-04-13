using System.Collections.Generic;
using System.Threading.Tasks;

using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.OutSystemsIntegrationV2.Client.Rosstat
{
    public interface IRosstatClient : IDI
    {
        Task<string> GetOkpoAsync(string innOrOgrn);
    }
}