using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BPM.ApiProxy.Client.CaseInfo
{
    public interface IBpmCaseInfoApiProxyClient : IDI
    {
        Task<int> GetOpenedCountAsync(int firmId);
        Task<int> GetOpenedCountAsync(string login);
        Task<List<CaseInfoDto>> GetOpenedInfoAsync(int firmId, int count = 20,
            int? afterNumber = null, bool orderAsc = false);
        Task<List<CaseInfoDto>> GetOpenedInfoAsync(string login, int count = 20,
            int? afterNumber = null, bool orderAsc = false);
        
        Task<List<CaseInfoDto>> CasesFeedStartAsync(int firmId);
        Task<List<CaseInfoDto>> CasesFeedAsync(int firmId, int caseNumber, DateTime date);

        /// <summary>
        ///     Получение документов обращений
        /// </summary>
        /// <param name="caseIds">идентификаторы обращений</param>
        /// <returns>документы обращений</returns>
        Task<CaseDocumentsDto[]> GetDocumentsAsync(string[] caseIds);

        Task<List<CaseMessageDto>> CaseMessagesAsync(string caseId, int count = 20, 
            DateTime? date = null, string lastId = null, bool orderAsc = false, int? firmId = null);

        Task<CaseMessageDto> GetCaseMessageInfoAsync(string caseMessageId, int? firmId = null);
        Task<CaseInfoDto> GetCaseInfoAsync(string caseId, int? firmId = null);
    }
}