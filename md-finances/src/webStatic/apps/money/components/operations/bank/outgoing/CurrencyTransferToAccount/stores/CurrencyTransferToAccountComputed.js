import { computed, override } from 'mobx';
import { getSymbol } from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import CommonOperationStore from '../../../common/CommonOperationStore';
import moneySourceHelper from '../../../../../../../../helpers/newMoney/moneySourceHelper';

class CurrencyTransferToAccountComputed extends CommonOperationStore {
    @override get isSavingBlocked() {
        return this.savePaymentPending || this.sendToBankPending;
    }

    @override get canDownload() {
        return false;
    }

    /* override */
    @computed get isNotTaxable() {
        return !!this.getTaxPostingsExplainingMsg();
    }

    /* override */
    @override get canSendToBank() {
        return false;
    }

    @computed get toSettlementAccountList() {
        const currency = this.SettlementAccounts
            .find(account => account.Id === this.model.SettlementAccountId).Currency;

        const accounts = this.SettlementAccounts.filter(account => {
            return (account.IsActive || account.Id === this.model.ToSettlementAccountId)
                && account.Id !== this.model.SettlementAccountId && account.Currency === currency;
        });

        return moneySourceHelper.mapSourceTypeForOperation(accounts);
    }

    @computed get currencyCode() {
        const primarySettlementAccount = this.SettlementAccounts.find(account => account.Id === this.model.SettlementAccountId);

        return primarySettlementAccount ? primarySettlementAccount.Currency : ``;
    }

    @computed get sumCurrencySymbol() {
        return getSymbol(this.currencyCode) || ``;
    }
}

export default CurrencyTransferToAccountComputed;
