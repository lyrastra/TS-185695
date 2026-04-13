namespace Moedelo.Money.Enums.Extensions
{
    public static class AccountCodeExtensions
    {
        public static bool IsSocialInsurance(this BudgetaryAccountCodes accountCode)
        {
            return IsSocialInsurance((int)accountCode);
        }

        public static bool IsTradingFee(this BudgetaryAccountCodes accountCode)
        {
            return accountCode == BudgetaryAccountCodes.TradingFees;
        }

        public static bool IsSocialInsurance(this int acountCode)
        {
            const int socialInsuranceMainCode = 690000;
            const int codeContainerRange = 10000;

            var minValue = socialInsuranceMainCode;
            var maxValue = minValue + codeContainerRange - 1;
            return acountCode >= minValue && acountCode <= maxValue;
        }
    }
}
