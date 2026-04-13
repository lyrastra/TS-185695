using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.PrimaryDocuments;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.PrimaryDocuments
{
    [InjectAsSingleton(typeof(IContractPrimaryDocumentsApiClient))]
    public class ContractPrimaryDocumentsApiClient : BaseApiClient, IContractPrimaryDocumentsApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public ContractPrimaryDocumentsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
        
        public async Task<List<ProjectLinkedDocumentDto>> GetPrimaryDocumentsByContractIdAsync(int firmId, int userId, int contractId)
        {
            var response = await GetAsync<DataResponseWrapper<ProjectLinkedDocumentsCollectionDto>>(
                    $"/ContractPrimaryDocumentsApi/GetPrimaryDocumentsByContractId?firmId={firmId}&userId={userId}&id={contractId}")
                .ConfigureAwait(false);
            return response.Data?.List ?? new List<ProjectLinkedDocumentDto>();
        }
        
        public async Task<BillsForContractDetailsDto> GetBillsByContractAsync(int firmId, int userId, int contractId)
        {
            var response = await GetAsync<DataResponseWrapper<BillsForContractDetailsDto>>(
                    $"/ContractPrimaryDocumentsApi/GetBillsByContract?firmId={firmId}&userId={userId}&contractId={contractId}")
                .ConfigureAwait(false);
            return response.Data;
        }
    }
}