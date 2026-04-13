using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BPM.ApiProxy.Client.Contacts
{
    [InjectAsSingleton]
    public class BpmContactApiProxyClient : BaseApiClient, IBpmContactApiProxyClient
    {
        private readonly SettingValue apiEndpoint;

        public BpmContactApiProxyClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("BPMApiUrl");
        }

        public async Task<List<string>> CheckEmailsAllowedAsync(IEnumerable<string> emails)
        {
            var tasks = emails
                .Select((item, index) => new { item, index })
                .GroupBy(x => x.index / 20)
                .Select(g => g.Select(x => x.item).ToList())
                .Select(e => PostAsync<List<string>, List<string>>("/Rest/Contacts/CheckEmailsAllowed", e));
            var res = await Task.WhenAll(tasks).ConfigureAwait(false);
            var sum = res.Sum(r => r.Count);
            var result = new List<string>(sum);
            foreach (var list in res)
            {
                result.AddRange(list);
            }

            return result;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/apiproxy";
        }
    }
}