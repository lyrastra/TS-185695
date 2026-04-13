using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.FrameInfo;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BillingV2.Client.FrameInfo
{
    [InjectAsSingleton]
    public class BillingFrameInfoClient : BaseApiClient, IBillingFrameInfoClient
    {
        private readonly SettingValue apiEndPoint;
        
        public BillingFrameInfoClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("InternalBillingApiEndpoint");
        }

        public Task<BillingFrameInfoForFirmResponseDto> GetForFirmByIdAsync(int id)
        {
            return GetAsync<BillingFrameInfoForFirmResponseDto>("/BillingFrameInfo/GetForFirmById", new {id});
        }

        public Task<BillingFrameInfoForFirmResponseDto> GetForFirmByInnAsync(string inn)
        {
            return GetAsync<BillingFrameInfoForFirmResponseDto>("/BillingFrameInfo/GetForFirmByInn", new {inn});
        }

        public Task<List<BillingFrameInfoForFirmResponseDto>> GetForFirmsByIdsAsync(List<int> ids)
        {
            return PostAsync<List<int>, List<BillingFrameInfoForFirmResponseDto>>("/BillingFrameInfo/GetForFirmsByIds", ids);
        }

        public Task<List<BillingFrameInfoPayHistoryDto>> GetPayHistoryByFirmId(int firmId)
        {
            return GetAsync<List<BillingFrameInfoPayHistoryDto>>("/BillingFrameInfo/GetPayHistoryByFirmId", new { firmId });
        }

        public Task<List<BillingFrameInfoPayHistoryDto>> GetPayHistoryByFirmIds(List<int> firmIds)
        {
            return PostAsync<List<int>, List <BillingFrameInfoPayHistoryDto>>("/BillingFrameInfo/GetPayHistoryByFirmIds", firmIds);
        }

        public Task<List<BillingFrameInfoPayHistoryDto>> GetPayHistoryByInn(string inn)
        {
            return GetAsync<List<BillingFrameInfoPayHistoryDto>>("/BillingFrameInfo/GetPayHistoryByInn", new { inn });
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}