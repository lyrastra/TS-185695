/**
 * @enum тип действия которое нужно выполнить в результате выполнения правила
 * Синхронизировать с md-paymentImportNetCore\src\apps\Moedelo.PaymentImport.Handler.Domain\Enums\ImportRules\RuleActionType.cs
 */
const RuleActionType = {
    /** Изменить Тип операции */
    ChangeOperationType: 0,

    /** Изменить СНО */
    ChangeTaxationSystem: 1,

    /** Изменить признак "Без нумерации" */
    ChangeIgnoreNumber: 2,

    /** Изменить признак "Посредничество" */
    ChangeMediation: 3
};

export default RuleActionType;
