using System.Threading.Tasks;
using Moedelo.HomeV2.Dto.TemporaryPassword;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HomeV2.Client.TemporaryPassword
{
    [InjectAsSingleton(typeof(ITemporaryPasswordApiClient))]
    public class TemporaryPasswordApiClient : BaseApiClient, ITemporaryPasswordApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public TemporaryPasswordApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) 
            : base(
                  httpRequestExecutor, 
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)

        {
            apiEndPoint = settingRepository.Get("HomePrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Rest/TemporaryPassword";
        }

        public Task<bool> UseTemporaryPasswordAsync(TemporaryPasswordDto passwordDto)
        {
            return PostAsync<TemporaryPasswordDto, bool>("/use", passwordDto);
        }

        public Task<bool> CheckTemporaryPasswordAsync(TemporaryPasswordDto tempPassword)
        {
            return GetAsync<bool>("/CheckTemporaryPassword", tempPassword);
        }

        public Task DestroyTemporaryPasswordAsync(TemporaryPasswordDto tempPassword)
        {
            return PostAsync("/DestroyTemporaryPassword", tempPassword);
        }

        public Task<TemporaryPasswordDto> GenerateTemporaryPasswordAsync(int userId)
        {
            return PostAsync<TemporaryPasswordDto, TemporaryPasswordDto>("/GenerateTemporaryPassword", new TemporaryPasswordDto {UserId = userId });
        }
    }
}