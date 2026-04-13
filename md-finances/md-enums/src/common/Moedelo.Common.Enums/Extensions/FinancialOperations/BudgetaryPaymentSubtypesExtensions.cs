using Moedelo.Common.Enums.Attributes;
using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.Common.Enums.Extensions.FinancialOperations
{
    public static class BudgetaryPaymentSubtypesExtensions
    {
        /// <summary>
        /// Получить текстовое значение типа бюджетного платежа
        /// </summary>
        /// <param name="type">Тип бюджетного платежа</param>
        /// <returns></returns>
        public static string ToText(this BudgetaryPaymentSubtype type)
        {
            return type.GetDescription();
        }

        public static bool IsPeni(this BudgetaryPaymentSubtype type)
        {
            var attr = type.GetEnumAttribute<PeniAttribute, BudgetaryPaymentSubtype>();
            return attr != null;
        }

        public static bool IsPenalty(this BudgetaryPaymentSubtype type)
        {
            var attr = type.GetEnumAttribute<PenaltyAttribute, BudgetaryPaymentSubtype>();
            return attr != null;
        }
    }
}
