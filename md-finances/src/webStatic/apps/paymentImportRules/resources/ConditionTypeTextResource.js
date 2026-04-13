import RuleConditionType from '../enums/RuleConditionType';

export default {
    [RuleConditionType.And]: `Все условия выполняются`,
    [RuleConditionType.Or]: `Одно из условий выполняется`
};
