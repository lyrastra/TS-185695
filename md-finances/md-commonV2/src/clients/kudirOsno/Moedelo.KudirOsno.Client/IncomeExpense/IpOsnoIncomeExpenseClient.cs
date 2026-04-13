using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.KudirOsno.Client.DtoWrappers;
using Moedelo.KudirOsno.Client.IncomeExpense.Dto;
using System.Threading.Tasks;

namespace Moedelo.KudirOsno.Client.IncomeExpense
{
    [InjectAsSingleton]
    public class IpOsnoIncomeExpenseClient : BaseCoreApiClient, IIpOsnoIncomeExpenseClient
    {
        private readonly ISettingRepository settingRepository;

        public IpOsnoIncomeExpenseClient(
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
            return settingRepository.Get("KudirOsnoApiEndpoint").Value;
        }

        public async Task<IncomeExpenseResponseDto[]> GetAsync(int firmId, int userId, int year, int[] quarters)
        {
            var url = $"/api/v1/IncomeExpense";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var data = new IncomeExpenseRequestDto
            {
                Year = year,
                Quarters = quarters
            };

            var response = await PostAsync<IncomeExpenseRequestDto, ApiDataDto<IncomeExpenseResponseDto[]>>(url, data, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }
    }
}
