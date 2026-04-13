using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.KontragentsV2.Client.DtoWrappers;
using Moedelo.KontragentsV2.Dto;

namespace Moedelo.KontragentsV2.Client.Kpps
{
    [InjectAsSingleton]
    public class KontragentKppsClient : BaseApiClient, IKontragentKppsClient
    {
        private readonly SettingValue apiEndpoint;

        public KontragentKppsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("KontragentsPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<long> SaveAsync(int firmId, int userId, KontragentKppDto kpp)
        {
            return PostAsync<KontragentKppDto, long>($"/KppV2/Save?firmId={firmId}&userId={userId}", kpp);
        }

        public async Task<KontragentKppDto> GetByKontragentAsync(int firmId, int userId, int kontragentId, DateTime date)
        {
            var result = await GetAsync<DataDto<KontragentKppDto>>("/Kpp/GetByKontragentAndDate", new {firmId, userId, kontragentId, date}).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<IList<KontragentKppDto>> GetByKontragentAsync(int firmId, int? userId, int kontragentId)
        {
            var result = await GetAsync<DataDto<IList<KontragentKppDto>>>("/Kpp/GetByKontragent", new { firmId, userId, kontragentId }).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<IList<KontragentKppDto>> GetByKontragentIdsAsync(int firmId, int userId, KontragentKppsRequestDto requestDto)
        {
            if (requestDto?.KontragentIds?.Any() != true)
            {
                return new List<KontragentKppDto>();
            }
            
            var result = await PostAsync<KontragentKppsRequestDto, DataDto<IList<KontragentKppDto>>>(
                $"/Kpp/GetByKontragentIds?firmId={firmId}&userId={userId}", 
                requestDto).ConfigureAwait(false);
            
            return result.Data;
        }
    }

}