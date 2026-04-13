using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.QRCode.Client
{
    [InjectAsSingleton]
    public class QrCodeApiClient : BaseCoreApiClient, IQrCodeApiClient
    {
        private readonly SettingValue apiEndpoint;

        public QrCodeApiClient(
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
            apiEndpoint = settingRepository.Get("QRCodeApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public async Task<HttpFileModel> GenerateAsync(string codeText, int firmId, int userId, float? sizeMultiplier = null)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var qrCode = await DownloadFileByPostMethodAsHttpFileModelAsync(
                $"/Generate/ToPay?sizeMultiplier={sizeMultiplier}", 
                codeText,
                tokenHeaders).ConfigureAwait(false);

            if (qrCode.Stream == null || qrCode.Stream.Length == 0)
            {
                return null;
            }

            return qrCode;
        }
    }
}