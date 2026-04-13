using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.PhoneVerification;
using Moedelo.HomeV2.Dto.PhoneVerification;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HomeV2.Client.PhoneVerification
{
    [InjectAsSingleton]
    public class PhoneVerificationV2Client : BaseApiClient, IPhoneVerificationV2Client
    {
        private readonly SettingValue apiEndPoint;
        
        public PhoneVerificationV2Client(
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
            apiEndPoint = settingRepository.Get("HomePrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Rest/PhoneVerification/";
        }

        public async Task<CheckFirmIsNeedVerifyPhoneV2Dto> CheckFirmIsNeedVerifyPhoneAdvancedAsync(FirmIsNeedVerifyRequestV2Dto requestDto)
        {
            var result = await GetAsync<DataRequestWrapper<CheckFirmIsNeedVerifyPhoneV2Dto>>(
                "CheckFirmIsNeedVerifyPhoneAdvancedAsync", 
                requestDto).ConfigureAwait(false);

            return result.Data;
        }

        public async Task<PhoneVerificationCodeDto> GetPhoneVerificationCodeAsync(PhoneDto phoneDto)
        {
            return (await GetAsync<DataDto<PhoneVerificationCodeDto>>("GetPhoneVerificationCodeAsync", phoneDto).ConfigureAwait(false)).Data;
        }

        public Task<PhoneVerificationStatusDto> GetPhoneVerificationStatusAsync(string phone, CancellationToken cancellationToken)
        {
            return GetAsync<PhoneVerificationStatusDto>("GetPhoneVerificationStatus", new { phone }, cancellationToken: cancellationToken);
        }

        public Task<NewPhoneVerificationCodeResponseDto> GenerateCodeAsync(
            NewPhoneVerificationCodeRequestDto requestDto,
            CancellationToken cancellationToken)
        {
            return PostAsync<NewPhoneVerificationCodeRequestDto, NewPhoneVerificationCodeResponseDto>(
                "GenerateCode",
                requestDto,
                cancellationToken: cancellationToken);
        }

        public Task<PhoneVerificationCodeValidationResult> ValidateAsync(
            PhoneVerificationCodeValidationRequestDto requestDto,
            CancellationToken cancellationToken)
        {
            return PostAsync<PhoneVerificationCodeValidationRequestDto, PhoneVerificationCodeValidationResult>(
                "Validate",
                requestDto,
                cancellationToken: cancellationToken);
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
            public BaseDto() { }

            public BaseDto(bool status, string message = null)
            {
                ResponseStatus = status;
                ResponseMessage = message;
            }
            public bool ResponseStatus { get; set; }
            public string ResponseMessage { get; set; }
        }
    }
}