import SyntheticAccountCodesEnum from '../../../../../../../../../../enums/SyntheticAccountCodesEnum';
import { cashOrderOperationResources } from '../../../../../../../../../../resources/MoneyOperationTypeResources';

export function isUnifiedBP({ BudgetaryTaxesAndFees, OperationType } = {}) {
    return (Number(OperationType) === cashOrderOperationResources.CashOrderBudgetaryPayment.value
        || Number(OperationType) === cashOrderOperationResources.UnifiedCashOrderBudgetaryPayment.value)
        && Number(BudgetaryTaxesAndFees) === SyntheticAccountCodesEnum.unified;
}

export function isSubPaymentEmpty(data) {
    return !data.AccountCode && !data.Sum;
}
