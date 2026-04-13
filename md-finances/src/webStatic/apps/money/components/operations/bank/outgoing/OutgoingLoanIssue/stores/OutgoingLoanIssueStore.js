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
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import notTaxableReasonGetter from '../notTaxableReasonGetter';
import { autocomplete } from '../../../../../../../../services/contractService';
import { mapForAutocomplete } from '../../../../../../../../helpers/newMoney/contractHelper';
import { getSettlementAccounts } from '../../../../../../../../services/newMoney/contractorService';
import { getKontragentSettlements } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';

class OutgoingLoanIssueStore extends Actions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Kontragent: ``,
        Contract: ``,
        KontragentSettlementAccount: ``,
        KontragentInn: ``,
        KontragentKpp: ``,
        Description: ``
    };

    @observable model = { ...defaultModel };

    needAllSumValidation = false;

    availableTaxPostingDirection = AvailableTaxPostingDirection.Outgoing;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        /* у данной операции может быть договор только с видом Выданный займ */
        this.model.Contract?.ProjectKind !== ContractKind.OutgoingLoan && this.setContract();

        !this.model.Description && this.handleDescriptionMessage();

        this.initializeKontragentSettlements();
        this.initTaxPostings();
        this.initAccountingPostings();
        this.initReactions();
    }

    initReactions = () => {
        reaction(() => [this.validationState.KontragentSettlementAccount, this.validationState.KontragentInn, this.validationState.KontragentKpp], this.checkKontragentRequisitesVisibility);
        reaction(() => [this.model.Kontragent], this.loadKontragentRequisites);
        reaction(() => [this.model.Kontragent, this.model.Sum], this.handleDescriptionMessage);

        reaction(() => this.model.Date, async Date => {
            if (!this.validationState.Date) {
                this.setTaxationSystem(await taxationSystemService.getTaxSystem(Date));
            }
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
            Sum: this.model.Sum,
            needAllSumValidation: this.needAllSumValidation
        };

        return !Object.keys(this.validationState).find(key => {
            return this.validationState[key] !== ``;
        }) && (this.isNotTaxable || taxPostingsValidator.isValid(this.model.TaxPostings.Postings, options));
    };

    validateModel() {
        Object.keys(validationRules).forEach(fieldName => {
            this.validateField(fieldName, this.model[fieldName]);
        });

        this.checkKontragentRequisitesVisibility();
        !this.isNotTaxable && this.validateTaxPostingsList();
    }

    handleDescriptionMessage = () => {
        if (!this.isNew) {
            return;
        }

        const { Contract } = this.model;
        const contractText = Contract.ProjectNumber && Contract.ProjectNumber.length > 0
            ? ` по договору N ${Contract.ProjectNumber} от ${Contract.Date}`
            : ``;

        this.setDescription(`Выдача займа${contractText}. НДС не облагается.`);
    };

    getContractAutocomplete = async ({ query = `` }) => {
        const response = await autocomplete({
            query,
            kontragentId: this.model.Kontragent.KontragentId,
            withMainContract: false,
            kind: [ContractKind.OutgoingLoan]
        });

        return mapForAutocomplete(response, query);
    };

    /* override */
    modelForSave() {
        const { model } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderOutgoingLoanIssue;
        const result = {
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
            IsLongTermLoan: model.IsLongTermLoan || false,
            ProvideInAccounting: model.ProvideInAccounting,
            IsPaid: model.Status === DocumentStatusEnum.Payed
        };

        return result;
    }

    /* override */
    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.Sum,
            this.model.Kontragent.KontragentId,
            this.model.Status,
            this.model.IsLongTermLoan,
            this.TaxationSystem.StartYear
        ]);
    };

    /* override */
    getRequiredFieldsForTaxPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForTaxPostings.date;
        }

        if (!this.model.Kontragent.KontragentId) {
            return requiredFieldForTaxPostings.outgoingPayer;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForTaxPostings.sum;
        }

        if (this.model.Status === DocumentStatusEnum.NotPayed) {
            return requiredFieldForTaxPostings.notPaid;
        }

        return null;
    };

    /* override */
    getTaxPostingsExplainingMsg = () => {
        return notTaxableReasonGetter.get({
            taxationSystem: this.TaxationSystem
        }) || this.getRequiredFieldsForTaxPostingMsg();
    };

    /* override */
    getFieldsForAccountingPostings = () => {
        return JSON.stringify([
            this.model.Sum,
            this.model.Date,
            this.model.KontragentAccountCode,
            this.model.Kontragent.KontragentId,
            this.model.SettlementAccountId,
            this.model.ProvideInAccounting,
            this.model.IsLongTermLoan,
            this.model.Status
        ]);
    };

    getRequiredFieldsForAccountingPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForAccountingPostings.date;
        }

        if (!this.model.Kontragent.KontragentId) {
            return requiredFieldForAccountingPostings.outgoingPayer;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForAccountingPostings.sum;
        }

        if (this.model.Status === DocumentStatusEnum.NotPayed) {
            return requiredFieldForAccountingPostings.notPaid;
        }

        return null;
    };

    /* override */
    getAccountingPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForAccountingPostingMsg();
    };
}

export default OutgoingLoanIssueStore;

