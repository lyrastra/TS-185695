using System.Threading.Tasks;
using Moedelo.BankIntegrations.Dto;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.BssBanks
{
    public interface IBankAdapterClient
    {
        Task<ApiDataResult<T>> GetClientInfoAsync<T>(string code, string redirectUri, string dboServerUri, HttpQuerySetting setting = null);
        Task<ApiDataResult<int>> SaveIntegrationDataAsync<T>(T data, HttpQuerySetting setting = null) where T : class;
    }
}
