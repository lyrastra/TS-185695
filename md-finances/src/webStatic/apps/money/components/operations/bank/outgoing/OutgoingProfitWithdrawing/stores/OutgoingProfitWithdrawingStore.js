import { makeObservable, observable, reaction } from 'mobx';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import kontragentForm from '@moedelo/frontend-enums/mdEnums/KontragentsForm';
import KontragentType from '@moedelo/frontend-enums/mdEnums/KontragentType';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Actions from './Actions';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import defaultModel from './Model';
import validationRules from './../validationRules';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';
import { getNaturalPersonAutocomplete, getSettlementAccounts, getSelfKontragent } from '../../../../../../../../services/newMoney/contractorService';
import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';
import { getKontragentSettlements } from '../../../../../../../../helpers/newMoney/kontragentHelper';

const availableKontragentForm = kontragentForm.FL;

class OutgoingProfitWithdrawingStore extends Actions {
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

    availableTaxPostingDirection = AvailableTaxPostingDirection.Outgoing;

    availableContractorForms = [availableKontragentForm];

    availableContractorTypes = [KontragentType.Seller];

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        !this.model.Description && this.handleDescriptionMessage();
        this.model.TaxPostings.ExplainingMessage = notTaxableMessages.simple;

        this.showRequisitesOnNew();

        this.initKontragentModel();
        this.initializeKontragentSettlements();
        this.initReactions();
    }

    showRequisitesOnNew = () => {
        this.isNew && this.toggleKontragentRequisitesVisibility();
    }

    initKontragentModel = () => {
        return (!this.isNew || this.model.Kontragent.KontragentId)
            ? Object.assign(this.model.Kontragent, { KontragentId: this.model.KontragentId })
            : this.loadSelfKontragentData();
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
    };

    loadSelfKontragentData = async () => {
        const {
            Id, Name, Inn, Kpp
        } = await getSelfKontragent();

        const {
            Number, BankName, Bik, KontragentBankCorrespondentAccount
        } = (await getSettlementAccounts({ kontragentId: Id }))[0] || {};

        const data = {
            Id, Name, Inn, Kpp, Number, BankName, Bik, KontragentBankCorrespondentAccount
        };

        this.setAllContractorFields(data);
    }

    getContractorAutocomplete = async ({ query = `` }) => {
        const { SettlementAccountId, Date } = this.model;

        const data = {
            kontragentType: KontragentType.Kontragent,
            onlyFounders: null,
            count: 5,
            form: availableKontragentForm,
            SettlementAccountId,
            Date,
            query
        };

        return getNaturalPersonAutocomplete(data);
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

        this.setDescription(`Перевод прибыли от предпринимательской деятельности.`);
    };

    /* override */
    modelForSave() {
        const { model } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderOutgoingProfitWithdrawing;

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
                BankBik: model.Kontragent.KontragentBankBIK,
                BankCorrespondentAccount: model.Kontragent.KontragentBankCorrespondentAccount
            },
            Sum: model.Sum,
            IsPaid: model.Status === DocumentStatusEnum.Payed,
            Description: model.Description,
            OperationType: operationType.value
        };
    }

    /* override */
    getFieldsForTaxPostings = () => {
    };

    /* override */
    getRequiredFieldsForTaxPostingMsg = () => {
    };

    /* override */
    getTaxPostingsExplainingMsg = () => {
    };

    /* override */
    getFieldsForAccountingPostings = () => {
    };

    getRequiredFieldsForAccountingPostingMsg = () => {
    };

    /* override */
    getAccountingPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForAccountingPostingMsg();
    };
}

export default OutgoingProfitWithdrawingStore;

