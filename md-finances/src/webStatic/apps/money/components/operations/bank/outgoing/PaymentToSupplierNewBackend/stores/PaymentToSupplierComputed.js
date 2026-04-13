import { computed, override, toJS } from 'mobx';
import { toAmountString, toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import CommonOperationStore from '../../../common/CommonOperationStore';
import { getNdsTypes, hasNds } from '../../../../../../../../helpers/newMoney/ndsHelper';
import { canAddMoreDocuments } from '../../../../../../../../helpers/newMoney/linkedDocumentsHelper';
import TaxStatusEnum from '../../../../../../../../enums/TaxStatusEnum';

class PaymentToSupplierComputed extends CommonOperationStore {
    @computed get documentsTotalSum() {
        const totalSum = toJS(this.documents).reduce((sum, doc) => sum + toFloat(doc.Sum), 0);

        return toAmountString(totalSum);
    }

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

    @computed get documents() {
        return this.model.Documents || [];
    }

    @computed get canAddDocument() {
        return this.canEdit && canAddMoreDocuments(this.model.Documents, toFloat(this.model.Sum));
    }

    @computed get canAutoLinkDocuments() {
        return !this.model.DocumentBaseId
            && !this.isDocumentsChanged
            && this.model.Kontragent.KontragentId > 0
            && toFloat(this.model.Sum) > 0;
    }

    @computed get isNotTaxable() {
        return !!this.getTaxPostingsExplainingMsg() || this.model.TaxPostings.TaxStatus === TaxStatusEnum.NotTax;
    }

    @override get isSavingBlocked() {
        return !!this.model.TaxPostings.Error || this.taxPostingsLoading
            || !!this.model.AccountingPostings.Error || this.accountingPostingsLoading
            || this.savePaymentPending || this.sendToBankPending;
    }

    @computed get bills() {
        return this.model.Bills || [];
    }
}

export default PaymentToSupplierComputed;
