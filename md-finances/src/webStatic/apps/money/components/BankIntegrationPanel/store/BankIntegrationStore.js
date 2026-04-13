import {
    observable,
    action,
    runInAction,
    computed,
    makeObservable
} from 'mobx';
import { showSettlementAccountDialog } from '@moedelo/frontend-common-v2/apps/requisites/components/SettlementAccountDialog';
import { isWhiteLabel } from '@moedelo/frontend-common-v2/apps/marketing/helpers/whiteLabelHelper';
import navigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import { getUrlWithId } from '@moedelo/frontend-core-v2/helpers/companyId';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import userDataService from '@moedelo/frontend-core-v2/services/userDataService';
import ChatHelper from '@moedelo/chatbot/src/Helpers/ChatHelper';
import BankIntegrationStateEnum from '../enums/BankIntegrationPanelStateEnum';
import { getBankIntegrationData } from '../services/bankIntegrationService';
import { showIntegrationDialog } from '../helpers/turnIntegrationHelper';

class Store {
    @observable opened = false;
    @observable loading = true;
    @observable state = BankIntegrationStateEnum.Empty;

    @observable turnedOnBanks = [];
    @observable accessibleBanks = [];

    @observable isDisabled = false;
    @observable hasLimit = false;
    @observable hasUpsale = false;

    refreshSourceList;

    constructor({ refreshSourceList }) {
        makeObservable(this);

        this.refreshSourceList = refreshSourceList;
        this.init();
    }

    @action init = async () => {
        const { IsExpired } = await userDataService.get();

        runInAction(() => {
            this.isDisabled = IsExpired;
        });

        this.loadBankIntegrationData();
    };

    @action loadBankIntegrationData = async () => {
        this.loading = true;

        const { data = {} } = await getBankIntegrationData();

        runInAction(() => {
            this.loading = false;
        });

        if (!data) {
            return;
        }

        runInAction(() => {
            this.turnedOnBanks = data.TurnedOn;
            this.accessibleBanks = data.Accessible;
            this.state = data.UserIntegrationState;
            this.hasLimit = data.HasLimit;
            this.hasUpsale = data.HasUpsale;
        });
    }

    @action openPanel = () => {
        mrkStatService.sendEventWithoutInternalUser({
            event: `expand_integration_panel_integration_panel_click_button`,
            st5: this.state
        });
        this.opened = true;
    };

    @action closePanel = () => {
        this.opened = false;
    };

    onRequisitesLinkClick = () => {
        mrkStatService.sendEventWithoutInternalUser({
            event: `go_to_requisites_integration_panel_click_link`,
            st5: this.state
        });

        navigateHelper.push(getUrlWithId(`/Requisites?settlements`));
    };

    onMarketplaceLinkClick = type => {
        mrkStatService.sendEventWithoutInternalUser({
            event: `go_to_marketplace_integration_panel_click_link`,
            st5: this.state,
            st6: type
        });

        const url = isWhiteLabel() ? `/Pay` : `/Marketplace`;

        navigateHelper.push(getUrlWithId(url));
    };

    onClickTurnIntegration = partnerId => {
        mrkStatService.sendEventWithoutInternalUser({
            event: `add_bank_integration_integration_panel_click_button`,
            st5: this.state,
            st6: partnerId
        });

        showIntegrationDialog(partnerId, () => {
            this.refreshSourceList();

            this.loadBankIntegrationData();
        });
    }

    onAddSettlementAccountClick = () => {
        mrkStatService.sendEventWithoutInternalUser({
            event: `add_settlement_account_integration_panel_click_button`,
            st5: this.state
        });

        showSettlementAccountDialog({
            onSave: () => {
                this.refreshSourceList();

                this.loadBankIntegrationData();
            },
            showChat: ChatHelper.showChat
        });
    }

    @computed get isAddButtonDisabled() {
        return this.isDisabled;
    }

    @computed get isTurnedOnTileDisabled() {
        return this.isDisabled;
    }

    @computed get isTileDisabled() {
        return this.isDisabled || this.hasLimit;
    }

    @computed get needToShowNotification() {
        return this.state === BankIntegrationStateEnum.Notification;
    }

    @computed get needToShowCount() {
        return this.state === BankIntegrationStateEnum.Count && !this.isDisabled;
    }

    @computed get availableIntegrationsCount() {
        return this.accessibleBanks.length;
    }
}

export default Store;
