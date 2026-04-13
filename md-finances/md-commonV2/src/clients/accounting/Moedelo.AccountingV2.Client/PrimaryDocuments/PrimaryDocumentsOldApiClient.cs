using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.PaymentDocuments;
using Moedelo.AccountingV2.Dto.PrimaryDocuments;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.PrimaryDocuments
{
    [InjectAsSingleton]
    public class PrimaryDocumentsOldApiClient : BaseApiClient, IPrimaryDocumentsOldApiClient
    {
        private readonly SettingValue apiEndPoint;

        public PrimaryDocumentsOldApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("PrimaryDocsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<DocumentsSumsDto> GetDocumentSumsAsync(int firmId, int userId, int kontragentId, PrimaryDocumentsTransferDirection direction)
        {
            var response = await GetAsync<DataResponseWrapper<DocumentsSumsDto>>(
                $"/Document/GetDocumentSums?firmId={firmId}&userId={userId}&kontragentId={kontragentId}&direction={(int)direction}")
                .ConfigureAwait(false);
            return response.Data;
        }

        public async Task<List<KontragentDocumentsSumsAndDirectionDto>> GetOverallDocumentSumsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds)
        {
            var response = await PostAsync<IReadOnlyCollection<int>, ListResponseWrapper<KontragentDocumentsSumsAndDirectionDto>>(
                $"/Document/GetOverallDocumentSums?firmId={firmId}&userId={userId}", kontragentIds).ConfigureAwait(false);
            return response.Items;
        }

        public async Task<List<DocumentsCountByKontragentDto>> GetDocumentCountsByKontragentsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds)
        {
            var response = await PostAsync<IReadOnlyCollection<int>, ListResponseWrapper<DocumentsCountByKontragentDto>>(
                $"/Document/GetDocumentCountsByKontragents?firmId={firmId}&userId={userId}", kontragentIds).ConfigureAwait(false);
            return response.Items;
        }

        public Task<FakeDocumentsDto> GetFakeDocumentsCountAsync(int firmId, int userId)
        {
            return GetAsync<FakeDocumentsDto>($"/Document/GetFakeDocumentsCount?firmId={firmId}&userId={userId}");
        }

        public async Task<List<FakeDocumentDto>> GetFakeDocumentsAsync(int firmId, int userId, AccountingDocumentType documentType)
        {
            var response = await GetAsync<ListResponseWrapper<FakeDocumentDto>>(
                $"/Document/GetFakeDocuments?firmId={firmId}&userId={userId}&documentType={documentType}").ConfigureAwait(false);

            return response.Items;
        }

        public Task<BillResponseDto> ChangeStatusByIdAndSumAsync(int firmId, int userId, BillChangeStatusQueryParamsDto dto)
        {
            return PostAsync<BillChangeStatusQueryParamsDto, BillResponseDto>($"/Bill/ChangeStatusByIdAndSum?firmId={firmId}&userId={userId}", dto);
        }

        public async Task<List<BillShortInfoDto>> GetByNamesAsync(int firmId, int userId, IList<string> names)
        {
            return (await PostAsync<IList<string>, DataResponseWrapper<List<BillShortInfoDto>>>($"/Bill/GetByNames?firmId={firmId}&userId={userId}", names).ConfigureAwait(false)).Data;
        }
        
        public Task<ListDto<PaymentDocumentLinkDto>> GetPaymentDocumentLinksAsync(int firmId, int userId, PaymentDocumentLinkRequestDto requestDto)
        {
            return PostAsync<PaymentDocumentLinkRequestDto, ListDto<PaymentDocumentLinkDto>>(
                $"/Document/GetPaymentDocumentLinks?firmId={firmId}&userId={userId}",
                requestDto);
        }

        public Task UpdateConfirmingDocumentsAsync(int firmId, int userId, ConfirmingDocumentsDto dto)
        {
            return PostAsync($"/Document/UpdateConfirmingDocuments?firmId={firmId}&userId={userId}", dto);
        }

        public Task UpdateConfirmingLinkedDocumentsAsync(int firmId, int userId, ConfirmingLinkedDocumentsDto dto)
        {
            return PostAsync($"/Document/UpdateConfirmingLinkedDocuments?firmId={firmId}&userId={userId}", dto);
        }

        public Task DeleteConfirmingDocumentsAsync(int firmId, int userId, int primaryDocumentId,
            AccountingDocumentType documentType)
        {
            return GetAsync($"/Document/DeleteConfirmingDocuments?firmId={firmId}&userId={userId}&primaryDocumentId={primaryDocumentId}&primaryDocumentType={documentType}");
        }
    }
}
