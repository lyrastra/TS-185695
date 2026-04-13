using Moedelo.Money.BankBalanceHistory.Domain;
using Moedelo.Parsers.Klto1CParser.Models;
using Moedelo.Parsers.Klto1CParser.Models.Klto1CParser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.BankBalanceHistory.Business.Balances
{
    public static class BalanceRequestCreator
    {
        private const string OldBicForTochkaBank = "044525999";
        private const string NewBicForTochkaBank = "044525104";
        
        public static IReadOnlyCollection<BankBalanceUpdateRequest> Create(
            int settlementAccountId,
            MovementList movementList)
        {
            return movementList.Documents.Count == 0
                ? GetRequestsOnlyFromBalances(settlementAccountId, movementList)
                : GetRequestsFromDocuments(settlementAccountId, movementList);
        }

        private static IReadOnlyCollection<BankBalanceUpdateRequest> GetRequestsFromDocuments(
            int settlementAccountId, 
            MovementList movementList)
        {
            var documentsByDay = movementList.Documents
                .GroupBy(d => d.PayerAccount == movementList.SettlementAccount
                    ? d.GetOutgoingDate() //  ПСБ И Точка банк озоруют с датой списания\поступления пока решено так заткнуть этот фонтан ошибок 
                    : d.GetIncomingDate())
                .OrderBy(d => d.Key)
                .ToDictionary(x => x.Key, x => x.ToArray());

            var countOfDays = (movementList.EndDate.Date - movementList.StartDate.Date).Days + 1;
            var result = new BankBalanceUpdateRequest[countOfDays];
            for (var i = 0; i < countOfDays; i++)
            {
                var date = movementList.StartDate.AddDays(i);
                var documentsForOneDay = documentsByDay.GetValueOrDefault(date) ?? [];

                if (i == 0)
                {
                    if (movementList.Balances.Count == 0)
                        throw new Exception("Список остатков по расчетному счету пуст");
                    if (!movementList.Balances[0].StartBalance.HasValue)
                        throw new Exception("Начальный остаток денежных средств не указан");
                }

                var startBalance = i == 0
                    ? movementList.Balances[0].StartBalance.Value
                    : result[i - 1].EndBalance;

                var incomingBalance = documentsForOneDay
                        .Where(d => IncomingDirectionIdentifier(movementList, d))
                        .Sum(d => d.Summa);

                var outgoingBalance = documentsForOneDay
                        .Where(d => OutgoingDirectionIdentifier(movementList, d))
                        .Sum(d => d.Summa);

                var endBalance = startBalance + incomingBalance - outgoingBalance;

                result[i] = new BankBalanceUpdateRequest
                {
                    SettlementAccountId = settlementAccountId,
                    BalanceDate = date,
                    StartBalance = startBalance,
                    IncomingBalance = incomingBalance,
                    OutgoingBalance = outgoingBalance,
                    EndBalance = endBalance
                };
            }

            return result;
        }

        private static bool IncomingDirectionIdentifier(MovementList movementList, Document d)
        {
            // Банк Точка: Перечисление собственных средств.
            if (d.PayerBik == NewBicForTochkaBank && d.ContractorBik == OldBicForTochkaBank)
            {
                return false;
            }
            
            return d.ContractorAccount == movementList.SettlementAccount;
        }
        
        private static bool OutgoingDirectionIdentifier(MovementList movementList, Document d)
        {
            // Банк Точка: Перечисление собственных средств.
            if (d.PayerBik == OldBicForTochkaBank && d.ContractorBik == NewBicForTochkaBank)
            {
                return false;
            }
            
            return d.PayerAccount == movementList.SettlementAccount;
        }

        private static IReadOnlyCollection<BankBalanceUpdateRequest> GetRequestsOnlyFromBalances(
            int settlementAccountId, 
            MovementList movementList)
        {
            if (movementList.Balances.Any(b => 
                (b.IncomingBalance.HasValue && b.IncomingBalance.Value > 0) ||
                (b.OutgoingBalance.HasValue && b.OutgoingBalance.Value > 0)))
            {
                return [];
            }

            if (movementList.EndDate != movementList.StartDate)
            {
                return [];
            }

            var balance = CreateRequestFromBalance(
                settlementAccountId,
                movementList.Balances[0],
                movementList.StartDate);
 
            return [balance];
        }

        private static BankBalanceUpdateRequest CreateRequestFromBalance(
            int settlementAccountId,
            Balance balance,
            DateTime startDate)
        {
            return new BankBalanceUpdateRequest
            {
                SettlementAccountId = settlementAccountId,
                BalanceDate = startDate,
                StartBalance = balance.StartBalance ?? 0,
                EndBalance = balance.EndBalance ?? 0,
                IncomingBalance = balance.IncomingBalance ?? 0,
                OutgoingBalance = balance.OutgoingBalance ?? 0
            };
        }

        private static DateTime GetIncomingDate(this Document doc)
        {
            return doc.IncomingDate?.Date ?? doc.OutgoingDate?.Date ?? doc.DocDate?.Date ?? throw new Exception($"GetIncomingDate Exception.");
        }

        private static DateTime GetOutgoingDate(this Document doc)
        {
            return doc.OutgoingDate?.Date ?? doc.IncomingDate?.Date ?? doc.DocDate?.Date ?? throw new Exception($"GetOutgoingDate Exception.");
        }
    }
}
