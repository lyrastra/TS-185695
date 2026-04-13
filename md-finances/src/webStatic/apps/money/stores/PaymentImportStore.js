import {
    observable, action, runInAction, computed, makeObservable
} from 'mobx';
import ImportStatusEnum from '@moedelo/frontend-common-v2/apps/finances/enums/ImportStatusEnum';
import ImportHelper from '@moedelo/frontend-common-v2/apps/finances/helpers/ImportHelper';
import ImportService from '@moedelo/frontend-common-v2/apps/finances/service/ImportService';
import { isCurrencyByAccount } from '@moedelo/frontend-common-v2/apps/finances/helpers/currencyHelper';
import requisitesService from '@moedelo/frontend-common-v2/apps/requisites/services/requisitesService';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import settlementAccountsService from '@moedelo/frontend-common-v2/apps/requisites/services/settlementAccountsService';
import requisitesAccessService from '@moedelo/frontend-common-v2/apps/requisites/services/requisitesAccessService';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import SettlementAccountTypeEnum from '../../../enums/newMoney/SettlementAccountTypeEnum';
import validator from './validator';
import { VALID_RU_SETTLEMENT_NUMBER_LENGTH } from '../../../constants/requisitesConstants';

const manualFileUploadToImportEvent = `zagruzit_file_stranitsa_dengi_click_button`;

class Store {
    @observable loading = false;

    @observable importLoading = false;

    @observable importStatus = null;

    @observable isConfirmDialogShown = false;

    @observable isBigFileNotificationDialogShown = false;

    @observable reconciliationByHandDialogShown = false;

    @observable FileId = null;

    @observable SettlementAccountType = SettlementAccountTypeEnum.Default;

    @observable SecondSettlementAccount = null;

    @observable SettlementAccount = null;

    @observable checkDocuments = true;

    @observable importMessage = {
        header: ``,
        text: ``
    };

    @observable paymentImportParams = {
        startDate: null,
        endDate: null,
        sourceId: null,
        sourceType: null
    };

    @observable moneyAccess = {};

    @observable taxationSystem = {};

    @observable requisites = {};

    @observable settlementAccounts = [];

    @observable validationState = {
        SecondSettlementAccount: ``
    };

    constructor() {
        makeObservable(this);

        runInAction(async () => {
            const [
                taxationSystem,
                requisites,
                settlementAccounts,
                moneyAccess
            ] = await Promise.all([
                taxationSystemService.getTaxSystem(),
                requisitesService.get(),
                settlementAccountsService.get(),
                requisitesAccessService.getMoneyAccess()
            ]);
            this.taxationSystem = taxationSystem;
            this.requisites = requisites;
            this.settlementAccounts = settlementAccounts;
            this.moneyAccess = moneyAccess;
        });
    }

    @action setPaymentImportParams(startDate, endDate, sourceId, sourceType) {
        this.paymentImportParams = {
            startDate,
            endDate,
            sourceId,
            sourceType
        };
    }

    @action toggleReconciliationByHandDialogVisibility = visible => {
        this.reconciliationByHandDialogShown = visible;
    };

    @action uploadFile = (input, options) => {
        this.importLoading = true;

        mrkStatService.sendEventWithoutInternalUser(manualFileUploadToImportEvent);

        const { uploadImportFile, uploadPurseImportFile } = ImportService;
        const { isPurse, kontragentId } = options;
        const uploadMethod = isPurse ? uploadPurseImportFile : uploadImportFile;
        const otherData = isPurse ? {
            kontragentId
        } : null;

        return uploadMethod({ files: input.files, otherData })
            .then(response => {
                this.importLoading = false;
                const { SettlementAccount, Bik } = response.ExData || {}; // при импорте выписки с несколькими р/сч ExData приходит null

                const wrongStatus = response.Status === ImportStatusEnum.newSettlement ||
                response.Status === ImportStatusEnum.archiveSettlement ||
                response.Status === ImportStatusEnum.wrongOperations;

                if (wrongStatus && !isPurse) {
                    this.importMessage = ImportHelper.getImportModalMessage(SettlementAccount, response.Status);
                    this.importStatus = response.Status;
                    this.SettlementAccount = SettlementAccount;
                    this.Bik = Bik;
                    this.FileId = response.FileId;
                    this.isConfirmDialogShown = true;

                    if (!this.moneyAccess.CurrencyAccountAsRubAccount && isCurrencyByAccount(this.SettlementAccount)) {
                        this.SettlementAccountType = SettlementAccountTypeEnum.Currency;
                    }

                    if (response.Status === ImportStatusEnum.wrongOperations) {
                        this.checkDocuments = false;
                    }
                }

                return response;
            });
    };

    @action confirmImport = () => {
        const { FileId } = this;

        this.importLoading = true;

        return ImportService.confirmImport({
            FileId,
            CheckDocuments: this.checkDocuments,
            SecondSettlementAccount: this.SecondSettlementAccount,
            SettlementAccountType: this.SettlementAccountType
        }).then(response => {
            this.importLoading = false;
            this.isConfirmDialogShown = false;
            const SettlementAccount = response?.ExData?.Bik;
            const Bik = response?.ExData?.SettlementAccount;

            if (response.Status === ImportStatusEnum.wrongOperations) {
                this.importMessage = ImportHelper.getImportModalMessage(SettlementAccount, response.Status);
                this.importStatus = response.Status;
                this.SettlementAccount = SettlementAccount;
                this.Bik = Bik;
                this.FileId = response.FileId;
                this.isConfirmDialogShown = true;
                this.checkDocuments = false;
            } else {
                this.resetImport();
            }

            return response;
        });
    };

    @action resetImport = () => {
        this.importLoading = false;
        this.isConfirmDialogShown = false;
        this.importStatus = null;
        this.SettlementAccount = null;
        this.FileId = null;
        this.checkDocuments = true;
        this.importMessage = {
            header: ``,
            text: ``
        };
        this.validationState = {
            SecondSettlementAccount: ``
        };
        this.SettlementAccountType = SettlementAccountTypeEnum.Default;
        this.SecondSettlementAccount = ``;
        this.Bik = ``;
    };

    @action toggleBigFileNotificationDialogVisibility = () => {
        this.isBigFileNotificationDialogShown = !this.isBigFileNotificationDialogShown;
    };

    @action isValid = () => {
        if (!this.isCurrencyAccount) {
            return true;
        }

        this.validateSecondSettlementAccount();

        return !Object.values(this.validationState).some(x => x);
    };

    @action onChangeSettlementAccountType = ({ value }) => {
        if (this.SettlementAccountType !== value) {
            this.SettlementAccountType = value;
        }
    };

    @action onChangeSecondSettlementAccount = ({ value }) => {
        const newValue = value.replace(/[^\d]/g, ``);

        if (this.SecondSettlementAccount !== newValue) {
            this.SecondSettlementAccount = newValue;

            if (this.SecondSettlementAccount.length === VALID_RU_SETTLEMENT_NUMBER_LENGTH || this.validationState.SecondSettlementAccount) {
                this.validateSecondSettlementAccount();
            }
        }
    };

    @action validateSecondSettlementAccount = () => {
        this.validationState.SecondSettlementAccount = validator.validateSecondSettlementAccount(this);
    };

    @computed get isCurrencyAccount() {
        return !this.moneyAccess.CurrencyAccountAsRubAccount && this.SettlementAccount && isCurrencyByAccount(this.SettlementAccount);
    }
}

export default Store;
