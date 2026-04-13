using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.AccountingStatement;
using Moedelo.AccountingV2.Dto.MoneyStatistics;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.MoneyStatistics
{
    [InjectAsSingleton]
    public class IncomeApiClient: BaseApiClient, IIncomeApiClient
    {
        private readonly SettingValue apiEndPoint;

        public IncomeApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("MoneyStatisticsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
        
        public async Task<List<FirmIncomeDto>> CalculateByFirmIdsAsync(ExecuteByFirmIdsRequestDto requestDto,
            CancellationToken cancellationToken)
        {
            var response = await PostAsync<ExecuteByFirmIdsRequestDto, ApiDataResult<List<FirmIncomeDto>>>(
                "/private/v1/Income/CalculateByFirmIds", 
                requestDto, 
                cancellationToken: cancellationToken);
            
            return response.data;
        }
    }
}