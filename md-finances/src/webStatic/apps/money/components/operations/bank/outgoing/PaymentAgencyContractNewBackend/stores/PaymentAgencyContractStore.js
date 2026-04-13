import { makeObservable, observable, reaction } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import ContractKind from '@moedelo/frontend-enums/mdEnums/ContractKind';
import Actions from './Actions';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import defaultModel from './Model';
import validationRules from './../validationRules';
import requiredFieldForAccountingPostings
    from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import { autocomplete } from '../../../../../../../../services/contractService';
import { mapForAutocomplete } from '../../../../../../../../helpers/newMoney/contractHelper';
import { getSettlementAccounts } from '../../../../../../../../services/newMoney/contractorService';
import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import { getKontragentSettlements } from '../../../../../../../../helpers/newMoney/kontragentHelper';

class PaymentAgencyContractStore extends Actions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Kontragent: ``,
        KontragentSettlementAccount: ``,
        KontragentInn: ``,
        KontragentKpp: ``,
        Contract: ``,
        Description: ``
    };

    @observable model = { ...defaultModel };

    availableTaxPostingDirection = AvailableTaxPostingDirection.Outgoing;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        /* у данной операции может быть договор только с видом Посреднический */
        this.model.Contract?.ProjectKind !== ContractKind.Mediation && this.setContract();

        !this.model.Description && this.handleDescriptionMessage();

        this.initializeKontragentSettlements();
        this.initAccountingPostings();
        this.initTaxPostings();
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
        return !Object.keys(this.validationState).find(key => {
            return this.validationState[key] !== ``;
        });
    };

    validateModel() {
        Object.keys(validationRules).forEach(fieldName => {
            this.validateField(fieldName, this.model[fieldName]);
        });
    }

    handleDescriptionMessage = () => {
        if (!this.isNew) {
            return;
        }

        this.setDescription(`Выплата по агентскому договору. НДС не облагается.`);
    };

    getContractAutocomplete = async ({ query = `` }) => {
        const response = await autocomplete({
            query,
            kontragentId: this.model.Kontragent.KontragentId,
            withMainContract: false,
            kind: [ContractKind.Mediation] // договоры займа и кредита
        });

        return mapForAutocomplete(response, query);
    };

    /* override */
    modelForSave() {
        const { model } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderOutgoingPaymentAgencyContract;

        return {
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
            OperationType: operationType.value,
            IsPaid: model.Status === DocumentStatusEnum.Payed,
            DocumentBaseId: model.DocumentBaseId
        };
    }

    /* override */
    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.Sum,
            this.model.Kontragent.KontragentId,
            this.model.Status,
            this.TaxationSystem.StartYear,
            this.model.Contract.ContractBaseId
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
        return this.getRequiredFieldsForTaxPostingMsg();
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
            this.model.Status,
            this.model.Contract.ContractBaseId
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

export default PaymentAgencyContractStore;

