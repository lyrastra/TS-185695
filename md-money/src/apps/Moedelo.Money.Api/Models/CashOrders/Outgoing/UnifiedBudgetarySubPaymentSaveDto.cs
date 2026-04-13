using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Money.Api.Infrastructure.Validation;
using Moedelo.Money.Api.Models.TaxPostings;
using System.ComponentModel;

namespace Moedelo.Money.Api.Models.CashOrders.Outgoing
{
    public class UnifiedBudgetarySubPaymentSaveDto
    {
        /// <summary>
        /// Базовый идентификатор дочерней операции
        /// </summary>
        [DefaultValue(0)]
        public long? DocumentBaseId { get; set; }

        /// <summary>
        /// Идентификатор КБК
        /// </summary>
        [IdIntValue]
        [RequiredValue]
        public int KbkId { get; set; }

        /// <summary>
        /// Бюджетный период
        /// </summary>
        [RequiredValue]
        public BudgetaryPeriodSaveDto Period { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        [OperationSum]
        [RequiredValue]
        public decimal Sum { get; set; }

        /// <summary>
        /// Идентификатор патента
        /// </summary>
        [IdLongValue]
        [DefaultValue(null)]
        public long? PatentId { get; set; }

        /// <summary>
        /// Параметры налогового учёта
        /// </summary>
        public TaxPostingsSaveDto TaxPostings { get; set; }
    }
}
