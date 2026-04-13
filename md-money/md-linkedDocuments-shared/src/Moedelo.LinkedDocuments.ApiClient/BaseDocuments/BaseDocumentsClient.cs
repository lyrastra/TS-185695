using System;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Infrastructure.Http.Abstractions.Exceptions;

namespace Moedelo.LinkedDocuments.ApiClient.BaseDocuments
{
    [InjectAsSingleton(typeof(IBaseDocumentsClient))]
    internal sealed class BaseDocumentsClient : BaseApiClient, IBaseDocumentsClient
    {
        public BaseDocumentsClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<BaseDocumentsClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("LinkedDocumentsApiEndpoint"),
                logger)
        {
        }

        public async Task<BaseDocumentDto> GetByIdAsync(long id)
        {
            var uri = $"/api/v1/BaseDocuments/{id}";

            try
            {
                var response = await GetAsync<DataResponse<BaseDocumentDto>>(uri).ConfigureAwait(false);
                return response.Data;
            }
            catch (HttpRequestResponseStatusException hrscex)
                when (hrscex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<BaseDocumentDto[]> GetByIdsAsync(IReadOnlyCollection<long> ids)
        {
            if (ids?.Any() != true)
            {
                return Array.Empty<BaseDocumentDto>();
            }

            var uri = $"/api/v1/BaseDocuments/GetByIds";
            var response = await PostAsync<IReadOnlyCollection<long>, DataResponse<BaseDocumentDto[]>>(uri, ids.ToDistinctReadOnlyCollection()).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<long> CreateAsync(BaseDocumentCreateDto dto)
        {
            var uri = $"/api/v1/BaseDocuments";
            var response = await PostAsync<BaseDocumentCreateDto, DataResponse<long>>(uri, dto);
            return response.Data;
        }

        public Task UpdateAsync(BaseDocumentUpdateDto dto)
        {
            var uri = $"/api/v1/BaseDocuments/{dto.Id}";
            return PutAsync(uri, dto);
        }
    }
}