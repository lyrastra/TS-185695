import {
    makeObservable, observable, reaction, toJS
} from 'mobx';
import { getUniqueId } from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import UsnTypeEnum from '@moedelo/frontend-enums/mdEnums/UsnType';
import KontragentsFormEnum from '@moedelo/frontend-enums/mdEnums/KontragentsForm';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import { calculateNdsBySumAndType } from '../../../../../../../../helpers/newMoney/ndsCalculationHelper';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import accountingPostingsValidator from '../../../../validation/accountingPostingsValidator';
import validationRules from '../validationRules';
import { autocomplete } from '../../../../../../../../services/contractService';
import { mapForAutocomplete } from '../../../../../../../../helpers/newMoney/contractHelper';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import defaultModel from './Model';
import Actions from './Actions';
import PostingDirection from '../../../../../../../../enums/newMoney/TaxPostingDirectionEnum';
import PostingTransferType from '../../../../../../../../enums/newMoney/TaxPostingTransferTypeEnum';
import TaxPostingTransferKind from '../../../../../../../../enums/newMoney/TaxPostingTransferKindEnum';
import TaxPostingNormalizedCostType from '../../../../../../../../enums/newMoney/TaxPostingNormalizedCostTypeEnum';
import { getContractorAutocomplete, getSettlementAccounts } from '../../../../../../../../services/newMoney/contractorService';
import { getSyntheticAccountAutocomplete } from '../../../../../../../../services/newMoney/syntheticAccountService';
import { subcontoLevelForAccount } from '../../../../../../../../services/newMoney/subcontoService';
import {
    customOutgoingAccountingPostingsForNewBackend,
    handleDebitSubcontoIds,
    mapPostingsToModel
} from '../../../../../../../../mappers/accountingPostingsMapper';
import { validCreditSyntheticAccount } from '../accountingPostingResource';
import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';
import SyntheticAccountCodesEnum from '../../../../../../../../enums/SyntheticAccountCodesEnum';
import { getKontragentSettlements } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import KontragentType from '../../../../../../../../enums/KontragentType';
import TaxStatusEnum from '../../../../../../../../enums/TaxStatusEnum';
import { generate, generateForOsno } from '../../../../../../../../services/taxPostingService';
import DeferredLoader from '../../../../../../../../helpers/newMoney/DeferredLoader';
import postingGenerationError from '../../../../../../../../resources/newMoney/postingGenerationError';
import { updateAccountingPostingsModelForOtherOperations } from '../../../../../../../../helpers/newMoney/postingsHelpers';

class OtherStore extends Actions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        NdsSum: ``,
        Kontragent: ``,
        MediationCommission: ``,
        KontragentSettlementAccount: ``,
        KontragentInn: ``,
        KontragentKpp: ``,
        Description: ``
    };

    @observable model = { ...defaultModel };

    availableTaxPostingDirection = AvailableTaxPostingDirection.Outgoing;

    isTaxPostingsLoaded = false;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        this.defaultKontragentForm = this.Requisites.IsOoo ? KontragentsFormEnum.UL : KontragentsFormEnum.IP;

        /** если в качестве контрагента выбрана фирма, проставляем ей KontragentForm,
         *  т.к. в модели значение null, срабатывает валидация ИНН */
        if ((!this.isNew || this.isCopy()) && !this.model.Kontragent.KontragentId && !this.model.Kontragent.SalaryWorkerId) {
            Object.assign(this.model.Kontragent, { KontragentForm: this.defaultKontragentForm });
        }

        if (typeof this.model.IncludeNds !== `boolean`) {
            const { isAfter2025, IsUsn, IsOsno } = this.isAfter2025WithTaxation;

            if (isAfter2025 && IsUsn) {
                this.setIncludeNds({ checked: true });
            } else {
                this.setIncludeNds({ checked: IsOsno });
            }
        }

        if (!this.Requisites.IsOoo) {
            this.model.ProvideInAccounting = false;
        }

        this.initializeKontragentSettlements();
        this.initAvailableTaxPostingDirection();

        if (this.model.IsTypeChanged) {
            this.calculateNdsBySumAndType([this.model.Sum, this.model.NdsType, this.model.IncludeNds]);
        }

        this.initAccountingPostings();
        this.initTaxPostings();
        this.initReactions();

        this.model.TaxPostingsMode = ProvidePostingType.ByHand;
    }

    initReactions = () => {
        reaction(() => [this.validationState.KontragentSettlementAccount, this.validationState.KontragentInn, this.validationState.KontragentKpp], this.checkKontragentRequisitesVisibility);
        reaction(() => [this.model.Sum, this.model.NdsType, this.model.IncludeNds], this.calculateNdsBySumAndType);
        reaction(() => this.model.Kontragent, this.loadKontragentRequisites);
        reaction(() => this.model.Date, async Date => {
            if (!this.validationState.Date) {
                this.setTaxationSystem(await taxationSystemService.getTaxSystem(Date));
            }
        });
        reaction(this.getFieldsForTaxPostings, this.localLoadTaxPostings);
        reaction(this.getFieldsForAccountingPostings, this.loadAccountingPostings);
    };

    localLoadTaxPostings = async () => {
        this.model.TaxPostings.ExplainingMessage = this.getRequiredFieldsForTaxPostingMsg();

        !this.model.TaxPostings.ExplainingMessage && await this.loadTaxPostings();
    };

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

                    return this.taxationForPostings.IsOsno && this.Requisites.IsOoo
                        ? generateForOsno(model)
                        : generate(model);
                },
                getRequestData: this.getFieldsForTaxPostings
            });

            if (status !== DeferredLoader.Status.aborted) {
                const metaData = {
                    ExplainingMessage: result.ExplainingMessage,
                    HasPostings: result.HasPostings || result.Postings.length > 0,
                    TaxStatus: result.TaxStatus
                };

                if (result.TaxStatus === TaxStatusEnum.No) {
                    metaData.Postings = [];
                }

                /** нужно сохранять текущие НУ записи. см. метод setTaxPostingList */
                Object.assign(this.model.TaxPostings, metaData);

                [TaxStatusEnum.Yes, TaxStatusEnum.ByHand, TaxStatusEnum.No].includes(result.TaxStatus) &&
                !this.model.TaxPostings?.Postings?.length &&
                this.setTaxPostingList(result);

                this.taxPostingsLoading = false;
            }
        } catch (e) {
            this.model.TaxPostings = { Error: postingGenerationError };
            this.taxPostingsLoading = false;

            throw new Error(e);
        }
    };

    initAvailableTaxPostingDirection = () => {
        const {
            IsUsn, UsnType
        } = this.TaxationSystem;

        if (IsUsn && UsnType === UsnTypeEnum.ProfitAndOutgo) {
            this.availableTaxPostingDirection = AvailableTaxPostingDirection.Both;
        }
    };

    calculateNdsBySumAndType = ([Sum, NdsType, IncludeNds, NdsSum]) => {
        if (NdsSum || NdsSum === 0) {
            return;
        }

        if (IncludeNds) {
            const ndsSum = NdsType === null ? `` : calculateNdsBySumAndType(Sum, NdsType);

            this.setNdsSum({ value: ndsSum });
        }
    };

    loadKontragentRequisites = async () => {
        const kontragent = this.model.Kontragent;

        this.kontragentSettlements = kontragent.KontragentId
            ? await getSettlementAccounts({ kontragentId: kontragent.KontragentId })
            : [{ Number: kontragent.KontragentSettlementAccount, BankName: kontragent.KontragentBankName }];

        if (this.kontragentSettlements.length) {
            const {
                Number, BankName, Bik, KontragentBankCorrespondentAccount
            } = this.kontragentSettlements[0];

            this.setContractorSettlementAccount({ value: Number });
            this.setContractorBankName({ value: BankName });
            this.setContractorBankCorrespondentAccount({ value: KontragentBankCorrespondentAccount });
            this.setContractorBankBIK({ value: Bik });
        }
    };

    initializeKontragentSettlements = async () => {
        this.kontragentSettlements = await getKontragentSettlements(this.model);
    };

    isValid = () => {
        const options = {
            Sum: this.model.Sum,
            isOsno: this.isOsno,
            needAllSumValidation: this.needAllSumValidation
        };

        const taxPostingsIsValid = taxPostingsValidator.isValid(this.model.TaxPostings.Postings, options);
        const accountingPostingsIsValid = this.isAccountingValid();
        const modelIsValid = !Object.keys(this.validationState).find(key => {
            return this.validationState[key] !== ``;
        });

        return taxPostingsIsValid && accountingPostingsIsValid && modelIsValid;
    };

    isAccountingValid = () => {
        if (!this.canViewAccountingPostings || !this.model.ProvideInAccounting) {
            return true;
        }

        return accountingPostingsValidator.isValid(this.model.AccountingPostings.Postings, { Sum: this.model.Sum, needAllSumValidation: this.needAllSumValidation });
    };

    validateModel() {
        Object.keys(validationRules).forEach(fieldName => {
            this.validateField(fieldName, this.model[fieldName]);
        });

        this.checkKontragentRequisitesVisibility();
        this.validateTaxPostingsList();
        this.validateAccountingPostingsList();
    }

    getContractAutocomplete = async ({ query = `` }) => {
        let response = [];

        if (!this.contractorIsWorker && !this.contractIsMainFirm) {
            response = await autocomplete({ query, kontragentId: this.model.Kontragent.KontragentId });
        }

        return mapForAutocomplete(response, query);
    };

    modelForSave = () => {
        const { model, TaxationSystem } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderOutgoingOther;
        const customPostings = model.TaxPostingsMode === ProvidePostingType.ByHand ? model.TaxPostings.Postings : [];
        const TaxPostings = this.getTaxPostingForSave(customPostings, model.TaxPostingsMode, TaxationSystem);
        const AccountingPosting = !this.canViewAccountingPostings || !model.ProvideInAccounting
            ? null
            : customOutgoingAccountingPostingsForNewBackend(model.AccountingPostings.Postings);
        const contractorId = model.Kontragent.SalaryWorkerId || model.Kontragent.KontragentId;
        let contractorType;

        if (model.Kontragent.SalaryWorkerId > 0) {
            contractorType = KontragentType.Worker;
        } else if (model.Kontragent.KontragentId > 0) {
            contractorType = KontragentType.Kontragent;
        } else {
            contractorType = KontragentType.Ip;
        }

        return {
            DocumentBaseId: model.DocumentBaseId || 0,
            OperationType: operationType.value,
            Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
            Number: model.Number,
            SettlementAccountId: model.SettlementAccountId,
            Contractor: {
                Id: contractorId,
                Name: model.Kontragent.KontragentName,
                Inn: model.Kontragent.KontragentINN,
                Kpp: model.Kontragent.KontragentKPP,
                SettlementAccount: model.Kontragent.KontragentSettlementAccount,
                BankName: model.Kontragent.KontragentBankName,
                BankBik: model.Kontragent.KontragentBankBIK,
                BankCorrespondentAccount: model.Kontragent.KontragentBankCorrespondentAccount
            },
            ContractorType: contractorType,
            Contract: {
                DocumentBaseId: model.Contract.ContractBaseId
            },
            Nds: {
                IncludeNds: model.IncludeNds,
                Type: model.NdsType,
                Sum: model.NdsSum || 0
            },
            Sum: model.Sum,
            Description: model.Description,
            ProvideInAccounting: model.ProvideInAccounting,
            IsPaid: model.Status === DocumentStatusEnum.Payed,
            TaxPostings,
            AccountingPosting,
            NoAutoDeleteOperation: model.NoAutoDeleteOperation
        };
    };

    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.Sum,
            this.model.Status,
            this.TaxationSystem.StartYear
        ]);
    };

    getRequiredFieldsForTaxPostingMsg = () => {
        const {
            Date, Sum, Status
        } = this.model;

        if (!Date || this.validationState.Date) {
            return requiredFieldForTaxPostings.date;
        }

        if (!toFloat(Sum)) {
            return requiredFieldForTaxPostings.sum;
        }

        if (Status === DocumentStatusEnum.NotPayed) {
            return requiredFieldForTaxPostings.notPaid;
        }

        return null;
    };

    getTaxPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForTaxPostingMsg();
    };

    getFieldsForAccountingPostings = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.Sum,
            this.model.ProvideInAccounting,
            this.model.Status
        ]);
    };

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

        return null;
    };

    getAccountingPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForAccountingPostingMsg();
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

    getContractorAutocomplete = async ({ query = `` }) => {
        const { SettlementAccountId, Date } = this.model;
        const list = await getContractorAutocomplete({
            kontragentType: null,
            onlyFounders: false,
            count: 5,
            SettlementAccountId,
            Date,
            query
        });

        return list.map(kontragent => {
            if (!kontragent.KontragentForm && !kontragent.KontragentId && !kontragent.SalaryWorkerId) {
                return Object.assign(kontragent, { KontragentForm: this.defaultKontragentForm });
            }

            return kontragent;
        });
    };

    getCredits = () => {
        // не требуется реализации, т.к. всегда только один дебет
    };

    getDebits = async ({ query = ``, count = 5 }) => {
        const accounts = await getSyntheticAccountAutocomplete();

        return Promise.resolve({
            value: query,
            data: accounts
                .filter(item => validCreditSyntheticAccount.findIndex(acc => acc === item.Code) !== -1)
                .filter(item => `${item.Number} ${item.Name}`.indexOf(query) !== -1)
                .slice(0, count)
                .map(item => ({
                    text: `${item.Number} ${item.Name}`,
                    value: item
                }))
        });
    };

    loadAccountingPostings = async () => {
        const msg = this.getAccountingPostingsExplainingMsg();

        if (msg) {
            this.model.AccountingPostings = { Postings: [], ExplainingMessage: msg };
            this.accountingPostingsLoading = false;

            return;
        }

        if (!this.model.AccountingPostings.Postings || !this.model.AccountingPostings.Postings.length) {
            this.accountingPostingsLoading = true;
            const credits = await getSyntheticAccountAutocomplete();

            const credit = credits.find(item => item.Code === SyntheticAccountCodesEnum._51_01);
            credit.ReadOnly = true;

            const creditSubconto = await subcontoLevelForAccount({
                settlementAccountId: this.model.SettlementAccountId,
                syntheticAccountTypeId: credit.TypeId
            });

            const posting = {
                key: getUniqueId(),
                Credit: credit,
                SubcontoCredit: creditSubconto,
                Sum: this.model.Sum
            };

            this.model.AccountingPostings = { Postings: [posting], ExplainingMessage: msg };
            this.accountingPostingsLoading = false;
        }
    };

    initAccountingPostings = async () => {
        if (this.isCopy()) {
            this.model.AccountingPostings = {};
        }

        const isAccountingPostingsExist = Object.values(this.model.AccountingPostings)?.reduce((acc, v) => (toJS(v)?.length ? [...acc, v] : [...acc]), [])?.length;

        if (!this.isNew && Object.keys(this.model.AccountingPostings).length && isAccountingPostingsExist) {
            this.model.AccountingPostings.ExplainingMessage = this.getAccountingPostingsExplainingMsg() || this.model.AccountingPostings.ExplainingMessage;

            const postings = mapPostingsToModel({
                postings: handleDebitSubcontoIds(this.model.AccountingPostings.Postings),
                readOnly: !this.canEdit
            });

            // eslint-disable-next-line no-param-reassign,no-return-assign
            postings.forEach(item => item.Credit.ReadOnly = true);

            const updatedPostingsWithSubcontos = await updateAccountingPostingsModelForOtherOperations(postings);
            this.model.AccountingPostings.Postings = updatedPostingsWithSubcontos;
        } else {
            this.loadAccountingPostings();
        }
    };

    /* override */
    getValidatedUsnPosting = posting => {
        return taxPostingsValidator.getValidatedNegativeUsnPosting(posting);
    };

    setNoAutoDeleteOperation = ({ checked }) => {
        this.model.NoAutoDeleteOperation = checked;
    };
}

function mapModelForPostingsGeneration(data) {
    return {
        ...data,
        PostingsAndTaxMode: ProvidePostingType.Auto,
        TaxPostings: []
    };
}

export default OtherStore;
