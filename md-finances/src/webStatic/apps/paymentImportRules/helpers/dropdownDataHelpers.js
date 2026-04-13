import RuleConditionObject from '../enums/RuleConditionObject';
import RuleEqualVariant from '../enums/RuleEqualVariant';
import RuleActionType from '../enums/RuleActionType';
import RuleActionTypesResource from '../resources/RuleActionTypesResource';
import ConditionTypeTextResource from '../resources/ConditionTypeTextResource';
import { getConditions } from './settingsHelper';

export function getConditionTypeDropdownData(type) {
    const conditions = getConditions(type);
    const result = [];

    if (!conditions.length) {
        return result;
    }

    conditions.forEach(conditionType => {
        const conditionTypeText = ConditionTypeTextResource[conditionType];

        !!conditionTypeText && result.push({
            text: conditionTypeText,
            value: conditionType
        });
    });

    return result;
}

export function getTypesDropdownData(showContractorCondition, showOperationTypeCondition) {
    const data = [{
        text: `Назначение платежа`,
        value: RuleConditionObject.PaymentPurpose
    }];

    if (showContractorCondition) {
        data.push({
            text: `Контрагент`,
            value: RuleConditionObject.Contractor
        });
    }

    if (showOperationTypeCondition) {
        data.push({
            text: `Тип операции`,
            value: RuleConditionObject.OperationType
        });
    }

    return data;
}

export function getEqualDropdownData(type, hideNegativeConditions) {
    if (type === RuleConditionObject.OperationType) {
        return [
            {
                text: `Совпадает`,
                value: RuleEqualVariant.Equal
            }
        ];
    }

    if (type === RuleConditionObject.PaymentPurpose) {
        const data = [
            {
                text: `Содержит`,
                value: RuleEqualVariant.Contains
            },
            {
                text: `Не содержит`,
                value: RuleEqualVariant.NotContains
            }
        ];

        if (hideNegativeConditions) {
            data.splice(1, 1);
        }

        return data;
    }

    const data = [
        {
            text: `Совпадает с`,
            value: RuleEqualVariant.Equal
        },
        {
            text: `Не совпадает с`,
            value: RuleEqualVariant.NotEqual
        }
    ];

    if (hideNegativeConditions) {
        data.splice(1, 1);
    }

    return data;
}

export function getActionTypeDropdownData(showTaxationSystems, showMediation) {
    const data = [{
        text: RuleActionTypesResource[RuleActionType.ChangeOperationType],
        value: RuleActionType.ChangeOperationType
    }];
    const addActionType = actionType => data.push({
        text: RuleActionTypesResource[actionType],
        value: actionType
    });

    if (showTaxationSystems) {
        addActionType(RuleActionType.ChangeTaxationSystem);
    }

    addActionType(RuleActionType.ChangeIgnoreNumber);

    if (showMediation) {
        addActionType(RuleActionType.ChangeMediation);
    }

    return data;
}
