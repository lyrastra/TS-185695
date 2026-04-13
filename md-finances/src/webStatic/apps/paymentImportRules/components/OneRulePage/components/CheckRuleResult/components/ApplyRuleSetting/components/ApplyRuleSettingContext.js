import React from 'react';
import PropTypes from 'prop-types';

const defaultContext = {
    visible: false,
    checked: false,
    startDate: ``,
    minStartDate: ``,
    onCheckedChange: () => {},
    onStartDateChange: () => {}
};

const ApplyRuleSettingContext = React.createContext(defaultContext);

export const useApplyRuleSettingContext = () => React.useContext(ApplyRuleSettingContext);

export const ApplyRuleSettingContextProvider = ({ value, children }) => {
    return <ApplyRuleSettingContext.Provider value={value}>
        {children}
    </ApplyRuleSettingContext.Provider>;
};

ApplyRuleSettingContextProvider.propTypes = {
    value: PropTypes.exact({
        visible: PropTypes.bool,
        checked: PropTypes.bool,
        startDate: PropTypes.string,
        minStartDate: PropTypes.string,
        onCheckedChange: PropTypes.func,
        onStartDateChange: PropTypes.func
    }),
    children: PropTypes.node
};

export default { useApplyRuleSettingContext, ApplyRuleSettingContextProvider };
