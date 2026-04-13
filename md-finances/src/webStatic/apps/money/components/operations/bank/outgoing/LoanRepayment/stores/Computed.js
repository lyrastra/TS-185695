import { computed, override } from 'mobx';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';

/* for try */
import CommonOperationStore from '../../../common/CommonOperationStore';

class Computed extends CommonOperationStore {
    @computed get isNotTaxable() {
        return !!this.getTaxPostingsExplainingMsg();
    }

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
        return !!this.model.TaxPostings.Error || this.taxPostingsLoading
            || !!this.model.AccountingPostings.Error || this.accountingPostingsLoading
            || this.savePaymentPending || this.sendToBankPending;
    }

    @override get hasTaxPostings() {
        return true;
    }
}

export default Computed;
