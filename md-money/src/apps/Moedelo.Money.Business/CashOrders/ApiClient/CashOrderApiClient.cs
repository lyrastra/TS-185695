using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Exceptions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Wrappers;
using Moedelo.Money.CashOrders.Dto.CashOrders;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Moedelo.Money.Common.Domain.Models;

namespace Moedelo.Money.Business.CashOrders.ApiClient
{
    [InjectAsSingleton(typeof(ICashOrderApiClient))]
    internal class CashOrderApiClient : BaseApiClient, ICashOrderApiClient
    {
        private const string prefix = "/private/api/v1";
        private static readonly HttpQuerySetting CashOrderDefaultHttpSetting = new(TimeSpan.FromSeconds(30));

        public CashOrderApiClient(
            ISettingRepository settingRepository,
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ILogger<CashOrderApiClient> logger)
            : base(httpRequestExecuter,
                  uriCreator,
                  auditTracer,
                  authHeadersGetter,
                  auditHeadersGetter,
                  settingRepository.Get("CashOrderApiEndpoint"),
                  logger)
        {
        }

        public async Task<T> GetAsync<T>(string path) where T : class
        {
            try
            {
                var response = await base.GetAsync<DataWrapper<T>>($"{prefix}/{path}", setting: CashOrderDefaultHttpSetting);
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

        public async Task CreateAsync<T>(string path, T dto) where T : class
        {
            await PostAsync($"{prefix}/{path}", dto, setting: DefaultSetting);
        }

        public async Task UpdateAsync<T>(string path, T dto) where T : class
        {
            try
            {
                await PutAsync($"{prefix}/{path}", dto, setting: DefaultSetting);
            }
            catch (HttpRequestResponseStatusException hrscex)
                when (hrscex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new OperationNotFoundException();
            }
            catch (HttpRequestResponseStatusException hrscex)
               when (hrscex.StatusCode == HttpStatusCode.Conflict)
            {
                throw new OperationMismatchTypeExcepton();
            }
            catch
            {
                throw;
            }
        }

        public async Task<TResponse> UpdateAsync<TRequest, TResponse>(string path, TRequest dto)
            where TRequest : class
        {
            try
            {
                var response = await PutAsync<TRequest, DataWrapper<TResponse>>($"{prefix}/{path}", dto, setting: DefaultSetting);
                return response.Data;
            }
            catch (HttpRequestResponseStatusException hrscex)
                when (hrscex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new OperationNotFoundException();
            }
            catch (HttpRequestResponseStatusException hrscex)
               when (hrscex.StatusCode == HttpStatusCode.Conflict)
            {
                throw new OperationMismatchTypeExcepton();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAsync(string path)
        {
            try
            {
                await base.DeleteAsync($"{prefix}/{path}", setting: DefaultSetting);
            }
            catch (HttpRequestResponseStatusException hrscex)
                when (hrscex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new OperationNotFoundException();
            }
            catch
            {
                throw;
            }
        }

        public async Task<TResponse> DeleteAsync<TResponse>(string path)
        {
            try
            {
                var response = await base.PostAsync<DataWrapper<TResponse>>($"{prefix}/{path}/Delete", setting: DefaultSetting);
                return response.Data;
            }
            catch (HttpRequestResponseStatusException hrscex)
                when (hrscex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new OperationNotFoundException();
            }
            catch
            {
                throw;
            }
        }

        public async Task<OperationTypeDto[]> GetOperationTypeByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var response = await base.PostAsync<IReadOnlyCollection<long>, ApiDataResponseWrapper<OperationTypeDto[]>>(
                $"{prefix}/CashOrders/OperationType", documentBaseIds, setting: CashOrderDefaultHttpSetting);
            return response.Data;
        }
        
        public async Task<IReadOnlyList<DocumentStatus>> GetDocumentsStatusByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds, 
            HttpQuerySetting setting = null)
        {
            var response = await PostAsync<IReadOnlyCollection<long>, ApiDataResponseWrapper<DocumentStatus[]>>(
                $"{prefix}/CashOrders/GetDocumentsStatusByBaseIds", 
                documentBaseIds,
                setting: setting ?? CashOrderDefaultHttpSetting);
            
            return response.Data;
        }

        private class DataWrapper<T>
        {
            public T Data { get; set; }
        }
    }
}
