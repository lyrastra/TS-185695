using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.PurseOperation;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.PurseOperations
{
    [InjectAsSingleton]
    public class PurseOperationApiClient : BaseApiClient, IPurseOperationApiClient
    {
        private readonly SettingValue apiEndPoint;

        public PurseOperationApiClient(
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
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task DeleteAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds.Count == 0)
            {
                return Task.CompletedTask;
            }
            return DeleteByRequestAsync($"/PurseOperationApi/Delete?firmId={firmId}&userId={userId}", baseIds);
        }

        public async Task<PurseOperationResultDto> SavePurseOperationAsync(int firmId, int userId, PurseOperationDto dto)
        {
            var response = await PostAsync<PurseOperationDto, DataResponseWrapper<PurseOperationResultDto>>($"/PurseOperationApi/SavePurseOperation?firmId={firmId}&userId={userId}", dto).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<CreatedPurseOperationDto> SavePurseOperationWithTypeAsync(int firmId, int userId, PurseOperationForMultipleTypesDto dto)
        {
            var response = await PostAsync<PurseOperationForMultipleTypesDto, DataResponseWrapper<CreatedPurseOperationDto>>($"/PurseOperationApi/SavePurseOperationWithType?firmId={firmId}&userId={userId}", dto).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<CreatedPurseOperationDto> SavePurseOperationWithWaybill(int firmId, int userId, PurseOperationClientDataDto dto)
        {
            var response = await PostAsync<PurseOperationClientDataDto, DataResponseWrapper<CreatedPurseOperationDto>>
                ($"/PurseOperationApi/SavePurseOperationWithWaybill?firmId={firmId}&userId={userId}", dto).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<CreatedPurseOperationDto> SaveRefundToClientAsync(int firmId, int userId, PurseOperationForMultipleTypesDto dto)
        {
            var response = await PostAsync<PurseOperationForMultipleTypesDto, DataResponseWrapper<CreatedPurseOperationDto>>($"/PurseOperationApi/SaveRefundToClient?firmId={firmId}&userId={userId}", dto).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<PurseOperationsCountDto[]> GetNumberOfPurseOperationsAsync(int firmId, int userId,
            IReadOnlyCollection<int> kontragentIds)
        {
            if (kontragentIds.Count == 0)
            {
                return Array.Empty<PurseOperationsCountDto>();
            }

            var response =
                await PostAsync<IReadOnlyCollection<int>, DataResponseWrapper<PurseOperationsCountDto[]>>(
                        $"/PurseOperationApi/GetNumberOfPurseOperations?firmId={firmId}&userId={userId}", kontragentIds)
                    .ConfigureAwait(false);
            return response.Data;
        }

        public Task ProvideAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds == null || !baseIds.Any())
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/PurseOperationApi/Provide?firmId={firmId}&userId={userId}", baseIds);
        }

        public Task<PurseOperationClientDataDto> GetPurseOperationByBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            return PostAsync<PurseOperationClientDataDto>($"/PurseOperationApi/GetByBaseId?firmId={firmId}&userId={userId}&baseId={documentBaseId}");
        }
    }
}