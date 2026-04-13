import { computed, override } from 'mobx';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import CommonOperationStore from '../../IncomingCommonOperationStore/CommonOperationStore';
import { canAddMoreDocuments } from '../../../../../../../../helpers/newMoney/linkedDocumentsHelper';
import { getNdsTypes, hasNds } from '../../../../../../../../helpers/newMoney/ndsHelper';

class Computed extends CommonOperationStore {
    @computed get getDataFromContract() {
        const contractor = this.model.Kontragent || {};

        return {
            Direction: Direction.Incoming,
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

    @computed get hasNds() {
        return hasNds(this.model);
    }

    @computed get ndsTypes() {
        const date = this.model.Date || null;
        const taxationSystem = this.isAfter2025WithTaxation;

        return getNdsTypes({ date, taxationSystem, direction: Direction.Incoming });
    }

    @computed get bills() {
        return this.model.Bills || [];
    }

    @computed get canAddBill() {
        return canAddMoreDocuments(this.model.Bills, toFloat(this.model.Sum));
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

    @computed get contractorIsWorker() {
        return !!this.model.Kontragent && !!this.model.Kontragent.SalaryWorkerId;
    }

    @override get hasTaxPostings() {
        return true;
    }
}

export default Computed;
