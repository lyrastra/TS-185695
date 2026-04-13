/**
 * Настройки действия для каждого RuleActionType
 * @typedef {Object<string, any>} ActionSetting
 * @property {RuleConditionType[]} conditionTypes                - список возможных вариантов комбинаций условий
 * @property {ConditionConstructor} defaultConditionConstructor  - функция-конструктор, возвращающая объект условий по умолчанию для действия
 * @property {ActionModel} defaultModel                          - модель по умолчанию для действия
 * @property {boolean} negativeConditionsIsHidden                - необходимо ли скрывать отрицательные варианты условий
 * @property {RuleConditionObject|null} requiredConditionObject  - установка обязательного объекта условия, который нельзя изменить или удалить
 */
