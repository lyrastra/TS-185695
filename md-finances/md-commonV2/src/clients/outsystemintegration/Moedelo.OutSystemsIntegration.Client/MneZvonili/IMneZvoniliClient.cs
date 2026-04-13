using System.Collections.Generic;
using System.Threading.Tasks;

using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.OutSystemsIntegrationV2.Client.MneZvonili
{
    public interface IMneZvoniliClient : IDI
    {
        Task<int> GetRegionIdByPhoneAsync(string phone);
    }
}