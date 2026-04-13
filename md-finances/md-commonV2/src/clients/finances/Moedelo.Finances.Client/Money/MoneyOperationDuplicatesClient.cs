using System.Threading.Tasks;
using Moedelo.Finances.Dto.Money.Duplicates;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Finances.Client.Money
{
    [InjectAsSingleton]
    public class MoneyOperationDuplicatesClient : BaseApiClient, IMoneyOperationDuplicatesClient
    {
        private readonly SettingValue apiEndpoint;

        public MoneyOperationDuplicatesClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("FinancesPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<int?> GetRoboAndSapeIncomingOperationIdAsync(int firmId, int userId, DuplicateRoboAndSapeOperationRequestDto request)
        {
            return PostAsync<DuplicateRoboAndSapeOperationRequestDto, int?>($"/Money/Duplicates/GetRoboAndSapeIncomingOperationId?firmId={firmId}&userId={userId}", request);
        }

        public Task<int?> GetRoboAndSapeOutgoingOperationIdAsync(int firmId, int userId, DuplicateRoboAndSapeOperationRequestDto request)
        {
            return PostAsync<DuplicateRoboAndSapeOperationRequestDto, int?>($"/Money/Duplicates/GetRoboAndSapeOutgoingOperationId?firmId={firmId}&userId={userId}", request);
        }

        public Task<int?> GetYandexIncomingOperationIdAsync(int firmId, int userId, DuplicateYandexOperationRequestDto request)
        {
            return PostAsync<DuplicateYandexOperationRequestDto, int?>($"/Money/Duplicates/GetYandexIncomingOperationId?firmId={firmId}&userId={userId}", request);
        }

        public Task<int?> GetYandexOutgoingOperationIdAsync(int firmId, int userId, DuplicateYandexOperationRequestDto request)
        {
            return PostAsync<DuplicateYandexOperationRequestDto, int?>($"/Money/Duplicates/GetYandexOutgoingOperationId?firmId={firmId}&userId={userId}", request);
        }

        public Task<int?> GetYandexMovementOperationIdAsync(int firmId, int userId, DuplicateMovementYandexOperationRequestDto request)
        {
            return PostAsync<DuplicateMovementYandexOperationRequestDto, int?>($"/Money/Duplicates/GetYandexMovementOperationId?firmId={firmId}&userId={userId}", request);
        }

        public Task<DuplicateResultDto> GetIncomingOperationIdExtAsync(int firmId, int userId, DuplicateIncomingOperationRequestDto request)
        {
            return PostAsync<DuplicateIncomingOperationRequestDto, DuplicateResultDto>($"/Money/Duplicates/GetIncomingOperationIdExt?firmId={firmId}&userId={userId}", request);
        }

        public Task<DuplicateResultDto> GetOutgoingOperationIdExtAsync(int firmId, int userId, DuplicateOutgoingOperationRequestDto request)
        {
            return PostAsync<DuplicateOutgoingOperationRequestDto, DuplicateResultDto>($"/Money/Duplicates/GetOutgoingOperationIdExt?firmId={firmId}&userId={userId}", request);
        }

        public Task<DuplicateResultDto> GetBankFeeOutgoingOperationIdExtAsync(int firmId, int userId, DuplicateBankFeeOutgoingOperationRequestDto request)
        {
            return PostAsync<DuplicateBankFeeOutgoingOperationRequestDto, DuplicateResultDto>($"/Money/Duplicates/GetBankFeeOutgoingOperationIdExt?firmId={firmId}&userId={userId}", request);
        }

        public Task<DuplicateDetectionResultDto[]> DetectAsync(int firmId, int userId, DuplicateDetectionRequestDto request)
        {
            return PostAsync<DuplicateDetectionRequestDto, DuplicateDetectionResultDto[]>($"/Money/Duplicates/Detect?firmId={firmId}&userId={userId}", request);
        }
    }
}