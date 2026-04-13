export const getCommonOperationStoreData = props => {
    return {
        requisites: props.requisites,
        settlementAccounts: props.settlementAccounts,
        taxationSystem: props.taxationSystem,
        hasPurseAccount: props.hasPurseAccount,
        operation: props.operation,
        ndsRatesFromAccPolicy: props.ndsRatesFromAccPolicy,
        userInfo: props.userInfo,
        onSave: props.onSave,
        viewPaymentNotificationPanel: props.viewPaymentNotificationPanel,
        setViewPaymentNotificationPanel: props.setViewPaymentNotificationPanel
    };
};

export const getOperationDataForTest = operationData => {
    return {
        operation: {},
        requisites: {},
        operationTypes: [],
        settlementAccounts: [],
        dsRatesFromAccPolicy: [],
        taxationSystem: {},
        userInfo: { AccessRuleFlags: {} },
        ...operationData
    };
};

export default {
    getCommonOperationStoreData
};
