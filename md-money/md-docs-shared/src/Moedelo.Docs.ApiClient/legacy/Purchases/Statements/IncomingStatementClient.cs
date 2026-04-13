using System;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Statements;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Statements.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Types;

namespace Moedelo.Docs.ApiClient.legacy.Purchases.Statements
{
    [InjectAsSingleton(typeof(IIncomingStatementClient))]
    public class IncomingStatementClient : BaseLegacyApiClient, IIncomingStatementClient
    {
        private readonly IHttpRequestExecuter httpRequestExecuter;
        private readonly SettingValue docsApiEndpoint;

        public IncomingStatementClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<IncomingStatementClient> logger)
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

        public Task<List<PurchasesStatementDocDto>> GetByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<PurchasesStatementDocDto>());
            }

            var uri = $"/IncomingStatement/GetByBaseIds?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<long>, List<PurchasesStatementDocDto>>(uri, baseIds);
        }

        public Task<PurchasesStatementWithItemsDto[]> GetByBaseIdsWithItemsAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(Array.Empty<PurchasesStatementWithItemsDto>());
            }

            var uri = $"/IncomingStatement/GetByBaseIdsWithItems?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<long>, PurchasesStatementWithItemsDto[]>(uri, baseIds);
        }

        // todo: вынести в другой клиент, наследованный от BaseLegacyApiClient (сейчас метод не перехватывается аудитом)
        public async Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return new List<PaidSumDto>();
            }

            var uri = $"{docsApiEndpoint.Value}/PurchasesStatements/GetPaidSumByBaseIds?firmId={firmId}&userId={userId}";
            var response = await httpRequestExecuter.PostAsync(uri, baseIds.ToUtf8JsonContent()).ConfigureAwait(false);
            return response.FromJsonString<List<PaidSumDto>>();
        }

        public Task<PurchasesStatementResponseDto> SaveAsync(FirmId firmId, UserId userId, PurchasesStatementSaveRequestDto saveDto)
        {
            return PostAsync<PurchasesStatementSaveRequestDto, PurchasesStatementResponseDto>(
                $"/api/v1/purchases/act?firmId={firmId}&userId={userId}", 
                saveDto);
        }
    }
}