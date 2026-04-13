import { makeObservable, observable, reaction } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import ContractKind from '@moedelo/frontend-enums/mdEnums/ContractKind';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Actions from './Actions';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import defaultModel from './Model';
import validationRules from './../validationRules';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import notTaxableReasonGetter from '../notTaxableReasonGetter';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import { autocomplete } from '../../../../../../../../services/contractService';
import { mapForAutocomplete } from '../../../../../../../../helpers/newMoney/contractHelper';
import { getSettlementAccounts, getCommissionAgentsAutocomplete } from '../../../../../../../../services/newMoney/contractorService';
import kontragentHelper from '../../../../../../../../helpers/newMoney/kontragentHelper';
import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';
import validationState from './validationState';

class IncomeFromCommissionAgentStore extends Actions {
    @observable validationState = { ...validationState };

    @observable model = { ...defaultModel };

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        this.model.Contract?.ProjectKind !== ContractKind.Mediation && Object.assign(this.model.Contract, { ...defaultModel.Contract });
        this.model.TaxPostings.ExplainingMessage = notTaxableReasonGetter.get({ taxationSystem: this.TaxationSystem });

        this.initializeKontragentSettlements();
        this.initAccountingPostings();
        this.initReactions();
    }

    initReactions = () => {
        reaction(
            () => [
                this.validationState.KontragentSettlementAccount,
                this.validationState.KontragentInn,
                this.validationState.KontragentKpp],
            this.checkKontragentRequisitesVisibility
        );

        reaction(() => this.model.Kontragent.KontragentId, this.loadKontragentRequisites);

        reaction(() => this.model.Date, async Date => {
            if (!this.validationState.Date) {
                this.setTaxationSystem(await taxationSystemService.getTaxSystem(Date));
            }
        });

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

    getContractorAutocomplete = ({ query = `` }) => {
        return getCommissionAgentsAutocomplete({ query });
    };

    initializeKontragentSettlements = async () => {
        // eslint-disable-next-line import/no-named-as-default-member
        this.kontragentSettlements = await kontragentHelper.getKontragentSettlements(this.model);
    };

    isValid = () => {
        return !Object.keys(this.validationState).find(key => {
            return this.validationState[key] !== ``;
        });
    };

    validateModel() {
        Object.keys(validationRules).forEach(fieldName => {
            this.validateField(fieldName, this.model[fieldName]);
        });

        this.checkKontragentRequisitesVisibility();
        !this.isNotTaxable && this.validateTaxPostingsList();
    }

    getContractAutocomplete = async ({ query = `` }) => {
        if (!this.model.Kontragent.KontragentId) {
            return {
                data: [],
                value: query
            };
        }

        const response = await autocomplete({
            query,
            kontragentId: this.model.Kontragent.KontragentId,
            withMainContract: false,
            kind: [ContractKind.Mediation]
        });

        return mapForAutocomplete(response, query);
    };

    /* override */
    modelForSave() {
        const { model } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderIncomingIncomeFromCommissionAgent;
        let postingsAndTaxMode = model.TaxPostingsMode;

        if (!model.ProvideInAccounting) {
            postingsAndTaxMode = ProvidePostingType.ByHand;
        }

        return {
            DocumentBaseId: model.DocumentBaseId,
            OperationType: operationType.value,
            Direction: operationType.Direction,
            Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
            Number: model.Number,
            SettlementAccountId: model.SettlementAccountId,
            Contractor: {
                Id: model.Kontragent.KontragentId,
                Name: model.Kontragent.KontragentName,
                Inn: model.Kontragent.KontragentINN,
                Kpp: model.Kontragent.KontragentKPP,
                SettlementAccount: model.Kontragent.KontragentSettlementAccount,
                BankName: model.Kontragent.KontragentBankName,
                BankBik: model.Kontragent.KontragentBankBIK,
                BankCorrespondentAccount: model.Kontragent.KontragentBankCorrespondentAccount
            },
            Contract: {
                DocumentBaseId: model.Contract.ContractBaseId
            },
            Sum: model.Sum,
            Description: model.Description,
            ProvideInAccounting: model.ProvideInAccounting,
            PostingsAndTaxMode: postingsAndTaxMode,
            IsPaid: model.Status === DocumentStatusEnum.Payed
        };
    }

    /* override */
    getFieldsForAccountingPostings = () => {
        return [
            this.model.Sum,
            this.model.Date,
            this.model.KontragentAccountCode,
            this.model.Kontragent.KontragentId,
            this.model.SettlementAccountId,
            this.model.ProvideInAccounting,
            this.model.Contract.ContractBaseId,
            this.model.Status
        ];
    };

    getRequiredFieldsForAccountingPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForAccountingPostings.date;
        }

        if (!this.model.Kontragent.KontragentId) {
            return requiredFieldForAccountingPostings.payer;
        }

        if (!this.model?.Contract?.ContractBaseId) {
            return requiredFieldForAccountingPostings.middlemanContract;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForAccountingPostings.sum;
        }

        return null;
    };

    /* override */
    getAccountingPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForAccountingPostingMsg();
    };
}

export default IncomeFromCommissionAgentStore;

