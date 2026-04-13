using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AgentsV2.Dto.Enums;
using Moedelo.AgentsV2.Dto.WithdrawalMoney;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.AgentsV2.Client.WithdrawalMoney
{
    public interface IWithdrawalMoneyApiClient : IDI
    {
        Task<WithdrawalMoneyDto> ConfirmWithdrawalRequest(WithdrawalMoneyDto withdrawalMoneyDto);

        Task<ResponseStatusCode> UndoWithdrawalRequest(WithdrawalMoneyDto withdrawalMoneyDto);

        Task<ResponseStatusCode> WithdrawMoneyOnWebMoneyWallet(WithdrawalOnWebMoneyDto request);

        Task<ResponseStatusCode> WithdrawMoneyOnYandexMoneyWallet(WithdrawalOnYandexMoneyDto request);

        Task<ResponseStatusCode> WithdrawMoneyOnSettlementAccount(WithdrawalOnSettlementAccountDto request);

        Task<List<RequestForWithdrawalOnWebMoneyWalletDto>> GetRequestsForWithdrawalOnWebMoneyWallet();

        Task<List<RequestForWithdrawalOnYandexMoneyWalletDto>> GetRequestsForWithdrawalOnYandexMoneyWallet();

        Task<List<RequestForWithdrawalOnSettlementAccountDto>> GetRequestsForWithdrawalOnSettlementAccount();
    }
}
