using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Exceptions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.Business.Abstractions.Exceptions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using System;
using System.Collections.Generic;
using Moedelo.Money.Business.Wrappers;
using Moedelo.Money.Common.Domain.Models;

namespace Moedelo.Money.Business.PurseOperations.ApiClient
{
    [InjectAsSingleton(typeof(IPurseOperationApiClient))]
    internal sealed class PurseOperationApiClient : BaseApiClient, IPurseOperationApiClient
    {
        private const string prefix = "/private/api/v1";
        private static readonly HttpQuerySetting DefaultHttpSetting = new(TimeSpan.FromSeconds(30));

        public PurseOperationApiClient(
            ISettingRepository settingRepository,
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ILogger<PurseOperationApiClient> logger)
            : base(httpRequestExecuter,
                  uriCreator,
                  auditTracer,
                  authHeadersGetter,
                  auditHeadersGetter,
                  settingRepository.Get("PurseOperationApiEndpoint"),
                  logger)
        {
        }

        public async Task<T> GetAsync<T>(string path) where T : class
        {
            try
            {
                var response = await base.GetAsync<DataWrapper<T>>($"{prefix}/{path}", setting: DefaultHttpSetting);
                return response.Data;
            }
            catch (HttpRequestResponseStatusException hrscex)
                when (hrscex.StatusCode == HttpStatusCode.NotFound ||
                hrscex.StatusCode == HttpStatusCode.Conflict)
            {
                throw new OperationNotFoundException();
            }
            catch
            {
                throw;
            }
        }
        
        public async Task<IReadOnlyList<DocumentStatus>> GetDocumentsStatusByBaseIdsAsync(
            IReadOnlyCollection<long> documentBaseIds, 
            HttpQuerySetting setting = null)
        {
            var response = await PostAsync<IReadOnlyCollection<long>, ApiDataResponseWrapper<DocumentStatus[]>>(
                $"{prefix}/PurseOperations/GetDocumentsStatusByBaseIds", 
                documentBaseIds,
                setting: setting ?? DefaultHttpSetting);
            
            return response.Data;
        }

        private class DataWrapper<T>
        {
            public T Data { get; set; }
        }
    }
}
