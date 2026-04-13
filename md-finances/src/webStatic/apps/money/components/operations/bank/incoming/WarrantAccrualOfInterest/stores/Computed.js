import { computed, override } from 'mobx';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

/* for try */
import CommonOperationStore from '../../IncomingCommonOperationStore/CommonOperationStore';

class WarrantAccrualOfInterestComputed extends CommonOperationStore {
    @override get canDownload() {
        return false;
    }

    @override get isSavingBlocked() {
        return !!this.model.TaxPostings.Error || this.taxPostingsLoading
            || !!this.model.AccountingPostings.Error || this.accountingPostingsLoading || this.savePaymentPending;
    }

    /* override */
    @computed get isNotTaxable() {
        return !!this.getTaxPostingsExplainingMsg();
    }

    @override get hasTaxPostings() {
        return true;
    }

    @computed get canShowTooltip() {
        return this.canShowTaxationSystemTypeDropdown && !this.Requisites.IsOoo && dateHelper(this.model.Date).year() >= 2026;
    }
}

export default WarrantAccrualOfInterestComputed;
