import RuleActionType from '../enums/RuleActionType';
import {
    defaultOperationTypeModel,
    defaultTaxationSystemTypeModel,
    defaultIgnoreNumberTypeModel,
    defaultMediationTypeModel
} from '../resources/DefaultModelResource';
import RuleConditionType from '../enums/RuleConditionType';
import RuleConditionObject from '../enums/RuleConditionObject';
import {
    EmptyIgnoreNumberCondition,
    EmptyPaymentOperationTypeCondition,
    EmptyTaxationSystemTypeCondition,
    EmptyMediationCondition
} from '../helpers/conditionsHelper';

/**
 * Настройки действия по назначению типа операции
 * @type ActionSetting
 */
const changeOperationTypeSettings = {
    defaultModel: defaultOperationTypeModel,
    defaultConditionConstructor: EmptyPaymentOperationTypeCondition,
    conditionTypes: [RuleConditionType.And, RuleConditionType.Or],
    requiredConditionObject: null,
    negativeConditionsIsHidden: false
};

/**
 * Настройки действия по назначению системы налогообложения
 * @type ActionSetting
 */
const changeTaxationSystemTypeSettings = {
    defaultModel: defaultTaxationSystemTypeModel,
    defaultConditionConstructor: EmptyTaxationSystemTypeCondition,
    conditionTypes: [RuleConditionType.And],
    requiredConditionObject: RuleConditionObject.OperationType,
    negativeConditionsIsHidden: true
};

/**
 * Настройки действия по назначению признака "Без категории"
 * @type ActionSetting
 */
const changeIgnoreNumberSettings = {
    defaultModel: defaultIgnoreNumberTypeModel,
    defaultConditionConstructor: EmptyIgnoreNumberCondition,
    conditionTypes: [RuleConditionType.And],
    requiredConditionObject: RuleConditionObject.OperationType,
    negativeConditionsIsHidden: false
};

/**
 * Настройки действия по назначению признака "Посредничество"
 * @type ActionSetting
 */
const changeMediationSettings = {
    defaultModel: defaultMediationTypeModel,
    defaultConditionConstructor: EmptyMediationCondition,
    conditionTypes: [RuleConditionType.And],
    requiredConditionObject: RuleConditionObject.OperationType,
    negativeConditionsIsHidden: false
};

/**
 * @type {Object<RuleActionType, ActionSetting>}
 */
export default {
    [RuleActionType.ChangeOperationType]: changeOperationTypeSettings,
    [RuleActionType.ChangeTaxationSystem]: changeTaxationSystemTypeSettings,
    [RuleActionType.ChangeIgnoreNumber]: changeIgnoreNumberSettings,
    [RuleActionType.ChangeMediation]: changeMediationSettings
};
