import React, { useEffect } from 'react';
import PropTypes from 'prop-types';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import { mapOperationsToDropdownData } from '../../../../helpers/paymentImportRulesMapper';

const OperationTypeSelect = ({
    operationTypes, value, onChange, className, disabled, error
}) => {
    useEffect(() => {
        if (!operationTypes) {
            return;
        }

        const dataContainsValue = operationTypes.some(x => x.OperationType === value);

        if (value && !dataContainsValue) {
            onChange(null);
        }
    }, [operationTypes, value]);

    const dropDownData = operationTypes && mapOperationsToDropdownData(operationTypes);

    return <Dropdown
        placeholder="Укажите тип"
        data={dropDownData || []}
        width="100%"
        onSelect={({ value: val }) => onChange(val)}
        className={className}
        value={value}
        disabled={disabled}
        error={!!error}
        message={error}
        loading={operationTypes == null}
        allowEmpty
    />;
};

OperationTypeSelect.defaultProps = {
    disabled: false
};

OperationTypeSelect.propTypes = {
    operationTypes: PropTypes.arrayOf(PropTypes.shape({
        OperationType: PropTypes.number.isRequired,
        Direction: PropTypes.oneOf(Object.values(Direction)).isRequired,
        Description: PropTypes.string.isRequired
    })),
    className: PropTypes.string,
    onChange: PropTypes.func.isRequired,
    value: PropTypes.number,
    disabled: PropTypes.bool,
    error: PropTypes.string
};

export default OperationTypeSelect;
