using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto.Patent;

namespace Moedelo.RequisitesV2.Client.Patent
{
    [InjectAsSingleton(typeof(IPatentNotifyClient))]
    public class PatentNotifyClient : BaseApiClient, IPatentNotifyClient
    {
        private readonly SettingValue apiEndPoint;

        public PatentNotifyClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<List<PatentDto>> GetPatentsForProlongationEventAsync(int firmId, int userId, DateTime endDate)
        {
            var result = await GetAsync<ListWrapper<PatentDto>>(
                "/Patent/Notify/GetPatentsForProlongationEvent",
                new
                {
                    firmId = firmId,
                    userId = userId,
                    endDate = endDate
                }).ConfigureAwait(false);

            return result.Items;
        }
    }
}
