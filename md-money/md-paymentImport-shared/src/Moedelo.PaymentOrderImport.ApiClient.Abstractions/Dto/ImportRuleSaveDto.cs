using System;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Enums;
using Moedelo.PaymentOrderImport.Enums;

namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto
{
    /// <summary>
    /// Модель для сохранения/изменения правила импорта
    /// </summary>
    public class ImportRuleSaveDto
    {
    /// <summary>
        /// Название правила
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип условия для применения правил
        /// </summary>
        public RuleConditionType ConditionType { get; set; }

        /// <summary>
        /// Список условия для правила
        /// </summary>
        public ImportConditionSaveDto[] ConditionList { get; set; }

        /// <summary>
        /// Тип действия при применении правила
        /// </summary>
        public RuleActionType ActionType { get; set; }

        /// <summary>
        /// Тип операции при применении правила
        /// </summary>
        public PaymentImportOperationType? OperationType { get; set; }

        /// <summary>
        /// СНО при применении правила
        /// </summary>
        public PaymentImportTaxationSystemType? TaxationSystem { get; set; }

        /// <summary>
        /// Дата начала применения правила
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Применить правило к существующим операциям
        /// </summary>
        public bool ApplyToOperations { get; set; }

        /// <summary>
        /// Сотрудник
        /// </summary>
        public int? EmployeeId { get; set; }

        /// <summary>
        /// Договор
        /// </summary>
        public int? ContractId { get; set; }
        
        /// <summary>
        /// Как учитывать НУ (для типов "Прочее" поступление/списание)
        /// </summary>
        public TaxableSumType? TaxableSumType{ get; set; }
        
        /// <summary>
        /// Счет в БУ-проводке (для типов "Прочее" поступление/списание)
        /// </summary>
        /// <remarks>
        /// Поступление: ДТ SyntheticAccountCode КТ 51.01
        /// Списание: ДТ 51.01 КТ SyntheticAccountCode
        /// Субконто берутся из операции согласно выбранному счету
        /// </remarks>
        public int? SyntheticAccountCode { get; set; }
    }
}