import React, { useEffect } from 'react';
import PropTypes from 'prop-types';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import { mapTaxationSystemsData } from '../../../../helpers/paymentImportRulesMapper';

const TaxationSystemTypeSelect = ({
    taxationSystemsData, value, onChange, className, disabled, error, operationType
}) => {
    const getTaxationSystems = () => taxationSystemsData?.find(tsd => tsd.OperationType === operationType)?.TaxationSystems ?? [];

    useEffect(() => {
        if (!taxationSystemsData) {
            return;
        }

        const dataContainsValue = getTaxationSystems().some(x => x.Type === value);

        if (value && !dataContainsValue) {
            onChange(null);
        }
    }, [taxationSystemsData, value]);

    const dropDownData = mapTaxationSystemsData(getTaxationSystems());

    return <Dropdown
        placeholder="Укажите систему налогообложения"
        data={dropDownData || []}
        width="100%"
        onSelect={({ value: val }) => onChange(val)}
        className={className}
        value={value}
        disabled={disabled}
        error={!!error}
        message={error}
        loading={taxationSystemsData == null}
        allowEmpty
    />;
};

TaxationSystemTypeSelect.defaultProps = {
    disabled: false
};

TaxationSystemTypeSelect.propTypes = {
    taxationSystemsData: PropTypes.arrayOf(PropTypes.shape({
        OperationType: PropTypes.number.isRequired,
        TaxationSystems: PropTypes.arrayOf(PropTypes.shape({
            Type: PropTypes.number.isRequired,
            Description: PropTypes.string.isRequired
        }).isRequired)
    })),
    className: PropTypes.string,
    onChange: PropTypes.func.isRequired,
    value: PropTypes.number,
    disabled: PropTypes.bool,
    error: PropTypes.string,
    operationType: PropTypes.number
};

export default TaxationSystemTypeSelect;
