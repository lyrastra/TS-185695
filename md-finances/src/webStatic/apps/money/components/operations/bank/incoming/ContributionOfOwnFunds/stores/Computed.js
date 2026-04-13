import { computed, override } from 'mobx';

/* for try */
import CommonOperationStore from '../../IncomingCommonOperationStore/CommonOperationStore';
import StateMessageResource from '../../../../../../../../resources/newMoney/StateMessageResource';

class ContributionOfOwnFundsComputed extends CommonOperationStore {
    /* override */
    @computed get isNotTaxable() {
        return true;
    }

    /* override */
    @override get isSavingBlocked() {
        return !!this.model.AccountingPostings.Error || this.accountingPostingsLoading
            || this.savePaymentPending || this.sendToBankPending;
    }

    @computed get canShowBubble() {
        return !!StateMessageResource[this.model.OperationState];
    }
}

export default ContributionOfOwnFundsComputed;
