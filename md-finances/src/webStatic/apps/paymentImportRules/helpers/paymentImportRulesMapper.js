import _ from 'underscore';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import RuleConditionObject from '../enums/RuleConditionObject';
import RuleActionTypesResource from '../resources/RuleActionTypesResource';
import RuleConditionOperator from '../enums/RuleConditionOperator';
import RuleEqualVariant from '../enums/RuleEqualVariant';
import TaxableSumTypeEnum from '../enums/TaxableSumTypeEnum';

const dateFormat = `DD.MM.YYYY`;
const dateFormatForSave = `YYYY-MM-DD`;

export function mapBackPaymentImportRuleToTableData(backModel) {
    return {
        key: backModel.Id,
        id: backModel.Id,
        creationDate: dateHelper(backModel.CreateDate).format(dateFormat),
        startDate: dateHelper(backModel.StartDate).format(dateFormat),
        name: backModel.Name,
        description: RuleActionTypesResource[backModel.ActionType],
        type: backModel.Description
    };
}

function getConditionOperator(type, operator) {
    if (type === RuleConditionObject.Contractor) {
        if (operator === RuleEqualVariant.Equal) {
            return RuleConditionOperator.ContractorEqueal;
        }

        if (operator === RuleEqualVariant.NotEqual) {
            return RuleConditionOperator.ContractorNotEqueal;
        }
    }

    if (type === RuleConditionObject.PaymentPurpose) {
        if (operator === RuleEqualVariant.Contains) {
            return RuleConditionOperator.PaymentPurposeContain;
        }

        if (operator === RuleEqualVariant.NotContains) {
            return RuleConditionOperator.PaymentPurposeNotContain;
        }
    }

    if (type === RuleConditionObject.OperationType) {
        if (operator === RuleEqualVariant.Equal) {
            return RuleConditionOperator.OperationTypeEqual;
        }
    }

    return null;
}

export function buildRuleModelForSave(ruleData, conditionsData) {
    return {
        Name: ruleData.name,
        ConditionType: ruleData.conditionType,
        ConditionList: conditionsData.map(condition => {
            let value;

            switch (condition.type) {
                case RuleConditionObject.Contractor: {
                    value = condition.contractorId;

                    break;
                }

                case RuleConditionObject.PaymentPurpose: {
                    value = condition.paymentPurpose;

                    break;
                }

                case RuleConditionObject.OperationType: {
                    value = condition.operationType;

                    break;
                }

                default:
                    value = null;
            }

            return {
                Operator: getConditionOperator(condition.type, condition.operator),
                Value: value
            };
        }),
        ActionType: ruleData.actionType,
        OperationType: ruleData.operationType,
        TaxationSystem: ruleData.taxationSystemType,
        StartDate: dateHelper(ruleData.startDate).format(dateFormatForSave),
        ApplyToOperations: ruleData.applyToOperations,
        EmployeeId: ruleData.employeeId,
        ContractId: ruleData.contractId,
        TaxableSumType: ruleData.taxableSumType || null,
        SyntheticAccountCode: ruleData.syntheticAccountCode?.Code || ruleData.syntheticAccountCode
    };
}

export function buildRuleModelForCheck(ruleData, conditionsData) {
    return {
        ConditionType: ruleData.conditionType,
        ConditionList: conditionsData.map(condition => {
            let value;

            switch (condition.type) {
                case RuleConditionObject.Contractor: {
                    value = condition.contractorId;

                    break;
                }

                case RuleConditionObject.PaymentPurpose: {
                    value = condition.paymentPurpose;

                    break;
                }

                case RuleConditionObject.OperationType: {
                    value = condition.operationType;

                    break;
                }

                default:
                    value = null;
            }

            return {
                Operator: getConditionOperator(condition.type, condition.operator),
                Value: value
            };
        }),
        ActionType: ruleData.actionType,
        OperationType: ruleData.operationType,
        TaxationSystem: ruleData.taxationSystemType,
        StartDate: dateHelper(ruleData.startDate).format(dateFormat)
    };
}

export function mapBackPaymentImportRuleToFrontModel(backModel) {
    return {
        id: backModel.Id,
        name: backModel.Name,
        conditionType: backModel.ConditionType,
        operationType: backModel.OperationType,
        taxationSystemType: backModel.TaxationSystem,
        actionType: backModel.ActionType,
        conditions: backModel.ConditionResponseList.map(mapBackRuleConditionToFrontModel),
        isDeleted: backModel.IsDeleted,
        employeeId: backModel.EmployeeId,
        contractId: backModel.ContractId,
        startDate: dateHelper(backModel.StartDate).format(dateFormat),
        taxableSumType: backModel.TaxableSumType || TaxableSumTypeEnum.Empty,
        syntheticAccountCode: backModel.SyntheticAccountCode
    };
}

function getOperatorAndType(operator) {
    if (operator === RuleConditionOperator.ContractorEqueal) {
        return {
            type: RuleConditionObject.Contractor,
            operator: RuleEqualVariant.Equal
        };
    }

    if (operator === RuleConditionOperator.ContractorNotEqueal) {
        return {
            type: RuleConditionObject.Contractor,
            operator: RuleEqualVariant.NotEqual
        };
    }

    if (operator === RuleConditionOperator.PaymentPurposeContain) {
        return {
            type: RuleConditionObject.PaymentPurpose,
            operator: RuleEqualVariant.Contains
        };
    }

    if (operator === RuleConditionOperator.PaymentPurposeNotContain) {
        return {
            type: RuleConditionObject.PaymentPurpose,
            operator: RuleEqualVariant.NotContains
        };
    }

    if (operator === RuleConditionOperator.OperationTypeEqual) {
        return {
            type: RuleConditionObject.OperationType,
            operator: RuleEqualVariant.Equal
        };
    }

    return { type: null, operator: null };
}

export function mapBackRuleConditionToFrontModel(backModel) {
    const { type, operator } = getOperatorAndType(backModel.Operator);

    return {
        id: _.uniqueId(`paymentImportOperationRule`),
        type,
        operator,
        contractorName: backModel.ContractorName,
        contractorId: type === RuleConditionObject.Contractor ? parseInt(backModel.Value, 10) : null,
        paymentPurpose: type === RuleConditionObject.PaymentPurpose ? backModel.Value : null,
        operationType: type === RuleConditionObject.OperationType ? parseInt(backModel.Value, 10) : null
    };
}

function mapOperationToDropdown(backModelOperation) {
    return {
        text: backModelOperation.Description,
        value: backModelOperation.OperationType
    };
}

export function mapOperationsToDropdownData(backModel) {
    const outgoingOperations = backModel.filter(x => x.Direction === Direction.Outgoing).map(mapOperationToDropdown);
    const incomingOperations = backModel.filter(x => x.Direction === Direction.Incoming).map(mapOperationToDropdown);

    return [
        {
            title: `Списания`,
            data: outgoingOperations
        },
        {
            title: `Поступления`,
            data: incomingOperations
        }
    ];
}

export function mapTaxationSystemsData(backModel) {
    return backModel.map(model => {
        return {
            text: model.Description,
            value: model.Type
        };
    });
}

export function mapConditionOperationTypes(backModel) {
    if (!backModel) {
        return [];
    }

    return backModel.map(model => {
        return {
            text: model.OperationDescription,
            value: model.OperationType
        };
    });
}

export function mapBackOperationsAffectedByRuleToFront(backData) {
    return {
        id: backData.DocumentBaseId,
        date: backData.Date,
        number: backData.Number,
        contractorName: backData.ContractorName,
        purpose: backData.PaymentPurpose,
        operationName: backData.OperationDescription,
        sum: backData.Sum,
        direction: backData.Direction,
        currencyCode: backData.Currency
    };
}
