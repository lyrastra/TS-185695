using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System;
using Moedelo.AccountingV2.Dto.AutoBb;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.AutoBb
{
    [InjectAsSingleton]
    public class AutoBbApiClient : BaseApiClient, IAutoBbApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public AutoBbApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager
                )
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<bool> CheckBalance(int firmId, int userId)
        {
            return GetAsync<bool>("/AutoBalance/CheckBalance", new { firmId, userId });
        }

        public async Task<BalanceFileDto> GetBalanceFileAsync(int firmId, int userId, int year)
        {
            var response = await GetAsync<BalanceFileResponse>("/AutoBalance/GetBalanceFile", new { firmId, userId, year }).ConfigureAwait(false);
            return new BalanceFileDto
            {
                Filename = response.Filename,
                MimeType = response.MimeType,
                Content = Convert.FromBase64String(response.Content)
            };
        }

        public async Task<BalanceFileDto> GetBizBalanceFileAsync(int firmId, int userId, int year)
        {
            var response = await GetAsync<BalanceFileResponse>("/AutoBalance/GetBizBalanceFile", new { firmId, userId, year }).ConfigureAwait(false);
            return new BalanceFileDto
            {
                Filename = response.Filename,
                MimeType = response.MimeType,
                Content = Convert.FromBase64String(response.Content)
            };
        }

        public async Task<BalanceFileDto> GetBizTurnoverFileAsync(int firmId, int userId, int year)
        {
            var response = await GetAsync<BalanceFileResponse>("/AutoBalance/GetBizTurnoverFile", new { firmId, userId, year }).ConfigureAwait(false);
            return new BalanceFileDto
            {
                Filename = response.Filename,
                MimeType = response.MimeType,
                Content = Convert.FromBase64String(response.Content)
            };
        }

        public async Task<BalanceFileDto> GetBizPostingsFileAsync(int firmId, int userId, int year)
        {
            var response = await GetAsync<BalanceFileResponse>("/AutoBalance/GetBizPostingsFile", new { firmId, userId, year }).ConfigureAwait(false);
            return new BalanceFileDto
            {
                Filename = response.Filename,
                MimeType = response.MimeType,
                Content = Convert.FromBase64String(response.Content)
            };
        }
    }
}