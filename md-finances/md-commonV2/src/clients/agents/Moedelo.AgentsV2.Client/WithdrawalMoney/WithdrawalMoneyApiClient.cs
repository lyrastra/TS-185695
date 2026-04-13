using System.Collections.Generic;
using System.Threading.Tasks;


using Moedelo.AgentsV2.Dto.Enums;
using Moedelo.AgentsV2.Dto.WithdrawalMoney;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AgentsV2.Client.WithdrawalMoney
{
    [InjectAsSingleton]
    public class WithdrawalMoneyApiClient : BaseApiClient, IWithdrawalMoneyApiClient
    {
        private readonly SettingValue apiEndPoint;

        public WithdrawalMoneyApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager
        ) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AgentsApiUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<WithdrawalMoneyDto> ConfirmWithdrawalRequest(WithdrawalMoneyDto withdrawalMoneyDto)
        {
            var result = await PostAsync<WithdrawalMoneyDto, DataWrapper<WithdrawalMoneyDto>>("/WithdrawalMoney/ConfirmWithdrawalRequest", withdrawalMoneyDto).ConfigureAwait(false);
            return result.Data;
        }

        public async Task<ResponseStatusCode> UndoWithdrawalRequest(WithdrawalMoneyDto withdrawalMoneyDto)
        {
            var result = await PostAsync<WithdrawalMoneyDto, StatusWrapper<ResponseStatusCode>>("/WithdrawalMoney/UndoWithdrawalRequest", withdrawalMoneyDto).ConfigureAwait(false);
            return result.StatusCode;
        }

        public async Task<ResponseStatusCode> WithdrawMoneyOnWebMoneyWallet(WithdrawalOnWebMoneyDto request)
        {
            var result = await PostAsync<WithdrawalOnWebMoneyDto, StatusWrapper<ResponseStatusCode>>("/WithdrawalMoney/WithdrawMoneyOnWebMoneyWallet", request).ConfigureAwait(false);
            return result.StatusCode;
        }

        public async Task<ResponseStatusCode> WithdrawMoneyOnYandexMoneyWallet(WithdrawalOnYandexMoneyDto request)
        {
            var result = await PostAsync<WithdrawalOnYandexMoneyDto, StatusWrapper<ResponseStatusCode>>("/WithdrawalMoney/WithdrawMoneyOnYandexMoneyWallet", request).ConfigureAwait(false); ;
            return result.StatusCode;
        }

        public async Task<ResponseStatusCode> WithdrawMoneyOnSettlementAccount(WithdrawalOnSettlementAccountDto request)
        {
            var result = await PostAsync<WithdrawalOnSettlementAccountDto, StatusWrapper<ResponseStatusCode>>("/WithdrawalMoney/WithdrawMoneyOnSettlementAccount", request).ConfigureAwait(false); ;
            return result.StatusCode;
        }

        public async Task<List<RequestForWithdrawalOnWebMoneyWalletDto>> GetRequestsForWithdrawalOnWebMoneyWallet()
        {
            var result = await GetAsync<DataWrapper<List<RequestForWithdrawalOnWebMoneyWalletDto>>>("/WithdrawalMoney/GetRequestsForWithdrawalOnWebMoneyWallet").ConfigureAwait(false);
            return result.Data;
        }

        public async Task<List<RequestForWithdrawalOnYandexMoneyWalletDto>> GetRequestsForWithdrawalOnYandexMoneyWallet()
        {
            var result = await GetAsync<DataWrapper<List<RequestForWithdrawalOnYandexMoneyWalletDto>>>("/WithdrawalMoney/GetRequestsForWithdrawalOnYandexMoneyWallet").ConfigureAwait(false);
            return result.Data;
        }

        public async Task<List<RequestForWithdrawalOnSettlementAccountDto>> GetRequestsForWithdrawalOnSettlementAccount()
        {
            var result = await GetAsync<DataWrapper<List<RequestForWithdrawalOnSettlementAccountDto>>>("/WithdrawalMoney/GetRequestsForWithdrawalOnSettlementAccount").ConfigureAwait(false);
            return result.Data;
        }
    }
}
