using System;
using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.BankOperation;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrationsV2.Client.BankOperation
{
    [InjectAsSingleton]
    public class BankOperationClientNetCore : BaseApiClient, IBankOperationClientNetCore
    {
        private const string ControllerName = "/private/api/v1/BankOperation/";
        private readonly SettingValue apiEndPoint;

        public BankOperationClientNetCore(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("IntegrationApiNetCore");
        }

        public async Task<RequestMovementListResponseNetCoreDto> RequestMovementListAsync(RequestMovementListRequestDto dto)
        {
            var response = await PostAsync<RequestMovementListRequestDto, DataResponseWrapper<RequestMovementListResponseNetCoreDto>>("RequestMovements", dto).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<bool> SendDailyRequestAsync(DateTime beginDate, DateTime finishDate, int integrationPartner, bool withoutCheckCreated = false)
        {
            var response = await GetAsync<bool>("SendDailyRequest",
                new { beginDate = beginDate.ToString("yyyy-MM-dd"),
                    finishDate = finishDate.ToString("yyyy-MM-dd"),
                    integrationPartner,
                    withoutCheckCreated })
                .ConfigureAwait(false);
            return response;
        }

        public async Task<bool> SendDailyLagRequestAsync(DateTime beginDate, DateTime finishDate, int integrationPartner)
        {
            var response = await GetAsync<bool>("SendDailyLagRequest",
                new
                {
                    beginDate = beginDate.ToString("yyyy-MM-dd"),
                    finishDate = finishDate.ToString("yyyy-MM-dd"),
                    integrationPartner
                })
                .ConfigureAwait(false);
            return response;
        }

        public async Task<CreateSalaryPaymentRegistryResponseDto> CreateSalaryPaymentRegistryAsync(SalaryPaymentRegistryDto dto)
        {
            HttpQuerySetting setting = new HttpQuerySetting { Timeout = new TimeSpan(0, 0, 2, 30) };
            var response = await PostAsync<SalaryPaymentRegistryDto, DataResponseWrapper<CreateSalaryPaymentRegistryResponseDto>>("CreatePaymentRegistry", dto, null, setting).ConfigureAwait(false);
            return response.Data;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerName;
        }
    }
}
