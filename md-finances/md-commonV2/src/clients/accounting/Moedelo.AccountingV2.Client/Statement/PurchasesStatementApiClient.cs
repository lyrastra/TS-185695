using Moedelo.AccountingV2.Dto.Statements.Purchases;
using Moedelo.CommonV2.Extensions.System;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.Statement
{
    [InjectAsSingleton]
    public class PurchasesStatementApiClient : BaseApiClient, IPurchasesStatementApiClient
    {
        private readonly SettingValue apiEndPoint;

        public PurchasesStatementApiClient(
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

        /// <inheritdoc />
        public Task<PurchasesStatementCollectionDto> GetAsync(
            int firmId, 
            int userId, 
            uint pageNo = 1,
            uint pageSize = 50,
            string number = null,
            DateTime? docAfterDate = null,
            DateTime? docBeforeDate = null,
            DateTime? afterDate = null,
            DateTime? beforeDate = null,
            int? kontragentId= null)
        {
            var url = new StringBuilder($"/api/v1/purchases/act?firmId={firmId}&userId={userId}");
            url.Append($"&pageNo={pageNo}");
            url.Append($"&pageSize={pageSize}");
            url.Append($"&number={number}");
            url.Append($"&docAfterDate={docAfterDate?.ToString("yyyy-MM-dd")}");
            url.Append($"&docBeforeDate={docBeforeDate?.ToString("yyyy-MM-dd")}");
            url.Append($"&afterDate={afterDate?.ToStrictString()}");
            url.Append($"&beforeDate={beforeDate?.ToStrictString()}");
            url.Append($"&kontragentId={kontragentId}");
            
            return GetAsync<PurchasesStatementCollectionDto>(url.ToString());
        }

        /// <inheritdoc />
        public Task<PurchasesStatementDto> GetByBaseIdAsync(int firmId, int userId, long baseId)
        {
            return GetAsync<PurchasesStatementDto>($"/api/v1/purchases/act/{baseId}?firmId={firmId}&userId={userId}");
        }

        /// <inheritdoc />
        public Task<PurchasesStatementDto> SaveAsync(int firmId, int userId, PurchasesStatementSaveRequestDto dto)
        {
            if (dto.Id != 0)
            {
                throw new NotImplementedException("Saving of existent statement is not implemented. Waiting for PutAsync");
            }

            return PostAsync<PurchasesStatementSaveRequestDto, PurchasesStatementDto>($"/api/v1/purchases/act?firmId={firmId}&userId={userId}", dto);
        }
    }
}