using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Enums.TaxPostings;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.Postings.Dto;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Postings.Client.LinkedDocument
{
    [InjectAsSingleton(typeof(ILinkedDocumentClient))]
    public class LinkedDocumentClient : BaseApiClient, ILinkedDocumentClient
    {
        private readonly SettingValue apiEndpoint;

        public LinkedDocumentClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("PostingPrivateApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/Rest/LinkedDocument";
        }

        public Task DeleteAsync(long id, int firmId, int userId)
        {
            return PostAsync($"/Delete?firmId={firmId}&userId={userId}&id={id}");
        }
        
        public Task DeleteAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }
            
            return PostAsync($"/DeleteByIds?firmId={firmId}&userId={userId}", data: baseIds);
        }

        public Task<long> CreateOrUpdateAsync(LinkedDocumentDto dto, int firmId, int userId)
        {
            var uri = $"/CreateOrUpdate?firmId={firmId}&userId={userId}";

            return PostAsync<LinkedDocumentDto, long>(uri, dto);
        }

        public Task<List<DocumentTypeDto>> GetDocumentTypeListByIdListAsync(IReadOnlyCollection<long> idList,
            int firmId, int userId, bool useReadonlyDb, CancellationToken cancellationToken)
        {
            var uri = $"/GetDocumentTypeListByIdList?firmId={firmId}&userId={userId}&useReadonlyDb={useReadonlyDb}";

            return PostAsync<IReadOnlyCollection<long>, List<DocumentTypeDto>>(uri, idList, cancellationToken: cancellationToken);
        }

        public Task<LinkedDocumentDto> GetByIdAsync(long id, int firmId, int userId)
        {
            return GetAsync<LinkedDocumentDto>("/GetById", new {id, userId, firmId});
        }

        public Task<List<LinkedDocumentDto>> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<long> ids,
            bool useReadonlyDb = false,
            HttpQuerySetting httpQuerySettings = null,
            CancellationToken cancellationToken = default)
        {
            if (ids?.Any() != true)
            {
                return Task.FromResult(new List<LinkedDocumentDto>());
            }

            var uri = $"/GetByIds?firmId={firmId}&userId={userId}&useReadonlyDb={useReadonlyDb}";

            return PostAsync<IReadOnlyCollection<long>, List<LinkedDocumentDto>>(
                uri, ids, setting: httpQuerySettings, cancellationToken: cancellationToken);
        }

        public Task<List<LinkedDocumentDto>> GetByTypeAndSumAsync(
            int firmId, int userId,
            AccountingDocumentType type,
            decimal? minSum, decimal? maxSum)
        {
            return GetAsync<List<LinkedDocumentDto>>(
                "/GetByTypeAndSum",
                new {firmId, userId, type, minSum, maxSum});
        }

        public Task<List<LinkedDocumentDto>> GetParentDocumentsByBaseId(
            int firmId, int userId,
            long documentBaseId)
        {
            return GetAsync<List<LinkedDocumentDto>>(
                "/GetParentDocumentsByBaseId",
                new {firmId, userId, documentBaseId});
        }

        public Task<List<LinkedDocumentDto>> GetCreatedByUserAsync(int firmId, int userId,
            bool useReadonlyDb, CancellationToken cancellationToken)
        {
            const string uri = "/CreatedByUser";

            return GetAsync<List<LinkedDocumentDto>>(uri,new {firmId, userId, useReadonlyDb}, cancellationToken: cancellationToken);
        }

        public Task<List<long>> GetBaseIdsByAsync(int firmId, int userId, GetBaseIdsByRequestDto request)
        {
            return PostAsync<GetBaseIdsByRequestDto, List<long>>(
                $"/GetBaseIdsBy?firmId={firmId}&userId={userId}",
                request);
        }

        public Task UpdateTaxStatusesAsync(int firmId, int userId, IReadOnlyDictionary<long, TaxPostingStatus> docTaxStatusMap)
        {
            if (docTaxStatusMap?.Any() != true)
            {
                return Task.CompletedTask;
            }
            
            return PostAsync(
                $"/UpdateTaxStatuses?firmId={firmId}&userId={userId}",
                docTaxStatusMap);
        }

        public Task<List<long>> GetChildIdListByChildDocTypeAndLinkTypeAsync(
            long id, int firmId, int userId,
            AccountingDocumentType childDocumentType,
            LinkType linkType)
        {
            return GetAsync<List<long>>("/GetChildIdListByChildDocTypeAndLinkType",
                new {id, firmId, userId, childDocumentType, linkType});
        }

        public Task<List<PaidSumDocumentDto>> GetPaidSumsForDocumentsAsync(
            int firmId,
            int userId,
            IReadOnlyCollection<long> documentsBaseIdList,
            AccountingDocumentType excludeDocumentType)
        {
            if (documentsBaseIdList?.Any() != true)
            {
                return Task.FromResult(new List<PaidSumDocumentDto>());
            }

            return PostAsync<IReadOnlyCollection<long>, List<PaidSumDocumentDto>>(
                    $"/GetPaidSumsForDocuments?firmId={firmId}&userId={userId}&excludeDocumentType={excludeDocumentType}",
                    documentsBaseIdList);
        }

        public Task<IReadOnlyDictionary<long, long>> CreateMultipleAsync(int firmId, int userId, IReadOnlyCollection<CreateLinkedDocumentRequestDto> documents)
        {
            if (documents?.Any() != true)
            {
                return Task.FromResult<IReadOnlyDictionary<long, long>>(new Dictionary<long, long>());
            }

            return PostAsync<IReadOnlyCollection<CreateLinkedDocumentRequestDto>, IReadOnlyDictionary<long, long>>(
                $"/CreateMultiple?firmId={firmId}&userId={userId}",
                documents);
        }
    }
}