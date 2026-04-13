using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.BizToAccMoneySource;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.BizToAccMoneySource
{
    [InjectAsSingleton(typeof(IBizToAccMoneySourceApiClient))]
    public class BizToAccMoneySourceApiClient : BaseApiClient, IBizToAccMoneySourceApiClient
    {
        private readonly SettingValue apiEndPoint;

        public BizToAccMoneySourceApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingsRepository)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            apiEndPoint = settingsRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<IReadOnlyCollection<BizPaymentDocumentDto>> GetMoneySourceDataAsync(int firmId, int userId, DateTime onDate)
        {
            var settings = new HttpQuerySetting(TimeSpan.FromMinutes(1));
            return GetAsync<IReadOnlyCollection<BizPaymentDocumentDto>>("/BizToAccMoneySource/Get", new { firmId, userId, onDate }, null, settings);
        }
    }
}
