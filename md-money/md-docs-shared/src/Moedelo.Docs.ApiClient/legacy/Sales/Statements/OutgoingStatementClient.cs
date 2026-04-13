using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Statements;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Statements.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Exceptions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Docs.ApiClient.legacy.Sales.Statements
{
    [InjectAsSingleton(typeof(IOutgoingStatementClient))]
    public class OutgoingStatementClient : BaseLegacyApiClient, IOutgoingStatementClient
    {
        private readonly IHttpRequestExecuter httpRequestExecuter;
        private readonly SettingValue docsApiEndpoint;

        public OutgoingStatementClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<OutgoingStatementClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
            docsApiEndpoint = settingRepository.Get("DocsApiEndpoint");
            this.httpRequestExecuter = httpRequestExecuter;
        }

        public Task<List<SalesStatementDocDto>> GetByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<SalesStatementDocDto>());
            }

            var uri = $"/OutgoingStatement/GetByBaseIds?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<long>, List<SalesStatementDocDto>>(uri, baseIds);
        }

        public Task<SalesStatementWithItemsDto[]> GetByBaseIdsWithItemsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(Array.Empty<SalesStatementWithItemsDto>());
            }

            var uri = $"/OutgoingStatement/GetByBaseIdsWithItems?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<long>, SalesStatementWithItemsDto[]>(uri, baseIds);
        }

        // todo: вынести в другой клиент, наследованный от BaseLegacyApiClient (сейчас метод не перехватывается аудитом)
        public async Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return new List<PaidSumDto>();
            }

            var uri = $"{docsApiEndpoint.Value}/SalesStatements/GetPaidSumByBaseIds?firmId={firmId}&userId={userId}";
            var response = await httpRequestExecuter.PostAsync(uri, baseIds.ToUtf8JsonContent()).ConfigureAwait(false);
            return response.FromJsonString<List<PaidSumDto>>();
        }

        public async Task<SalesStatementDto> GetByBaseIdAsync(FirmId firmId, UserId userId, long baseId)
        {
            try
            {
                var uri = $"/api/v1/sales/act/{baseId}?firmId={firmId}&userId={userId}";
                return await GetAsync<SalesStatementDto>(uri);
            }
            catch (HttpRequestResponseStatusException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                throw;
            }
        }

        public Task<SalesStatementDto> SaveAsync(FirmId firmId, UserId userId, SalesStatementSaveRequestDto saveDto)
        {
            var uri = $"/api/v1/sales/act?firmId={firmId}&userId={userId}";
            return PostAsync<SalesStatementSaveRequestDto, SalesStatementDto>(uri, saveDto);
        }
    }
}