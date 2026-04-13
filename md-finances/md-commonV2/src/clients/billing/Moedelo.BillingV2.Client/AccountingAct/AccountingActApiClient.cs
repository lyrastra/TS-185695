using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.AccountingAct;
using Moedelo.BillingV2.Dto.Billing;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BillingV2.Client.AccountingAct
{
    [InjectAsSingleton]
    public class AccountingActApiClient : BaseApiClient, IAccountingActApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public AccountingActApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("InternalBillingApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<PaymentAccountingActDto>> GetActsForPaymentAsync(int paymentId)
        {
            return GetAsync<List<PaymentAccountingActDto>>($"/AccountingAct/ByPayment/{paymentId}");
        }
        
        public Task<List<PaymentAccountingActFor1CDto>> GetActsFor1CAsync(IEnumerable<PaymentHistoryDto> payments)
        {
            return PostAsync<IEnumerable<PaymentHistoryDto>, List<PaymentAccountingActFor1CDto>>("/AccountingAct/For1C", payments);
        }

        public Task<List<PaymentAccountingActFor1CDto>> GetActsFor1CAsync(AccountingActsCriteriaDto criteriaDto)
        {
            // этот запрос может исполняться достаточно долго
            var settings = new HttpQuerySetting(TimeSpan.FromSeconds(600));
            
            return PostAsync<AccountingActsCriteriaDto, List<PaymentAccountingActFor1CDto>>(
                "/AccountingAct/For1C/GetByCriteria",
                criteriaDto,
                setting: settings);
        }
    }
}