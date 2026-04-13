using System;
using System.Threading.Tasks;
using Moedelo.CommonV2.EventBus;
using Moedelo.CommonV2.EventBus.Crm;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SuiteCrm.Dto.SendAsterisk;

namespace Moedelo.SuiteCrm.Client
{
    [InjectAsSingleton]
    public class SuiteCrmAsteriskApiClient : BaseApiClient, ISuiteCrmAsteriskApiClient
    {
        private readonly HttpQuerySetting setting = new HttpQuerySetting {Timeout = new TimeSpan(0, 0, 20, 0)};
        private readonly SettingValue apiEndPoint;
        private readonly IPublisher<LeadsOverSubscriptionCommand> leadsOverSubscriptionPublisher;
        private readonly IPublisher<LeadsReprocessingCommand> leadsReprocessingPublisher;

        public SuiteCrmAsteriskApiClient(IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,  
            ISettingRepository settingRepository,
            IPublisherFactory publisherFactory)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("SuiteCrmApiUrl");
            leadsOverSubscriptionPublisher = publisherFactory.GetForAllClient(EventBusMessages.LeadsOverSubscription);
            leadsReprocessingPublisher = publisherFactory.GetForAllClient(EventBusMessages.LeadsReprocessing);
        }

        public Task<ObjectsForSendToAsteriskDto> ObjectsForSendToAsteriskAsync()
        {
            return GetAsync<ObjectsForSendToAsteriskDto>("/AsteriskPrepare/ObjectsForSendToAsterisk");
        }

        public Task<OperationResultDto> UpdateStatusAsync(UpdateStatusDto dto)
        {
            return PostAsync<UpdateStatusDto, OperationResultDto>($"/AsteriskPrepare/UpdateStatus", dto);
        }

        public Task<ObjectsForLoginOpportunitiesToAsteriskDto> ObjectsForLoginOpportunitiesToAsteriskAsync()
        {
            return GetAsync<ObjectsForLoginOpportunitiesToAsteriskDto>("/AsteriskPrepare/ObjectsForLoginOpportunitiesToAsterisk");
        }

        public Task<ObjectsForProcessingLeadsDto> GetObjectsForProcessingLeadsAsync()
        {
            return GetAsync<ObjectsForProcessingLeadsDto>("/AsteriskPrepare/GetObjectsForProcessingLeads");
        }

        public Task<ObjectWithValues> GetBucketsAsync(string campaignId)
        {
            return GetAsync<ObjectWithValues>("/AsteriskPrepare/GetBuckets", new { campaignId });
        }

        public async Task SendToReprocessingAsync(SendToReprocessingDto dto)
        {
            await leadsReprocessingPublisher.PublishAsync(new LeadsReprocessingCommand
            {
                Email = dto.Email,
                OpportunityName = dto.OpportunityName,
                Logins = dto.Logins,
                BucketId = dto.BucketId,
                FunnelId = dto.FunnelId,
            }).ConfigureAwait(false);
        }

        public async Task SendToOverSubscriptionAsync(SendToOverSubscriptionDto dto)
        {
            await leadsOverSubscriptionPublisher.PublishAsync(new LeadsOverSubscriptionCommand
            {
                OperatorLogin = dto.OperatorLogin,
                Description = dto.Description,
                Logins = dto.Logins,
                BucketId = dto.BucketId,
                FunnelId = dto.FunnelId,
                TaskStatusCompleted = dto.TaskStatusCompleted ?? false,
                Email = dto.EmailForSendResult
            }).ConfigureAwait(false);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}