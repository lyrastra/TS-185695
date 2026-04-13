using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Exceptions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Infrastructure.Json;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Wrappers;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.Common.Domain.Models.Reports;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Snapshot;
using Moedelo.Spam.ApiClient.Abastractions.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Moedelo.Money.Business.PaymentOrders.ApiClient
{
    [InjectAsSingleton(typeof(IPaymentOrderApiClient))]
    internal sealed class PaymentOrderApiClient : BaseApiClient, IPaymentOrderApiClient
    {
        private const string prefix = "/private/api/v1";
        private static readonly HttpQuerySetting PaymentOrderDefaultSetting = new(TimeSpan.FromSeconds(30));

        public PaymentOrderApiClient(
            ISettingRepository settingRepository,
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ILogger<PaymentOrderApiClient> logger)
            : base(
                  httpRequestExecuter,
                  uriCreator,
                  auditTracer,
                  authHeadersGetter,
                  auditHeadersGetter,
                  settingRepository.Get("PaymentOrderApiEndpoint"),
                  logger)
        {
        }

        public async Task<T> GetAsync<T>(string path) where T : class
        {
            try
            {
                var response = await base.GetAsync<ApiDataResponseWrapper<T>>($"{prefix}/{path}", setting: PaymentOrderDefaultSetting).ConfigureAwait(false);
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
                var response = hrscex.Content?.FromJsonString<MismatchOperationTypeResponseDto>();
                throw new OperationMismatchTypeExcepton
                {
                    DocumentBaseId = response?.DocumentBaseId ?? 0,
                    ExpectedType = response?.ExpectedType ?? 0,
                    ActualType = response?.ActualType ?? 0
                };
            }
        }

        public async Task CreateAsync<T>(string path, T dto) where T : class
        {
            await PostAsync($"{prefix}/{path}", dto, setting: PaymentOrderDefaultSetting);
        }

        public async Task UpdateAsync<T>(string path, T dto) where T : class
        {
            try
            {
                var settings = new HttpQuerySetting(TimeSpan.FromSeconds(30), true);
                await base.PutAsync($"{prefix}/{path}", dto, setting: settings);
            }
            catch (HttpRequestResponseStatusException hrscex)
                when (hrscex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new OperationNotFoundException();
            }
            catch (HttpRequestResponseStatusException hrscex)
               when (hrscex.StatusCode == HttpStatusCode.Conflict)
            {
                var response = hrscex.Content?.FromJsonString<MismatchOperationTypeResponseDto>();
                throw new OperationMismatchTypeExcepton
                {
                    DocumentBaseId = response?.DocumentBaseId ?? 0,
                    ExpectedType = response?.ExpectedType ?? 0,
                    ActualType = response?.ActualType ?? 0
                };
            }
        }

        public async Task<TResponse> UpdateAsync<TRequest, TResponse>(string path, TRequest dto) where TRequest : class
        {
            try
            {
                var settings = new HttpQuerySetting(TimeSpan.FromSeconds(60), true);
                return await base.PutAsync<TRequest, TResponse>($"{prefix}/{path}", dto, setting: settings);
            }
            catch (HttpRequestResponseStatusException hrscex)
                when (hrscex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new OperationNotFoundException();
            }
            catch (HttpRequestResponseStatusException hrscex)
               when (hrscex.StatusCode == HttpStatusCode.Conflict)
            {
                var response = hrscex.Content?.FromJsonString<MismatchOperationTypeResponseDto>();
                throw new OperationMismatchTypeExcepton
                {
                    DocumentBaseId = response?.DocumentBaseId ?? 0,
                    ExpectedType = response?.ExpectedType ?? (OperationType)0,
                    ActualType = response?.ActualType ?? (OperationType)0
                };
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
                await base.DeleteAsync($"{prefix}/{path}", setting: PaymentOrderDefaultSetting);
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
                var response = await base.PostAsync<DataWrapper<TResponse>>($"{prefix}/{path}/Delete", setting: PaymentOrderDefaultSetting);
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
                $"{prefix}/PaymentOrders/OperationType", documentBaseIds, setting: PaymentOrderDefaultSetting);
            return response.Data;
        }

        public async Task<long[]> DeleteInvalidAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var response = await base.PostAsync<IReadOnlyCollection<long>, ApiDataResponseWrapper<long[]>>(
                $"{prefix}/PaymentOrders/Invalid/Delete", documentBaseIds, setting: PaymentOrderDefaultSetting);
            return response.Data;
        }

        public Task ApplyIgnoreNumberAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds == null || !documentBaseIds.Any())
            {
                return Task.CompletedTask;
            }

            return base.PostAsync($"{prefix}/PaymentOrders/ApplyIgnoreNumber/", documentBaseIds);
        }

        public async Task<PaymentOrderWithMissingEmployeeResponse[]> GetPaymentOrdersWithMissingEmployee()
        {
            var response = await base.GetAsync<ApiDataResponseWrapper<PaymentOrderWithMissingEmployeeResponse[]>>(
                $"{prefix}/PaymentOrders/WithMissingEmployee", setting: PaymentOrderDefaultSetting);
            return response.Data;
        }

        public Task PostAsync(string uri)
        {
            return base.PostAsync($"{prefix}/{uri}");
        }

        public Task PostAsync<TRequest>(string uri, TRequest data) where TRequest : class
        {
            return base.PostAsync($"{prefix}/{uri}", data);
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest data, CancellationToken ct) where TRequest : class
        {
            var respone = await base.PostAsync<TRequest, ApiDataResponseWrapper<TResponse>>($"{prefix}/{uri}", data, cancellationToken: ct);
            return respone.Data;
        }

        public Task PutAsync<TRequest>(string uri, TRequest data) where TRequest : class
        {
            try
            {
                return base.PutAsync($"{prefix}/{uri}", data);
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

        public Task ApproveImportedAsync(int? settlementAccountId, DateTime? skipline)
        {
            var request = new ApproveImportedRequestDto
            {
                SettlementAccountId = settlementAccountId,
                Skipline = skipline
            };
            return base.PostAsync(
                $"{prefix}/PaymentOrders/Imported/Approve", request, setting: PaymentOrderDefaultSetting);
        }

        public async Task<IReadOnlyList<long>> GetBaseIdsByOperationTypeAsync(OperationType operationType, int? year)
        {
            var request = new
            {
                operationType = (int)operationType,
                year
            };
            var respone = await GetAsync<ApiDataResponseWrapper<IReadOnlyList<long>>>(
                $"{prefix}/PaymentOrders/BaseIdsByOperationType", 
                request, 
                setting: new HttpQuerySetting(TimeSpan.FromSeconds(90)));

            return respone.Data;
        }

        public async Task<ReportFile> DownloadFileAsync(string path)
        {
            try
            {
                var response = await base.DownloadFileAsync($"{prefix}/{path}");

                using var ms = new MemoryStream();
                response.Stream.CopyTo(ms);

                return new ReportFile
                {
                    Content = ms.ToArray(),
                    ContentType = response.ContentType,
                    FileName = response.FileName
                };
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

        public async Task<IReadOnlyList<int>> GetOutgoingNumbersAsync(int settlementAccountId, int? year, int? cut)
        {
            var request = new
            {
                settlementAccountId,
                year,
                cut
            };
            var response = await GetAsync<ApiDataResponseWrapper<IReadOnlyList<int>>>(
                $"{prefix}/PaymentOrders/GetOutgoingNumbers",
                request,
                setting: new HttpQuerySetting(TimeSpan.FromSeconds(120)));

            return response.Data;
        }
        
        public async Task<bool> GetIsPaidAsync(long documentBaseId)
        {
            var request = new
            {
                documentBaseId
            };
            
            var response = await GetAsync<ApiDataResponseWrapper<bool>>($"{prefix}/PaymentOrders/GetIsPaid", request);
            return response.Data;
        }

        public async Task<IReadOnlyList<DocumentStatus>> GetDocumentsStatusAsync(
            DocumentsStatusRequest request, 
            HttpQuerySetting setting = null)
        {
            var response = await base.PostAsync<DocumentsStatusRequest, ApiDataResponseWrapper<DocumentStatus[]>>(
                $"{prefix}/PaymentOrders/GetDocumentsStatus", 
                request, 
                setting: setting ?? PaymentOrderDefaultSetting);
            
            return response.Data;
        }

        public async Task<PaymentOrderSnapshotDto> GetPaymentOrderSnapshotAsync(
            long documentBaseId,
            HttpQuerySetting setting = null)
        {
            var request = new
            {
                documentBaseId
            };

            var response = await base.GetAsync<ApiDataResponseWrapper<PaymentOrderSnapshotDto>>(
                $"{prefix}/PaymentOrders/GetPaymentOrderSnapshot",
                request,
                setting: setting ?? PaymentOrderDefaultSetting);

            return response.Data;
        }
    }
}
