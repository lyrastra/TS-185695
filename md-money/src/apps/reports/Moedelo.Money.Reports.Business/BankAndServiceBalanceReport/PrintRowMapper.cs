using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Reports.Domain.BankAndServiceBalanceReport;

namespace Moedelo.Money.Reports.Business.BankAndServiceBalanceReport
{
    internal static class PrintRowMapper
    {
        public static IReadOnlyCollection<BankAndServiceBalanceReportPrintRow> Map(
            IReadOnlyCollection<BankAndServiceBalanceReportRow> rows)
        {
            return rows
                .Select(Map)
                .ToArray();
        }

        private static BankAndServiceBalanceReportPrintRow Map(BankAndServiceBalanceReportRow row)
        {
            var serviceBalanceWithUnrecognizedSum =
                row.ServiceBalance + row.UnrecognizedIncomingSum - row.UnrecognizedOutgoingSum;

            return new BankAndServiceBalanceReportPrintRow
            {
                Login = row.Login,
                Tariff = row.Tariff,
                Product = row.Product,
                Opf = row.IsOoo ? "ООО" : "ИП",
                SettlementAccount = row.SettlementAccount,
                BankName = row.BankName,
                BankBalanceDate = row.BankBalanceDate,
                BankBalance = row.BankBalance,
                ServiceBalance = row.ServiceBalance,
                BalancesIsEqual = row.BankBalance == row.ServiceBalance ? "Да" : "Нет",
                BalancesDiff = row.BankBalance - row.ServiceBalance,
                BankBalanceGtServiceBalance = row.BankBalance == row.ServiceBalance
                    ? "равно"
                    : row.BankBalance > row.ServiceBalance
                        ? "больше"
                        : "меньше",
                RemainsFilled = row.RemainsFilled ? "Да" : "Нет",
                RemainsFillDate = row.RemainsFillDate,
                CountSettlementAccountWithRemainsFilled = row.CountSettlementAccountWithRemainsFilled,
                ReconciliationState = row.ReconciliationState,
                LastReconciliationStartDate = row.LastReconciliationStartDate,
                HasUnrecognizedOperations = row.UnrecognizedIncomingCount > 0 || row.UnrecognizedOutgoingCount > 0 ? "Да" : "Нет",
                UnrecognizedIncomingCount = row.UnrecognizedIncomingCount,
                UnrecognizedIncomingSum = row.UnrecognizedIncomingSum,
                UnrecognizedOutgoingCount = row.UnrecognizedOutgoingCount,
                UnrecognizedOutgoingSum = row.UnrecognizedOutgoingSum,
                ServiceBalanceWithUnrecognizedSum = serviceBalanceWithUnrecognizedSum,
                IsBalancesEqualWithUnrecognizedSum = row.BankBalance == serviceBalanceWithUnrecognizedSum ? "Да" : "Нет"
            };
        }
    }
}
