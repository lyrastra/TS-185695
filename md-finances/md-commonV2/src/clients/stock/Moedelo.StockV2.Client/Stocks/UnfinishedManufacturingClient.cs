using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.StockV2.Client.ResponseWrappers;
using Moedelo.StockV2.Dto.Stocks;

namespace Moedelo.StockV2.Client.Stocks
{
    [InjectAsSingleton(typeof(IUnfinishedManufacturingClient))]
    public class UnfinishedManufacturingClient : BaseApiClient, IUnfinishedManufacturingClient
    {
        private readonly SettingValue apiEndPoint;

        public UnfinishedManufacturingClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) :
            base(
                httpRequestExecutor,
                uriCreator,
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("StockServiceUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<UnfinishedManufacturingItemDto>> GetByDateAsync(int firmId, int userId, DateTime date, CancellationToken cancellationToken)
        {
            const string uri = "/UnfinishedManufacturing/GetByDate";
            var queryParams = new { firmId, userId, date };

            return GetAsync<List<UnfinishedManufacturingItemDto>>(uri,queryParams, cancellationToken: cancellationToken);
        }

        public Task<List<UnfinishedManufacturingItemDto>> GetAsync(int firmId, int userId, CancellationToken cancellationToken)
        {
            return GetAsync<List<UnfinishedManufacturingItemDto>>(
                "/UnfinishedManufacturing", new { firmId, userId }, cancellationToken: cancellationToken);
        }

        public Task SaveAsync(int firmId, int userId, UnfinishedManufacturingSaveRequestDto dto, CancellationToken cancellationToken)
        {
            var uri = $"/UnfinishedManufacturing/Save?firmId={firmId}&userId={userId}";

            return PostAsync(uri, dto, cancellationToken: cancellationToken);
        }

        public Task<bool> ExistsForDivisionAsync(int firmId, int divisionId, CancellationToken cancellationToken)
        {
            var uri = $"/UnfinishedManufacturing/ExistsForDivision?firmId={firmId}&divisionId={divisionId}";

            return GetAsync<bool>(uri, cancellationToken: cancellationToken);
        }

        public Task DeleteByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> ids, CancellationToken cancellationToken)
        {
            var uri = $"/UnfinishedManufacturing/DeleteByIds?firmId={firmId}&userId={userId}";

            return PostAsync(uri, ids, cancellationToken: cancellationToken);
        }
    }
}
