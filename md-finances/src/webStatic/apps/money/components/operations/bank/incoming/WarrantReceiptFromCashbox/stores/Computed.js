import { computed, override } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import CommonOperationStore from '../../IncomingCommonOperationStore/CommonOperationStore';
import { canAddMoreDocuments } from '../../../../../../../../helpers/newMoney/linkedDocumentsHelper';

class Computed extends CommonOperationStore {
    @computed get showMediationCommission() {
        return this.model.IsMediation;
    }

    @override get canDownload() {
        return false;
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

    @override get isSavingBlocked() {
        return !!this.model.AccountingPostings.Error || this.accountingPostingsLoading || this.savePaymentPending;
    }
}

export default Computed;
