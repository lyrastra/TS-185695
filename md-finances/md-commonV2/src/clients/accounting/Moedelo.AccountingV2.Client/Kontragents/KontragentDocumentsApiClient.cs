using System;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Client.Kontragents.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Newtonsoft.Json;

namespace Moedelo.AccountingV2.Client.Kontragents
{
    [InjectAsSingleton]
    public class KontragentDocumentsApiClient : BaseApiClient, IKontragentDocumentsApiClient
    {
        private readonly SettingValue apiEndPoint;
        private readonly HttpQuerySetting fileSetting = new HttpQuerySetting(new TimeSpan(0, 0, 1, 0));

        public KontragentDocumentsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<HttpFileModel> DownloadReconcillationStatementAsync(int firmId, int userId, ReconcillationStatementRequestDto dto)
        {
            var url = $"/KontragentDocuments/DownloadReconcillationStatement?firmId={firmId}&userId={userId}&clientData=" + JsonConvert.SerializeObject(dto);
            var file = await GetFileAsync(url, null, setting: fileSetting).ConfigureAwait(false);
            return new HttpFileModel
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                Stream = file.Stream
            };
        }
    }
}