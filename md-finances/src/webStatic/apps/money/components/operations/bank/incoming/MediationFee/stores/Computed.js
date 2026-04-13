import { computed, override } from 'mobx';
import AccountingDocumentType from '@moedelo/frontend-enums/mdEnums/AccountingDocumentType';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import { round } from '../../../../../../../../helpers/numberConverter';
import CommonOperationStore from '../../IncomingCommonOperationStore/CommonOperationStore';
import { getNdsTypes, hasNds } from '../../../../../../../../helpers/newMoney/ndsHelper';
import { canAddMoreDocuments } from '../../../../../../../../helpers/newMoney/linkedDocumentsHelper';

class Computed extends CommonOperationStore {
    @computed get ndsTypes() {
        const date = this.model.Date || null;
        const taxationSystem = this.isAfter2025WithTaxation;

        return getNdsTypes({ date, taxationSystem, direction: Direction.Incoming });
    }

    @computed get hasNds() {
        return hasNds(this.model);
    }

    @computed get canAddBill() {
        return canAddMoreDocuments(this.model.Bills, this.model.Sum);
    }

    @computed get canAddDocument() {
        return canAddMoreDocuments(this.model.Documents, this.model.Sum);
    }

    @computed get kontragentId() {
        return (this.model.Kontragent || {}).KontragentId;
    }

    @computed get contractBaseId() {
        return (this.model.MiddlemanContract || {}).DocumentBaseId;
    }

    @computed get isNotTaxable() {
        return !!this.getTaxPostingsExplainingMsg();
    }

    @override get isSavingBlocked() {
        return !!this.model.TaxPostings.Error || this.taxPostingsLoading
            || !!this.model.AccountingPostings.Error || this.accountingPostingsLoading || this.savePaymentPending;
    }

    @computed get documentsSumExceptMiddlemanReport() {
        const documentsSum = this.model.Documents.reduce((sum, doc) => {
            if (doc.DocumentType === AccountingDocumentType.MiddlemanReport) {
                return sum;
            }

            return sum + toFloat(doc.Sum);
        }, 0);

        return round(documentsSum);
    }
}

export default Computed;
