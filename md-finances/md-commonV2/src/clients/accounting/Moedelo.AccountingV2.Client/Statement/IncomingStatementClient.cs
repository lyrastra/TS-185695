using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Statements;
using Moedelo.AccountingV2.Dto.Statements.Purchases;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.Statement
{
    [InjectAsSingleton]
    public class IncomingStatementClient : BaseApiClient, IIncomingStatementClient
    {
        private const string ControllerUri = "/IncomingStatement/";
        private readonly SettingValue apiEndPoint;

        public IncomingStatementClient(
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
            return apiEndPoint.Value + ControllerUri;
        }

        public Task<List<PurchasesStatementDocDto>> GetByBaseIdsAsync(
            int firmId,
            int userId,
            IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<PurchasesStatementDocDto>());
            }
            
            return PostAsync<IEnumerable<long>, List<PurchasesStatementDocDto>>(
                $"/GetByBaseIds?firmId={firmId}&userId={userId}",
                baseIds);
        }

        public Task<List<PurchasesStatementWithItemsDto>> GetByBaseIdsWithItemsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<PurchasesStatementWithItemsDto>());
            }

            var uri = $"/IncomingStatement/GetByBaseIdsWithItems?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<long>, List<PurchasesStatementWithItemsDto>>(uri, baseIds);
        }

        public Task<List<long>> GetStatementBaseIdsAsync(int firmId, int userId, long offset, int count, DateTime? date)
        {
            var uri = $"GetStatementBaseIds?firmId={firmId}&userId={userId}&offset={offset}&count={count}&date={date}";
            return GetAsync<List<long>>(uri);
        }

        public Task<StatementWithItemDto> GetStatementByBaseIdAsync(int firmId, int userId, long baseId)
        {
            var uri = $"GetStatementWithItemsByBaseId?firmId={firmId}&userId={userId}&baseId={baseId}";
            return GetAsync<StatementWithItemDto>(uri);
        }
    }
}
