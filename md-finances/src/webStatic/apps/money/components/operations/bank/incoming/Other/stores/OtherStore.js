import {
    observable, reaction, toJS, makeObservable
} from 'mobx';
import { getUniqueId } from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import { calculateNdsBySumAndType } from '../../../../../../../../helpers/newMoney/ndsCalculationHelper';
import { getKontragentSettlements } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import accountingPostingsValidator from '../../../../validation/accountingPostingsValidator';
import validationRules from '../validationRules';
import { autocomplete } from '../../../../../../../../services/contractService';
import { mapForAutocomplete } from '../../../../../../../../helpers/newMoney/contractHelper';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import { mapLinkedBillToBackend } from '../../../../../../../../mappers/linkedBills/linkedBillsMapper';
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
    customIncomingAccountingPostingsForNewBackend,
    handleCreditSubcontoIds,
    mapPostingsToModel
} from '../../../../../../../../mappers/accountingPostingsMapper';
import { validCreditSyntheticAccount } from '../accountingPostingResource';
import KontragentType from '../../../../../../../../enums/KontragentType';
import SyntheticAccountCodesEnum from '../../../../../../../../enums/SyntheticAccountCodesEnum';
import { getValidTaxationSystemType } from '../../../../../../../../mappers/taxationSystemMapper';
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
        Description: ``,
        BillsSum: ``,
        TaxationSystemType: ``,
        Patent: ``
    };

    /* ui state */
    @observable isContractorRequisitesShown = false;
    /* ui state END */

    @observable model = { ...defaultModel };

    @observable kontragentSettlements = [];

    @observable taxPostingsLoading = false;

    @observable accountingPostingsLoading = false;

    @observable kontragentLoading = false;

    availableTaxPostingDirection = AvailableTaxPostingDirection.Incoming;

    isTaxPostingsLoaded = false;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        this.model.TaxPostingsMode = ProvidePostingType.ByHand;
        this.setActivePatents(options.activePatents);

        this.model.TaxationSystemType = getValidTaxationSystemType({
            taxationSystem: options.taxationSystem,
            taxationSystemType: options.operation.TaxationSystemType,
            isOoo: options.requisites.IsOoo
        });

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

        this.handleSelfKontragentForm();

        this.setBills(this.model.Bills || []);
        this.initializeKontragentSettlements();

        this.initAccountingPostings();
        this.initTaxPostings();

        if (this.model.IsTypeChanged) {
            this.calculateNdsBySumAndType([this.model.Sum, this.model.NdsType, this.model.IncludeNds]);
        }

        this.initReactions();
    }

    initReactions = () => {
        reaction(() => [this.validationState.KontragentSettlementAccount, this.validationState.KontragentInn, this.validationState.KontragentKpp], this.checkKontragentRequisitesVisibility);

        reaction(() => [this.model.Sum, this.model.NdsType, this.model.IncludeNds], this.calculateNdsBySumAndType);

        reaction(() => this.model.Kontragent, this.loadKontragentRequisites);

        reaction(() => [this.currentNdsRateFromAccPolicy], this.checkNdsFromAccPol);

        reaction(() => this.model.Date, async Date => {
            if (!this.validationState.Date) {
                this.setTaxationSystem(await taxationSystemService.getTaxSystem(Date));
                this.updateActivePatents();
            }
        });

        reaction(this.getFieldsForTaxPostings, this.localLoadTaxPostings);
        reaction(this.getFieldsForAccountingPostings, this.loadAccountingPostings);
    };

    localLoadTaxPostings = async () => {
        const explainingMessage = this.getRequiredFieldsForTaxPostingMsg();

        this.model.TaxPostings.ExplainingMessage = explainingMessage;

        if (!this.isTaxPostingsLoaded && this.isNew && !explainingMessage) {
            await this.loadTaxPostings();

            this.isTaxPostingsLoaded = true;
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

        return accountingPostingsValidator.isValid(this.model.AccountingPostings.Postings, { Sum: this.model.Sum });
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
        const response = await autocomplete({ query, kontragentId: this.model.Kontragent.KontragentId });

        return mapForAutocomplete(response, query);
    };

    getContractorType = () => {
        if (this.isSelfKontragent) {
            return KontragentType.Ip; // без разницы, для ИП или ООО, бэк ждет 3
        }

        if (this.isWorkerKontragent) {
            return KontragentType.Worker;
        }

        return KontragentType.Kontragent;
    };

    modelForSave = () => {
        const { model, bills, TaxationSystem } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderIncomingOther;
        const customPostings = model.TaxPostingsMode === ProvidePostingType.ByHand ? model.TaxPostings.Postings : [];
        const TaxPostings = this.getTaxPostingForSave(customPostings, model.TaxPostingsMode, TaxationSystem);

        let postingsAndTaxMode = model.TaxPostingsMode;

        if (!model.ProvideInAccounting) {
            postingsAndTaxMode = ProvidePostingType.ByHand;
        }

        const AccountingPosting = !this.canViewAccountingPostings || !model.ProvideInAccounting ?
            null :
            customIncomingAccountingPostingsForNewBackend(model.AccountingPostings.Postings);
        const { SalaryWorkerId } = this.model.Kontragent;
        const contractorId = SalaryWorkerId || model.Kontragent.KontragentId;

        return {
            Id: model.Id,
            BaseDocumentId: model.BaseDocumentId,
            DocumentBaseId: model.DocumentBaseId,
            OperationType: operationType.value,
            Direction: operationType.Direction,
            Number: model.Number,
            Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
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
            ContractorType: this.getContractorType(),
            Sum: model.Sum,
            Description: model.Description,
            ContractBaseId: model.Contract.ContractBaseId,
            Contract: {
                DocumentBaseId: model.Contract.ContractBaseId
            },
            Nds: {
                IncludeNds: model.IncludeNds,
                Type: model.NdsType,
                Sum: model.NdsSum || 0
            },
            Bills: toJS(bills).filter(doc => doc.DocumentBaseId > 0 && toFloat(doc.Sum) > 0).map(mapLinkedBillToBackend),
            ProvideInAccounting: model.ProvideInAccounting,
            TaxationSystemType: model.TaxationSystemType,
            PatentId: this.patentIdForSave,
            PostingsAndTaxMode: postingsAndTaxMode,
            TaxPostings,
            AccountingPosting,
            NoAutoDeleteOperation: model.NoAutoDeleteOperation,
            IsTargetIncome: model.IsTargetIncome
        };
    };

    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.Sum,
            this.TaxationSystem.StartYear,
            this.model.TaxationSystemType
        ]);
    };

    getRequiredFieldsForTaxPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForTaxPostings.date;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForTaxPostings.sum;
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
            this.model.TaxationSystemType
        ]);
    };

    getRequiredFieldsForAccountingPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForAccountingPostings.date;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForAccountingPostings.sum;
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

        return getContractorAutocomplete({
            kontragentType: null,
            onlyFounders: false,
            count: 20,
            SettlementAccountId,
            Date,
            query
        });
    };

    getDebits = () => {
        // не требуется реализации, т.к. всегда только один дебет
    };

    getCredits = async ({ query = ``, count = 5 }) => {
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
            this.model.AccountingPostings = { Postings: [], ExplainingMessage: msg || `` };
            this.accountingPostingsLoading = false;

            return;
        }

        if (!this.model.AccountingPostings.Postings || !this.model.AccountingPostings.Postings.length) {
            this.accountingPostingsLoading = true;
            const debits = await getSyntheticAccountAutocomplete();
            const debit = debits.find(item => item.Code === SyntheticAccountCodesEnum._51_01);
            debit.ReadOnly = true;

            const debitSubconto = await subcontoLevelForAccount({
                settlementAccountId: this.model.SettlementAccountId,
                syntheticAccountTypeId: debit.TypeId
            });

            const posting = {
                key: getUniqueId(),
                Debit: debit,
                SubcontoDebit: debitSubconto,
                Sum: this.model.Sum
            };

            this.model.AccountingPostings = { Postings: [posting], ExplainingMessage: msg };
            this.accountingPostingsLoading = false;
        }
    };

    initAccountingPostings = async () => {
        const isAccountingPostingsExist = Object.values(this.model.AccountingPostings)?.reduce((acc, v) => (toJS(v)?.length ? [...acc, v] : [...acc]), [])?.length;

        if (this.model.Id > 0 && Object.keys(this.model.AccountingPostings).length && isAccountingPostingsExist) {
            this.model.AccountingPostings.ExplainingMessage = this.getAccountingPostingsExplainingMsg() || this.model.AccountingPostings.ExplainingMessage;
            const postings = mapPostingsToModel({
                postings: handleCreditSubcontoIds(this.model.AccountingPostings.Postings),
                readOnly: !this.canEdit
            });

            // eslint-disable-next-line no-param-reassign,no-return-assign
            postings.forEach(item => item.Debit.ReadOnly = true);
            const updatedPostingsWithSubcontos = await updateAccountingPostingsModelForOtherOperations(postings);
            this.model.AccountingPostings.Postings = updatedPostingsWithSubcontos;
        } else {
            this.loadAccountingPostings();
        }
    };

    setNoAutoDeleteOperation = ({ checked }) => {
        this.model.NoAutoDeleteOperation = checked;
    };
}

export default OtherStore;
