import { computed, override } from 'mobx';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import ContractKind from '@moedelo/frontend-enums/mdEnums/ContractKind';

/* for try */
import CommonOperationStore from '../../../common/CommonOperationStore';

class Computed extends CommonOperationStore {
    @computed get isNotTaxable() {
        return !!this.getTaxPostingsExplainingMsg();
    }

    @computed get getDataFromContract() {
        const contractor = this.model.Kontragent || {};

        return {
            Direction: Direction.Outgoing,
            KontragentId: contractor.KontragentId,
            KontragentName: contractor.KontragentName,
            ContractKind: ContractKind.OutgoingLoan,
            isLoanIssueOperation: true
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
