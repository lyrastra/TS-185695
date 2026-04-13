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
    [InjectAsSingleton]
    public class PatentEventAttributeV2Client : BaseApiClient, IPatentEventAttributeV2Client
    {
        private readonly SettingValue apiEndPoint;

        public PatentEventAttributeV2Client(
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

        public Task SaveAsync(int firmId, int userId, PatentEventAttributeV2Dto attribute)
        {
            return PostAsync<PatentEventAttributeV2Dto, object>(
                $"/PatentEventAttribute/Save?firmId={firmId}&userId={userId}", 
                attribute);
        }

        public async Task<List<PatentEventAttributeV2Dto>> GetByPatentIdAsync(int firmId, int userId, long patentId)
        {
            var result = await GetAsync<ListWrapper<PatentEventAttributeV2Dto>>(
                "/PatentEventAttribute/GetByPatentId",
                new
                {
                    firmId = firmId,
                    userId = userId,
                    patentId = patentId
                }).ConfigureAwait(false);

            return result.Items;
        }

        public Task<PatentEventAttributeV2Dto> GetByWizardIdAsync(int firmId, int userId, long wizardId)
        {
            return GetAsync<PatentEventAttributeV2Dto>(
                "/PatentEventAttribute/GetByWizardId",
                new
                {
                    firmId = firmId,
                    userId = userId,
                    wizardId = wizardId
                });
        }

        public async Task<List<PatentEventAttributeV2Dto>> GetAllEventsByWizardIdsAsync(int firmId, int userId,
            IReadOnlyCollection<long> wizardIds)
        {
            var result = await PostAsync<IReadOnlyCollection<long>, ListWrapper<PatentEventAttributeV2Dto>>(
                $"/PatentEventAttribute/GetAllEventsByWizardIds?firmId={firmId}&userId={userId}",
                wizardIds).ConfigureAwait(false);

            return result.Items;
        }

        public Task<PatentEventAttributeV2Dto> GetByEventIdAsync(int firmId, int userId, int eventId)
        {
            return GetAsync<PatentEventAttributeV2Dto>(
                "/PatentEventAttribute/GetByEventId",
                new
                {
                    firmId = firmId,
                    userId = userId,
                    eventId = eventId
                });
        }

        public Task AssignWizardForPatentEventAsync(int firmId, int userId, long attributeId, long wizardId)
        {
            return PostAsync(
                $"/PatentEventAttribute/AssignWizardForPatentEvent?firmId={firmId}&userId={userId}&attributeId={attributeId}&wizardId={wizardId}");
        }
    }
}