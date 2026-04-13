import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import { computed, override } from 'mobx';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import { getSymbol } from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import CommonOperationStore from '../../../common/CommonOperationStore';
import { getNdsTypes, hasNds } from '../../../../../../../../helpers/newMoney/ndsHelper';

class OutgoingCurrencyOtherComputed extends CommonOperationStore {
    @computed get getDataFromContract() {
        const contractor = this.model.Kontragent || {};

        return {
            Direction: Direction.Outgoing,
            KontragentId: contractor.KontragentId,
            KontragentName: contractor.KontragentName
        };
    }

    @computed get currencyCode() {
        const primarySettlementAccount = this.SettlementAccounts.find(account => account.Id === this.model.SettlementAccountId);

        return primarySettlementAccount ? primarySettlementAccount.Currency : ``;
    }

    @computed get sumCurrencySymbol() {
        return getSymbol(this.currencyCode) || ``;
    }

    @computed get showCalculatedFields() {
        return this.model.Sum && this.model.CentralBankRate;
    }

    @computed get contractBaseId() {
        return this.model.Contract?.ContractBaseId;
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

    @computed get kontragentId() {
        return this.model.Kontragent?.KontragentId;
    }

    /* override */
    @computed get isNotTaxable() {
        return !!this.getTaxPostingsExplainingMsg();
    }

    /* override */
    @override get isSavingBlocked() {
        return !!this.model.TaxPostings.Error || this.taxPostingsLoading
            || this.savePaymentPending || this.sendToBankPending;
    }

    @computed get contractorIsWorker() {
        return !!this.model.Kontragent && !!this.model.Kontragent.SalaryWorkerId;
    }

    @override get isSelfKontragent() {
        return !!this.model.Kontragent && !this.model.Kontragent.SalaryWorkerId && !this.kontragentId;
    }

    @override get hasTaxPostings() {
        return true;
    }

    @computed get needToValidateTotalSum() {
        return this.model.TaxPostings.Postings.length > 0;
    }

    @override get canDownload() {
        return false;
    }

    @computed get isTransit() {
        return this.SettlementAccounts.find(account => account.Id === this.model.SettlementAccountId)?.IsTransit;
    }

    @override get sumOperation() {
        return toFloat(this.model.TotalSum);
    }
}

export default OutgoingCurrencyOtherComputed;
