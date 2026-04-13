import { computed } from 'mobx';
import CommonOperationStore from '../../../common/CommonOperationStore';
import moneySourceHelper from '../../../../../../../../helpers/newMoney/moneySourceHelper';
import rubCodesResource from '../../../../../../../../resources/newMoney/rubCodesResource';

class Computed extends CommonOperationStore {
    @computed get isSavingBlocked() {
        return !!this.model.AccountingPostings.Error || this.accountingPostingsLoading
            || this.savePaymentPending || this.sendToBankPending;
    }

    @computed get isNotTaxable() {
        return true;
    }

    @computed get transferSettlementAccountList() {
        const accounts = this.SettlementAccounts.filter(account => {
            return (
                (account.IsActive && rubCodesResource.includes(account.Currency)) ||
                (
                    account.Id === this.model.TransferSettlementaccountId ||
                    account.Id === this.model.initialSecondarySource
                )
            ) && account.Id !== this.model.SettlementAccountId;
        });

        return moneySourceHelper.mapSourceTypeForOperation(accounts);
    }
}

export default Computed;
