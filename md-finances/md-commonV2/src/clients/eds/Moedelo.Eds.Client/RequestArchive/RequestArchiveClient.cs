using System.Threading.Tasks;
using Moedelo.Eds.Dto.RequestArchive;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Eds.Client.RequestArchive
{
    [InjectAsSingleton]
    public class RequestArchiveClient : BaseCoreApiClient, IRequestArchiveClient
    {
        private readonly SettingValue apiEndpoint;

        public RequestArchiveClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator,
            IResponseParser responseParser, ISettingRepository settingRepository, IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager) : base(httpRequestExecutor, uriCreator,
            responseParser, settingRepository, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("RequestArchivePrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<EdsRequestDto> GetByIdAsync(int id) =>
            GetAsync<EdsRequestDto>($"/api/v1/Requests/{id}/Get");

        public Task<RequestArchiveResponse> GetByCriteriaAsync(RequestCriteria criteria)
            => PostAsync<RequestCriteria, RequestArchiveResponse>("/api/v1/Requests/GetByCriteria", criteria);

        public Task<RequestArchiveResponse> UpdateRowsAsync(UpdateRequestDto updateRequest)
            => PostAsync<UpdateRequestDto, RequestArchiveResponse>("/api/v1/Requests/UpdateRows", updateRequest);

        public Task<byte[]> GetExcelSheetByCriteria(RequestCriteria criteria)
            => PostAsync<RequestCriteria, byte[]>("/api/v1/Reports/GetExcelSheetByCriteria", criteria);
    }
}