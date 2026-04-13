using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PayrollV2.Dto.Transfer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.PayrollV2.Client.Transfer
{
    [InjectAsSingleton]
    public class TransferApiClient : BaseApiClient, ITransferApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public TransferApiClient(
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
            apiEndPoint = settingRepository.Get("PayrollPrivateApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Transfer";
        }

        public async Task<Dictionary<int, TransferWorkerDto>> TransferWorkersAsync(TransferWorkersRequestDto request, TimeSpan? timeout = null)
        {
            var result = await PostAsync<TransferWorkersRequestDto, Dictionary<int, TransferWorkerDto>>($"/Workers", request,
                setting: new HttpQuerySetting(timeout))
                .ConfigureAwait(false);
            return result;
        }

        public Task TransferSalarySettingsAsync(int bizUserId, int bizFirmId, int accFirmId)
        {
            return PostAsync($"/SalarySettings?bizUserId={bizUserId}&bizFirmId={bizFirmId}&accFirmId={accFirmId}");
        }

        public Task TransferFiredPaymentsAsync(int bizFirmId, int bizUserId, int accFirmId, int accUserId, HttpQuerySetting setting = null) {
            return PostAsync($"/FiredPayments?bizFirmId={bizFirmId}&bizUserId={bizUserId}&accFirmId={accFirmId}&accUserId={accUserId}", setting: setting);
        }
    }
}