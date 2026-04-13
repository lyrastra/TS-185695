using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RptV2.Dto.DownloadReport;
using Moedelo.RptV2.Dto.File;

namespace Moedelo.RptV2.Client.DownloadReport
{
    [InjectAsSingleton]
    public class DownloadReportApiClient : BaseApiClient, IDownloadReportApiClient
    {
        private readonly SettingValue apiEndpoint;
        public DownloadReportApiClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("RptApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public async Task<FileResultDto> DownloadPaymentOrder(DownloadPaymentOrderRequestDto dto)
        {
            var result = await PostAsync<DownloadPaymentOrderRequestDto, FileTransferDto>("/DownloadReport/DownloadPaymentOrder", dto).ConfigureAwait(false);
            return result.ToFileResult();
        } 

        public async Task<FileResultDto> DownloadBill(DownloadBillRequestDto dto)
        {
            var result = await PostAsync<DownloadBillRequestDto, FileTransferDto>("/DownloadReport/DownloadBill", dto).ConfigureAwait(false);
            return result.ToFileResult();
        }

        public async Task<FileResultDto[]> DownloadBills(DownloadBillsRequestDto dto)
        {
            if (!dto.Formats.Any())
            {
                return new FileResultDto[0];
            }

            var result = await PostAsync<DownloadBillsRequestDto, FileTransferDto[]>("/DownloadReport/DownloadBills", dto).ConfigureAwait(false);
            return result.Select(r => r.ToFileResult()).ToArray();
        }

        public async Task<FileResultDto> DownloadSzvm(int firmId, int userId, DownloadSzvmRequestDto dto)
        {
            var result =
                await PostAsync<DownloadSzvmRequestDto, FileTransferDto>(
                    $"/DownloadReport/DownloadSzvm?firmId={firmId}&userId={userId}", dto).ConfigureAwait(false);
            return result.ToFileResult();
        }

        public async Task<FileResultDto> DownloadSzvExperience(int firmId, int userId, DownloadSzvExperienceRequestDto dto)
        {
            var result =
                await PostAsync<DownloadSzvExperienceRequestDto, FileTransferDto>(
                        $"/DownloadReport/DownloadSzvExperience?firmId={firmId}&userId={userId}", dto)
                    .ConfigureAwait(false);
            return result.ToFileResult();
        }
    }
}
