using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.AccountablePersonMoneyPayments;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.AccountablePersonMoneyPayments
{
    [InjectAsSingleton]
    public class AccAccountablePersonPaymentsApiClient : BaseApiClient, IAccAccountablePersonPaymentsApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public AccAccountablePersonPaymentsApiClient(
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
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<long> CreatePaymentForAccountablePersonAsync(int firmId, int userId, PaymentForAccountablePersonDto request)
        {
            var response = await PostAsync<PaymentForAccountablePersonDto, long>(
                    $"/AccountablePersonPayments/CreatePaymentForAccountablePersonV2Async?firmId={firmId}&userId={userId}",
                    request)
                .ConfigureAwait(false);

            return response;
        }
        
        public async Task<MoneyOperationAdditionalDto> GetMoneyOperationAdditionalDataAsync(int firmId, int userId, long? baseId)
        {
            return await GetAsync<MoneyOperationAdditionalDto>(
                    "/AccountablePersonPayments/GetMoneyOperationAdditionalData",
                    new {firmId, userId, baseId})
                .ConfigureAwait(false);
        }
    }
}