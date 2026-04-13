using System.Collections.Generic;

namespace Moedelo.RptV2.Dto.ProfitWidget
{
    public class ProfitWidgetDto
    {
        /// <summary>
        /// Доход
        /// </summary>
        public decimal Profit { get; set; }

        /// <summary>
        /// Расход
        /// </summary>
        public decimal Expense { get; set; }
        
        /// <summary>
        /// Строка 110 из декларации или УбытУмНБ из остатков
        /// </summary>
        public decimal LossesFromPreviousYears { get; set; }
        
        /// <summary>
        /// Строка 300 из декларации - начисленные авансы в фед бюджет
        /// </summary>
        public decimal AdvancePaymentSumToFederalBudget { get; set; }
        
        /// <summary>
        /// Строка 310 из декларации - начисленные авансы в бюджет суб
        /// </summary>
        public decimal AdvancePaymentSumToRegionalBudget { get; set; }
        
        /// <summary>
        /// Платежи из мастера авансовых платежей
        /// </summary>
        public List<ProfitAdvancePaymentDto> ProfitAdvancePayments { get; set; }

        /// <summary>
        /// Строка 330 из декларации предыдущий период - начисленные авансы в фед бюджет
        /// </summary>
        public decimal AdvancePaymentFederalBudgetPreviousYear { get; set; }
        
        /// <summary>
        /// Строка 340 из декларации предыдущий период - начисленные авансы в бюджет суб
        /// </summary>
        public decimal AdvancePaymentRegionalBudgetPreviousYear { get; set; }

        /// <summary>
        /// Есть завершённая декларация или загруженная в остатках
        /// </summary>
        public bool HasCompletedDeclaration { get; set; }
    }
}