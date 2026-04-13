using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.TaxReductionTip;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.AccountingV2.Client.TaxReductionTip
{
    [InjectAsSingleton]
    public class TaxReductionTipClient : BaseApiClient, ITaxReductionTipClient
    {
        private readonly SettingValue apiEndPoint;

        public TaxReductionTipClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<TaxReductionTipItemDto>> GetAsync(int firmId, int userId)
        {
            // Установлен таймаут ожидания ответа от апишки в 3 минуты. Т.к. дефолтных 30 секунд не хватает.
            return GetAsync<List<TaxReductionTipItemDto>>(
                "/TaxReductionTip/Get", 
                new { firmId, userId }, 
                setting: new HttpQuerySetting(TimeSpan.FromSeconds(180)));
        }
    }
}