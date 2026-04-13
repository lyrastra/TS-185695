import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import RuleConditionType from '../enums/RuleConditionType';
import RuleActionType from '../enums/RuleActionType';
import TaxableSumTypeEnum from '../enums/TaxableSumTypeEnum';

const today = dateHelper().format(`DD.MM.YYYY`);

/**
 * Модель по умолчанию для действия по определению типа операции
 * @type {ActionModel}
 */
export const defaultOperationTypeModel = {
    conditionType: RuleConditionType.And,
    operationType: null,
    taxationSystemType: null,
    actionType: RuleActionType.ChangeOperationType,
    conditions: [],
    startDate: today,
    applyToOperations: false,
    employeeId: null,
    contractId: null,
    taxableSumType: TaxableSumTypeEnum.Empty
};

/**
 * Модель по умолчанию для действия по определению системы налогообложения
 * @type {ActionModel}
 */
export const defaultTaxationSystemTypeModel = {
    conditionType: RuleConditionType.And,
    operationType: null,
    taxationSystemType: null,
    actionType: RuleActionType.ChangeTaxationSystem,
    conditions: [],
    startDate: today,
    applyToOperations: false
};

/**
 * Модель по умолчанию для действия по определению признака "Без нумерации"
 * @type {ActionModel}
 */
export const defaultIgnoreNumberTypeModel = {
    conditionType: RuleConditionType.And,
    operationType: null,
    taxationSystemType: null,
    actionType: RuleActionType.ChangeIgnoreNumber,
    conditions: [],
    startDate: today,
    applyToOperations: false
};

/**
 * Модель по умолчанию для действия по определению признака "Посредничество"
 * @type {ActionModel}
 */
export const defaultMediationTypeModel = {
    conditionType: RuleConditionType.And,
    operationType: null,
    taxationSystemType: null,
    actionType: RuleActionType.ChangeMediation,
    conditions: [],
    startDate: today,
    applyToOperations: false
};

export default { defaultOperationTypeModel, defaultTaxationSystemTypeModel, defaultMediationTypeModel };
