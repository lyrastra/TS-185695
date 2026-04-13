using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Money.ApiClient.Abstractions.Money.Dto;

namespace Moedelo.Money.ApiClient.Abstractions.Money
{
    public interface IBankBalanceHistoryApiClient
    {
        Task<BankBalanceResponseDto> GetAsync(int settlementAccountId, DateTime startDate, DateTime endDate);
        Task<IReadOnlyDictionary<int, LastBankBalanceResponseDto[]>> OnDateByFirms(BankBalancesOnDateByFirmsRequestDto request);
    }
}
