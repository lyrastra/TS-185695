import { computed, override } from 'mobx';
import { getSymbol } from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import CommonOperationStore from '../../../common/CommonOperationStore';
import moneySourceHelper from '../../../../../../../../helpers/newMoney/moneySourceHelper';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import rubCodesResource from '../../../../../../../../resources/newMoney/rubCodesResource';

class OutgoingCurrencySaleComputed extends CommonOperationStore {
    /* override */
    @computed get isNotTaxable() {
        return !this.model.TaxPostings || !this.model.TaxPostings.Postings || this.model.TaxPostings.Postings.length === 0;
    }

    @override get isSavingBlocked() {
        return !!this.model.AccountingPostings.Error || this.accountingPostingsLoading
            || this.savePaymentPending || this.sendToBankPending;
    }

    @computed get showCalculatedFields() {
        return this.model.Sum && this.model.ExchangeRate;
    }

    @computed get toSettlementAccounts() {
        const settlementAccounts = this.SettlementAccounts
            .filter(account => rubCodesResource.includes(account.Currency)
                && (account.Id === this.model.ToSettlementAccountId || account.IsActive));

        return moneySourceHelper.mapSourceTypeForOperation(settlementAccounts);
    }

    @computed get currencyCode() {
        const primarySettlementAccount = this.SettlementAccounts.find(account => account.Id === this.model.SettlementAccountId);

        return primarySettlementAccount ? primarySettlementAccount.Currency : ``;
    }

    @computed get sumCurrencySymbol() {
        return getSymbol(this.currencyCode) || ``;
    }

    @override get canDownload() {
        return false;
    }

    @computed get needToValidateExchangeRateDiff() {
        return this.model.TaxPostings.Postings.length > 0;
    }

    @computed get separatorSymbol() {
        return `×`;
    }

    @computed get availableTaxPostingDirection() {
        return AvailableTaxPostingDirection.Incoming;
    }

    @override get isOutsourceUser() {
        const {
            IsTrial, IsPaid, IsExpired, IsProfOutsourceUser
        } = this.UserInfo;

        return (IsTrial || IsPaid) && !IsExpired && IsProfOutsourceUser;
    }
}

export default OutgoingCurrencySaleComputed;
