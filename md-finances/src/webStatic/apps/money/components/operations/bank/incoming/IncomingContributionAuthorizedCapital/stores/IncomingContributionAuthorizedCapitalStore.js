import { observable, reaction, makeObservable } from 'mobx';
import { toFloat, toAmountString } from '@moedelo/frontend-core-v2/helpers/converter';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Actions from './Actions';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import defaultModel from './Model';
import validationRules from './../validationRules';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import { autocomplete } from '../../../../../../../../services/contractService';
import { mapForAutocomplete } from '../../../../../../../../helpers/newMoney/contractHelper';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';
import { getContractorAutocompleteSortedByFounders, getSettlementAccounts } from '../../../../../../../../services/newMoney/contractorService';
import { getKontragentSettlements } from '../../../../../../../../helpers/newMoney/kontragentHelper';

class IncomingContributionAuthorizedCapitalStore extends Actions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Kontragent: ``,
        KontragentSettlementAccount: ``,
        KontragentInn: ``,
        KontragentKpp: ``,
        Description: ``
    };

    @observable model = { ...defaultModel };

    @observable kontragentLoading = false;

    @observable kontragentSettlements = {};

    @observable accountingPostingsLoading = false;

    @observable isContractorRequisitesShown = false;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        if (options.operation?.Contract?.Data) {
            const { DocumentBaseId, Number, Date } = options.operation.Contract.Data;

            Object.assign(this.model.Contract, {
                ContractBaseId: DocumentBaseId || null,
                ProjectNumber: Number || null,
                Date: Date || null
            });
        }

        this.model.TaxPostings.ExplainingMessage = notTaxableMessages.simple;

        this.initializeKontragentSettlements();
        this.initAccountingPostings();
        this.initReactions();
    }

    initReactions = () => {
        reaction(() => [this.validationState.KontragentSettlementAccount, this.validationState.KontragentInn, this.validationState.KontragentKpp], this.checkKontragentRequisitesVisibility);
        reaction(() => [this.model.Kontragent], this.loadKontragentRequisites);
        reaction(() => [this.model.Kontragent.KontragentName, this.model.Sum], ([name, sum]) => {
            let msg = ``;

            if (name) {
                msg = `Взнос в уставный капитал от  ${name}`;
            }

            if (sum) {
                msg += ` на сумму ${toAmountString(sum)} р. НДС не облагается`;
            }

            msg += `.`;

            this.setDescription(msg);
        });
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

    getContractAutocomplete = async ({ query = `` }) => {
        const response = await autocomplete({
            query,
            kontragentId: this.model.Kontragent.KontragentId,
            withMainContract: false,
            kind: [1, 2] // договоры займа и кредита
        });

        return mapForAutocomplete(response, query);
    }

    /* override */
    modelForSave() {
        const { model } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderIncomingContributionAuthorizedCapital;

        return {
            DocumentBaseId: model.DocumentBaseId,
            BaseDocumentId: model.DocumentBaseId,
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
                Form: model.Kontragent.KontragentForm,
                BankBik: model.Kontragent.KontragentBankBIK,
                BankCorrespondentAccount: model.Kontragent.KontragentBankCorrespondentAccount
            },
            Sum: model.Sum,
            ProvideInAccounting: model.ProvideInAccounting,
            Description: model.Description,
            OperationType: operationType.value
        };
    }

    /* override */
    getFieldsForAccountingPostings = () => {
        return JSON.stringify([
            this.model.Sum,
            this.model.Date,
            this.model.KontragentAccountCode,
            this.model.Kontragent.KontragentId,
            this.model.SettlementAccountId,
            this.model.ProvideInAccounting,
            this.model.IsLongTermLoan
        ]);
    };

    getRequiredFieldsForAccountingPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForAccountingPostings.date;
        }

        if (!this.model.Kontragent.KontragentId) {
            return requiredFieldForAccountingPostings.payer;
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

        return getContractorAutocompleteSortedByFounders(data);
    };
}

export default IncomingContributionAuthorizedCapitalStore;

