import {
    observable, toJS, reaction, runInAction, action, makeObservable
} from 'mobx';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import { toDate } from '@moedelo/frontend-core-v2/helpers/converter';
import NotificationManager from '@moedelo/frontend-core-react/helpers/notificationManager';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import KontragentsForm from '@moedelo/frontend-enums/mdEnums/KontragentsForm';
import KontragentType from '@moedelo/frontend-enums/mdEnums/KontragentType';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Logger from '@moedelo/frontend-core-v2/helpers/logger/index';
import { getAccessToPostings } from '@moedelo/frontend-common-v2/apps/finances/service/setupDataPreloadingService';
import Actions from './Actions';
import accountingPostingService from '../../../../../../../services/accountingPostingService';
import DeferredLoader from '../../../../../../../helpers/newMoney/DeferredLoader';
import postingGenerationError from '../../../../../../../resources/newMoney/postingGenerationError';
import { actionEnum } from '../../../../../../../resources/newMoney/saveButtonResource';
import ProvidePostingType from '../../../../../../../enums/ProvidePostingTypeEnum';
import {
    osnoTaxPostingsForServerNewBackend,
    usnTaxPostingsForServerNewBackend,
    mapLinkedDocumentsTaxPostingsToModel
} from '../../../../../../../mappers/taxPostingsMapper';
import { getContractorAutocomplete, getContractorBankAutocomplete } from '../../../../../../../services/newMoney/contractorService';
import {
    mapPostingsToModel,
    mapLinkedDocumentsPostingsToModel
} from '../../../../../../../mappers/accountingPostingsMapper';
import MoneyOperationService from '../../../../../../../services/newMoney/moneyOperationService';
import taxPostingsValidator from '../../../validation/taxPostingsValidator';
import { scrollTo } from '../../../../../../../helpers/newMoney/domHelper';
import {
    createPaymentOrder,
    deletePaymentOrder,
    updatePaymentOrder
} from '../../../../../../../services/newMoney/newMoneyOperationService';
import TaxStatusEnum from '../../../../../../../enums/TaxStatusEnum';
import { generate, generateForOsno } from '../../../../../../../services/taxPostingService';
import { enableShowImportRuleNewFutureDialog } from '../../../../../../../services/importRuleService';
import storage from '../../../../../../../helpers/newMoney/storage';
import { sendChangeOperationTypeEvent } from '../../helpers/moneyMrkStatHelper';
import { getApproveById, getInitialDate, putApproveById } from '../../../../../../../services/approvedService';

export default class CommonOperationStore extends Actions {
    @observable savePaymentPending = false;

    @observable disabledSaveButton = false;

    @observable kontragentBanksLoading = false;

    @observable isApproved = null;

    @observable isShowApprove = false;

    @observable initialDate = null;

    @observable TaxationSystem = null;

    @observable ndsRatesFromAccPolicy = [];

    @observable ActivePatents = [];

    // сообщение в БУ при отсутствии объектов учета
    messageNoAccountingObjects = `По данному счёту нет ни одного объекта учёта`;

    /** Доступные формы собственности для создания контрагента */
    availableContractorForms = Object.values(KontragentsForm);

    /** Доступные типы контрагентов для создания */
    availableContractorTypes = Object.values(KontragentType);

    needAllSumValidation = true;

    constructor(options) {
        super(options);
        makeObservable(this);

        this.Requisites = options.requisites;
        this.OperationTypes = options.operationTypes;
        this.SettlementAccounts = options.settlementAccounts;
        this.TaxationSystem = options.taxationSystem;
        this.HasPurseAccount = options.hasPurseAccount;
        this.UserInfo = options.userInfo;
        this.onSave = options.onSave;
        this.ndsRatesFromAccPolicy = options.ndsRatesFromAccPolicy;

        if (!this.model) {
            this.model = {};
        }

        Object.assign(this.model, options.operation || {});

        this.model.TaxPostingsMode = options.operation.TaxPostingsInManualMode
            ? ProvidePostingType.ByHand
            : ProvidePostingType.Auto;

        this.taxPostingsLoader = new DeferredLoader();
        this.accountingPostingsLoader = new DeferredLoader();

        this.updateOperationTypes();
        this.initCommonReactions();
        this.initApproved();
    }

    _initModel(model, options) {
        if (!this.model) {
            this.model = { ...model };
        }

        runInAction(() => {
            if (options) {
                Object.assign(this.model, options);
            }
        });
        this.initCommonReactions();
    }

    modelForSave() {
        this.throwOverrideError(`modelForSave`);
    }

    /* tax postings */
    initTaxPostings() {
        if (!this.isNew && !this.isCopy && Object.keys(this.model.TaxPostings).length && !this.shouldRegenerateTaxPostings()) {
            this.model.TaxPostings.ExplainingMessage = this.getTaxPostingsExplainingMsg() || this.model.TaxPostings.ExplainingMessage;
            /** костыль до полного переезда на новый бэк */
            this.model.TaxPostingsMode = this.model.PostingsAndTaxMode !== undefined && !this.model.TaxPostingsInManualMode ? this.model.PostingsAndTaxMode : this.model.TaxPostingsMode;
            this.setTaxPostingList(this.model.TaxPostings);
        } else {
            this.loadTaxPostings();
        }
    }

    initCommonReactions = () => {
        reaction(() => [this.model.Date], this.handleDefaultNdsType);
    };

    @action initApproved = async () => {
        if (!this.isOutsourceUser) {
            return;
        }

        try {
            const date = await getInitialDate();
            runInAction(() => { this.initialDate = date; });

            const isValidDate = dateHelper(date).isSameOrBefore(dateHelper(this.model.Date));

            if (isValidDate) {
                if (this.model?.BaseDocumentId) {
                    const res = await getApproveById({ Id: this.model?.BaseDocumentId });
                    runInAction(() => { this.isApproved = res; this.isShowApprove = true; });
                } else {
                    runInAction(() => { this.isApproved = false; this.isShowApprove = true; });
                }
            }
        } catch (e) {
            Logger.error(e);
        }
    };

    /* кейс:
    * 1. Мы на ЕНДВ, пп налогом не облагается, НУ записей нет.
    *   Сохраняем пп без проводок.
    * 2. Меняем СНО на УСН. Открываем пп, НУ записи должны быть, но в БД их нет по причине п.1
    *   соотв в ответе получаем TaxStatus === 2, тоесть, должны быть записи, и пустой список Postings
    * 3. Генерируем НУ записи заново. */
    shouldRegenerateTaxPostings() {
        const { TaxStatus, Postings } = this.model.TaxPostings;
        const { IsTypeChanged } = this.model;

        return (TaxStatus === TaxStatusEnum.Yes && !Postings.length) || IsTypeChanged;
    }

    loadTaxPostings = async () => {
        this.model.TaxPostingsMode = ProvidePostingType.Auto;

        const msg = this.getTaxPostingsExplainingMsg();

        if (msg) {
            this.model.TaxPostings = { Postings: [], ExplainingMessage: msg };
            this.taxPostingsLoading = false;

            return;
        }

        try {
            this.taxPostingsLoading = true;

            const { status, result } = await this.taxPostingsLoader.load({
                load: () => {
                    const model = mapModelForPostingsGeneration(this.modelForSave());

                    return this.taxationForPostings.IsOsno && this.isOoo
                        ? generateForOsno(model)
                        : generate(model);
                },
                getRequestData: this.getFieldsForTaxPostings
            });

            if (status !== DeferredLoader.Status.aborted) {
                const metaData = {
                    ExplainingMessage: result.ExplainingMessage,
                    HasPostings: result.HasPostings || result.Postings.length > 0,
                    TaxStatus: result.TaxStatus,
                    ...{ LinkedDocuments: mapLinkedDocumentsTaxPostingsToModel({ postings: result.LinkedDocuments, isOsno: this.isOsno, isOoo: this.isOoo }) }
                };

                /** нужно сохранять текущие НУ записи. см. метод setTaxPostingList */
                Object.assign(this.model.TaxPostings, metaData);

                this.setTaxPostingList(result);
                this.taxPostingsLoading = false;
            }
        } catch (e) {
            this.model.TaxPostings = { Error: postingGenerationError };
            this.taxPostingsLoading = false;

            throw new Error(e);
        }
    };

    get isNotTaxable() {
        this.throwOverrideError(`isNotTaxable`);

        return true;
    }

    getFieldsForTaxPostings = () => {
        this.throwOverrideError(`getFieldsForTaxPostings`);
    };

    getTaxPostingsExplainingMsg = () => {
        this.throwOverrideError(`getTaxPostingsExplainingMsg`);
    };

    getValidatedIpOsnoPosting = posting => {
        return taxPostingsValidator.getValidatedIpOsnoPosting(posting);
    }

    /** используется в Usn.js. По умолчанию проверка идет на заполнение суммы */
    getValidatedUsnPosting = posting => {
        return taxPostingsValidator.getValidatedUsnPosting(posting);
    };
    /* tax postings END */

    /* accounting postings */
    initAccountingPostings = () => {
        if (this.model.DocumentBaseId > 0 && this.model.AccountingPostings && Object.keys(this.model.AccountingPostings).length) {
            this.model.AccountingPostings.ExplainingMessage = this.getAccountingPostingsExplainingMsg() || this.model.AccountingPostings.ExplainingMessage;
            this.model.AccountingPostings.Postings = mapPostingsToModel({ postings: this.model.AccountingPostings.Postings });
            this.model.AccountingPostings.LinkedDocuments = mapLinkedDocumentsPostingsToModel({ postings: this.model.AccountingPostings?.LinkedDocuments });
        } else {
            this.loadAccountingPostings();
        }
    };

    getFieldsForAccountingPostings = () => {
        this.throwOverrideError(`getFieldsForAccountingPostings`);
    };

    getAccountingPostingsExplainingMsg = () => {
        this.throwOverrideError(`getAccountingPostingsExplainingMsg`);
    };

    loadAccountingPostings = async () => {
        const msg = this.getAccountingPostingsExplainingMsg();

        if (msg || !this.model.ProvideInAccounting) {
            this.model.AccountingPostings = { Postings: [], ExplainingMessage: msg || `` };
            this.accountingPostingsLoading = false;

            return;
        }

        try {
            this.accountingPostingsLoading = true;
            const { status, result } = await this.accountingPostingsLoader.load({
                load: () => accountingPostingService.generate(this.modelForSave()),
                getRequestData: this.getFieldsForAccountingPostings
            });

            if (status !== DeferredLoader.Status.aborted) {
                this.model.AccountingPostings = {
                    ...result,
                    ...{ Postings: mapPostingsToModel({ postings: result.Postings }) },
                    ...{ LinkedDocuments: mapLinkedDocumentsPostingsToModel({ postings: result.LinkedDocuments }) }
                };

                this.accountingPostingsLoading = false;
            }
        } catch (e) {
            this.model.AccountingPostings = { Error: postingGenerationError };
            this.accountingPostingsLoading = false;
        }
    };
    /* accounting postings END */

    throwOverrideError = methodName => {
        throw new Error(`Need to override method ${methodName} in ${this.constructor.name}`);
    };

    getPrimarySourceList = () => {
        return this.SettlementAccounts.filter(account => (account.IsActive ||
            (account.Id === this.model.SettlementAccountId || account.Id === this.model.initialPrimarySource)));
    };

    getContractorAutocomplete = async ({ query = `` }) => {
        const { SettlementAccountId, Date } = this.model;

        const data = {
            kontragentType: 1,
            onlyFounders: false,
            count: 5,
            SettlementAccountId,
            Date,
            query
        };

        return getContractorAutocomplete(data);
    };

    getContractorBankAutocomplete = async ({ query }) => {
        const data = {
            count: 5,
            query
        };

        return getContractorBankAutocomplete(data);
    };

    onClickSave = async event => {
        try {
            // Получаем действительные патенты на момент сохранения операции если СНО ПСН
            if (this.model.TaxationSystemType === TaxationSystemType.Patent) {
                this.model.CurrentActivePatents = await this.getNewActivePatents();
            }

            this.validateModel();

            if (!this.isValid()) {
                return false;
            }

            return this.handleSaveOperation(event.value);
        } catch (error) {
            return false;
        }
    };

    handleSaveOperation = async additionalActionValue => {
        if (!this.savePaymentPending) {
            try {
                const {
                    SavedBaseId, data: { DocumentBaseId = null } = {}
                } = await this.save();

                const { SettlementAccountId, OperationType } = this.model;
                const {
                    CreateNew, DownloadAcc, DownloadXLS, DownloadPDF
                } = actionEnum;

                const baseId = SavedBaseId || DocumentBaseId;

                if (this.isOutsourceUser) {
                    await putApproveById({ Id: baseId, isApproved: this.isApproved || false });
                }

                if (additionalActionValue === CreateNew) {
                    NavigateHelper.push(`reload/add/incoming/settlement/${SettlementAccountId}/${OperationType}`);

                    return;
                }

                if ([DownloadAcc, DownloadXLS, DownloadPDF].includes(additionalActionValue)) {
                    await this.download({ baseId, format: additionalActionValue });
                }

                this.onSave(toJS(this.model));
            } catch (error) {
                NotificationManager.show({
                    message: `При сохранении операции возникла ошибка`,
                    type: `error`,
                    duration: 5000
                });
            }
        }
    }

    save = async () => {
        this.savePaymentPending = true;

        if (this.model.IsTypeChanged && this.model.IsFromImport) {
            await enableShowImportRuleNewFutureDialog();
        }

        // eslint-disable-next-line consistent-return
        return new Promise(async resolve => {
            if (this.isValid()) {
                const task = this.getSaveTask();

                return resolve(task);
            }

            if (this.validationState.Number.length) {
                scrollTo();
            }
        }).finally(() => {
            if (this.model.oldOperationType !== this.model.OperationType) {
                sendChangeOperationTypeEvent(this.model);
            }

            this.savePaymentPending = false;
        });
    };

    getSaveTask = async () => {
        const saveModel = this.modelForSave();

        if (saveModel.DocumentBaseId) {
            return updatePaymentOrder(saveModel)
                .catch(e => {
                    this.savePaymentPending = false;
                    this.disabledSaveButton = true;

                    throw new Error(e);
                });
        }

        storage.save(`Scroll`, 0);
        storage.save(`tableData`, {});

        return createPaymentOrder(saveModel).catch(e => {
            this.savePaymentPending = false;
            this.disabledSaveButton = true;

            throw new Error(e);
        });
    };

    remove = () => {
        return deletePaymentOrder(this.model);
    };

    download = ({ baseId, format }) => {
        return MoneyOperationService.downloadFile(baseId, format, this.model.OperationType);
    };

    getTaxPostingForSave(posting = [], postingsAndTaxMode, TaxationSystem) {
        if (postingsAndTaxMode !== ProvidePostingType.ByHand) {
            return { IsManual: false };
        }

        if (TaxationSystem.IsOsno && this.isOoo) {
            return {
                IsManual: true,
                Postings: osnoTaxPostingsForServerNewBackend(posting, { Date: this.model.Date })
            };
        }

        return {
            IsManual: true,
            Postings: usnTaxPostingsForServerNewBackend(posting, { Date: this.model.Date })
        };
    }

    getActivePatents = async () => {
        const date = toDate(this.model.Date);
        const activePatents = await taxationSystemService.getActivePatents(date);

        return activePatents;
    }

    getNewActivePatents = async () => {
        taxationSystemService.clearCache();
        const activePatents = await this.getActivePatents();

        return activePatents;
    }

    updateActivePatents = async () => {
        const activePatents = await this.getActivePatents();

        this.setActivePatents(activePatents);
    }

    canViewPostings = async () => {
        return getAccessToPostings();
    }
}

function mapModelForPostingsGeneration(data) {
    return {
        ...data,
        PostingsAndTaxMode: ProvidePostingType.Auto,
        TaxPostings: []
    };
}
