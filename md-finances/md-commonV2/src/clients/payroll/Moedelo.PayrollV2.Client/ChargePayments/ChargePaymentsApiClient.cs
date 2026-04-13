using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PayrollV2.Client.ChargePayments.DTO;
using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.Common.Enums.Enums.Payroll.Deductions;

namespace Moedelo.PayrollV2.Client.Payments
{
    [InjectAsSingleton]
    public class ChargePaymentsApiClient : BaseApiClient, IChargePaymentsApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public ChargePaymentsApiClient(
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
            return apiEndPoint.Value + "/ChargePayments";
        }

        public Task<WorkerChargePaymentsListDto> GetByDocumentBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            return GetAsync<WorkerChargePaymentsListDto>($"/Get/{documentBaseId}", new { firmId, userId });
        }

        public Task<WorkerChargePaymentsListDto[]> GetByDocumentBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            return PostAsync<IReadOnlyCollection<long>, WorkerChargePaymentsListDto[]>($"/GetByDocumentBaseIds?firmId={firmId}&userId={userId}", documentBaseIds);
        }

        public Task<ApplyDeductionStatus> GetApplyDeductionStatusAsync(int firmId, int userId, long documentBaseId, DateTime documentDate)
        {
            return GetAsync<ApplyDeductionStatus>($"/GetApplyDeductionStatus", new { firmId, userId, documentBaseId, documentDate });
        }
    }
}