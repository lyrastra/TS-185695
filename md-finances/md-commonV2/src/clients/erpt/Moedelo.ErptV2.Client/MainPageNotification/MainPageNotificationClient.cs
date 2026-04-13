using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.ErptV2.Client.MainPageNotification
{
    [InjectAsSingleton]
    public class MainPageNotificationClient : BaseApiClient, IMainPageNotificationClient
    {
        private readonly SettingValue apiEndpoint;

        public MainPageNotificationClient(
           IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(httpRequestExecutor,
                  uriCreator,
                  responseParser,
                  auditTracer,
                  auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }

        public Task CloseOverdueNotificationAsync(int firmId, int userId)
        {
            return PostAsync($"/MainPageNotification/CloseOverdueMainPageNotification?firmId={firmId}&userId={userId}");
        }

        public Task SetRequestRejectedNotificationAsync(int firmId, int userId)
        {
            return PostAsync($"/MainPageNotification/SetRequestRejectedMainPageNotification?firmId={firmId}&userId={userId}");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}
