import { computed, override } from 'mobx';

/* for try */
import CommonOperationStore from '../../../common/CommonOperationStore';

class Computed extends CommonOperationStore {
    @override get canCopy() {
        const { DocumentBaseId, CanCopy } = this.model;

        return DocumentBaseId > 0 && CanCopy;
    }

    @override get canDownload() {
        return this.model.DocumentBaseId > 0;
    }

    @override get showDeleteIcon() {
        return this.model.DocumentBaseId > 0 && this.canEdit;
    }

    @computed get isNotTaxable() {
        return true;
    }

    @override get isSavingBlocked() {
        return !!this.model.AccountingPostings.Error || this.accountingPostingsLoading
            || this.savePaymentPending || this.sendToBankPending;
    }
}

export default Computed;
