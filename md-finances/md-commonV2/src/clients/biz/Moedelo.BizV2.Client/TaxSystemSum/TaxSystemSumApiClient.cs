using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BizV2.Client.TaxSystemSum;
using Moedelo.BizV2.Dto.TaxSystemSum;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BizV2.Client.Income
{
    [InjectAsSingleton(typeof(ITaxSystemSumApiClient))]
    public class TaxSystemSumApiClient : BaseApiClient, ITaxSystemSumApiClient
    {
        private readonly SettingValue apiEndPoint;

        public TaxSystemSumApiClient(
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
            apiEndPoint = settingRepository.Get("BizPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<TaxSystemSumDto>> GetByPeriodAsync(int firmId, int userId, int year, int quarter)
        {
            return GetAsync<List<TaxSystemSumDto>>($"/TaxSystemSum/ByPeriod?firmId={firmId}&userId={userId}&year={year}&quarter={quarter}");
        }
    }
}
