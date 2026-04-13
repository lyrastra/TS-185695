using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.TaxSelfCost.Dto.FiFo;
using Moedelo.TaxSelfCost.Dto.FiFo.Enums;
using System.Threading.Tasks;

namespace Moedelo.TaxSelfCost.Client.Fifo
{
    [InjectAsSingleton]
    public sealed class PostingsGenerationProcessClient : BaseCoreApiClient, IPostingsGenerationProcessClient
    {
        private readonly ISettingRepository settingRepository;

        public PostingsGenerationProcessClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("TaxSelfCostApiEndpoint").Value;
        }

        public async Task StartAsync(int firmId, int userId, StartPostingsGenerationDTO parameters)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            await PostAsync<StartPostingsGenerationDTO>($"/v1/EventsGeneration/StartPostingsGeneration?firmId={firmId}&userId={userId}", parameters, queryHeaders: tokenHeaders).ConfigureAwait(false);
        }

        public async Task<GenerationStatusEnum> GetStatusAsync(int firmId, int userId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            return await GetAsync<GenerationStatusEnum>($"/v1/EventsGeneration/GetPostingsGenerationStatus?firmId={firmId}&userId={userId}", queryHeaders: tokenHeaders).ConfigureAwait(false);
        }

        public async Task<bool> IsFifoActiveByYearAsync(int firmId, int userId, int year)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            return (await GetAsync<IsFifoActiveDto>($"/v1/EventsGeneration/IsFifoActiveByYear?year={ year }", queryHeaders: tokenHeaders).ConfigureAwait(false)).Data;
        }
    }
}
