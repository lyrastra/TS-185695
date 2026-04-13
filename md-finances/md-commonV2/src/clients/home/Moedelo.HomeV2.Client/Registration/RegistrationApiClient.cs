using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.HomeV2.Dto.Registration;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HomeV2.Client.Registration
{
    [InjectAsSingleton]
    public class RegistrationApiClient : BaseApiClient, IRegistrationApiClient
    {
        private readonly SettingValue apiEndPoint;
        private const string HeaderHost = "Host-Email";
        
        public RegistrationApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("HomePrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Rest/Registration";
        }

        public async Task<RegistrationResponseDto> RegisterForEvotorAsync(int firmId, int userId, RegistrationRequestDto dto)
        {
            var response = await PostAsync<RegistrationRequestDto, DataRequestWrapper<RegistrationResponseDto>>($"/RegisterForEvotor?firmId={firmId}&userId={userId}", dto).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<BaseRegistrationResponseDto> RegisterForAccountAsync(int firmId, int userId, RegistrationDto dto)
        {
            var response = await PostAsync<RegistrationDto, DataRequestWrapper<BaseRegistrationResponseDto>>($"/RegisterForAccount?firmId={firmId}&userId={userId}", dto).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<UserFirmResponseDto> SaveUserFirmAsync(int firmId, int userId, UserFirmDto dto, string host)
        {
            var response =
                await PostAsync<UserFirmDto, DataRequestWrapper<UserFirmResponseDto>>(
                        $"/SaveUserFirm?firmId={firmId}&userId={userId}", dto,
                        new[] {new KeyValuePair<string, string>(HeaderHost, host)},
                        new HttpQuerySetting(new TimeSpan(0, 2, 0)))
                    .ConfigureAwait(false);
            return response.Data;
        }

        public async Task<UserFirmResponseDto> AttachUserForAccountAsync(int firmId, int userId, UserFirmDto dto, string host)
        {
            var response =
                await PostAsync<UserFirmDto, DataRequestWrapper<UserFirmResponseDto>>(
                    $"/AttachUserForAccount?firmId={firmId}&userId={userId}", dto,
                    new[] {new KeyValuePair<string, string>(HeaderHost, host)}).ConfigureAwait(false);
            return response.Data;
        }
    }
}