import {
    observable, action, computed, runInAction, makeObservable
} from 'mobx';
import { getAccessToMoneyEdit } from '@moedelo/frontend-common-v2/apps/finances/service/setupDataPreloadingService';
import { getStartEnpDate } from '../../../services/enpEnabledService';

class CommonDataStore {
    @observable loadingAccessToMoney = true;
    @observable isAccessToMoneyEnabled = true;
    @observable enpStartDate = null;

    constructor() {
        makeObservable(this);
        this.checkAccessToMoneyEditRule();
    }

    async loadEnpStartDate() {
        const enpStartDate = await getStartEnpDate();

        runInAction(() => {
            this.enpStartDate = enpStartDate;
        });
    }

    @action checkAccessToMoneyEditRule() {
        return getAccessToMoneyEdit()
            .then(response => {
                this.loadingAccessToMoney = false;

                this.isAccessToMoneyEnabled = response;
            }).catch(() => {
                this.loadingAccessToMoney = false;

                return false;
            });
    }

    @computed get hasAccessToMoneyEdit() {
        return this.isAccessToMoneyEnabled;
    }
}

export default CommonDataStore;
