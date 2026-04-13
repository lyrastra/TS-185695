export const mapPaymentRulesToDropdownDate = paymentRules => {
    if (!paymentRules) {
        return [];
    }

    return paymentRules.map(paymentRule => ({
        text: paymentRule.Name,
        value: `/Finances/PaymentImportRules#edit/${paymentRule.Id}`
    }));
};

export const mapOutsourcePaymentRulesToDropdownDate = (paymentRules, isOutsourceRuleDisabled) => {
    if (!paymentRules) {
        return [];
    }

    return paymentRules.map(paymentRule => ({
        text: paymentRule.Name,
        value: `/outsource/paymentImportRules/edit/${paymentRule.Id}`,
        disabled: isOutsourceRuleDisabled
    }));
};

export default { mapPaymentRulesToDropdownDate, mapOutsourcePaymentRulesToDropdownDate };
