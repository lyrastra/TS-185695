import { computed, override } from 'mobx';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import CommonOperationStore from '../../../common/CommonOperationStore';

const dash = `—`;

class Computed extends CommonOperationStore {
    @computed get getDataFromContract() {
        const contractor = this.model.Kontragent || {};

        return {
            Direction: Direction.Outgoing,
            KontragentId: contractor.KontragentId,
            KontragentName: contractor.KontragentName
        };
    }

    @computed get contractBaseId() {
        return (this.model.Contract || {}).ContractBaseId;
    }

    @computed get kontragentId() {
        return (this.model.Kontragent || {}).KontragentId;
    }

    /* override */
    @computed get isNotTaxable() {
        return !!this.getTaxPostingsExplainingMsg();
    }

    @override get isSavingBlocked() {
        return !!this.model.TaxPostings.Error || this.taxPostingsLoading
            || !!this.model.AccountingPostings.Error || this.accountingPostingsLoading
            || this.savePaymentPending || this.sendToBankPending;
    }

    @computed get contractIsMainFirm() {
        return !this.model?.Kontragent?.SalaryWorkerId && !this.model?.Kontragent?.KontragentId;
    }

    @computed get contractorIsWorker() {
        return !!this.model?.Kontragent?.SalaryWorkerId;
    }

    @override get hasTaxPostings() {
        return true;
    }

    @computed get kbkValue() {
        const { model: { Kbk }, canEdit } = this;

        if (canEdit) {
            return Kbk;
        }

        return !Kbk?.length > 0 ? dash : Kbk;
    }

    @computed get deductionWorkerDocumentNumberValue() {
        const { model: { DeductionWorkerDocumentNumber }, canEdit } = this;

        if (canEdit) {
            return DeductionWorkerDocumentNumber;
        }

        return !DeductionWorkerDocumentNumber?.length > 0 ? dash : DeductionWorkerDocumentNumber;
    }

    @override get canSendInvoiceToBank() {
        return false;
    }
}

export default Computed;
