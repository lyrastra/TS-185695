using Moedelo.Common.Enums.Enums.FinancialOperations;

namespace Moedelo.Common.Enums.Extensions.FinancialOperations
{
    public static class BudgetaryPaymentTurnsExtensions
    {
        /// <summary>
        /// Получить текстовое значение типа бюджетного платежа
        /// </summary>
        /// <param name="type">Тип бюджетного платежа</param>
        /// <returns></returns>
        public static string ToText(this BudgetaryPaymentTurn type)
        {
            return type.GetDescription();
        }
    }
}
