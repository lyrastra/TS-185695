using System;

namespace Moedelo.PaymentImport.Dto
{
    /// <summary>
    /// Применёное правило импорта в массовых операциях. (AppliedImportRuleDto из md-outsource-paymentImport-shared)
    /// </summary>
    public class OutsourceAppliedImportRuleDto
    {
        /// <summary>
        /// Идентификатор платежа
        /// </summary>
        public long DocumentBaseId { get; set; }
        /// <summary>
        /// Идентификатор правила
        /// </summary>
        public int RuleId { get; set; }
        /// <summary>
        /// Название правила
        /// </summary>
        public string RuleName { get; set; }
        /// <summary>
        /// Удалено ли правило на текущий момент
        /// </summary>
        public bool IsDeletedRule { get; set; }
        /// <summary>
        /// Дата применения правила
        /// </summary>
        public DateTime ApplyDate { get; set; }
    }
}