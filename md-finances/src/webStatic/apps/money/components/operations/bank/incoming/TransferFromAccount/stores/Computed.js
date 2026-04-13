import { computed, override } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';

/* for try */
import CommonOperationStore from '../../IncomingCommonOperationStore/CommonOperationStore';
import { canAddMoreDocuments } from '../../../../../../../../helpers/newMoney/linkedDocumentsHelper';
import StateMessageResource from '../../../../../../../../resources/newMoney/StateMessageResource';

class Computed extends CommonOperationStore {
    /* override */
    @computed get isNotTaxable() {
        return true;
    }

    @override get canEdit() {
        return this.model.CanEdit;
    }

    @override get canCopy() {
        return false;
    }

    @override get canDownload() {
        return true;
    }

    @computed get canAddDocument() {
        return this.canEdit && canAddMoreDocuments(this.model.Documents, toFloat(this.model.Sum));
    }

    @computed get canAutoLinkDocuments() {
        return !this.model.BaseDocumentId
            && !this.isDocumentsChanged
            && this.model.Kontragent.KontragentId > 0
            && toFloat(this.model.Sum) > 0;
    }

    @override get canEditDate() {
        return !this.isClosed;
    }

    @override get canEditNumber() {
        return !this.isClosed;
    }

    @override get canToggleProvideInAccounting() {
        return !this.model.Closed && !this.isReadOnly;
    }

    @override get canShowAccountingPostingsSwitch() {
        return false;
    }

    @computed get canShowBubble() {
        return !!StateMessageResource[this.model.OperationState];
    }

    @override get isSavingBlocked() {
        return this.savePaymentPending;
    }

    @computed get fromSettlementAccountNumber() {
        const fromSettlementAccount = this.SettlementAccounts.find(account => account.Id === this.model.FromSettlementAccountId);

        return fromSettlementAccount?.Number;
    }
}

export default Computed;
