import { computed, override } from 'mobx';

/* for try */
import CommonOperationStore from '../../IncomingCommonOperationStore/CommonOperationStore';

class IncomingFromPurseComputed extends CommonOperationStore {
    /* override */
    @computed get isNotTaxable() {
        return true;
    }

    /* override */
    @override get isSavingBlocked() {
        return !!this.model.AccountingPostings.Error || this.accountingPostingsLoading || this.savePaymentPending;
    }
}

export default IncomingFromPurseComputed;
