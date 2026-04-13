import RuleActionTypeSettingsResource from '../resources/RuleActionTypeSettingsResource';

/**
 * Возвращает модель по умолчанию для выбранного действия
 * @param {RuleActionType} type
 * @returns {ActionModel}
 */
export function getDefaultModelForActionType(type) {
    return getActionTypeSettingsItem(type, `defaultModel`);
}

/**
 * Возвращает список доступных условий для выбранного действия
 * @param {RuleActionType} type
 * @returns {RuleConditionType[]}
 */
export function getConditions(type) {
    return getActionTypeSettingsItem(type, `conditionTypes`);
}

/**
 * Возвращает пустую модель условий для выбранного действия
 * @param {RuleActionType} type
 * @returns {RuleConditionType[]}
 */
export function getEmptyCondition(type) {
    const Constructor = getActionTypeSettingsItem(type, `defaultConditionConstructor`);

    return new Constructor();
}

/**
 * Возвращает тип обязательного условия для выбранного действия
 * @param {RuleActionType} type
 * @returns {RuleConditionObject | null}
 */
export function getRequiredConditionObject(type) {
    return getActionTypeSettingsItem(type, `requiredConditionObject`);
}

/**
 * Возвращает признак необходимости скрытия отрицательный условий для выбранного действия
 * @param {RuleActionType} type
 * @returns {boolean}
 */
export function getNegativeConditionsIsHidden(type) {
    return getActionTypeSettingsItem(type, `negativeConditionsIsHidden`);
}

/**
 * Возвращает настройки для выбранного действия
 * @param {RuleActionType} type
 * @param {string} name - название поля из настроек
 */
function getActionTypeSettingsItem(type, name) {
    const settings = RuleActionTypeSettingsResource[type];

    if (!settings) {
        throw new Error(`Settings for actionType: ${type} is not defined. Please define settings in RuleActionSettingsResource.`);
    }

    const item = settings[name];

    if (item === undefined) {
        throw new Error(`${name} field for actionType: ${type} is not defined. Please define setting ${name} for field ${type} in RuleActionSettingsResource`);
    }

    return item;
}
