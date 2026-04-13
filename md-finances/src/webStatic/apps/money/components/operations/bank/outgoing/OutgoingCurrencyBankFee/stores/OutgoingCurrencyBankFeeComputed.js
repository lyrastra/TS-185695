import { computed, override } from 'mobx';
import { getSymbol } from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import CommonOperationStore from '../../../common/CommonOperationStore';

class OutgoingCurrencyBankFeeComputed extends CommonOperationStore {
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

    @override get canDownload() {
        return false;
    }

    @computed get needToValidateTotalSum() {
        return this.model.TaxPostings.Postings.length > 0;
    }

    @override get isOutsourceUser() {
        const {
            IsTrial, IsPaid, IsExpired, IsProfOutsourceUser
        } = this.UserInfo;

        return (IsTrial || IsPaid) && !IsExpired && IsProfOutsourceUser;
    }
}

export default OutgoingCurrencyBankFeeComputed;
