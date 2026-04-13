import { computed, override } from 'mobx';
import CommonOperationStore from '../../../common/CommonOperationStore';

class Computed extends CommonOperationStore {
    @override get canDownload() {
        return false;
    }

    @override get isSavingBlocked() {
        return !!this.model.AccountingPostings.Error || this.accountingPostingsLoading
            || this.savePaymentPending || this.sendToBankPending;
    }

    @computed get isNotTaxable() {
        return true;
    }
}

export default Computed;
