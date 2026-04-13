using Moedelo.Common.Enums.Enums.SpecialOffers;
using Moedelo.HomeV2.Dto.SpecialOffers;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HomeV2.Client.SpecialOffers
{
    [InjectAsSingleton]
    public class SpecialOffersClient : BaseApiClient, ISpecialOffersClient
    {
        private const string ControllerUri = "/Rest/SpecialOffer";
        private readonly SettingValue apiEndPoint;

        public SpecialOffersClient(IHttpRequestExecutor httpRequestExecutor,
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

        public async Task<List<SpecialOffersDto>> GetAllSpecialOffers()
        {
            var result = await GetAsync<DataRequestWrapper<List<SpecialOffersDto>>>("").ConfigureAwait(false);
            return result.Data;
        }

        public async Task<List<SpecialOffersDto>> GetAllSpecialOffersByCritrions(
            IEnumerable<SpecialOffersFilterCriterions> filterCriterions)
        {
            var result = await GetAsync<DataRequestWrapper<List<SpecialOffersDto>>>("/ByCriterions", new { filterCriterions }).ConfigureAwait(false);
            return result.Data;
        }

        public Task<SpecialOffersDto> GetSpecialOfferById(int id)
        {
            return GetAsync<SpecialOffersDto>("", new { id });
        }

        public Task<SpecialOffersDto> CreateSpecialOffer(SpecialOffersDto specialOffersDto)
        {
            return PostAsync<SpecialOffersDto, SpecialOffersDto>("", specialOffersDto);
        }

        public Task UpdateSpecialOffer(SpecialOffersDto specialOffersDto)
        {
            return PutAsync("", specialOffersDto);
        }

        public Task DeleteSpecialOfferById(int id)
        {
            return DeleteAsync("", new { id });
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerUri;
        }
    }
}
