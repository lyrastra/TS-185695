using Moedelo.AccountingV2.Dto.Statements;
using Moedelo.AccountingV2.Dto.Statements.Sales;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.AccountingV2.Client.Statement
{
    [InjectAsSingleton]
    public class OutgoingStatementClient : BaseApiClient, IOutgoingStatementClient
    {
        private readonly SettingValue apiEndPoint;

        public OutgoingStatementClient(
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

        public Task<List<SalesStatementDocDto>> GetByBaseIdsAsync(
            int firmId,
            int userId,
            IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<SalesStatementDocDto>());
            }
            
            return PostAsync<IEnumerable<long>, List<SalesStatementDocDto>>(
                $"/OutgoingStatement/GetByBaseIds?firmId={firmId}&userId={userId}",
                baseIds);
        }

        public Task<List<SalesStatementWithItemsDto>> GetByBaseIdsWithItemsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<SalesStatementWithItemsDto>());
            }

            var uri = $"/OutgoingStatement/GetByBaseIdsWithItems?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<long>, List<SalesStatementWithItemsDto>>(uri, baseIds);
        }

        public Task<List<long>> GetStatementBaseIdsAsync(int firmId, int userId, long offset,  int count, DateTime? date)
        {
            var uri = $"/OutgoingStatement/GetStatementBaseIds?firmId={firmId}&userId={userId}&offset={offset}&count={count}&date={date}";
            return GetAsync<List<long>>(uri);
        }

        public Task<StatementWithItemDto> GetStatementByBaseIdAsync(int firmId, int userId, long baseId)
        {
            var uri = $"/OutgoingStatement/GetStatementWithItemsByBaseId?firmId={firmId}&userId={userId}&baseId={baseId}";
            return GetAsync<StatementWithItemDto>(uri);
        }
    }
}
