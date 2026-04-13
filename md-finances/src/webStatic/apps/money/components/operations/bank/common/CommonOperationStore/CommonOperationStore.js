import {
    observable, toJS, runInAction, reaction, action, makeObservable
} from 'mobx';
import NavigateHelper from '@moedelo/frontend-core-v2/helpers/NavigateHelper';
import NotificationManager, { NotificationType } from '@moedelo/frontend-core-react/helpers/notificationManager';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import KontragentsForm from '@moedelo/frontend-enums/mdEnums/KontragentsForm';
import KontragentType from '@moedelo/frontend-enums/mdEnums/KontragentType';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toInt } from '@moedelo/frontend-core-v2/helpers/converter';
import Logger from '@moedelo/frontend-core-v2/helpers/logger/index';
import { getAccessToPostings } from '@moedelo/frontend-common-v2/apps/finances/service/setupDataPreloadingService';
import whiteLabelHelper from '@moedelo/frontend-common-v2/apps/marketing/helpers/whiteLabelHelper';
import { getId } from '@moedelo/frontend-core-v2/helpers/companyId';
import { showDirectPaymentInstructionsAsync } from '../../helpers/directPaymentInstructionsHelper';
import InvoiceStatusCodeEnum from '../../enums/InvoiceStatusCodeEnum';
import InvoiceStatusCodeNotificationResource from '../../resources/InvoiceStatusCodeNotificationResource';
import InvoiceStatusCodeNotificationLinkResource from '../../resources/InvoiceStatusCodeNotificationLinkResource';
import Actions from './Actions';
import accountingPostingService from '../../../../../../../services/accountingPostingService';
import DeferredLoader from '../../../../../../../helpers/newMoney/DeferredLoader';
import * as sendToBankHelper from '../../../../../../../helpers/newMoney/sendToBankHelper';
import postingGenerationError from '../../../../../../../resources/newMoney/postingGenerationError';
import ProvidePostingType from '../../../../../../../enums/ProvidePostingTypeEnum';
import { generate, generateForOsno } from '../../../../../../../services/taxPostingService';
import {
    mapLinkedDocumentsTaxPostingsToModel,
    osnoTaxPostingsForServerNewBackend,
    usnTaxPostingsForServerNewBackend
} from '../../../../../../../mappers/taxPostingsMapper';
import {
    getContractorAutocomplete,
    getContractorBankAutocomplete
} from '../../../../../../../services/newMoney/contractorService';
import {
    mapLinkedDocumentsPostingsToModel,
    mapPostingsToModel
} from '../../../../../../../mappers/accountingPostingsMapper';
import MoneyOperationService from '../../../../../../../services/newMoney/moneyOperationService';
import OrderType from '../../../../../../../enums/OrderTypeEnum';
import { calculateNdsBySumAndType } from '../../../../../../../helpers/newMoney/ndsCalculationHelper';
import taxPostingsValidator from '../../../validation/taxPostingsValidator';
import PostingsTabsEnum from '../../../../../../../enums/newMoney/PostingsTabsEnum';
import { scrollTo } from '../../../../../../../helpers/newMoney/domHelper';
import { isValidDownloadOperationActionType } from '../../../../../../../helpers/newMoney/operationActionsHelper';
import { actionEnum, actionForOperationFromWarningTable } from '../../../../../../../resources/newMoney/saveButtonResource';
import { paymentOrderOperationResources as operationTypes } from '../../../../../../../resources/MoneyOperationTypeResources';
import {
    createPaymentOrder,
    deletePaymentOrder,
    updatePaymentOrder
} from '../../../../../../../services/newMoney/newMoneyOperationService';
import BankIntegrationStatusCode from '../../../../../../../enums/BankIntegrationStatusCodeEnum';
import TaxStatusEnum from '../../../../../../../enums/TaxStatusEnum';
import { enableShowImportRuleNewFutureDialog } from '../../../../../../../services/importRuleService';
import storage from '../../../../../../../helpers/newMoney/storage';
import { sendChangeOperationTypeEvent } from '../../helpers/moneyMrkStatHelper';
import SendPaymentErrorCodeEnum from '../../../../../../../enums/newMoney/SendPaymentErrorCodeEnum';
import SendToBankErrorsResource from '../../../../../../../resources/newMoney/SendToBankErrorsResource';
import { handleSendPaymentError } from '../../../../../../../helpers/newMoney/sendToBankHelper';
import { clearDescriptionControlCharacters } from '../../../../../../../helpers/newMoney/operationHelper';
import { putApproveById, getApproveById, getInitialDate } from '../../../../../../../services/approvedService';

export default class CommonOperationStore extends Actions {
    @observable isContractorRequisitesShown = false;

    @observable savePaymentPending = false;

    @observable sendToBankPending = false;

    @observable sendToBankErrorMessage = null;

    @observable disabledSaveButton = false;

    @observable disabledSendToBankButton = false;

    @observable disabledSendInvoiceToBank = false

    @observable kontragentSettlements = [];

    @observable TaxationSystem = null;

    @observable OperationTypes = [];

    @observable taxPostingsLoading = false;

    @observable accountingPostingsLoading = false;

    @observable kontragentLoading = false;

    @observable kontragentBanksLoading = false;

    @observable isApproved = null;

    @observable isShowApprove = false;

    @observable initialDate = null;

    // for outgoing
    @observable isBusyNumberModalShown = false;

    /** в случае попытки сохранить и скачать операцию, или сохранить и отправить
     *  в банк с не уникальным номером, в operationAdditionalActionType храним тип действия. */
    @observable operationAdditionalActionType = null;

    /** нужно для управлением компонентами операции в зависимости от активной вкладки в БУНУ.
     * актуально, например, в БП */
    @observable currentPostingsTab = PostingsTabsEnum.tax;

    /** поля для работы с диалогом подтверждения по смс отправки пп в банк */
    @observable phoneMask = ``;

    @observable smsConfirmModalVisible = false;

    @observable ActivePatents = [];

    /** нужно для обработки сохранения операции с не уникальным номером */
    initialOperationNumber = null;

    /** нужно ли валидировать сумму пп и записей в НУ */
    needAllSumValidation = true;

    /** сообщение в БУ при отсутствии объектов учета */
    messageNoAccountingObjects = `По данному счёту нет ни одного объекта учёта`;

    /** Доступные формы собственности для создания контрагента */
    availableContractorForms = Object.values(KontragentsForm);

    /** Доступные типы контрагентов для создания */
    availableContractorTypes = Object.values(KontragentType);

    /** Список полей, измененных пользователем вручную */
    changedByHandFields = new Set();

    constructor(options) {
        super(options);
        makeObservable(this);

        this.Requisites = options.requisites;
        this.SettlementAccounts = options.settlementAccounts;
        this.TaxationSystem = options.taxationSystem;
        this.HasPurseAccount = options.hasPurseAccount;
        this.UserInfo = options.userInfo;
        this.onSave = options.onSave;
        this.viewPaymentNotificationPanel = options.viewPaymentNotificationPanel;
        this.setViewPaymentNotificationPanel = options.setViewPaymentNotificationPanel;

        if (!this.model) {
            this.model = {};
        }

        Object.assign(this.model, options.operation || {});

        this.initialOperationNumber = options.operation.initialOperationNumber || options.operation.Number;
        this.taxPostingsLoader = new DeferredLoader();
        this.accountingPostingsLoader = new DeferredLoader();

        this.updateOperationTypes();
        this.initCommonReactions();
        this.handleSendToBankErrorAfterCreate();
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

    /**
     * при СОЗДАНИИ операции, если жмем "сохранить и отправить в банк"
     * и происходит какая то ошибка при отправке в банк,
     * редиректим на роут редактирования операции, соотв, создается новый
     * экземпляр стора, а код ошибки пробрасываем и обрабатываем через local storage
     * */
    handleSendToBankErrorAfterCreate() {
        if (this.isNew) {
            return;
        }

        const { DocumentBaseId } = this.model;
        const errorData = sendToBankHelper.getErrorAfterOperationCreated({ DocumentBaseId });

        if (!errorData) {
            return;
        }

        const { ErrorCode, Message } = errorData;

        handleSendPaymentError({
            ErrorCode,
            DocumentBaseId,
            Message,
            callback: () => this.setSendToBankErrorMessage(SendToBankErrorsResource[ErrorCode].message)
        });

        sendToBankHelper.clearErrorAfterOperationCreated({ DocumentBaseId });
    }

    modelForSave() {
        this.throwOverrideError(`modelForSave`);
    }

    /* tax postings */
    initTaxPostings() {
        if (!this.isNew && Object.keys(this.model.TaxPostings).length && !this.shouldRegenerateTaxPostings()) {
            this.model.TaxPostings.ExplainingMessage = this.getTaxPostingsExplainingMsg() || this.model.TaxPostings.ExplainingMessage;
            this.model.TaxPostingsMode = this.model.PostingsAndTaxMode;
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
                    ...{
                        LinkedDocuments: mapLinkedDocumentsTaxPostingsToModel({
                            postings: result.LinkedDocuments,
                            isOsno: this.isOsno,
                            isOoo: this.isOoo
                        })
                    }
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
    };

    /** используется в Usn.js. По умолчанию проверка идет на заполнение суммы */
    getValidatedUsnPosting = posting => {
        return taxPostingsValidator.getValidatedUsnPosting(posting);
    };
    /* tax postings END */

    /* accounting postings */
    initAccountingPostings = () => {
        if (!this.isNew && !this.isCopy() && !this.model.IsTypeChanged) {
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
        if (!this.isOoo) {
            return;
        }

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
        return this.SettlementAccounts.filter(account => account.IsActive ||
            (account.Id === this.model.SettlementAccountId || account.Id === this.model.initialPrimarySource));
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

    calculateNdsBySumAndType = ([Sum, NdsType, IncludeNds, NdsSum]) => {
        if (NdsSum || NdsSum === 0) {
            return;
        }

        if (IncludeNds) {
            this.model.NdsSum = NdsType === null ? `` : calculateNdsBySumAndType(Sum, NdsType);
        }
    };

    isNumberBusy = async () => {
        const {
            Number, Date, SettlementAccountId, DocumentBaseId
        } = this.model;
        const memorialTypes = [
            operationTypes.BankFee.value,
            operationTypes.WithdrawalFromAccount.value
        ];
        const orderType = memorialTypes.includes(this.model.OperationType) ? OrderType.MemorialWarrant : OrderType.PaymentOrder;
        const isNumberNotBusy = await MoneyOperationService.isDocumentNumberNotBusy({
            orderType,
            Number,
            Date,
            SettlementAccountId,
            excludedDocumentBaseId: DocumentBaseId || null
        });

        return !isNumberNotBusy;
    };

    remove = () => {
        return deletePaymentOrder(this.model);
    };

    download = ({ baseId, format }) => {
        return MoneyOperationService.downloadFile(baseId, format, this.model.OperationType);
    };

    /**
     * @param {number} documentBaseId ID базового документа
     * @return {Promise<void>}
     */
    sendToBank = async documentBaseId => {
        try {
            const { onSave } = this;
            this.setSendToBankPending(true);
            this.setDisabledSaveButton(true);
            const {
                StatusCode, Message, PhoneMask, ErrorCode
            } = await MoneyOperationService.sendToBank([documentBaseId]);
            this.setSendToBankPending(false);
            this.setDisabledSaveButton(false);

            this.setOperationAdditionalActionType();

            if (StatusCode === BankIntegrationStatusCode.NeedSms) {
                runInAction(() => {
                    this.setPhoneMask(PhoneMask);
                    this.toggleSmsConfirmDialogVisibility({ isVisible: true });
                });

                return;
            }

            if (StatusCode === BankIntegrationStatusCode.Error) {
                handleSendPaymentError({
                    DocumentBaseId: documentBaseId,
                    ErrorCode,
                    Message,
                    callback: () => this.setSendToBankErrorMessage(SendToBankErrorsResource[ErrorCode].message)
                });

                return;
            }

            runInAction(() => {
                this.setPhoneMask(``);
                this.toggleSmsConfirmDialogVisibility({ isVisible: false });
            });

            NotificationManager.show({
                message: `Платежное поручение отправлено`,
                type: `success`,
                duration: 3000
            });

            onSave && onSave(toJS(this.model));
        } catch (e) {
            this.setOperationAdditionalActionType();

            sendToBankHelper.showErrorModal(SendToBankErrorsResource[SendPaymentErrorCodeEnum.Common]);
        }
    };

    sendInvoiceToBank = async documentBaseId => {
        try {
            this.setSendToBankPending(true);
            this.setDisabledSaveButton(true);

            const backUrl = `${window.location.protocol}//${window.location.host}/Finances?_companyId=${getId()}`;
            const {
                StatusCode,
                Message,
                InvoiceUrl,
                InvoiceStatusCode
            } = await MoneyOperationService.sendInvoiceToBank(documentBaseId, backUrl);

            this.setOperationAdditionalActionType();

            // пока убрал всю обработку смс для втб

            if (InvoiceStatusCode && InvoiceStatusCode !== InvoiceStatusCodeEnum.None) {
                let params = { statusCodeInvoice: InvoiceStatusCode, ...InvoiceStatusCodeNotificationResource[InvoiceStatusCode] };

                if ([InvoiceStatusCodeEnum.AccessError, InvoiceStatusCodeEnum.InvalidSettlementAccount].includes(InvoiceStatusCode)) {
                    const isWl = whiteLabelHelper.isWhiteLabel();
                    const isWlSber = whiteLabelHelper.isSberWl();
                    const link = isWl && isWlSber ? InvoiceStatusCodeNotificationLinkResource.wl : InvoiceStatusCodeNotificationLinkResource.ib;
                    this.setDisabledSendInvoiceToBank(true);
                    params = { ...params, ...link };
                }

                this.setViewPaymentNotificationPanel(params);

                return;
            }

            if (StatusCode === BankIntegrationStatusCode.Error) {
                if (documentBaseId && !window.location.hash.includes(`edit`)) {
                    NavigateHelper.replace(`edit/settlement/${documentBaseId}`);
                }

                NotificationManager.show({
                    message: `${Message}`,
                    type: NotificationType.error,
                    duration: 8000
                });

                return;
            }

            if (InvoiceUrl) {
                await showDirectPaymentInstructionsAsync(this.integrationPartner);
                NavigateHelper.push(InvoiceUrl);
            }
        } catch (e) {
            this.setOperationAdditionalActionType();

            NotificationManager.show({
                message: `При отправке п/п в банк произошла неизвестная ошибка`,
                type: NotificationType.error,
                duration: 5000
            });
        } finally {
            this.setSendToBankPending(false);
            this.setDisabledSaveButton(false);
        }
    };

    isCopy = () => {
        return window.location.hash.includes(`copy`);
    };

    save = async () => {
        this.savePaymentPending = true;
        this.closeBusyNumberModal();

        if (this.model.IsTypeChanged && this.model.IsFromImport) {
            await enableShowImportRuleNewFutureDialog();
        }

        return new Promise(async resolve => {
            const task = this.getSaveTask();

            return resolve(task);
        }).finally(() => {
            if (this.model.oldOperationType !== this.model.OperationType) {
                sendChangeOperationTypeEvent(this.model);
            }

            this.savePaymentPending = false;
        });
    };

    getSaveTask = async () => {
        const saveModel = clearDescriptionControlCharacters(this.modelForSave());

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

        if (this.initialOperationNumber === toInt(saveModel.Number)) {
            saveModel.IsSaveNumeration = true;
        }

        return createPaymentOrder(saveModel).catch(e => {
            this.savePaymentPending = false;
            this.disabledSaveButton = true;

            throw new Error(e);
        });
    };

    onClickSave = async event => {
        const { value: additionalActionValue, target } = event;

        if (this.isSavingBlocked) {
            return false;
        }

        try {
            // Получаем действительные патенты на момент сохранения операции если СНО Патент
            if (this.model.TaxationSystemType === TaxationSystemType.Patent) {
                this.model.CurrentActivePatents = await this.getNewActivePatents();
            }

            this.validateModel();

            if (this.validationState.Number.length) {
                scrollTo();
            }

            if (!this.isValid()) {
                return false;
            }

            if (target?.innerText === actionForOperationFromWarningTable.text) {
                this.setOperationAdditionalActionType(actionForOperationFromWarningTable.value);
            }

            if (isValidDownloadOperationActionType(additionalActionValue)) {
                this.setOperationAdditionalActionType(additionalActionValue);
            }

            if (this.needToCheckBusyNumber) {
                const isNumberBusy = await this.isNumberBusy();

                if (isNumberBusy) {
                    this.showBusyNumberModal();

                    return false;
                }
            }

            return this.handleSaveOperation();
        } catch (e) {
            return null;
        }
    };

    handleButtonsState = operationAdditionalActionType => {
        const { SendToBank, SendInvoiceToBank } = actionEnum;
        const needToDisableSaveButton = [SendToBank, SendInvoiceToBank].includes(operationAdditionalActionType);

        if (needToDisableSaveButton) {
            this.setDisabledSaveButton(true);
        } else {
            this.setDisabledSendToBankButton(true);
        }
    };

    handleSaveOperation = async () => {
        if (!this.savePaymentPending) {
            try {
                const { operationAdditionalActionType, onSave, model } = this;
                const { SettlementAccountId, OperationType } = model;
                const {
                    CreateNew, DownloadAcc, DownloadPDF, DownloadXLS, SendToBank, SaveAndGoToNext, SendInvoiceToBank
                } = actionEnum;

                this.handleButtonsState(operationAdditionalActionType);

                const { data: { DocumentBaseId, TransferFromAccountBaseId } } = await this.save();

                model.DocumentBaseId = DocumentBaseId;

                if (this.isOutsourceUser) {
                    await putApproveById({ Id: model.DocumentBaseId, isApproved: this.isApproved || false });

                    if (TransferFromAccountBaseId) {
                        await putApproveById({ Id: TransferFromAccountBaseId, isApproved: this.isApproved || false });
                    }
                }

                if (operationAdditionalActionType === CreateNew) {
                    NavigateHelper.push(`reload/add/outgoing/settlement/${SettlementAccountId}/${OperationType}`);

                    return;
                }

                if ([DownloadAcc, DownloadXLS, DownloadPDF].includes(operationAdditionalActionType)) {
                    await this.download({ baseId: DocumentBaseId, format: operationAdditionalActionType });
                }

                if (operationAdditionalActionType === SendToBank) {
                    await this.sendToBank(DocumentBaseId);

                    return;
                }

                if (operationAdditionalActionType === SendInvoiceToBank) {
                    await this.sendInvoiceToBank(DocumentBaseId);

                    return;
                }

                if (operationAdditionalActionType === SaveAndGoToNext) {
                    mrkStatService.sendEventWithoutInternalUser(`saveandgotonext_stranica_operacii_click_button`);

                    const warningOperations = storage.get(`warningOperations`);
                    const currentOperationIndex = warningOperations.findIndex(p => p.documentBaseId === DocumentBaseId);

                    if (currentOperationIndex !== -1) {
                        warningOperations.splice(currentOperationIndex, 1);
                    }

                    // удаляем из warningOperations текущую операцию
                    storage.save(`warningOperations`, warningOperations);

                    if (warningOperations.length) {
                        const { documentBaseId, operationType } = warningOperations[0];
                        NavigateHelper.replace(`edit/settlement/${documentBaseId}/${operationType}`);
                    }

                    return;
                }

                onSave && onSave(toJS(model));
            } catch (e) {
                NotificationManager.show({
                    message: `При сохранении операции возникла ошибка`,
                    type: `error`,
                    duration: 5000
                });

                throw new Error(e);
            } finally {
                this.setDisabledSaveButton(false);
                this.model.isSubmitAttempted = false;
            }
        }
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
        const date = dateHelper(this.model.Date).toDate();
        const activePatents = await taxationSystemService.getActivePatents(date);

        return activePatents;
    };

    getNewActivePatents = async () => {
        taxationSystemService.clearCache();
        const activePatents = await this.getActivePatents();

        return activePatents;
    };

    updateActivePatents = async () => {
        const activePatents = await this.getActivePatents();

        this.setActivePatents(activePatents);
    };

    canViewPostings = async () => {
        return getAccessToPostings();
    };
}

function mapModelForPostingsGeneration(data) {
    return {
        ...data,
        PostingsAndTaxMode: ProvidePostingType.Auto,
        TaxPostings: []
    };
}
