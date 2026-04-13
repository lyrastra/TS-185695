import {
    observable, reaction, toJS, makeObservable
} from 'mobx';
import { getUniqueId } from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import { getKontragentSettlements } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import validationRules from '../validationRules';
import { autocomplete } from '../../../../../../../../services/contractService';
import { mapForAutocomplete } from '../../../../../../../../helpers/newMoney/contractHelper';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import { usnTaxPostingsForServerNewBackend } from '../../../../../../../../mappers/taxPostingsMapper';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import defaultModel from './IncomingCurrencyOtherModel';
import IncomingCurrencyOtherActions from './IncomingCurrencyOtherActions';
import { getContractorAutocomplete, getSettlementAccounts } from '../../../../../../../../services/newMoney/contractorService';
import KontragentType from '../../../../../../../../enums/KontragentType';
import accountingPostingsValidator from '../../../../validation/accountingPostingsValidator';
import {
    handleCreditSubcontoIds,
    mapPostingsToModel,
    customIncomingAccountingPostingsForNewBackend
} from '../../../../../../../../mappers/accountingPostingsMapper';
import requiredFieldForAccountingPostings
    from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import { getSyntheticAccountAutocomplete } from '../../../../../../../../services/newMoney/syntheticAccountService';
import SyntheticAccountCodesEnum from '../../../../../../../../enums/SyntheticAccountCodesEnum';
import { validCreditSyntheticAccount } from '../../Other/accountingPostingResource';
import {
    subcontoLevelForAccount
} from '../../../../../../../../services/newMoney/subcontoService';
import { updateAccountingPostingsModelForOtherOperations } from '../../../../../../../../helpers/newMoney/postingsHelpers';
import { calculateNdsBySumAndType } from '../../../../../../../../helpers/newMoney/ndsCalculationHelper';

class IncomingCurrencyOtherStore extends IncomingCurrencyOtherActions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Kontragent: ``,
        MediationCommission: ``,
        KontragentSettlementAccount: ``,
        KontragentInn: ``,
        KontragentKpp: ``,
        Description: ``,
        NdsSum: ``
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

    needAllSumValidation = true;

    needAllTotalSumValidation = true;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        this.updateCurrencyRate();

        if (typeof this.model.IncludeNds !== `boolean`) {
            const { isAfter2025, IsUsn } = this.isAfter2025WithTaxation;

            if (isAfter2025 && IsUsn) {
                this.setIncludeNds({ checked: true });
            }
        }

        this.initializeKontragentSettlements();

        if (this.model.IsTypeChanged) {
            this.calculateNdsBySumAndType([this.model.TotalSum, this.model.NdsType, this.model.IncludeNds]);
        }

        this.initAccountingPostings();
        this.initTaxPostings();
        this.initReactions();
    }

    initReactions = () => {
        reaction(() => [this.validationState.KontragentSettlementAccount, this.validationState.KontragentInn, this.validationState.KontragentKpp], this.checkKontragentRequisitesVisibility);

        reaction(() => this.model.Kontragent, this.loadKontragentRequisites);

        reaction(() => this.model.SettlementAccountId, this.updateCurrencyRate);

        reaction(() => [this.model.TotalSum, this.model.NdsType, this.model.IncludeNds], this.calculateNdsBySumAndType);

        reaction(() => [this.currentNdsRateFromAccPolicy], this.checkNdsFromAccPol);

        reaction(() => this.model.Date, async Date => {
            if (!this.validationState.Date) {
                this.setTaxationSystem(await taxationSystemService.getTaxSystem(Date));
                this.updateCurrencyRate();
            }
        });

        reaction(() => this.model.SettlementAccountId, async () => {
            this.model.AccountingPostings.Postings = [];
            this.loadAccountingPostings();
        });

        reaction(this.getFieldsForTaxPostings, this.loadTaxPostings);
        reaction(this.getFieldsForAccountingPostings, this.loadAccountingPostings);
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
            Sum: this.model.TotalSum,
            isOsno: this.isOsno,
            needAllSumValidation: this.needAllSumValidation,
            needAllTotalSumValidation: this.needAllTotalSumValidation
        };

        const taxPostingsIsValid = taxPostingsValidator.isValid(this.model.TaxPostings.Postings, options);
        const accountingPostingsIsValid = this.isAccountingValid();
        const modelIsValid = !Object.keys(this.validationState).find(key => {
            return this.validationState[key] !== ``;
        });

        return taxPostingsIsValid && accountingPostingsIsValid && modelIsValid;
    };

    isAccountingValid = () => {
        if (!this.model.ProvideInAccounting || !this.canViewAccountingPostings) {
            return true;
        }

        return accountingPostingsValidator.isValid(this.model.AccountingPostings.Postings, { Sum: this.model.TotalSum, needAllSumValidation: this.needAllSumValidation });
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

    calculateNdsBySumAndType = ([TotalSum, NdsType, IncludeNds, NdsSum]) => {
        if (NdsSum || NdsSum === 0) {
            return;
        }

        if (IncludeNds) {
            const ndsSum = NdsType === null ? `` : calculateNdsBySumAndType(TotalSum, NdsType);

            this.setNdsSum({ value: ndsSum });
        } else {
            this.setNdsSum({ value: null });
            this.setNdsType({ value: null });
        }
    };

    modelForSave = () => {
        const { model } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderIncomingCurrencyOther;

        let contractorType;

        if (model.Kontragent.SalaryWorkerId > 0) {
            contractorType = KontragentType.Worker;
        } else if (model.Kontragent.KontragentId > 0) {
            contractorType = KontragentType.Kontragent;
        } else {
            contractorType = KontragentType.Ip;
        }

        const AccountingPosting = !this.canViewAccountingPostings || !model.ProvideInAccounting
            ? null
            : customIncomingAccountingPostingsForNewBackend(model.AccountingPostings.Postings);

        const baseModel = {
            DocumentBaseId: model.DocumentBaseId,
            OperationType: operationType.value,
            Direction: operationType.Direction,
            Number: model.Number,
            Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
            SettlementAccountId: model.SettlementAccountId,
            Contractor: {
                Id: model.Kontragent.SalaryWorkerId > 0 ? model.Kontragent.SalaryWorkerId : model.Kontragent.KontragentId,
                Name: model.Kontragent.KontragentName,
                Inn: model.Kontragent.KontragentINN,
                Kpp: model.Kontragent.KontragentKPP,
                SettlementAccount: model.Kontragent.KontragentSettlementAccount,
                BankName: model.Kontragent.KontragentBankName,
                BankBik: model.Kontragent.KontragentBankBIK,
                BankCorrespondentAccount: model.Kontragent.KontragentBankCorrespondentAccount
            },
            ContractorType: contractorType,
            Sum: model.Sum,
            TotalSum: model.TotalSum,
            Description: model.Description,
            Contract: {
                DocumentBaseId: model.Contract.ContractBaseId
            },
            TaxPostings: {
                Postings: usnTaxPostingsForServerNewBackend(model.TaxPostings.Postings, { Date: model.Date })
            },
            ProvideInAccounting: model.ProvideInAccounting,
            AccountingPosting
        };

        const usnAfter2025Model = {
            Nds: {
                IncludeNds: model.IncludeNds,
                Type: model.NdsType,
                Sum: model.NdsSum || 0
            }
        };

        const { isAfter2025, IsUsn } = this.isAfter2025WithTaxation;

        return isAfter2025 && IsUsn ? { ...baseModel, ...usnAfter2025Model } : baseModel;
    };

    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.Sum,
            this.TaxationSystem.StartYear
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
            this.model.Sum,
            this.model.TotalSum,
            // this.model.Date,
            this.model.ProvideInAccounting,
            this.model.Kontragent.KontragentId,
            this.model.Contract.ContractBaseId,
            this.model.SettlementAccountId
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

    getContractorAutocomplete = async ({ query = `` }) => {
        const { SettlementAccountId, Date } = this.model;

        return getContractorAutocomplete({
            kontragentType: null,
            onlyFounders: false,
            count: 5,
            SettlementAccountId,
            Date,
            query
        });
    };

    loadTaxPostings = async () => {
        this.model.TaxPostingsMode = ProvidePostingType.ByHand;
        const msg = this.getTaxPostingsExplainingMsg();

        if (msg) {
            this.model.TaxPostings = { Postings: [], ExplainingMessage: msg };

            return;
        }

        if (this.model.TaxPostings
            && (!this.model.TaxPostings.Postings || !this.model.TaxPostings.Postings.length)) {
            this.model.TaxPostings.ExplainingMessage = msg;
            this.setTaxPostingList({});
        }
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

        if (!this.model.AccountingPostings?.Postings?.length) {
            this.accountingPostingsLoading = true;
            const debits = await getSyntheticAccountAutocomplete();
            const debit = debits.find(item => item.Code === (this.isTransit ? SyntheticAccountCodesEnum._52_01_01 : SyntheticAccountCodesEnum._52_01_02));
            debit.ReadOnly = true;

            const debitSubconto = await subcontoLevelForAccount({
                settlementAccountId: this.model.SettlementAccountId,
                syntheticAccountTypeId: debit.TypeId
            });

            debitSubconto.forEach(d => {
                // eslint-disable-next-line no-param-reassign
                d.Subconto.ReadOnly = true;
            });

            const posting = {
                key: getUniqueId(),
                Debit: debit,
                SubcontoDebit: debitSubconto,
                Sum: this.model.TotalSum
            };

            this.model.AccountingPostings = { Postings: [posting], ExplainingMessage: msg };
            this.accountingPostingsLoading = false;
        } else {
            const accountingPostingsSum = this.model.AccountingPostings?.Postings?.reduce((agr, posting) => {
                const add = (posting.Sum !== null && !posting.ReadOnly) ? posting.Sum : 0;

                return agr + add;
            }, 0);

            if (accountingPostingsSum !== this.sumOperation) {
                const posting = { ...this.model.AccountingPostings?.Postings[0], Sum: this.sumOperation };
                this.model.AccountingPostings = { Postings: [posting], ExplainingMessage: msg };
            }
        }
    };

    initAccountingPostings = async () => {
        const isAccountingPostingsExist = Object.values(this.model.AccountingPostings)?.reduce((acc, v) => (toJS(v)?.length ? [...acc, v] : [...acc]), [])?.length;

        if (Object.keys(this.model.AccountingPostings).length && isAccountingPostingsExist) {
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
}

export default IncomingCurrencyOtherStore;
