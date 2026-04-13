using Moedelo.Chat.Client.Base;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Chat.SuiteCrmCaseEvents.Client
{
    [InjectAsSingleton]
    public class SuiteCrmCaseEventsClient: ChatPlatformBasePrivateApiClient, ISuiteCrmCaseEventsClient
    {
        public SuiteCrmCaseEventsClient
            (
                IHttpRequestExecutor httpRequestExecutor,
                IUriCreator uriCreator,
                IResponseParser responseParser,
                ISettingRepository settingRepository,
                IAuditTracer auditTracer,
                IAuditScopeManager auditScopeManager
            ) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager, settingRepository)
        {

        }

        public Task<bool> AddAllMessagesToCaseAsync(string caseId, Guid requestId)
        {
            return PostAsync<bool>($"Rest/Cases/{caseId}/caseUpdates/{requestId}");
        }

        public Task<bool> AddMessagesToCaseAsync(string caseId, IReadOnlyCollection<Guid> messagesIds)
        {
            return PostAsync<IReadOnlyCollection<Guid>, bool>($"Rest/Cases/{caseId}/caseUpdates", messagesIds);
        }

        public Task<bool> AddMessageAttachmentToCaseAsync(string caseId, Guid messageAttachmentId)
        {
            return PostAsync<bool>($"Rest/Cases/{caseId}/documents/{messageAttachmentId}");
        }
    }
}
