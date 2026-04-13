import { computed, override } from 'mobx';
import { getSymbol } from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import CommonOperationStore from '../../../common/CommonOperationStore';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import moneySourceHelper from '../../../../../../../../helpers/newMoney/moneySourceHelper';
import rubCodesResource from '../../../../../../../../resources/newMoney/rubCodesResource';

class OutgoingCurrencyPurchaseComputed extends CommonOperationStore {
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
        const toAccount = this.SettlementAccounts.find(account => account.Id === this.model.ToSettlementAccountId);
        const settlementAccounts = this.SettlementAccounts
            .filter(account => (this.model.Id && toAccount && account.Id === toAccount.Id) ||
                (account.IsActive && !rubCodesResource.includes(account.Currency)));

        return moneySourceHelper.mapSourceTypeForOperation(settlementAccounts);
    }

    @computed get currencyCode() {
        const secondarySettlementAccount = this.SettlementAccounts.find(account => account.Id === this.model.ToSettlementAccountId);

        return secondarySettlementAccount ? secondarySettlementAccount.Currency : ``;
    }

    @computed get totalSumCurrencySymbol() {
        return getSymbol(this.currencyCode) || ``;
    }

    @override get canDownload() {
        return false;
    }

    @computed get needToValidateExchangeRateDiff() {
        return this.model.TaxPostings.Postings.length > 0;
    }

    @computed get availableTaxPostingDirection() {
        return AvailableTaxPostingDirection.Incoming;
    }

    @computed get separatorSymbol() {
        return `÷`;
    }
}

export default OutgoingCurrencyPurchaseComputed;
