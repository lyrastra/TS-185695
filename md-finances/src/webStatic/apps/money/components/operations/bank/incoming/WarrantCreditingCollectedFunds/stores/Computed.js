import { computed, override } from 'mobx';

/* for try */
import CommonOperationStore from '../../IncomingCommonOperationStore/CommonOperationStore';

class WarrantCreditingCollectedFundsActionsComputed extends CommonOperationStore {
    @computed get showMediationCommission() {
        return this.model.IsMediation;
    }

    @override get canDownload() {
        return false;
    }

    @override get isSavingBlocked() {
        return !!this.model.AccountingPostings.Error || this.accountingPostingsLoading || this.savePaymentPending;
    }
}

export default WarrantCreditingCollectedFundsActionsComputed;
