using Moedelo.BankIntegrationsV2.Dto.SsoLogs;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrationsV2.Client.SsoLogs
{
    public interface ISsoLogClient : IDI
    {
        /// <summary>
        /// Сохранить Sso лог 
        /// </summary>
        Task SaveAsync(SsoLogSaveRequestDto dto);

        /// <summary>
        /// Получить список логов по идентификатору клиента
        /// </summary> 
        Task<List<SsoLogRequestDto>> GetLogRequestsAsync(string clientId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Получить лог по id запроса
        /// </summary>   
        Task<SsoLogFileDto> GetLogAsync(int id);
    }
}
