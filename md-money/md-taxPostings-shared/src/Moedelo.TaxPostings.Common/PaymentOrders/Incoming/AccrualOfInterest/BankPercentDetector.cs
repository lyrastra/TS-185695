namespace Moedelo.TaxPostings.Common.PaymentOrders.Incoming.AccrualOfInterest
{
    /// <summary>
    /// Определение "процентов по вкладам" в операции "Начисление процентов от банка"
    /// </summary>
    public static class BankPercentDetector
    {
        // дата с которой "проценты по вкладам" не учитываются в НУ
        private static readonly DateTime PercentDetectingStartDate = new DateTime(2026, 1, 1);

        public static bool IsBankPercent(
            DateTime operationDate,
            string settlementAccountNumber,
            string description)
        {
            if (operationDate < PercentDetectingStartDate)
            {
                return false;
            }

            settlementAccountNumber ??= string.Empty;
            description = description.ToLower().Replace("ё", "е");

            return settlementAccountNumber.StartsWith("423") ||
                settlementAccountNumber.StartsWith("421") ||
                description.Contains("депозит") ||
                description.Contains("остат") ||
                (description.Contains("процент") && description.Contains("вклад")) ||
                (description.Contains("процент") && description.Contains("счет"));
        }
    }
}
