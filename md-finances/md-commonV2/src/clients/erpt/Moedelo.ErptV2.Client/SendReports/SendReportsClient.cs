using System;
using Moedelo.ErptV2.Dto.NotSendedReports;
using Moedelo.ErptV2.Dto.SendReports;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Threading.Tasks;

namespace Moedelo.ErptV2.Client.SendReports
{
    [InjectAsSingleton]
    public class SendReportsClient : BaseApiClient, ISendReportsClient
    {
        private readonly SettingValue apiEndpoint;

        public SendReportsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager) : base(
            httpRequestExecutor,
            uriCreator,
            responseParser,
            auditTracer,
            auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<GetCodeResponseDto> GetCodeAsync(int firmId, int userId)
        {
            return GetAsync<GetCodeResponseDto>($"/SendReports/GetCode?firmId={firmId}&userId={userId}");
        }

        public Task<bool> CheckCodeAsync(int firmId, int userId, string code)
        {
            return PostAsync<bool>($"/SendReports/CheckCode?firmId={firmId}&userId={userId}&code={code}");
        }

        public Task<SendCodeResponseDto> SendAsync(int firmId, int userId, SendReportsRequestDto dto)
        {
            return PostAsync<SendReportsRequestDto, SendCodeResponseDto>(
                $"/SendReports/Send?firmId={firmId}&userId={userId}", dto, setting: new HttpQuerySetting(TimeSpan.FromMinutes(2)));
        }

        public Task<GetNotSentReportsResponseDto> GetNotSentReportsAsync(GetNotSentReportsRequestDto dto)
        {
            return PostAsync<GetNotSentReportsRequestDto, GetNotSentReportsResponseDto>(
                "/SendReports/GetNotSentReports", dto);
        }
    }
}
