/**
 * Модель данных для действия
 * @typedef {Object<string, any>} ActionModel
 * @property {RuleActionType} actionType         - тип действия
 * @property {RuleConditionType} conditionType   - один из вариантов комбинаций условий
 * @property {number|null} operationType         - тип операции (Оплата поставщику, Списана комиссия банка и т.д.)
 * @property {number|null} taxationSystemType    - тип системы налогообложения (УСН, ЕНВД и т.д.)
 * @property {RuleConditionObject[]} conditions  - список объектов для условий (Контрагент, Назначение платежа и т.д.)
 * @property {boolean} applyToOperations         - применяется ли правило к существующим операциям
 * @property {string} startDate                  - дата начала применения правила
 * @property {number|null} employeeId            - Id сотрудника
 * @property {number|null} contractId            - Id договора (тип операции Получение займа или кредита, Погашение займа или процентов, Возврат займа или процентов, Выдача займа)
 */
