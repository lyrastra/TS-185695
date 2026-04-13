import { computed, override } from 'mobx';
import { getSymbol } from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import CommonOperationStore from '../../IncomingCommonOperationStore/CommonOperationStore';
import moneySourceHelper from '../../../../../../../../helpers/newMoney/moneySourceHelper';

class IncomingCurrencyFromAnotherAccountComputed extends CommonOperationStore {
    /* override */
    @computed get isNotTaxable() {
        return true;
    }

    @override get canDownload() {
        return false;
    }

    @override get isSavingBlocked() {
        return this.savePaymentPending;
    }

    @computed get currencyCode() {
        const primarySettlementAccount = this.SettlementAccounts.find(account => account.Id === this.model.SettlementAccountId);

        return primarySettlementAccount ? primarySettlementAccount.Currency : ``;
    }

    @computed get sumCurrencySymbol() {
        return getSymbol(this.currencyCode) || ``;
    }

    @computed get fromSettlementAccountList() {
        const primarySettlementAccount = this.SettlementAccounts.find(account => account.Id === this.model.SettlementAccountId);
        const currency = primarySettlementAccount.Currency;

        const accounts = this.SettlementAccounts.filter(account => {
            return (account.IsActive || account.Id === this.model.FromSettlementAccountId)
                && account.Id !== this.model.SettlementAccountId && account.Currency === currency;
        });

        return moneySourceHelper.mapSourceTypeForOperation(accounts);
    }

    @override get isOutsourceUser() {
        const {
            IsTrial, IsPaid, IsExpired, IsProfOutsourceUser
        } = this.UserInfo;

        return (IsTrial || IsPaid) && !IsExpired && IsProfOutsourceUser;
    }
}

export default IncomingCurrencyFromAnotherAccountComputed;
