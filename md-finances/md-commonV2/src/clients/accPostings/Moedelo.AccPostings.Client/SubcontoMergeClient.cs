using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccPostings.Dto;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccPostings.Client
{
    [InjectAsSingleton]
    public class SubcontoMergeClient : BaseApiClient, ISubcontoMergeClient
    {
        private readonly SettingValue apiEndPoint;

        public SubcontoMergeClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository repository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = repository.Get("AccPostingsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task ReplaceRelationsAsync(int firmId, int userId, SubcontoMergeDto request)
        {
            return PostAsync($"/Subconto/Merge?firmId={firmId}&userId={userId}", request);
        }
    }
}
