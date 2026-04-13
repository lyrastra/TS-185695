using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.KontragentsV2.Client.DtoWrappers;
using Moedelo.KontragentsV2.Dto;

namespace Moedelo.KontragentsV2.Client.Kontragents
{
    [InjectAsSingleton]
    public class KontragentSignerClient : BaseApiClient, IKontragentSignerClient
    {
        private readonly SettingValue apiEndpoint;

        public KontragentSignerClient(
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
            apiEndpoint = settingRepository.Get("KontragentsPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<KontragentSignerDto> GetAsync(int firmId, int userId, int kontragentId)
        {
            return GetAsync<KontragentSignerDto>($"/V2/{kontragentId}/Signer", new {firmId, userId});
        }

        public async Task<KontragentSignerDto> GetByKontragentAsync(int firmId, int userId, int kontragentId)
        {
            var result = await GetAsync<DataDto<KontragentSignerDto>>(
                "/Signer/GetByKontragent",
                new {firmId, userId, kontragentId}).ConfigureAwait(false);
            return result.Data;
        }

        public Task SaveAsync(int firmId, int userId, int kontragentId, KontragentSignerDto dto)
        {
            return PostAsync($"/V2/{kontragentId}/Signer?firmId={firmId}&userId={userId}", dto);
        }
    }
}