import { override } from 'mobx';
import CommonOperationStore from '../../IncomingCommonOperationStore/CommonOperationStore';

class Computed extends CommonOperationStore {
    @override get isSavingBlocked() {
        return !!this.model.AccountingPostings.Error || this.accountingPostingsLoading || this.savePaymentPending;
    }
}

export default Computed;
