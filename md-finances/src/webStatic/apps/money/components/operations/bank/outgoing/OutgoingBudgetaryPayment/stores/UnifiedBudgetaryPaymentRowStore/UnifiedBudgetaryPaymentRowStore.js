import {
    observable, runInAction, reaction, makeObservable
} from 'mobx';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Actions from './Actions';
import { getUnifiedBudgetaryPaymentKbkAutocomplete } from '../../../../../../../../../services/newMoney/kbkService';
import model from './Model';
import PostingTransferType from '../../../../../../../../../enums/newMoney/TaxPostingTransferTypeEnum';
import TaxPostingTransferKind from '../../../../../../../../../enums/newMoney/TaxPostingTransferKindEnum';
import TaxPostingNormalizedCostType from '../../../../../../../../../enums/newMoney/TaxPostingNormalizedCostTypeEnum';
import DocumentStatusEnum from '../../../../../../../../../enums/DocumentStatusEnum';
import notTaxableReasonGetter from '../../notTaxableReasonGetter';
import requiredFieldForTaxPostings from '../../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import ProvidePostingType from '../../../../../../../../../enums/ProvidePostingTypeEnum';
import {
    generate,
    generateForOsno,
    getByBankOperationNewBackend
} from '../../../../../../../../../services/taxPostingService';
import DeferredLoader from '../../../../../../../../../helpers/newMoney/DeferredLoader';
import {
    mapLinkedDocumentsTaxPostingsToModel,
    osnoTaxPostingsForServerNewBackend, usnTaxPostingsForServerNewBackend
} from '../../../../../../../../../mappers/taxPostingsMapper';
import { mapCurrencyInvoicesToFrontendModel } from '../../../../../../../../../mappers/currencyInvoicesMapper';
import postingGenerationError from '../../../../../../../../../resources/newMoney/postingGenerationError';
import { paymentOrderOperationResources } from '../../../../../../../../../resources/MoneyOperationTypeResources';
import taxPostingsValidator from '../../../../../validation/taxPostingsValidator';

export default class UnifiedBudgetaryPaymentRowStore extends Actions {
    @observable model = null;

    @observable KbkDefault = [];

    @observable validationState = {
        AccountCode: ``,
        KBK: ``,
        Sum: ``,
        TradingObject: ``,
        Patent: ``
    };

    @observable isSomeFieldDirty = false;

    @observable accountCodes = [];

    TaxationSystem = {};

    Requisites = {};

    TradingObjectList = [];

    needAllSumValidation = true;

    budgetaryPaymentTypeLabel = `Вид налога/взноса`;

    constructor(options) {
        super(options);
        makeObservable(this);

        this.model = options.model || model;
        this.accountCodes = options.store.accountCodes;
        this.budgetaryPaymentModel = options.budgetaryPaymentModel;
        this.TaxationSystem = options.TaxationSystem;
        this.Requisites = options.Requisites;
        this.TradingObjectList = options.TradingObjectList;
        this.patents = options.patents;
        this.model.Date = options.budgetaryPaymentModel.Date;
        this.canEdit = options.canEdit;
        this.unifiedBudgetaryPaymentStore = options.store;

        this.modelForSave = options.modelForSave;

        this.subTaxPostingsLoader = new DeferredLoader();

        this.initTaxPostings();
        this.initReactions();
    }

    initReactions = () => {
        const { loadAccountingPostings } = this.unifiedBudgetaryPaymentStore.budgetaryPaymentStoreMethods;
        reaction(() => Object.values(this.validationState), () => {
            const isSomeFieldDirty = Object.values(this.validationState).some(value => !!value);

            runInAction(() => {
                this.isSomeFieldDirty = isSomeFieldDirty;
            });

            !isSomeFieldDirty && loadAccountingPostings();
        });

        reaction(this.getFieldsForDescriptionReaction, () => {
            !this.isEmpty && this.unifiedBudgetaryPaymentStore.reloadDescription();
        });

        reaction(this.getFieldsForAutoFieldsReaction, this.loadKbkList);
        reaction(this.getFieldsForTaxPostings, this.loadSubTaxPostings);
        reaction(() => [this.budgetaryPaymentModel.Date], this.setAccountCodes);
        reaction(() => [this.model?.Period?.Year], this.setAccountCodes);
    }

    isCopy = () => {
        return window.location.hash.includes(`copy`);
    };

    async initTaxPostings() {
        if (!this.isNew || this.isCopy()) {
            const options = {
                baseId: this.model.DocumentBaseId,
                taxationSystem: this.TaxationSystem,
                isOoo: this.isOoo
            };
            const postings = await getByBankOperationNewBackend(options);

            this.model.TaxPostings.ExplainingMessage = this.getTaxPostingsExplainingMsg() || postings.ExplainingMessage;
            this.model.TaxPostingsMode = postings.TaxStatus;
            this.setTaxPostingList(postings);
        } else {
            this.loadSubTaxPostings();
        }
    }

    static getDefaultFilled = options => {
        const store = new UnifiedBudgetaryPaymentRowStore(options);

        store.setPeriod();
        store.loadKbkList();

        return store;
    }

    static getFromModel = async ({ subPayment, options }) => {
        const remappedModel = {
            ...model, ...subPayment, AccountCode: subPayment.Kbk.AccountCode, CurrencyInvoices: mapCurrencyInvoicesToFrontendModel(subPayment.CurrencyInvoices.Data)
        };
        const store = new UnifiedBudgetaryPaymentRowStore({ ...options, model: remappedModel });

        await store.loadKbkList();

        return store;
    }

    loadKbkList = async () => {
        if (!this.model.AccountCode) {
            return;
        }

        const kbkDefault = await getUnifiedBudgetaryPaymentKbkAutocomplete(this.getRequestDataForKbkList());

        runInAction(() => {
            this.KbkDefault = kbkDefault;

            if (!this.model.Kbk.Id) {
                this.setKbk({ value: kbkDefault[0]?.Id });
            }

            if (!kbkDefault.length) {
                this.setKbk({ value: null });
            }
        });
    }

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

    loadSubTaxPostings = async () => {
        this.model.TaxPostingsMode = ProvidePostingType.Auto;

        const msg = this.getTaxPostingsExplainingMsg();

        if (msg) {
            this.model.TaxPostings = { Postings: [], ExplainingMessage: msg };
            this.setTaxPostingLoading(false);

            return;
        }

        try {
            this.setTaxPostingLoading(true);

            const { status, result } = await this.subTaxPostingsLoader.load({
                load: () => {
                    const mappedModel = this.mapModelForPostingsGeneration();

                    return this.isOsno && this.isOoo
                        ? generateForOsno(mappedModel)
                        : generate(mappedModel);
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
                this.setTaxPostingLoading(false);
                !this.isEmpty && this.unifiedBudgetaryPaymentStore.budgetaryPaymentStoreMethods.loadAccountingPostings();
            }
        } catch (e) {
            this.model.TaxPostings = { Error: postingGenerationError };
            this.setTaxPostingLoading(false);

            throw new Error(e);
        }
    };

    getFieldsForAutoFieldsReaction = () => {
        return JSON.stringify([
            this.model.Period.Month,
            this.model.Period.Quarter,
            this.model.Period.HalfYear,
            this.model.Period.Year
        ]);
    };

    getFieldsForDescriptionReaction = () => {
        return JSON.stringify([
            this.model.Sum,
            this.model.Date,
            this.model.PatentId,
            this.model.TradingObjectId,
            this.model.Kbk.Id,
            this.model.Period.Type,
            this.model.Period.Month,
            this.model.Period.Quarter,
            this.model.Period.HalfYear,
            this.model.Period.Year,
            this.budgetaryPaymentModel.Status
        ]);
    };

    getRequestDataForKbkList = () => {
        const { AccountCode, Period } = this.model;
        const {
            Month, HalfYear, Quarter, Year, Type
        } = Period;

        return {
            AccountCode,
            Date: dateHelper(this.budgetaryPaymentModel.Date).format(`YYYY-MM-DD`) || `2019-01-01`,
            Period: {
                Month,
                HalfYear,
                Quarter,
                Year,
                Type
            }
        };
    }

    /* override */
    getRequiredFieldsForTaxPostingMsg = () => {
        if (this.isIpOsno && this.isTradingFee) {
            return null;
        }

        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForTaxPostings.date;
        }

        if (!this.model.AccountCode) {
            return requiredFieldForTaxPostings.accountCode;
        }

        if (!this.model.Sum) {
            return requiredFieldForTaxPostings.sum;
        }

        if (this.budgetaryPaymentModel.Status === DocumentStatusEnum.NotPayed) {
            return requiredFieldForTaxPostings.notPaid;
        }

        if (!this.model.Kbk.Number?.length || this.validationState.KBK?.length) {
            return requiredFieldForTaxPostings.kbk;
        }

        return null;
    };

    /* override */
    getFieldsForTaxPostings = () => {
        const fields = [
            this.budgetaryPaymentModel.Date,
            this.model.Sum,
            this.model.AccountCode,
            this.budgetaryPaymentModel.Status,
            this.model.TradingObjectId,
            this.TaxationSystem.StartYear,
            this.model.Kbk.Id,
            this.model.Kbk.Number,
            this.model.Period.Year,
            this.model.Period.Type,
            this.model.Period.Month,
            this.model.Period.Quarter,
            this.model.Period.HalfYear
        ];

        // /** при создании операции загрузка БУ/НУ завязана
        //  *  на изменение кбк и так называемых kbk auto fields.
        //  *  а т.к. при редактировании операции мы их не изменяем автоматически,
        //  *  а бу/ну перезагружать нужно */
        // if (!this.isNew) {
        //     fields.push(this.model.Kbk.Id, this.model.Kbk.Number);
        // }

        return JSON.stringify(fields);
    };

    /* override */
    getTaxPostingsExplainingMsg = () => {
        const options = {
            taxationSystem: this.TaxationSystem,
            isTradingFee: this.isTradingFee,
            isIp: this.isIp,
            accountCode: this.model.AccountCode,
            accountType: this.accountType,
            kbk: this.model.Kbk,
            isIpOsno: this.isIpOsno
        };

        return notTaxableReasonGetter.get(options) || this.getRequiredFieldsForTaxPostingMsg();
    };

    getTransferType = () => {
        return [
            PostingTransferType.Direct,
            PostingTransferType.Indirect,
            PostingTransferType.NonOperating
        ];
    };

    getTransferKind = ({ Type }) => {
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
    };

    getNormalizedCostType = () => {
        return Object.values(TaxPostingNormalizedCostType);
    };

    mapModelForPostingsGeneration() {
        return {
            ParentBaseId: this.budgetaryPaymentModel.BaseDocumentId,
            DocumentBaseId: this.model.DocumentBaseId,
            Date: dateHelper(this.budgetaryPaymentModel.Date).format(`YYYY-MM-DD`),
            Period: this.model.Period,
            KbkId: this.model.Kbk.Id,
            Sum: this.model.Sum,
            Number: this.budgetaryPaymentModel.Number,
            TradingObjectId: this.model.TradingObjectId,
            PatentId: this.model.PatentId,
            OperationType: paymentOrderOperationResources.UnifiedBudgetaryPayment.value
        };
    }

    getValidatedIpOsnoPosting = posting => {
        const { Sum } = this.model;
        const defaultValidatedPosting = taxPostingsValidator.getValidatedUnifiedBudgetaryPaymentIpOsnoPosting(posting);

        defaultValidatedPosting.AllSumValidationMessage = taxPostingsValidator.getUnifiedBudgetarySubPaymentValidation([posting], { Sum });

        return defaultValidatedPosting;
    };

    /** используется в Usn.js. По умолчанию проверка идет на заполнение суммы */
    getValidatedUsnPosting = posting => {
        const { Sum } = this.model;
        const defaultValidatedPosting = taxPostingsValidator.getValidatedUnifiedBudgetaryPaymentUsnPosting(posting);

        defaultValidatedPosting.AllSumValidationMessage = taxPostingsValidator.getUnifiedBudgetarySubPaymentValidation([posting], { Sum });

        return defaultValidatedPosting;
    };
}
