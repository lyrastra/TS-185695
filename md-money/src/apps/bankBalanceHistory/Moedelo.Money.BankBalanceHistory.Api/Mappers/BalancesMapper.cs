using Moedelo.Money.BankBalanceHistory.Api.Models;
using Moedelo.Money.BankBalanceHistory.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.BankBalanceHistory.Api.Mappers
{
    internal static class BalancesMapper
    {
        public static BankBalanceRequest Map(BankBalanceRequestDto dto)
        {
            return new BankBalanceRequest
            {
                SettlementAccountId = dto.SettlementAccountId,
                StartDate = dto.StartDate.Date,
                EndDate = dto.EndDate.Date,
                IncludeUserMovement = dto.IncludeUserMovement
            };
        }

        public static BankBalanceResponseDto Map(BankBalanceResponse response)
        {
            if (response == null)
            {
                return null;
            }
            return new BankBalanceResponseDto
            {
                StartBalance = response.StartBalance,
                EndBalance = response.EndBalance,
                IncomingBalance = response.IncomingBalance,
                OutgoingBalance = response.OutgoingBalance
            };
        }

        public static IReadOnlyDictionary<int, LastBankBalanceResponseDto[]> Map(IReadOnlyDictionary<int, LastBankBalance[]> responses)
        {
            var result = new Dictionary<int, LastBankBalanceResponseDto[]>(responses.Count);
            foreach (var item in responses)
            {
                result.Add(item.Key, item.Value.Select(Map).ToArray());
            }
            return result;
        }

        public static LastBankBalanceResponseDto Map(LastBankBalance response)
        {
            return new LastBankBalanceResponseDto
            {
                SettlementAccountId = response.SettlementAccountId,
                Balance = response.Balance,
                BalanceDate = response.BalanceDate.Date,
                ModifyDate = response.ModifyDate,
            };
        }

        public static BankBalanceUpdateRequest Map(BalanceRequestDto response)
        {
            return new BankBalanceUpdateRequest
            {
                SettlementAccountId = response.SettlementAccountId,
                BalanceDate = response.BalanceDate,
                StartBalance = response.StartBalance,
                EndBalance = response.EndBalance,
                IncomingBalance = response.IncomingBalance,
                OutgoingBalance = response.OutgoingBalance,
                IsUserMovement = response.IsUserMovement
            };
        }
    }
}
