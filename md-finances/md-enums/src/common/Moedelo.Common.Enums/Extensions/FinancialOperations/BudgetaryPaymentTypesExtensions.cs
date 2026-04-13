using System.Linq;
using Moedelo.Common.Enums.Attributes;
using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.Common.Enums.Extensions.FinancialOperations
{
    public static class BudgetaryPaymentTypesExtensions
    {
        public static bool IsForEmployer(this BudgetaryPaymentType type)
        {
            var attr = EnumMethodExtensions.GetEnumAttribute<ForEmployeesAttribute, BudgetaryPaymentType>(type);
            return attr != null;
        }

        public static bool IsUsn(this BudgetaryPaymentType type)
        {
            var attr = EnumMethodExtensions.GetEnumAttribute<UsnAttribute, BudgetaryPaymentType>(type);
            return attr != null;
        }

        // типы взносов в бюджетном платеже
        private static readonly BudgetaryPaymentType[] FeeTypes = new[]
        {
            BudgetaryPaymentType.InsuranceForIp
            , BudgetaryPaymentType.AccumulateForIp
            , BudgetaryPaymentType.FederalFomsForIp
            , BudgetaryPaymentType.TerretorialFomsForIp
            , BudgetaryPaymentType.InjuredFSS
            , BudgetaryPaymentType.DisabilityFSS
            , BudgetaryPaymentType.TerrorotialFomsForEmployees
            , BudgetaryPaymentType.FederalFomsForEmployees
            , BudgetaryPaymentType.AccumulateForEmployees
            , BudgetaryPaymentType.InsuranceForEmployees
        };

        public static bool IsFeeType(this BudgetaryPaymentType type)
        {
            return FeeTypes.Contains(type);
        }
    }
}
