using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.ClientInfo;
using Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.Request;
using Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.Response;
using Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.StatementSummary;
using System.Threading.Tasks;

namespace Moedelo.SberbankCryptoEndpointV2.Client.Upg2
{
    [InjectAsSingleton]
    public class Upg2Client : BaseApiClient, IUpg2Client
    {
        private readonly SettingValue apiEndpoint;

        public Upg2Client(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("SberbankCryptoEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/Upg2V2/";
        }

        public Task<RequestMovementListResponseDto> RequestMovementListAsync(RequestMovementListRequestDto dto, HttpQuerySetting setting)
        {
            return PostAsync<RequestMovementListRequestDto, RequestMovementListResponseDto>("RequestMovementList", dto, setting: setting);
        }

        public Task<GetSberbankSettlementsToSsoResponseDto> GetSberbankSettlementsToSsoAsync(GetSberbankSettlementsToSsoRequestDto dto)
        {
            return PostAsync<GetSberbankSettlementsToSsoRequestDto, GetSberbankSettlementsToSsoResponseDto>("GetSberbankSettlementsToSso", dto);
        }

        public Task<GetSberbankPaymentsStatusResponseDto> GetSberbankPaymentsStatusAsync(GetSberbankPaymentsStatusRequestDto dto)
        {
            return PostAsync<GetSberbankPaymentsStatusRequestDto, GetSberbankPaymentsStatusResponseDto>("GetSberbankPaymentsStatus", dto);
        }

        public Task<GetStatementSummaryResponseDto> GetStatementSummaryAsync(GetStatementSummaryRequestDto dto)
        {
            return PostAsync<GetStatementSummaryRequestDto, GetStatementSummaryResponseDto>("GetStatementSummary", dto);
        }

        public Task<ClientInfoResponseDto> GetClientInfoAsync(int firmId)
        {
            return GetAsync<ClientInfoResponseDto>($"GetClientInfo?firmId={firmId}");
        }
    }
}