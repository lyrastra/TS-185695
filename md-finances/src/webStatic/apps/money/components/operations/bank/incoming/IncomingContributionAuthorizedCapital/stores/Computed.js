import { computed, override } from 'mobx';

/* for try */
import CommonOperationStore from '../../IncomingCommonOperationStore/CommonOperationStore';

class Computed extends CommonOperationStore {
    /* override */
    @computed get isNotTaxable() {
        return true;
    }

    @override get isSavingBlocked() {
        return !!this.model.AccountingPostings.Error || this.accountingPostingsLoading || this.savePaymentPending;
    }
}

export default Computed;
