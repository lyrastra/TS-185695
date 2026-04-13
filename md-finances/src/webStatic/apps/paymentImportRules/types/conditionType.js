/**
 * @typedef {Object<string, any>} Condition
 * @property {string} id                  - уникальный идентификатор
 * @property {number|null} operationType  - идентификатор типа операции (из MoneyOperationTypeResource)
 * @property {string|null} contractorName - название контрагента
 * @property {string|null} paymentPurpose - назначение платежа
 * @property {RuleConditionObject} type   - объект условия (Контрагент, Назначение платежа и т.д.)
 * @property {RuleEqualVariant} operator  - способ сравнения объекта условия со значением (Совпадает, Не совпадает)
 */

/**
 * Функция-кнструктор, возвращающая новую модель для условия
 * @constructor ConditionConstructor
 * @returns {Condition}
 */
