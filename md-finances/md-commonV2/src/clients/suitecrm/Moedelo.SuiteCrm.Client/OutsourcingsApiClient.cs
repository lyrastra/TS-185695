using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SuiteCrm.Dto;
using Moedelo.SuiteCrm.Dto.Bpm;

namespace Moedelo.SuiteCrm.Client
{
    [InjectAsSingleton]
    public class OutsourcingsApiClient : BaseApiClient, IOutsourcingsApiClient
    {
        private readonly SettingValue apiEndPoint;

        public OutsourcingsApiClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) :
            base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("OutsourceClientApiEndpoint");
        }

        public async Task<OutsourcingDto[]> GetListByClientIdsAsync(IReadOnlyCollection<int> clientIds)
        {
            var uri = $"/v1/outsourcings/GetByIds";

            if (clientIds == null || !clientIds.Any())
            {
                return Array.Empty<OutsourcingDto>();
            }

            var result = await PostAsync<IReadOnlyCollection<int>, DataResponse<OutsourcingDto[]>>(
                uri,
                new HashSet<int>(clientIds))
                .ConfigureAwait(false);

            return result?.Data;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}
