import React from 'react';
import cn from 'classnames';
import PropTypes from 'prop-types';
import grid from '@moedelo/frontend-core-v2/styles/grid.m.less';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import P from '@moedelo/frontend-core-react/components/P';
import IconButton from '@moedelo/frontend-core-react/components/IconButton';
import Input from '@moedelo/frontend-core-react/components/Input';
import style from './style.m.less';
import { getEqualDropdownData, getTypesDropdownData } from '../../../../helpers/dropdownDataHelpers';
import ContractorAutocomplete from '../ContractorAutocomplete';
import CommissionAgentAutocomplete from '../CommissionAgentAutocomplete';
import RuleConditionObject from '../../../../enums/RuleConditionObject';
import RuleEqualVariant from '../../../../enums/RuleEqualVariant';
import RuleConditionType from '../../../../enums/RuleConditionType';
import { getConditionTypeOperandName, getPositiveEqualForType } from '../../../../helpers/conditionsHelper';

const ConditionRow = ({
    index,
    conditionType = RuleConditionType.And,
    condition,
    disabled,
    onChange,
    onDelete,
    showContractorCondition,
    showCommissionAgentCondition,
    negativeConditionsIsHidden,
    operationTypes,
    requiredConditionObject,
    kontragentForQuery
}) => {
    const {
        id,
        type,
        operator,
        paymentPurpose,
        operationType,
        contractorName,
        contractorError,
        paymentPurposeError,
        operationTypeError
    } = condition;
    const isOperationTypeConditionVisible = React.useMemo(() => {
        return index === 0 && requiredConditionObject === RuleConditionObject.OperationType;
    }, [index, requiredConditionObject]);
    const isOperationTypeConditionShownAsText = React.useMemo(() => {
        return index === 0 && requiredConditionObject !== null && requiredConditionObject !== undefined;
    }, [index, requiredConditionObject]);

    const renderContractorAutocomplete = () => {
        if (type === RuleConditionObject.Contractor) {
            if (showCommissionAgentCondition) {
                return <CommissionAgentAutocomplete
                    className={cn(grid.col_12, `qa-conditionContractor`)}
                    onChange={({ Id, Name }) => { onChange(id, { contractorId: Id, contractorName: Name }); }}
                    value={contractorName}
                    disabled={disabled}
                    error={contractorError}
                    kontragentName={kontragentForQuery}
                />;
            }

            return <ContractorAutocomplete
                className={cn(grid.col_12, `qa-conditionContractor`)}
                onChange={({ Id, Name }) => { onChange(id, { contractorId: Id, contractorName: Name }); }}
                value={contractorName}
                disabled={disabled}
                error={contractorError}
                kontragentName={kontragentForQuery}
            />;
        }

        return null;
    };

    return <div className={cn(grid.row, `qa-conditionRow`)}>
        <P className={cn(grid.col_2, style.logicalOperatorText)}>
            {index > 0 && getConditionTypeOperandName(conditionType)}
        </P>
        <Dropdown
            className={cn(grid.col_5, `qa-conditionType`)}
            data={getTypesDropdownData(showContractorCondition, isOperationTypeConditionVisible)}
            value={type}
            onSelect={({ value }) => onChange(id, { type: value, operator: getPositiveEqualForType(value) })}
            disabled={disabled}
            showAsText={!disabled && isOperationTypeConditionShownAsText}
        />
        <Dropdown
            className={cn(grid.col_4, `qa-conditionEqual`)}
            data={getEqualDropdownData(type, negativeConditionsIsHidden)}
            onSelect={({ value }) => onChange(id, { operator: value })}
            value={index === 0 ? getPositiveEqualForType(type) : operator}
            showAsText={!disabled && index === 0}
            disabled={disabled}
        />
        {renderContractorAutocomplete()}
        {
            type === RuleConditionObject.PaymentPurpose && <Input
                className={cn(grid.col_12, `qa-conditionPaymentPurpose`)}
                placeholder="Ключевое слово или словосочетание"
                maxLength={40}
                onChange={({ value }) => { onChange(id, { paymentPurpose: value }); }}
                value={paymentPurpose}
                disabled={disabled}
                error={!!paymentPurposeError}
                message={paymentPurposeError}
            />
        }
        {
            type === RuleConditionObject.OperationType && <Dropdown
                className={cn(grid.col_12, `qa-conditionOperationType`)}
                placeholder="Укажите тип операции"
                data={operationTypes}
                width="100%"
                onSelect={({ value }) => { onChange(id, { operationType: value }); }}
                value={operationType}
                disabled={disabled}
                error={!!operationTypeError}
                message={operationTypeError}
            />
        }
        <div className={grid.col_1}>
            {index > 0 && <IconButton
                onClick={() => { onDelete(id); }}
                className="qa-conditionDelete"
                icon="clear"
                disabled={disabled}
            />}
        </div>
    </div>;
};

ConditionRow.defaultProps = {
    disabled: false,
    operationTypes: [],
    showCommissionAgentCondition: false
};

ConditionRow.propTypes = {
    index: PropTypes.number.isRequired,
    conditionType: PropTypes.oneOf(Object.values(RuleConditionType)),
    operationTypes: PropTypes.arrayOf(PropTypes.shape({
        text: PropTypes.string.isRequired,
        value: PropTypes.number.isRequired
    })),
    condition: PropTypes.shape({
        id: PropTypes.node.isRequired,
        type: PropTypes.oneOf(Object.values(RuleConditionObject)),
        operator: PropTypes.oneOf(Object.values(RuleEqualVariant)),
        contractorName: PropTypes.string,
        contractorId: PropTypes.number,
        paymentPurpose: PropTypes.string,
        contractorError: PropTypes.string,
        paymentPurposeError: PropTypes.string,
        operationTypeError: PropTypes.string,
        operationType: PropTypes.number
    }).isRequired,
    onChange: PropTypes.func.isRequired,
    onDelete: PropTypes.func.isRequired,
    showContractorCondition: PropTypes.bool,
    showCommissionAgentCondition: PropTypes.bool,
    negativeConditionsIsHidden: PropTypes.bool,
    requiredConditionObject: PropTypes.oneOf(Object.values(RuleConditionObject)),
    disabled: PropTypes.bool,
    /** комиссионер из договора для корректного запроса комиссионера */
    kontragentForQuery: PropTypes.string
};

export default ConditionRow;
