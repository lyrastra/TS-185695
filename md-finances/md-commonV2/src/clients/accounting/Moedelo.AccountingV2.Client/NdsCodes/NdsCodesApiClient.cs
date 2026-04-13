using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.NdsCodes;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.NdsCodes
{
    [InjectAsSingleton]
    public class NdsCodesApiClient : BaseApiClient, INdsCodesApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public NdsCodesApiClient(
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
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<List<NdsCodeDto>> GetByDateAsync(int firmId, int userId, DateTime date)
        {
            var result = await GetAsync<DataResponseWrapper<List<NdsCodeDto>>>("/NdsCodes/GetByDate", new { firmId, userId, date }).ConfigureAwait(false);
            return result.Data;
        }
    }
}
