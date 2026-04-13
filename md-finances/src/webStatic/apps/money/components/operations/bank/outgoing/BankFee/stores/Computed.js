import { computed, override } from 'mobx';
import CommonOperationStore from '../../../common/CommonOperationStore';

class Computed extends CommonOperationStore {
    @override get canDownload() {
        return false;
    }

    @override get isSavingBlocked() {
        return !!this.model.TaxPostings.Error || this.taxPostingsLoading
            || !!this.model.AccountingPostings.Error || this.accountingPostingsLoading
            || this.savePaymentPending || this.sendToBankPending;
    }

    @computed get isNotTaxable() {
        return !!this.getTaxPostingsExplainingMsg();
    }

    @computed get transferSettlementAccountList() {
        return this.SettlementAccounts.filter(account => account.Id !== this.model.SettlementAccountId);
    }

    @override get hasTaxPostings() {
        return true;
    }
}

export default Computed;
