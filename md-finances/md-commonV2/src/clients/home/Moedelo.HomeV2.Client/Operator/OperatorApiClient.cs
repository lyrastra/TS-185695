using Moedelo.HomeV2.Dto.Operator;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Exceptions.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HomeV2.Client.Operator
{
    [InjectAsSingleton]
    public class OperatorApiClient : BaseApiClient, IOperatorApiClient
    {
        private const string ControllerUri = "/Rest/Operator";
        private readonly SettingValue apiEndPoint;

        public OperatorApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("HomePrivateApiEndpoint");
        }

        private class OperatorResponseDto<T> : BaseDto
        {
            public OperatorResponseDto(T data)
            {
                Data = data;
            }
            public T Data { get; set; }

            public string Message { get; set; }

            public bool Success { get; set; }
        }

        private class DataDto<T> : BaseDto
        {
            public DataDto(T data)
            {
                Data = data;
            }

            public T Data { get; set; }
        }

        private class BaseDto
        {
            public BaseDto(){}

            public BaseDto(bool status, string message = null)
            {
                ResponseStatus = status;
                ResponseMessage = message;
            }
            public bool ResponseStatus { get; set; }
            public string ResponseMessage { get; set; }
        }

        private class IdInfoDto<T> : BaseDto where T : struct
        {
            public IdInfoDto(T id)
            {
                Id = id;
            }

            public T Id { get; private set; }
        }

        public async Task<int> GetOperatorIdByCodeAsync(string code)
        {
            var result = await GetAsync<OperatorResponseDto<int>>("/Id", new { code }).ConfigureAwait(false);
            if (!result.ResponseStatus)
            {
                throw new HttpRequestResponseStatusException(HttpStatusCode.InternalServerError, result.ResponseMessage);
            }

            if (!result.Success)
            {
                throw new HttpRequestResponseStatusException(HttpStatusCode.InternalServerError, result.Message);
            }

            return result.Data;
        }

        public async Task<OperatorDto> GetOperatorByIdAsync(int id)
        {
            var result = await GetAsync<OperatorResponseDto<OperatorDto>>("/GetOperatorById", new { id }).ConfigureAwait(false);
            if (!result.ResponseStatus)
            {
                throw new HttpRequestResponseStatusException(HttpStatusCode.InternalServerError, result.ResponseMessage);
            }

            if (!result.Success)
            {
                throw new HttpRequestResponseStatusException(HttpStatusCode.InternalServerError, result.Message);
            }
            return result.Data;
        }

        public Task<OperatorDto> GetOperatorByUserIdAsync(int userId)
        {
            return GetAsync<OperatorDto>("/GetByUserId", new {userId});
        }

        public Task<List<OperatorDto>> GetOperatorsByIdsAsync(IReadOnlyCollection<int> operatorIds)
        {
            if (operatorIds.Any() != true)
            {
                return Task.FromResult(new List<OperatorDto>());
            }

            return PostAsync<IReadOnlyCollection<int>, List<OperatorDto>>(
                "/GetOperatorsByIds",
                operatorIds);
        }

        public async Task<int> GetUserIdByOperatorIdAsync(int operatorId)
        {
            var result = await GetAsync<DataDto<int>>("/GetUserIdByOperatorIdAsync", new { operatorId }).ConfigureAwait(false);
            if (!result.ResponseStatus)
            {
                throw new HttpRequestResponseStatusException(HttpStatusCode.InternalServerError, result.ResponseMessage);
            }
            return result.Data;
        }

        public Task<List<OperatorDto>> GetOperatorsByEmailAsync(string email)
        {
            return GetAsync<List<OperatorDto>>("/GetOperatorsByEmail", new {email});
        }

        public Task<List<OperatorDto>> GetOperatorsByEmailAsync(IReadOnlyCollection<string> emails)
        {
            if (emails?.Any() != true)
            {
                return Task.FromResult(new List<OperatorDto>());
            }

            return PostAsync<IReadOnlyCollection<string>, List<OperatorDto>>("/GetOperatorsByEmails", emails);
        }

        public async Task<List<OperatorDto>> GetOperatorsListAsync(OperatorRequestDto operatorRequestDto)
        {
            var result =
                await PostAsync<OperatorRequestDto, OperatorResponseDto<List<OperatorDto>>>(
                    "/GetOperatorsList", operatorRequestDto).ConfigureAwait(false);
            if (!result.ResponseStatus)
            {
                throw new HttpRequestResponseStatusException(HttpStatusCode.InternalServerError, result.ResponseMessage);
            }
            return result.Data;
        }

        public async Task DeleteOperatorAsync(int id)
        {
            var result = await PostAsync<IdInfoDto<int>, BaseDto>("/DeleteOperator", new IdInfoDto<int>(id)).ConfigureAwait(false);
            if (!result.ResponseStatus)
            {
                throw new HttpRequestResponseStatusException(HttpStatusCode.InternalServerError, result.ResponseMessage);
            }
        }

        public async Task<int> SaveOperatorAsync(OperatorDto oper)
        {
            var result = await PostAsync<OperatorDto, OperatorResponseDto<int>>("/SaveOperator", oper).ConfigureAwait(false);
            if (!result.ResponseStatus)
            {
                throw new HttpRequestResponseStatusException(HttpStatusCode.InternalServerError, result.ResponseMessage);
            }

            return result.Data;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerUri;
        }
    }
}