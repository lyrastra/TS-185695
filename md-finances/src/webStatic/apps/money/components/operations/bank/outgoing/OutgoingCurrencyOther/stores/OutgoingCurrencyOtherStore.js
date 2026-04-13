import {
    makeObservable, observable, reaction, toJS
} from 'mobx';
import { getUniqueId } from '@moedelo/frontend-core-v2/helpers/uniqueIdHelper';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import SubcontoTypeEnum from '../../../../../../../../enums/newMoney/SubcontoTypeEnum';
import SyntheticAccountCodesEnum from '../../../../../../../../enums/SyntheticAccountCodesEnum';
import { getKontragentSettlements } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import {
    customOutgoingAccountingPostingsForNewBackend,
    mapAutocompleteSubconto,
    mapPostingsToModel
} from '../../../../../../../../mappers/accountingPostingsMapper';
import requiredFieldForAccountingPostings
    from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import {
    getContractSubcontoAutocomplete,
    getSubcontosAutocomplete,
    subcontoLevelForAccount
} from '../../../../../../../../services/newMoney/subcontoService';
import { getSyntheticAccountAutocomplete } from '../../../../../../../../services/newMoney/syntheticAccountService';
import accountingPostingsValidator from '../../../../validation/accountingPostingsValidator';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import { validCreditSyntheticAccount } from '../accountingPostingResource';
import validationRules from '../validationRules';
import { autocomplete } from '../../../../../../../../services/contractService';
import { mapForAutocomplete } from '../../../../../../../../helpers/newMoney/contractHelper';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import { usnTaxPostingsForServerNewBackend } from '../../../../../../../../mappers/taxPostingsMapper';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import defaultModel from './OutgoingCurrencyOtherModel';
import OutgoingCurrencyOtherActions from './OutgoingCurrencyOtherActions';
import {
    getContractorAutocomplete,
    getSettlementAccounts
} from '../../../../../../../../services/newMoney/contractorService';
import KontragentType from '../../../../../../../../enums/KontragentType';
import { updateAccountingPostingsModelForOtherOperations } from '../../../../../../../../helpers/newMoney/postingsHelpers';
import { calculateNdsBySumAndType } from '../../../../../../../../helpers/newMoney/ndsCalculationHelper';

class OutgoingCurrencyOtherStore extends OutgoingCurrencyOtherActions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Kontragent: ``,
        KontragentSettlementAccount: ``,
        KontragentInn: ``,
        KontragentKpp: ``,
        Description: ``,
        NdsSum: ``
    };

    @observable model = { ...defaultModel };

    availableTaxPostingDirection = AvailableTaxPostingDirection.Outgoing;

    needAllSumValidation = true;

    needAllTotalSumValidation = true;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        this.updateCurrencyRate();
        this.updateAccessToPostings();
        this.initializeKontragentSettlements();

        if (typeof this.model.IncludeNds !== `boolean`) {
            const { isAfter2025, IsUsn } = this.isAfter2025WithTaxation;

            if (isAfter2025 && IsUsn) {
                this.setIncludeNds({ checked: true });
            }
        }

        if (this.model.IsTypeChanged) {
            this.calculateNdsBySumAndType([this.model.TotalSum, this.model.NdsType, this.model.IncludeNds]);
        }

        this.initTaxPostings();
        this.initAccountingPostings();
        this.initReactions();
    }

    initAccountingPostings = async () => {
        if (!this.Requisites.IsOoo) {
            // Нужно включать при каждом открытии операции на редатирование, т.к. может быть выключенно из-за контрагента-сотрудника
            this.model.ProvideInAccounting = true;
        }

        if (this.isCopy()) {
            this.model.AccountingPostings = {};
        }

        const isAccountingPostingsExist = Object.values(this.model.AccountingPostings)?.reduce((acc, v) => (toJS(v)?.length ? [...acc, v] : [...acc]), [])?.length;

        if (!this.isNew && Object.keys(this.model.AccountingPostings).length && isAccountingPostingsExist) {
            this.model.AccountingPostings.ExplainingMessage = this.getAccountingPostingsExplainingMsg() || this.model.AccountingPostings.ExplainingMessage;
            const postings = mapPostingsToModel({ postings: this.model.AccountingPostings.Postings, readOnly: false });

            postings.forEach(item => {
                // eslint-disable-next-line no-param-reassign
                item.Credit.ReadOnly = true;
                item.SubcontoCredit.forEach(s => {
                    // eslint-disable-next-line no-param-reassign
                    s.Subconto.ReadOnly = true;
                });
            });

            const updatedPostingsWithSubcontos = await updateAccountingPostingsModelForOtherOperations(postings);
            this.model.AccountingPostings.Postings = updatedPostingsWithSubcontos;
        } else {
            this.loadAccountingPostings();
        }
    };

    initReactions = () => {
        reaction(() => [this.validationState.KontragentSettlementAccount, this.validationState.KontragentInn, this.validationState.KontragentKpp], this.checkKontragentRequisitesVisibility);

        reaction(() => this.model.Kontragent, this.loadKontragentRequisites);

        reaction(() => this.model.SettlementAccountId, this.updateCurrencyRate);

        reaction(() => this.model.Date, async Date => {
            if (!this.validationState.Date) {
                this.setTaxationSystem(await taxationSystemService.getTaxSystem(Date));
                this.updateCurrencyRate();
            }
        });

        reaction(() => [this.model.TotalSum, this.model.NdsType, this.model.IncludeNds], this.calculateNdsBySumAndType);

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

    isValid = () => {
        const options = {
            Sum: this.model.TotalSum,
            TotalSum: this.model.TotalSum,
            needAllSumValidation: this.needAllSumValidation,
            needAllTotalSumValidation: this.needAllTotalSumValidation
        };

        const taxPostingsIsValid = taxPostingsValidator.isValid(this.model.TaxPostings.Postings, options);
        const accountingPostingsIsValid = this.isAccountingValid();
        const modelIsValid = !Object.keys(this.validationState).find(key => {
            return this.validationState[key] !== ``;
        });

        return taxPostingsIsValid && modelIsValid && accountingPostingsIsValid;
    };

    validateModel() {
        Object.keys(validationRules).forEach(fieldName => {
            this.validateField(fieldName, this.model[fieldName]);
        });

        this.checkKontragentRequisitesVisibility();
        this.validateTaxPostingsList();
        this.validateAccountingPostingsList();
    }

    isAccountingValid = () => {
        if (!this.model.ProvideInAccounting || !this.hasAccessToPostings) {
            return true;
        }

        return accountingPostingsValidator.isValid(this.model.AccountingPostings.Postings, { Sum: this.model.TotalSum, needAllSumValidation: this.needAllSumValidation });
    };

    getContractAutocomplete = async ({ query = `` }) => {
        const response = await autocomplete({ query, kontragentId: this.model.Kontragent.KontragentId });

        return mapForAutocomplete(response, query);
    };

    modelForSave = () => {
        const { model } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderOutgoingCurrencyOther;

        let contractorType;

        if (model.Kontragent.SalaryWorkerId > 0) {
            contractorType = KontragentType.Worker;
        } else if (model.Kontragent.KontragentId > 0) {
            contractorType = KontragentType.Kontragent;
        } else {
            contractorType = KontragentType.Ip;
        }

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
            IsPaid: true,
            TaxPostings: {
                Postings: usnTaxPostingsForServerNewBackend(model.TaxPostings.Postings, { Date: model.Date })
            },
            ProvideInAccounting: model.ProvideInAccounting,
            AccountingPosting: !this.model.ProvideInAccounting || !this.hasAccessToPostings ? null
                : customOutgoingAccountingPostingsForNewBackend(this.model.AccountingPostings.Postings)
        };

        if (!this.Requisites.IsOoo && !baseModel.AccountingPosting) {
            // Если для ИП не было сформировано проводок из-за выбранного сотрудника
            baseModel.ProvideInAccounting = false;
        }

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
            this.model.Status,
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

        this.model.TaxPostings = { Postings: [], ExplainingMessage: msg };

        if (this.model.TaxPostings && (!this.model.TaxPostings.Postings || !this.model.TaxPostings.Postings.length)) {
            this.model.TaxPostings.ExplainingMessage = msg;
            this.setTaxPostingList({});
        }
    };

    loadAccountingPostings = async () => {
        const msg = this.getAccountingPostingsExplainingMsg();

        if (msg) {
            this.model.AccountingPostings = { Postings: [], ExplainingMessage: msg };
            this.accountingPostingsLoading = false;

            return;
        }

        if (!this.Requisites.IsOoo) {
            this.accountingPostingsLoading = true;

            const posting = await this.loadPostingForIp();
            this.model.AccountingPostings = { Postings: posting && [posting], ExplainingMessage: msg };

            this.accountingPostingsLoading = false;

            return;
        }

        if (!this.model.AccountingPostings.Postings || !this.model.AccountingPostings.Postings.length) {
            this.accountingPostingsLoading = true;

            const posting = await this.loadPostingForOoo();
            this.model.AccountingPostings = { Postings: posting && [posting], ExplainingMessage: msg };

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

    loadPostingForOoo = async () => {
        const credits = await getSyntheticAccountAutocomplete();

        const credit = credits.find(item => item.Code === (this.isTransit ? SyntheticAccountCodesEnum._52_01_01 : SyntheticAccountCodesEnum._52_01_02));
        credit.ReadOnly = true;

        const creditSubcontos = await subcontoLevelForAccount({
            settlementAccountId: this.model.SettlementAccountId,
            syntheticAccountTypeId: credit.TypeId
        });

        creditSubcontos.forEach(s => {
            // eslint-disable-next-line no-param-reassign
            s.Subconto.ReadOnly = true;
        });

        return {
            key: getUniqueId(),
            Credit: credit,
            SubcontoCredit: creditSubcontos,
            Sum: this.model.TotalSum
        };
    };

    loadPostingForIp = async () => {
        const syntheticAccounts = await getSyntheticAccountAutocomplete();

        const debit = syntheticAccounts.find(item => item.Code === SyntheticAccountCodesEnum._76_06);

        const credit = syntheticAccounts.find(item => item.Code === (this.isTransit ? SyntheticAccountCodesEnum._52_01_01 : SyntheticAccountCodesEnum._52_01_02));
        const creditSubconto = await subcontoLevelForAccount({
            settlementAccountId: this.model.SettlementAccountId,
            syntheticAccountTypeId: credit.TypeId
        });

        const kontragentSubcontos = await getSubcontosAutocomplete({ type: SubcontoTypeEnum.Kontragent, query: this.model.Kontragent.KontragentName });
        const kontragentSubconto = kontragentSubcontos.find(x => x.Id === this.model.Kontragent.KontragentId);

        // Это кейс, когда контрагентом является сотрудник
        if (!kontragentSubconto) {
            return null;
        }

        const contractSubcontos = await getContractSubcontoAutocomplete({
            type: SubcontoTypeEnum.Contract,
            kontragentSubcontoId: kontragentSubconto.SubcontoId,
            query: this.model.Contract.ProjectNumber || `Основной договор`
        });

        const contractSubconto = contractSubcontos.find(x => x.Id === this.model.Contract.ProjectId) || contractSubcontos[0];

        return {
            key: getUniqueId(),
            Sum: this.model.TotalSum,
            SubcontoDebit: [
                { ...mapAutocompleteSubconto(kontragentSubconto), Level: 1 },
                { ...mapAutocompleteSubconto(contractSubconto), Level: 2 }
            ],
            SubcontoCredit: creditSubconto,
            Debit: debit,
            Credit: credit
        };
    };

    /* override */
    getFieldsForAccountingPostings = () => {
        return [
            this.model.TotalSum,
            this.model.Date,
            this.model.ProvideInAccounting,
            this.model.Kontragent.KontragentId,
            this.model.Contract.ContractBaseId
        ];
    };

    /* override */
    getAccountingPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForAccountingPostingMsg();
    };

    /* override */
    getRequiredFieldsForAccountingPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForAccountingPostings.date;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForAccountingPostings.sum;
        }

        return null;
    };

    getSalaryWorkerId = () => {
        return this.model.ContractorType === KontragentType.Worker ? this.model.Contractor.Id : null;
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
}

export default OutgoingCurrencyOtherStore;
