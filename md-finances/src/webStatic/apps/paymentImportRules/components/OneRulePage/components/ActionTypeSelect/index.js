import React from 'react';
import PropTypes from 'prop-types';
import Dropdown from '@moedelo/frontend-core-react/components/dropdown/Dropdown';
import { getActionTypeDropdownData } from '../../../../helpers/dropdownDataHelpers';
import RuleActionType from '../../../../enums/RuleActionType';

const ActionTypeSelect = ({
    onChange, value, className, disabled, hasTaxationSystems, showMediation
}) => {
    const showTaxationSystems = value === RuleActionType.ChangeTaxationSystem || hasTaxationSystems;

    return <Dropdown
        data={getActionTypeDropdownData(showTaxationSystems, showMediation)}
        width="100%"
        onSelect={({ value: val }) => onChange(val)}
        className={className}
        value={value}
        disabled={disabled}
    />;
};

ActionTypeSelect.defaultProps = {
    disabled: false,
    hasTaxationSystems: false
};

ActionTypeSelect.propTypes = {
    className: PropTypes.string,
    onChange: PropTypes.func.isRequired,
    value: PropTypes.number,
    disabled: PropTypes.bool,
    hasTaxationSystems: PropTypes.bool,
    showMediation: PropTypes.bool
};

export default ActionTypeSelect;
