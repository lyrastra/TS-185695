import _ from 'underscore';
import RuleConditionObject from '../enums/RuleConditionObject';
import RuleEqualVariant from '../enums/RuleEqualVariant';
import RuleConditionType from '../enums/RuleConditionType';
import { affectedConditionValidationFields, validateOneCondition } from './validateHelper';

/**
 * Конструктор для создания условия заполненного данными по умолчанию
 * @returns {Condition}
 * @constructor
 */
export function EmptyPaymentOperationTypeCondition() {
    return {
        id: _.uniqueId(`paymentImportOperationRule`),
        type: RuleConditionObject.PaymentPurpose,
        operator: RuleEqualVariant.Contains,
        contractorName: ``,
        paymentPurpose: ``
    };
}

/**
 * Конструктор для создания условия заполненного данными по умолчанию
 * @returns {Condition}
 * @constructor
 */
export function EmptyTaxationSystemTypeCondition() {
    return {
        id: _.uniqueId(`paymentImportOperationRule`),
        type: RuleConditionObject.OperationType,
        operator: RuleEqualVariant.Equal,
        operationType: null,
        contractorName: ``,
        paymentPurpose: ``
    };
}

/**
 * Конструктор для создания условия заполненного данными по умолчанию
 * @returns {Condition}
 * @constructor
 */
export function EmptyIgnoreNumberCondition() {
    return {
        id: _.uniqueId(`paymentImportOperationRule`),
        type: RuleConditionObject.OperationType,
        operator: RuleEqualVariant.Equal,
        operationType: null,
        contractorName: ``,
        paymentPurpose: ``
    };
}

/**
 * Конструктор для создания условия заполненного данными по умолчанию
 * @returns {Condition}
 * @constructor
 */
export function EmptyMediationCondition() {
    return {
        id: _.uniqueId(`paymentImportOperationRule`),
        type: RuleConditionObject.OperationType,
        operator: RuleEqualVariant.Equal,
        operationType: null,
        contractorName: ``,
        paymentPurpose: ``
    };
}

/**
 * @param {Array<Condition>} allConditions массив условий
 * @param {number} id идентификатор условия из массива allConditions
 * @param {Partial<Condition>} patch набор изменений которые применятся к элементу с Id id
 * @returns {Array<Condition>} массив allConditions с обновленным элементом
 */
export function getConditionsAndUpdateById(allConditions, id, patch) {
    const needValidate = Object.keys(patch).some(patchKey => affectedConditionValidationFields.includes(patchKey));

    return allConditions.map(cond => {
        if (cond.id === id) {
            const updatedData = Object.assign(cond, patch);

            return needValidate ? validateOneCondition(updatedData) : updatedData;
        }

        return cond;
    });
}

/**
 * Возвращает переданный список условий со всеми элементами кроме того чей id == переданному id.
 * Если после удаления остается пустой список то возвращается одно чистое условие
 * @param {Array<Condition>} allConditions
 * @param {number} id
 * @returns {Array<Condition>} Список условий
 */
export function getConditionsWithoutOneById(allConditions, id) {
    const result = allConditions.filter(x => x.id !== id);

    if (!result.length) {
        return [new EmptyPaymentOperationTypeCondition()];
    }

    return result;
}

/**
 * Возвращает позитивный кейс по типу условия
 * @param {RuleConditionObject} type
 * @returns {RuleEqualVariant<number>}
 */
export function getPositiveEqualForType(type) {
    switch (type) {
        case RuleConditionObject.Contractor:
            return RuleEqualVariant.Equal;
        case RuleConditionObject.PaymentPurpose:
            return RuleEqualVariant.Contains;
        default:
            return RuleEqualVariant.Equal;
    }
}

/**
 * Возвращает И или ИЛИ по переданному conditionType
 * @param {RuleConditionType} conditionType
 * @returns {string}
 */
export function getConditionTypeOperandName(conditionType) {
    return conditionType === RuleConditionType.And ? `и` : `или`;
}

export function filterConditionsAvailableForContractor(allConditions, availableWithContractor) {
    const result = availableWithContractor
        ? allConditions
        : allConditions.filter(x => x.type !== RuleConditionObject.Contractor);

    if (!result.length) {
        result.push(new EmptyPaymentOperationTypeCondition());
    }

    return result;
}
