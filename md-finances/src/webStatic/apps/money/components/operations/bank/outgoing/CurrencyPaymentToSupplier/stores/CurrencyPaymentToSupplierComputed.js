import { computed, toJS, override } from 'mobx';
import { getLettersByCode, getSymbol } from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toFinanceString, toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import CommonOperationStore from '../../../common/CommonOperationStore';
import { canAddMoreDocuments } from '../../../../../../../../helpers/newMoney/linkedDocumentsHelper';
import { getNdsTypes, hasNds } from '../../../../../../../../helpers/newMoney/ndsHelper';

class CurrencyPaymentToSupplierComputed extends CommonOperationStore {
    @computed get getDataFromContract() {
        const contractor = this.model.Kontragent || {};

        return {
            Direction: Direction.Outgoing,
            KontragentId: contractor.KontragentId,
            KontragentName: contractor.KontragentName
        };
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

    @computed get contractBaseId() {
        return (this.model.Contract || {}).ContractBaseId;
    }

    @computed get kontragentId() {
        return (this.model.Kontragent || {}).KontragentId;
    }

    @computed get isNotTaxable() {
        return !!this.getTaxPostingsExplainingMsg();
    }

    @override get isSavingBlocked() {
        return !!this.model.TaxPostings.Error || this.taxPostingsLoading
            || this.savePaymentPending || this.sendToBankPending;
    }

    @computed get sumCurrencySymbol() {
        return getSymbol(this.currencyCode) || ``;
    }

    @computed get sumCurrencyLetters() {
        return getLettersByCode(this.currencyCode) || ``;
    }

    @computed get currencyCode() {
        const primarySettlementAccount = this.SettlementAccounts.find(account => account.Id === this.model.SettlementAccountId);

        return primarySettlementAccount ? primarySettlementAccount.Currency : ``;
    }

    @computed get showCalculatedFields() {
        return this.model.Sum && this.model.CentralBankRate;
    }

    @override get canDownload() {
        return false;
    }

    @computed get canAddDocument() {
        return this.canEdit && canAddMoreDocuments(this.model.Documents, toFloat(this.model.Sum));
    }

    @computed get documentsTotalSum() {
        const totalSum = toJS(this.model.Documents).reduce((sum, doc) => sum + toFloat(doc.Sum), 0);

        return toFinanceString(totalSum);
    }

    @computed get needToValidateTotalSum() {
        return this.model.TaxPostings.Postings.length > 0;
    }
}

export default CurrencyPaymentToSupplierComputed;
