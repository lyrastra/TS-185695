using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SpamV2.Dto.MailSender.EmailTable;

namespace Moedelo.SpamV2.Client.MailSender
{
    [InjectAsSingleton]
    public class EmailsClient : BaseApiClient, IEmailsClient
    {
        private readonly SettingValue apiEndPoint;

        public EmailsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("mailServiceUrl");
        }

        public Task<FirmEmailDto> GetByIdAsync(int id)
        {
            return GetAsync<FirmEmailDto>($"/EmailTable/{id}");
        }

        public async Task<int> SaveAsync(FirmEmailDto request)
        {
            var response = await PostAsync<FirmEmailDto, SavedFirmEmailDto>("/EmailTable", request).ConfigureAwait(false);
            return response.Id;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/V2";
        }
    }
}
