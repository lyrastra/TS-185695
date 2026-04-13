using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.PaymentOrder;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.BudgetaryPaymentsClient
{
    [InjectAsSingleton]
    public class BudgetaryPaymentsClient : BaseApiClient, IBudgetaryPaymentsClient
    {
        private readonly SettingValue apiEndPoint;
        
        public BudgetaryPaymentsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }
        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<IList<BudgetaryPaymentDto>> GetUsnBudgetaryPrepaymentsAsync(int firmId, int userId, int year)
        {
            return (await GetAsync<ListResponseWrapper<BudgetaryPaymentDto>>($"/BudgetaryPayments/GetUsnBudgetaryPrepayments", new {firmId, userId, year}).
                ConfigureAwait(false)).Items;
        }

    }
}