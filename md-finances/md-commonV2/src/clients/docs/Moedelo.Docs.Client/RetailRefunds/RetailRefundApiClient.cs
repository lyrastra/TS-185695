using Moedelo.Docs.Dto.RetailRefunds;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Docs.Client.RetailRefunds
{
    [InjectAsSingleton]
    public class RetailRefundApiClient : BaseApiClient, IRetailRefundApiClient
    {
        private readonly SettingValue apiEndpoint;

        public RetailRefundApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor,
                uriCreator,
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("DocsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<long> SaveAsync(int firmId, int userId, RetailRefundDto dto)
        {
            return PostAsync<RetailRefundDto, long>(
                $"/RetailRefund?firmId={firmId}&userId={userId}",
                dto);
        }

        public Task<RetailRefundDto> GetByBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            return GetAsync<RetailRefundDto>(
                "/RetailRefund",
                new { firmId, userId, documentBaseId });
        }

        public Task<RetailRefundPageDto> GetPageAsync(int firmId, int userId, RetailRefundPaginationRequestDto request)
        {
            return GetAsync<RetailRefundPageDto>(
                "/RetailRefund/Page",
                new
                {
                    firmId,
                    userId,
                    request.AfterDate,
                    request.BeforeDate,
                    request.Offset,
                    request.PageSize
                });
        }

        public Task<List<RetailRefundDto>> GetByPeriodAsync(int firmId, int userId, DateTime startDate, DateTime endDate,
            long? stockId = null, CancellationToken cancellationToken = default)
        {
            return GetAsync<List<RetailRefundDto>>(
                "/RetailRefund/ByPeriod",
                new
                {
                    firmId,
                    userId,
                    startDate,
                    endDate,
                    stockId
                }, cancellationToken: cancellationToken);
        }

        public Task<List<RetailRefundDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<RetailRefundDto>());
            }

            return PostAsync<IReadOnlyCollection<long>, List<RetailRefundDto>>(
                $"/RetailRefund/ByBaseIds?firmId={firmId}&userId={userId}",
                baseIds);
        }

        public Task DeleteAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds == null || !baseIds.Any())
            {
                return Task.CompletedTask;
            }

            return PostAsync(
                $"/RetailRefund/Delete/?firmId={firmId}&userId={userId}",
                baseIds);
        }

        public Task DeleteAsync(int firmId, int userId, long retailRefundBaseId)
        {
            return DeleteAsync(firmId, userId, new[] { retailRefundBaseId });
        }

        public Task DeleteWithCashOrdersAsync(int firmId, int userId, long baseId)
        {
            return DeleteAsync($"/RetailRefund/DeleteWithCashOrders/?firmId={firmId}&userId={userId}&baseId={baseId}");
        }

        public Task<string> GetNextNumberAsync(int firmId, int userId, int? year)
        {
            return GetAsync<string>(
                "/RetailRefund/NextNumber",
                new
                {
                    firmId,
                    userId,
                    year
                });
        }
    }
}