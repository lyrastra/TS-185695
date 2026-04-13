import RuleConditionObject from '../enums/RuleConditionObject';
import RuleActionType from '../enums/RuleActionType';

const ruleErrors = {
    nameError: `Заполните название правила`,
    operationTypeError: `Выберите тип операции`,
    taxationSystemTypeError: `Выберите тип системы налогообложения`
};

const conditionErrors = {
    contractorError: `Выберите контрагента`,
    paymentPurposeError: `Заполните назначение платежа`,
    operationTypeError: `Заполните тип операции`
};

/** Поля при изменении которых стоит проводить валидацию */
export const affectedRuleValidationFields = [`name`, `operationType`, `taxationSystemType`];
export const affectedConditionValidationFields = [`contractorId`, `paymentPurpose`, `operationType`];

export function validateRule(data, field = null) {
    return {
        ...data,
        nameError: (!field || field === `name`) && !(data.name?.length) ? ruleErrors.nameError : null,
        operationTypeError: data.actionType === RuleActionType.ChangeOperationType && (!field || field === `operationType`) && !data.operationType ? ruleErrors.operationTypeError : null,
        taxationSystemTypeError: data.actionType === RuleActionType.ChangeTaxationSystem && (!field || field === `taxationSystemType`) && !data.taxationSystemType ? ruleErrors.taxationSystemTypeError : null
    };
}

export function validateOneCondition(condition) {
    return {
        ...condition,
        contractorError: condition.type === RuleConditionObject.Contractor && !condition.contractorId
            ? conditionErrors.contractorError
            : null,
        paymentPurposeError: condition.type === RuleConditionObject.PaymentPurpose && !condition?.paymentPurpose?.length
            ? conditionErrors.paymentPurposeError
            : null,
        operationTypeError: condition.type === RuleConditionObject.OperationType && !condition?.operationType
            ? conditionErrors.operationTypeError
            : null
    };
}

export function validateConditions(conditions) {
    return conditions.map(validateOneCondition);
}

export function isValidForSave(ruleData, conditions) {
    // в общих данных правила нет ошибок
    const isRuleDataValid = Object.keys(ruleErrors).every(errorKey => !ruleData[errorKey]);

    // не в одном из условий нет ошибок
    const isConditionsDataValid = Object.keys(conditionErrors).every(errorKey => conditions.every(condition => !condition[errorKey]));

    return isRuleDataValid && isConditionsDataValid;
}
