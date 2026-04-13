import { computed, override } from 'mobx';
import { getSymbol } from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import CommonOperationStore from '../../IncomingCommonOperationStore/CommonOperationStore';
import moneySourceHelper from '../../../../../../../../helpers/newMoney/moneySourceHelper';
import rubCodesResource from '../../../../../../../../resources/newMoney/rubCodesResource';

class IncomingCurrencySaleComputed extends CommonOperationStore {
    /* override */
    @computed get isNotTaxable() {
        return true;
    }

    @override get isSavingBlocked() {
        return !!this.model.AccountingPostings.Error || this.accountingPostingsLoading || this.savePaymentPending;
    }

    @computed get fromSettlementAccounts() {
        const settlementAccounts = this.SettlementAccounts
            .filter(account => account.Id === this.model.FromSettlementAccountId ||
                (account.IsActive && !rubCodesResource.includes(account.Currency)));

        return moneySourceHelper.mapSourceTypeForOperation(settlementAccounts);
    }

    @computed get currencyCode() {
        const primarySettlementAccount = this.SettlementAccounts.find(account => account.Id === this.model.SettlementAccountId);

        return primarySettlementAccount ? primarySettlementAccount.Currency : ``;
    }

    @override get canDownload() {
        return false;
    }

    @computed get sumCurrencySymbol() {
        return getSymbol(this.currencyCode) || ``;
    }

    @override get isOutsourceUser() {
        const {
            IsTrial, IsPaid, IsExpired, IsProfOutsourceUser
        } = this.UserInfo;

        return (IsTrial || IsPaid) && !IsExpired && IsProfOutsourceUser;
    }
}

export default IncomingCurrencySaleComputed;
