using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Hosting;
using Moedelo.Common.Enums.Enums.HistoricalLogs.Backoffice;
using Moedelo.HistoricalLogsV2.Dto.BackofficeLog;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HistoricalLogsV2.Client.BackofficeLog
{
    [InjectAsSingleton]
    public class BackofficeLogClient : BaseApiClient, IBackofficeLogClient
    {
        private readonly SettingValue endpointSetting;

        public BackofficeLogClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            endpointSetting = settingRepository.Get("HistoricalLogsApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return endpointSetting.Value;
        }

        public void Log(IReadOnlyCollection<BackofficeLogRequestDto> records)
        {
            if (!records.Any())
            {
                return;
            }

            HostingEnvironment.QueueBackgroundWorkItem( async t =>
            {
               await PostAsync("/BackofficeLog/List", records).ConfigureAwait(false);
            });
        }

        public Task LogAsync(IReadOnlyCollection<BackofficeLogRequestDto> records)
        {
            if (records?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync("/BackofficeLog/List", records);
        }

        public void LogSuccess(int userId, BackofficeLogActionType actionType, int? objectId = null, BackofficeLogActionDataDto actionData = null)
        {
            HostingEnvironment.QueueBackgroundWorkItem( async t =>
            {
                var dto = new BackofficeLogRequestDto
                {
                    UserId = userId,
                    ObjectId = objectId,
                    ActionType = actionType,
                    IsSuccess = true,
                    ActionData = actionData
                };

                await PostAsync("/BackofficeLog/Log", dto).ConfigureAwait(false);
            });
        }

        public Task LogSuccessAsync(int userId, BackofficeLogActionType actionType, int objectId,
            BackofficeLogActionDataDto actionData)
        {
            var dto = new BackofficeLogRequestDto
            {
                UserId = userId,
                ObjectId = objectId,
                ActionType = actionType,
                IsSuccess = true,
                ActionData = actionData
            };

            return PostAsync("/BackofficeLog/Log", dto);
        }

        public void LogError(int userId, BackofficeLogActionType actionType, int? objectId = null, BackofficeLogActionDataDto actionData = null)
        {
            HostingEnvironment.QueueBackgroundWorkItem(async t =>
            {
                var dto = new BackofficeLogRequestDto
                {
                    UserId = userId,
                    ObjectId = objectId,
                    ActionType = actionType,
                    IsSuccess = false,
                    ActionData = actionData
                };

                await PostAsync("/BackofficeLog/Log", dto).ConfigureAwait(false);
            });
        }
    }
}