using System;
using Moedelo.PaymentOrderImport.Enums;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto
{
    public class ImportRuleListResponseDto
    {
        /// <summary>
        /// Идентификатор правила
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название правила
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип действия при применении правила
        /// </summary>
        public RuleActionType ActionType { get; set; }

        /// <summary>
        /// Тип операции после применения правила
        /// </summary>
        public PaymentImportOperationType? OperationType { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// СНО при применении правила
        /// </summary>
        public PaymentImportTaxationSystemType? TaxationSystem { get; set; }

        /// <summary>
        /// Признак правило удалено
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Дата начала применения правила
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата создания правила
        /// </summary>
        public DateTime CreateDate { get; set; }
        
        /// <summary>
        /// Список условия для правила
        /// </summary>
        public ImportConditionSaveDto[] ConditionList { get; set; }
    }
}