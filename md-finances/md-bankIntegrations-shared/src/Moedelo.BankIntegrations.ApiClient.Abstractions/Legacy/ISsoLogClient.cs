using Moedelo.BankIntegrations.ApiClient.Dto.SsoLogs;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.Legacy
{
    public interface ISsoLogClient
    {
        /// <summary> Сохранить Sso лог </summary>
        Task SaveAsync(SsoLogSaveRequestDto dto, HttpQuerySetting setting = null);
    }
}
