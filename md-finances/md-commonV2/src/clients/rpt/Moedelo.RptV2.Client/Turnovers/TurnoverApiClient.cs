using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RptV2.Dto.Turnovers;

namespace Moedelo.RptV2.Client.Turnovers
{
    [InjectAsSingleton]
    public class TurnoverApiClient : BaseApiClient, ITurnoverApiClient
    {
        private readonly SettingValue apiEndpoint;
        
        public TurnoverApiClient(
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
            apiEndpoint = settingRepository.Get("RptApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public async Task<List<TurnoverDto>> GetTurnoverAsync(int userId, int firmId, TurnoverRequest request)
        {
            return await PostAsync<TurnoverRequest, List<TurnoverDto>>(
                $"/Turnover/GetTurnovers?firmId={firmId}&userId={userId}",
                request).ConfigureAwait(false);
        }

        public async Task<List<SubcontoTurnoverDto>> GetSubcontoTurnoversAsync(int userId, int firmId, SubcontoTurnoversRequestDto request)
        {
            return await PostAsync<SubcontoTurnoversRequestDto, List<SubcontoTurnoverDto>>(
                $"/Turnover/GetSubcontoTurnovers?firmId={firmId}&userId={userId}",
                request).ConfigureAwait(false);
        }
    }
}
