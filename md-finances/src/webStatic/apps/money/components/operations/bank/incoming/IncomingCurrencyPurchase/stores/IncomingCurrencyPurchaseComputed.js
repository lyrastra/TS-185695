import { computed, override } from 'mobx';
import { getSymbol } from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import CommonOperationStore from '../../IncomingCommonOperationStore/CommonOperationStore';
import moneySourceHelper from '../../../../../../../../helpers/newMoney/moneySourceHelper';
import rubCodesResource from '../../../../../../../../resources/newMoney/rubCodesResource';

class IncomingCurrencyPurchaseComputed extends CommonOperationStore {
    /* override */
    @computed get isNotTaxable() {
        return true;
    }

    @override get isSavingBlocked() {
        return !!this.model.AccountingPostings.Error || this.accountingPostingsLoading || this.savePaymentPending;
    }

    @computed get fromSettlementAccounts() {
        const settlementAccounts = this.SettlementAccounts
            .filter(account => rubCodesResource.includes(account.Currency) &&
                               (account.Id === this.model.FromSettlementAccountId || account.IsActive));

        return moneySourceHelper.mapSourceTypeForOperation(settlementAccounts);
    }

    @computed get currencyCode() {
        const toSettlementAccount = this.SettlementAccounts.find(account => account.Id === this.model.SettlementAccountId);

        return toSettlementAccount ? toSettlementAccount.Currency : ``;
    }

    @override get canDownload() {
        return false;
    }

    @computed get sumCurrencySymbol() {
        return getSymbol(this.currencyCode) || ``;
    }
}

export default IncomingCurrencyPurchaseComputed;
