import { makeObservable, observable, reaction } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import logger from '@moedelo/frontend-core-v2/helpers/logger';
import Actions from './Actions';
import defaultModel from './Model';
import DeferredLoader from '../../../../../../../../helpers/newMoney/DeferredLoader';
import validationRules from './../validationRules';
import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';
import notTaxableReasonGetter from './../notTaxableReasonGetter';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import {
    getDefaultFieldsByKbk,
    getBudgetaryPaymentKbkAutocomplete,
    getUnifiedBudgetaryPaymentDefaultFieldsByKbk
} from '../../../../../../../../services/newMoney/kbkService';
import { getBanksAutocomplete } from '../../../../../../../../services/newMoney/bankService';
import { getDefaultTaxationSystemType } from '../../../../../../../../mappers/taxationSystemMapper';
import { getDefaulNds } from '../../../../../../../../helpers/newMoney/ndsCalculationHelper';
import PostingDirection from '../../../../../../../../enums/newMoney/TaxPostingDirectionEnum';
import { removeKbkFromBankName } from '../../../../../../../../helpers/newMoney/budgetaryPayment/budgetaryKontragentHelper';
import PostingTransferType from '../../../../../../../../enums/newMoney/TaxPostingTransferTypeEnum';
import TaxPostingTransferKind from '../../../../../../../../enums/newMoney/TaxPostingTransferKindEnum';
import TaxPostingNormalizedCostType from '../../../../../../../../enums/newMoney/TaxPostingNormalizedCostTypeEnum';
import SyntheticAccountCodesEnum from '../../../../../../../../enums/SyntheticAccountCodesEnum';
import AccountTypeEnum from '../../../../../../../../enums/newMoney/budgetaryPayment/BudgetaryAccountTypeEnum';
import CalendarTypesEnum from '../../../../../../../../enums/newMoney/budgetaryPayment/BudgetaryCalendarTypesEnum';
import { getBudgetaryMetadataAsync } from '../../../../../../../../services/newMoney/budgeratyMetadataService';
import { parseComplexNumberToObj } from '../helpers/documentNumberHelper';
import { getCommonBudgetaryPaymentSaveModel, getUnifiedBudgetaryPaymentSaveModel } from '../helpers/savingHelper';
import scrollTo from '../../../../../../../../helpers/newMoney/domHelper';
import { actionForOperationFromWarningTable } from '../../../../../../../../resources/newMoney/saveButtonResource';
import { isValidDownloadOperationActionType } from '../../../../../../../../helpers/newMoney/operationActionsHelper';

class BudgetaryPaymentStore extends Actions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Uin: ``,
        Recipient: ``,
        SettlementAccount: ``,
        BankCorrespondentAccount: ``,
        BankName: ``,
        Inn: ``,
        Kpp: ``,
        Okato: ``,
        Oktmo: ``,
        KBK: ``,
        DocumentsSum: ``,
        DocumentDate: ``,
        DocumentNumber: ``,
        Description: ``,
        Period: ``
    };

    @observable model = { ...defaultModel };

    @observable KbkDefault = [];

    @observable kbkLoading = false;

    @observable loading = true;

    @observable kbkAutoFieldsLoading = false;

    @observable isFullKbkAutoFieldsShown = false;

    @observable TradingObjectList = [{ text: ``, value: 0 }];

    @observable metaData = {};

    @observable UnifiedBudgetaryPaymentStore;

    @observable prevAccountCode = null;

    @observable isSubmitAttempted = false;

    isCreatedNDFLin2023Message = `Данная операция была создана отдельно ошибочно, но ничего страшного, можете оставить её как есть, никаких ошибок в учёте это не повлечёт. С 2023 года НДФЛ необходимо платить с помощью ЕНП.`;

    isBudgetaryPayment = true;

    availableTaxPostingDirection = AvailableTaxPostingDirection.Outgoing;

    /** флаг, означающий, было ли выбрано основание платежа ТР, либо выбрано другое после него */
    isTPPaymentFoundationTouched = false;

    patents = [];

    prevDate = null;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        /** При копировании БП, у которого есть связанные инвойсы нужно подчищать НУ проводки полученные с бэка. Т.к. бэк их присылает не зная, что это копия.
         * Вместе с НУ проводками нужно еще удалить сами инвойсы, т.к. у копии их быть не должно.
         */
        if (this.isCopy() && this.model?.CurrencyInvoices?.length) {
            Object.assign(this.model.TaxPostings, { Postings: [], LinkedDocuments: [] });
            this.model.CurrencyInvoices = [];
        }

        this.model.TaxPostingsMode = options.operation.TaxPostingsInManualMode && !this.model?.CurrencyInvoices?.length
            ? ProvidePostingType.ByHand
            : ProvidePostingType.Auto;
        // родительский store ожидает поле PostingsAndTaxMode
        this.model.PostingsAndTaxMode = this.model.TaxPostingsMode;

        this.patents = options.patents || [];

        this.UnifiedBudgetaryPaymentStoreC = options.UnifiedBudgetaryPaymentStore;

        this.kbkListLoader = new DeferredLoader();
        this.kbkAutofieldsLoader = new DeferredLoader();
        this.metaDataLoader = new DeferredLoader();
        this.initBudgetaryDataAsync();
        this.initCommonReactions();
    }

    checkAndInitUnifiedBudgetaryPaymentStore = async () => {
        if (!this.UnifiedBudgetaryPaymentStore && this.isUnifiedBudgetaryPayment) {
            this.UnifiedBudgetaryPaymentStore = new this.UnifiedBudgetaryPaymentStoreC({
                budgetaryPaymentModel: this.model,
                canEdit: this.canEdit,
                TaxationSystem: this.TaxationSystem,
                Requisites: this.Requisites,
                TradingObjectList: this.TradingObjectList,
                patents: this.patents,
                budgetaryPaymentStoreMethods: {
                    validateField: this.validateField.bind(this),
                    modelForSave: this.modelForSave.bind(this),
                    setSum: this.setSum.bind(this),
                    loadAccountingPostings: this.loadAccountingPostings.bind(this),
                    setDescription: this.setDescription.bind(this)
                }
            });

            await this.UnifiedBudgetaryPaymentStore.init();
        }
    }

    initBudgetaryDataAsync = async () => {
        try {
            await this.initBudgetaryModel(this.model);

            /** АХТУНГ! Костыль от старого бэка, может быть актуален. 10.09.2019
             *  на случай, если при импорте в выписке был устаревший кбк
             if (!this.isNew && this.model.Kbk.Id && !this.KbkDefault.length) {
                await this.loadKbkList();
            }
             */

            await this.loadKbkList();

            const { PayerStatus, PaymentBase } = this.model;

            /** на случай, если мы меняем тип существующей операции на БП */
            if (!this.model.DocumentBaseId || [PayerStatus, PaymentBase].includes(null)) {
                await this.loadKbkAutoFields({ setOnlyNull: true });
            }

            /** при смене типа операции на БП. нужно обнулить Id,
             *  чтобы загрузить специфичные поля БП, но не загружать
             *  их при открытии на редактирование БП(см выше),
             *  а затем восстановить Id операции для возможности сохранения */
            if (!this.isCopy() && this.model.PreviousOperationId) {
                this.model.Id = this.model.PreviousOperationId;
                delete this.model.PreviousOperationId;
            }

            await this.initTaxPostings();
            await this.initAccountingPostings();

            this.isFullKbkAutoFieldsShown = this.isOtherTaxesAndFees;

            this.initBudgetaryReactions();
        } catch (e) {
            logger.error(e);
        } finally {
            this.loading = false;
        }
    };

    initCommonReactions = () => {
        reaction(() => this.model.Date, async date => {
            if (!this.validationState.Date) {
                const curYear = dateHelper(date).year();
                const prevYear = dateHelper(this.prevDate).year();
                const prevAccounts = [...this.metaData.Accounts];
                this.setTaxationSystem(await taxationSystemService.getTaxSystem(date));

                await this.loadMetadata();

                if (curYear !== prevYear) {
                    const needToUpdateAccountCode = prevAccounts.length !== this.metaData.Accounts.length;

                    needToUpdateAccountCode && this.onChangeAccountCode({ value: this.metaData.Accounts[0].Code });
                    this.checkAndInitUnifiedBudgetaryPaymentStore();
                }
            }
        });

        reaction(this.getFieldsForTaxPostings, this.loadTaxPostings);
        reaction(this.getFieldsForAccountingPostings, this.loadAccountingPostings);
    };

    initBudgetaryReactions = () => {
        reaction(this.getFieldsForAutoFieldsReaction, this.loadKbkList);
        reaction(() => [this.model.AccountCode], () => {
            this.updatePeriodType();
            this.updateTaxationSystemType();

            /** прочие налоги и сборы, нужно сбросить кбк показать пустое поле ввода */
            if (this.isOtherTaxesAndFees) {
                this.resetKbk();
                this.setDescription(``, { needValidate: false });
                this.isFullKbkAutoFieldsShown = true;
            }
        });
        reaction(() => [this.model.Sum], () => {
            if (this.isTradingFee && this.model.TaxPostings.Postings.length > 0) {
                this.model.TaxPostings.Postings[0].Sum = this.model.Sum;
            }
        });

        reaction(this.getFieldsForLinkedCurrencyInvoicesReaction, () => {
            this.resetLinkedCurrencyInvoices();
        });

        reaction(() => [this.model.Date, this.model.PaymentBase], this.handleDocumentNumberComplexity);
        reaction(() => [this.model.Date], this.loadKbkList);
    };

    initTaxPostings() {
        if ((!this.isNew || this.isCopy()) && Object.keys(this.model.TaxPostings).length) {
            this.model.TaxPostings.ExplainingMessage = this.getTaxPostingsExplainingMsg() || this.model.TaxPostings.ExplainingMessage;
            this.model.TaxPostingsMode = this.model.PostingsAndTaxMode;
            this.setTaxPostingList(this.model.TaxPostings);
        } else {
            this.loadTaxPostings();
        }
    }

    getMetadata = async () => getBudgetaryMetadataAsync(dateHelper(this.model.Date).format(`YYYY-MM-DD`));

    loadKbkList = async () => {
        try {
            this.kbkLoading = true;

            const { status, result } = await this.kbkListLoader.load({
                load: () => getBudgetaryPaymentKbkAutocomplete(this.getRequestDataForKbkList()),
                getRequestData: this.getFieldsForAutoFieldsReaction
            });

            if (status !== DeferredLoader.Status.aborted) {
                this.setKbkDefault(result);

                if (!result?.length && !this.isOtherTaxesAndFees) {
                    this.resetKbk();
                }

                this.loadKbkAutoFields();
            }
        } catch (e) {
            // eslint-disable-next-line no-console
            console.log(e);
            this.kbkLoading = false;
        }
    };

    onClickSave = async event => {
        const { value: additionalActionValue, target } = event;
        this.model.isSubmitAttempted = true;
    
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
    
            this.isSubmitAttempted = false;

            return this.handleSaveOperation();
        } catch (e) {
            return null;
        }
    };

    getBanksAutocompleteData = async ({ query }) => {
        const formattedQuery = removeKbkFromBankName(query);
        const data = {
            count: 5,
            query: formattedQuery
        };
        const { List = [] } = await getBanksAutocomplete(data);

        if (!query) {
            this.model.Recipient.BankName = ``;
            this.validateField(`BankName`);
        }

        return {
            data: List.map(bank => ({
                value: bank.Name,
                text: `${bank.Name}${bank.Bik ? ` // ${bank.Bik}` : ``}`,
                original: bank
            })),
            value: formattedQuery
        };
    };

    needToReloadDefaultFieldsByKbkAfterUnifiedBudgetaryPayment = () => {
        return this.isNew
            ? (this.wasUnifiedBudgetaryPayment || this.isUnifiedBudgetaryPayment)
            : ((this.prevAccountCode || this.wasNotBudgetaryPayment) && (this.wasUnifiedBudgetaryPayment || this.isUnifiedBudgetaryPayment));
    };

    needToReloadOnFeesPayment = () => (this.prevAccountCode && (this.isFeesPayment || this.wasFeesPayment));

    loadKbkAutoFields = async ({ setOnlyNull = false } = {}) => {
        /** на случай, если мы меняем тип существующей операции на БП */

        if (
            !this.isNew && !this.isCopy() && ((this.model.PaymentBase && !this.wasOldBudgetaryPayment && !this.needToReloadOnFeesPayment()) ||
            !this.needToReloadDefaultFieldsByKbkAfterUnifiedBudgetaryPayment())
        ) {
            return;
        }

        try {
            if (this.model.Kbk.Id || this.isOtherTaxesAndFees || this.isUnifiedBudgetaryPayment) {
                this.kbkLoading = true;
                const method = this.isUnifiedBudgetaryPayment ? getUnifiedBudgetaryPaymentDefaultFieldsByKbk : getDefaultFieldsByKbk;
                const { status, result } = await this.kbkListLoader.load({
                    load: () => method(this.getRequestDataForKbkAutoFields()),
                    getRequestData: this.getFieldsForAutoFieldsReaction
                });

                if (status !== DeferredLoader.Status.aborted) {
                    const { Status, StatisticsAction, ...fields } = result;

                    if (this.isTPPaymentFoundationTouched) {
                        delete fields.PaymentBase;
                    }

                    this.setKbkAutoFields(fields, { setOnlyNull });
                }
            }
        } catch (e) {
            // eslint-disable-next-line no-console
            console.log(e);
        } finally {
            /** нужно для генерации бу/ну после обновления полей кбк, периода и т.д. */
            this.loadTaxPostings();
            this.loadAccountingPostings();
            this.kbkLoading = false;
        }
    };

    getRequestDataForKbkList = () => {
        const { KbkPaymentType, AccountCode, Date } = this.model;
        const date = dateHelper(Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`);
        const periodDate = dateHelper(this.model.Period.Date, `DD.MM.YYYY`);

        return {
            AccountCode,
            PaymentType: KbkPaymentType,
            Date: dateHelper(Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`) || `2019-01-01`,
            Period: {
                Month: this.model.Period.Month,
                HalfYear: this.model.Period.HalfYear,
                Quarter: this.model.Period.Quarter,
                Year: this.model.Period.Year,
                Date: periodDate.isValid() ? periodDate.format(`YYYY-MM-DD`) : date,
                Type: this.model.Period.Type
            }
        };
    };

    getRequestDataForKbkAutoFields = () => {
        const {
            Date, AccountCode, Kbk: { Id: KbkId }, TradingObjectId
        } = this.model;
        const date = dateHelper(Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`);
        const periodDate = dateHelper(this.model.Period.Date, `DD.MM.YYYY`);

        return {
            AccountCode,
            KbkId,
            Period: {
                Month: this.model.Period.Month,
                HalfYear: this.model.Period.HalfYear,
                Quarter: this.model.Period.Quarter,
                Year: this.model.Period.Year,
                Date: periodDate.isValid() ? periodDate.format(`YYYY-MM-DD`) : date,
                Type: this.model.Period.Type
            },
            TradingObjectId,
            Date: date
        };
    };

    getFieldsForAutoFieldsReaction = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.AccountType,
            this.model.KbkPaymentType,
            this.model.Kbk.Id,
            this.model.Kbk.Number,
            this.model.AccountCode,
            this.model.TradingObjectId,
            this.model.Period.Type,
            this.model.Period.Month,
            this.model.Period.Quarter,
            this.model.Period.HalfYear,
            this.model.Period.Year,
            this.model.Period.Date
        ]);
    };

    getFieldsForLinkedCurrencyInvoicesReaction = () => {
        const fields = [
            this.model.Sum,
            this.model.KbkPaymentType,
            this.model.Kbk.Id,
            this.model.Kbk.Number
        ];

        return JSON.stringify(fields);
    };

    isValid = () => {
        const options = {
            Sum: this.model.Sum,
            needAllSumValidation: this.needAllSumValidation,
            isOsno: this.isOsno
        };
        let isSubPaymentsValid = true;

        if (this.isUnifiedBudgetaryPayment) {
            isSubPaymentsValid = this.UnifiedBudgetaryPaymentStore.isValid();
        }

        return !Object.keys(this.validationState).find(key => this.validationState[key] !== ``)
            && (this.isNotTaxable || taxPostingsValidator.isValid(this.model.TaxPostings.Postings, options))
            && isSubPaymentsValid;
    };

    validateModel() {
        Object.keys(validationRules).forEach(fieldName => {
            this.validateField(fieldName, this.model[fieldName]);
        });

        this.checkKontragentRequisitesVisibility();
        !this.isNotTaxable && this.validateTaxPostingsList();
    }

    /* override */
    modelForSave = () => {
        return this.isUnifiedBudgetaryPayment ? getUnifiedBudgetaryPaymentSaveModel(this) : getCommonBudgetaryPaymentSaveModel(this);
    };

    /* override */
    getFieldsForTaxPostings = () => {
        const fields = [
            this.model.Date,
            this.model.Sum,
            this.model.Status,
            this.model.TradingObjectId,
            this.TaxationSystem.StartYear
        ];

        /** при создании операции загрузка БУ/НУ завязана
         *  на изменение кбк и так называемых kbk auto fields.
         *  а т.к. при редактировании операции мы их не изменяем автоматически,
         *  а бу/ну перезагружать нужно */
        if (!this.isNew) {
            fields.push(this.model.Kbk.Id, this.model.Kbk.Number);
        }

        return JSON.stringify(fields);
    };

    /* override */
    getRequiredFieldsForTaxPostingMsg = () => {
        if (this.isIpOsno && this.isTradingFee) {
            return null;
        }

        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForTaxPostings.date;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForTaxPostings.sum;
        }

        if (this.model.Status === DocumentStatusEnum.NotPayed) {
            return requiredFieldForTaxPostings.notPaid;
        }

        if (!this.model.Kbk.Number?.length || this.validationState.KBK?.length) {
            return requiredFieldForTaxPostings.kbk;
        }

        return null;
    };

    /* override */
    getTaxPostingsExplainingMsg = () => {
        const options = {
            taxationSystem: this.TaxationSystem,
            isTradingFee: this.isTradingFee,
            isIp: this.isIp,
            accountCode: this.model.AccountCode,
            accountType: this.model.AccountType,
            kbk: this.model.Kbk,
            isIpOsno: this.isIpOsno
        };

        return notTaxableReasonGetter.get(options) || this.getRequiredFieldsForTaxPostingMsg();
    };

    /* override */
    getFieldsForAccountingPostings = () => {
        const fields = [
            this.model.Sum,
            this.model.Date,
            this.model.SettlementAccountId,
            this.model.Status,
            this.model.ProvideInAccounting,
            this.model.TradingObjectId,
            this.model.KbkPaymentType
        ];

        /** см getFieldsForTaxPostings */
        if (!this.isNew) {
            fields.push(this.model.Kbk.Id, this.model.Kbk.Number);
        }

        return JSON.stringify(fields);
    };

    /* override */
    getRequiredFieldsForAccountingPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForAccountingPostings.date;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForAccountingPostings.sum;
        }

        if (this.model.Status === DocumentStatusEnum.NotPayed) {
            return requiredFieldForAccountingPostings.notPaid;
        }

        if (!this.isUnifiedBudgetaryPayment && (!this.model.Kbk.Number?.length || this.validationState.KBK?.length > 0)) {
            return requiredFieldForAccountingPostings.kbk;
        }

        if (this.isUnifiedBudgetaryPayment && !this.UnifiedBudgetaryPaymentStore?.isValid()) {
            return requiredFieldForAccountingPostings.subPayments;
        }

        return null;
    };

    /* override */
    getAccountingPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForAccountingPostingMsg();
    };

    getDefaultFieldsForBudgetaryModel = currentModel => {
        const defaultFields = {};

        if (currentModel.DocumentBaseId && currentModel.Description) {
            defaultFields.Description = currentModel.Description;
        }

        if (currentModel.Sum !== 0) {
            defaultFields.Sum = currentModel.Sum;
        }

        if (currentModel.DocumentDate === null) {
            defaultFields.DocumentDate = 0;
        }

        if (currentModel.DocumentNumber === null) {
            defaultFields.DocumentNumber = 0;
        }

        if (currentModel.KbkPaymentType === null) {
            defaultFields.KbkPaymentType = 0;
        }

        if (currentModel.AccountCode === null) {
            defaultFields.AccountCode = SyntheticAccountCodesEnum.ndfl;
        }

        if (!currentModel.TaxationSystemType) {
            defaultFields.TaxationSystemType = getDefaultTaxationSystemType(this.TaxationSystem);
        }

        if (currentModel.NdsType === null) {
            defaultFields.NdsType = getDefaulNds(this.model.Date);
        }

        return defaultFields;
    };

    getTransferType = ({ Direction }) => {
        if (Direction === PostingDirection.Outgoing) {
            return [
                PostingTransferType.Direct,
                PostingTransferType.Indirect,
                PostingTransferType.NonOperating
            ];
        }

        return [
            PostingTransferType.NonOperating,
            PostingTransferType.OperationIncome
        ];
    };

    getTransferKind = ({ Direction, Type }) => {
        if (Direction === PostingDirection.Outgoing) {
            if (Type === PostingTransferType.Direct) {
                return [TaxPostingTransferKind.Material];
            }

            if (Type === PostingTransferType.Indirect) {
                return [
                    TaxPostingTransferKind.Material,
                    TaxPostingTransferKind.Salary,
                    TaxPostingTransferKind.Amortization,
                    TaxPostingTransferKind.OtherOutgo

                ];
            }

            return [TaxPostingTransferKind.None];
        }

        if (Type === PostingTransferType.OperationIncome) {
            return [
                TaxPostingTransferKind.Service,
                TaxPostingTransferKind.ProductSale,
                TaxPostingTransferKind.PropertyRight,
                TaxPostingTransferKind.OtherPropertySale
            ];
        }

        return [TaxPostingTransferKind.None];
    };

    getNormalizedCostType = ({ Direction }) => {
        if (Direction === PostingDirection.Outgoing) {
            return Object.values(TaxPostingNormalizedCostType);
        }

        return [TaxPostingNormalizedCostType.None];
    };

    updatePeriodType = () => {
        if (this.showSingleCalendar) {
            this.setDefaultCalendarType(CalendarTypesEnum.Date);
        } else if (this.model.AccountCode === SyntheticAccountCodesEnum.patent && this.model.AccountType === AccountTypeEnum.Taxes.value) {
            this.setDefaultCalendarType(CalendarTypesEnum.Month);
        } else if (this.model.Period.Type === CalendarTypesEnum.Date) {
            const selectedAccount = this.metaData?.Accounts?.find(account => account.Code === this.model.AccountCode);

            this.setDefaultCalendarType(selectedAccount?.DefaultPeriodType);
        }
    };

    updateTaxationSystemType = () => {
        const defaultTaxationSystemType = getDefaultTaxationSystemType(this.TaxationSystem);

        switch (this.model.AccountCode) {
            case SyntheticAccountCodesEnum.patent:
                this.setTaxationSystemType({ value: TaxationSystemType.Patent });

                break;
            default:
                this.model.TaxationSystemType !== defaultTaxationSystemType &&
                this.setTaxationSystemType({ value: defaultTaxationSystemType });

                break;
        }
    };

    initComplexDocumentNumber = () => {
        if (!this.model.isComplexDocumentNumber) {
            return;
        }

        const { literalCode, value } = parseComplexNumberToObj(this.model.DocumentNumber, this.metaData);

        this.setComplexNumberLiteralCode({ value: literalCode });
        this.setComplexNumberValue({ value });
    };
}

export default BudgetaryPaymentStore;
