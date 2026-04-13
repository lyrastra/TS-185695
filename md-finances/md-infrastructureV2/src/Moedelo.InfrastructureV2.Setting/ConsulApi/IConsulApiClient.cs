using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.InfrastructureV2.Setting.ConsulApi
{
    public interface IConsulApiClient : IDI
    {
        Task<List<ConsulKvEntry>> GetSettingsAsync(string settingName, HttpQuerySetting setting = null);
    }
}