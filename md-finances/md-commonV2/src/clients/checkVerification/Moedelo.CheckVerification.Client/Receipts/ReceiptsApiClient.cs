using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CheckVerification.Client.Receipts.Models;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.CheckVerification.Client.Receipts
{
    [InjectAsSingleton]
    public class ReceiptsApiClient : BaseApiClient, IReceiptsApiClient
    {
        private readonly SettingValue apiEndPoint;

        public ReceiptsApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : 
            base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("CheckVerificationApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<ApiDataDto<ReceiptDto>> GetAsync(ReceiptRequestDto model)
        {
            return GetAsync<ApiDataDto<ReceiptDto>>($"/receipts", model);
        }
    }
}