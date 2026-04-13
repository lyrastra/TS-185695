using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.RptV2.Dto;
using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.RptV2.Client.AnalyticsApi
{
    [InjectAsSingleton]
    public class AnalyticsApiClient :BaseApiClient,  IAnalyticsApiClient
    {
        private readonly SettingValue apiEndpoint;

        public AnalyticsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("RptApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public async Task<FileDto> GetTurnoverFileAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            var result = await GetAsync<FileResponseDto>("/AnalyticsApi/GetTurnoverFile", new { firmId, userId, startDate, endDate }).ConfigureAwait(false);
            return new FileDto
            {
                Filename = result.Filename,
                MimeType = result.MimeType,
                Content = Convert.FromBase64String(result.Content)
            };
        }

        public async Task<FileDto> GetPostingsFileAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            var result = await GetAsync<FileResponseDto>("/AnalyticsApi/GetPostingsFile", new { firmId, userId, startDate, endDate }).ConfigureAwait(false);
            return new FileDto
            {
                Filename = result.Filename,
                MimeType = result.MimeType,
                Content = Convert.FromBase64String(result.Content)
            };
        }
    }
}