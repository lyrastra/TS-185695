import { computed, override } from 'mobx';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';

/* for try */
import CommonOperationStore from '../../IncomingCommonOperationStore/CommonOperationStore';

class Computed extends CommonOperationStore {
    @computed get getDataFromContract() {
        const contractor = this.model.Kontragent || {};

        return {
            Direction: Direction.Incoming,
            KontragentId: contractor.KontragentId,
            KontragentName: contractor.KontragentName,
            ContractKind: 0,
            isReceivedOperation: true
        };
    }

    @override get isSavingBlocked() {
        return !!this.model.AccountingPostings.Error || this.accountingPostingsLoading || this.savePaymentPending;
    }
}

export default Computed;
