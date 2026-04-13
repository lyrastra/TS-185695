import {
    observable, action, toJS, computed, makeObservable
} from 'mobx';
import storage from '../../../helpers/newMoney/storage';
import MoneyOperationService from '../../../services/newMoney/moneyOperationService';
import MoneySourceType from '../../../enums/MoneySourceType';
import moneyOperationHelper from '../../../helpers/newMoney/moneySourceHelper';
import rubCodesResource from '../../../resources/newMoney/rubCodesResource';

class Store {
    @observable loading = true;

    @observable list = [];

    @observable viewPaymentNotificationPanel = { statusCodeInvoice: 0 };

    constructor() {
        makeObservable(this);
    }

    @action loadList(options = { withoutLoading: false }) {
        if (!options.withoutLoading) {
            this.loading = true;
        }

        return MoneyOperationService.getSources()
            .then(response => {
                this.list = response;
                this.loading = false;
            });
    }

    @action setViewPaymentNotificationPanel = value => {
        this.viewPaymentNotificationPanel = value;
    }

    @computed get sourceList() {
        return toJS(this.list);
    }

    @action getSourceById(id) {
        const sources = toJS(this.list);

        return sources.find(item => { return item.Id === id; });
    }

    @computed get settlementAccountListForOperation() {
        return moneyOperationHelper.mapSourceTypeForOperation(this.settlementAccounts);
    }

    @computed get settlementAccounts() {
        return this.list.filter(account => account.Type === MoneySourceType.SettlementAccount);
    }

    @computed get settlementAccountsIds() {
        return this.settlementAccounts.map(account => account.Id);
    }

    @computed get cashAccounts() {
        return this.list.filter(account => account.Type === MoneySourceType.Cash);
    }

    @computed get hasCurrencySettlementAccount() {
        return this.settlementAccounts.some(account => account.Currency !== 0 && !rubCodesResource.includes(account.Currency));
    }

    isArchiveSource = () => {
        if (this.list.length) {
            const { sourceId } = storage.get(`filter`) || {};
            const currentSource = this.getBySourceId(+sourceId);

            return currentSource && !currentSource.IsActive;
        }

        return false;
    }

    getBySourceId(sourceId) {
        const { list } = this;

        if (list.length) {
            return list.find(item => item.Id === sourceId);
        }

        return null;
    }
}

export default Store;
