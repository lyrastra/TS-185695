using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.Docs.Client.DocsStatements
{
    [InjectAsSingleton]
    public class DocsStatementsApiClient : BaseCoreApiClient, IDocsStatementsApiClient
    {
        private const string prefix = "/api/v1";
        private const string fileInfoQueryParam = "asFileInfo";
        private const string useStampAndSignParam = "useStampAndSign";
        
        private readonly ISettingRepository settingRepository;

        public DocsStatementsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("StatementsApiEndpoint").Value;
        }
        
        public async Task<HttpFileModel> GetPdfPrintFileAsync(int firmId, int userId, long baseId, bool useStampAndSign)
        {
            var uri = $"{prefix}/Sales/Download/{baseId}/pdf";
            var query = new Dictionary<string, string>()
            {
                { fileInfoQueryParam, bool.TrueString },
                { useStampAndSignParam, useStampAndSign.ToString() }
            };

            return await GetResponseDataApi(firmId, userId, uri, query).ConfigureAwait(false);
        }

        public Task<HttpFileModel> GetDocPrintFileAsync(int firmId, int userId, long baseId, bool useStampAndSign)
        {
            var uri = $"{prefix}/Sales/Download/{baseId}/doc";
            var query = new Dictionary<string, string>()
            {
                { fileInfoQueryParam, bool.TrueString },
                { useStampAndSignParam, useStampAndSign.ToString() }
            };

            return GetResponseDataApi(firmId, userId, uri, query);
        }

        private async Task<HttpFileModel> GetResponseDataApi(int firmId, int userId, string uri, IDictionary<string, string> query)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var queryString = string.Join("&", query.Select(x => $"{x.Key}={x.Value}"));
            return await GetFileAsync(
                $"{uri}?{queryString}",
                queryHeaders: tokenHeaders).ConfigureAwait(false);
        }
    }
}