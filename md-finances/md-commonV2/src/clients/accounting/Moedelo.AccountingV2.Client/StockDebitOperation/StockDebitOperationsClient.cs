using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.StockDebitOperation;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.StockDebitOperation
{
    [InjectAsSingleton]
    public class StockDebitOperationClient : BaseApiClient, IStockDebitOperationClient
    {
        private readonly SettingValue apiEndPoint;
        
        public StockDebitOperationClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<AccountingPostingsByDocumentDto> GetPostingsAsync(int firmId, int userId, StockOperationDto dto)
        {
            return PostAsync<StockOperationDto, AccountingPostingsByDocumentDto>(
                $"/PostingApi/GetPostingsDebitOperation?firmId={firmId}&userId={userId}", dto);
        }
    }
}
