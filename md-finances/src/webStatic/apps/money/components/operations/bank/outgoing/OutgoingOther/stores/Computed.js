import { computed, override } from 'mobx';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import CommonOperationStore from '../../../common/CommonOperationStore';
import { canAddMoreDocuments } from '../../../../../../../../helpers/newMoney/linkedDocumentsHelper';
import { getNdsTypes, hasNds } from '../../../../../../../../helpers/newMoney/ndsHelper';

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

    @computed get hasNds() {
        return hasNds(this.model);
    }

    @computed get ndsTypes() {
        const date = this.model.Date || null;
        const taxationSystem = this.isAfter2025WithTaxation;

        return getNdsTypes({ date, taxationSystem, direction: Direction.Outgoing });
    }

    @computed get isAfter2025WithTaxation() {
        const { IsUsn, IsOsno } = this.TaxationSystem;

        return {
            isAfter2026: this.isDateAfter2026,
            isAfter2025: this.isDateAfter2025,
            isDate2025: this.isDate2025,
            IsUsn,
            IsOsno
        };
    }

    @computed get isDateAfter2026() {
        return dateHelper(this.model.Date).isAfter(`31.12.2025`);
    }

    @computed get isDateAfter2025() {
        return dateHelper(this.model.Date).isAfter(`31.12.2024`);
    }

    @computed get isDate2025() {
        return dateHelper(this.model.Date).year() === 2025;
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
            || !!this.model.AccountingPostings.Error || this.accountingPostingsLoading || this.savePaymentPending;
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

    @override get canSendInvoiceToBank() {
        return !this.Requisites.IsOoo && super.canSendInvoiceToBank;
    }
}

export default Computed;
