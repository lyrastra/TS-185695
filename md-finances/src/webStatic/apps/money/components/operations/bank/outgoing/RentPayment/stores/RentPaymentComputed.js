import { computed, override } from 'mobx';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import { getSymbol } from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import CommonOperationStore from '../../../common/CommonOperationStore';
import PaymentType from '../../../../../../../../enums/newMoney/RentalPaymentTypeEnum';
import { getNdsTypes, hasNds } from '../../../../../../../../helpers/newMoney/ndsHelper';
import { isDate } from '@moedelo/frontend-core-v2/helpers/typeCheckHelper';

class RentPaymentComputed extends CommonOperationStore {
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

    @override get canEdit() {
        return !this.model.AccountingReadOnly
            && this.model.CanEdit
            && (!this.isClosed || (this.isClosed && this.isNew));
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

    @computed get kontragentId() {
        return this.model.Kontragent?.KontragentId;
    }

    @computed get isRentRemains() {
        return this.model.FixedAsset?.IsRentRemains;
    }

    @computed get remainsDate() {
        const requisites = this.Requisites;

        return dateHelper(requisites.BalanceDate, `DD.MM.YYYY`);
    }

    @computed get firstPeriodId() {
        return this.model.FirstPeriodId;
    }

    /* override */
    @computed get isNotTaxable() {
        return !!this.getTaxPostingsExplainingMsg();
    }

    @override get isSavingBlocked() {
        return !!this.model.TaxPostings.Error || this.taxPostingsLoading
            || this.savePaymentPending || this.sendToBankPending;
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

    @computed get leaseTotalSum() {
        return this.model.Periods.reduce((accumulator, currentValue) => {
            const sum = (currentValue.PaymentType === PaymentType.Monthly) && toFloat(currentValue.Sum) ? currentValue.Sum : 0;

            return accumulator + sum;
        }, 0);
    }

    @computed get redemptionTotalSum() {
        return this.model.Periods.reduce((accumulator, currentValue) => {
            const sum = (currentValue.PaymentType === PaymentType.Buyout) && toFloat(currentValue.Sum) ? currentValue.Sum : 0;

            return accumulator + sum;
        }, 0);
    }

    @computed get totalSum() {
        return this.model.Periods.reduce((accumulator, currentValue) => {
            const sum = toFloat(currentValue.Sum) ? currentValue.Sum : 0;

            return accumulator + sum;
        }, 0);
    }

    @override get isOutsourceUser() {
        const {
            IsTrial, IsPaid, IsExpired, IsProfOutsourceUser
        } = this.UserInfo;

        return (IsTrial || IsPaid) && !IsExpired && IsProfOutsourceUser;
    }
}

export default RentPaymentComputed;
