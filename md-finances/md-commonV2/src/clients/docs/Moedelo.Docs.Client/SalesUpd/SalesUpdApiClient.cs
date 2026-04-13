using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums;
using Moedelo.Docs.Dto;
using Moedelo.Docs.Dto.Docs;
using Moedelo.Docs.Dto.SalesUpd;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Docs.Client.SalesUpd
{
    [InjectAsSingleton]
    public class SalesUpdApiClient : BaseApiClient, ISalesUpdApiClient
    {
        private readonly SettingValue apiEndpoint;

        public SalesUpdApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("DocsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<SalesUpdDto> GetByDocumentBaseId(int firmId, int userId, long baseId)
        {
            return GetAsync<SalesUpdDto>("/SalesUpd/GetByDocumentBaseId", new
            {
                firmId,
                userId,
                baseId
            });
        }

        public Task<SalesUpdWithContractDto> GetByDocumentBaseIdWithContractAsync(int firmId, int userId, long baseId)
        {
            return GetAsync<SalesUpdWithContractDto>("/SalesUpd/GetByDocumentBaseIdWithContract", new
            {
                firmId,
                userId,
                baseId
            });
        }

        public Task<List<SalesUpdItemDto>> GetItemsByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> ids, CancellationToken cancellationToken = default)
        {
            if (ids?.Any() != true)
            {
                return Task.FromResult(new List<SalesUpdItemDto>());
            }

            return PostAsync<IReadOnlyCollection<int>, List<SalesUpdItemDto>>($"/SalesUpd/GetItemsByIds?firmId={firmId}&userId={userId}", ids, cancellationToken: cancellationToken);
        }

        public Task<bool> HasProductItemsAsync(int firmId, int userId, long documentBaseId, CancellationToken cancellationToken = default)
        {
            var queryParams = new { firmId, userId, documentBaseId };
            return GetAsync<bool>(uri: "/SalesUpd/HasProductItems", queryParams: queryParams, cancellationToken: cancellationToken);
        }

        public Task DeleteAsync(int firmId, int userId, long baseId)
        {
            return DeleteByBaseIdsAsync(firmId, userId, new[] { baseId });
        }

        public Task DeleteByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/SalesUpd/DeleteByBaseIds?firmId={firmId}&userId={userId}", baseIds);
        }

        public Task<List<SalesUpdDto>> GetByPeriodAsync(int firmId, int userId, DateTime startDate, DateTime endDate, bool withForgotten = false, bool useReadOnly = false)
        {
            return GetAsync<List<SalesUpdDto>>("/SalesUpd/GetByPeriod", new
            {
                firmId,
                userId,
                startDate,
                endDate,
                withForgotten,
                useReadOnly
            });
        }

        public Task<List<SalesUpdWithItemsDto>> GetWithItemsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<SalesUpdWithItemsDto>());
            }
            
            return PostAsync<IReadOnlyCollection<long>, List<SalesUpdWithItemsDto>>($"/SalesUpd/GetWithItems?firmId={firmId}&userId={userId}", baseIds);
        }

        public Task<List<SalesUpdWithItemsDto>> GetByPeriodWithItemsAsync(int firmId, int userId, DateTime startDate,
            DateTime endDate, CancellationToken cancellationToken = default)
        {
            return GetAsync<List<SalesUpdWithItemsDto>>("/SalesUpd/GetByPeriodWithItems", new
            {
                firmId,
                userId,
                startDate,
                endDate
            }, cancellationToken: cancellationToken);
        }

        public Task<DocFileDto> GetFileByBaseIdAsync(int firmId, int userId, long baseId, DocumentFormat format,
            bool? useStampAndSign = null)
        {
            return GetAsync<DocFileDto>("/SalesUpd/Download", new
            {
                firmId,
                userId,
                baseId,
                format,
                useStampAndSign
            });
        }

        public Task<List<SalesUpdDto>> GetByCriterionAsync(int firmId, int userId, SalesUpdRequestDto request)
        {
            return PostAsync<SalesUpdRequestDto, List<SalesUpdDto>>($"/SalesUpd/GetByCriterion?firmId={firmId}&userId={userId}", request);
        }

        public Task<long> GetNextNumberAsync(int firmId, int userId, int year)
        {
            return GetAsync<long>($"/SalesUpd/GetNextNumber", new
            {
                firmId,
                userId,
                year
            });
        }

        public Task<List<SalesUpdDto>> GetByDocumentBaseIds(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<SalesUpdDto>());
            }
            
            return PostAsync<IReadOnlyCollection<long>, List<SalesUpdDto>>($"/SalesUpd/GetByDocumentBaseIds?firmId={firmId}&userId={userId}", baseIds);
        }

        public Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<PaidSumDto>());
            }
            return PostAsync<IReadOnlyCollection<long>, List<PaidSumDto>>($"/SalesUpd/GetPaidSumByBaseIds?firmId={firmId}&userId={userId}", baseIds);
        }

        public Task UpdateLinkWithBillAsync(int firmId, int userId, SalesUpdBillSaveRequestDto dto)
        {
            return PostAsync($"/SalesUpd/UpdateLinkWithBill?firmId={firmId}&userId={userId}", dto);
        }
    }
}