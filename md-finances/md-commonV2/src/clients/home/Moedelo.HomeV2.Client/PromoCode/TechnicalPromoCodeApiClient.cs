using System.Threading.Tasks;
using Moedelo.HomeV2.Dto.PromoCode.Technical;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HomeV2.Client.PromoCode
{
    [InjectAsSingleton(typeof(ITechnicalPromoCodeApiClient))]
    public class TechnicalPromoCodeApiClient : BaseApiClient, ITechnicalPromoCodeApiClient
    {
        private readonly SettingValue apiEndpoint;

        public TechnicalPromoCodeApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser, auditTracer, auditScopeManager
                )
        {
            apiEndpoint = settingRepository.Get("HomePrivateApiEndpoint");
        }

        public Task<GeneratedTechnicalPromoCodeDto> GenerateBizAccFixedSumPromoCodeAsync(int firmId, decimal fixedSum)
        {
            var uri = "/Rest/TechnicalPromoCode/BizAcc/GenerateFixedSumPromoCode";
            var dto = new BizAccFixedSumTechnicalPromoCodeCreationRequestDto
            {
                FirmId = firmId,
                FixedSum = fixedSum
            };

            return PostAsync<BizAccFixedSumTechnicalPromoCodeCreationRequestDto, GeneratedTechnicalPromoCodeDto>(uri, dto);
        }

        public Task<TechnicalPromoCodeDto> GetTechnicalPromoCodeByNameAsync(int firmId, string promoCode)
        {
            var uri = "/Rest/TechnicalPromoCode/GetByName";
            var dto = new DecodeTechnicalPromoCodeRequestDto
            {
                FirmId = firmId,
                PromoCode = promoCode
            };

            return PostAsync<DecodeTechnicalPromoCodeRequestDto, TechnicalPromoCodeDto>(uri, dto);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}
