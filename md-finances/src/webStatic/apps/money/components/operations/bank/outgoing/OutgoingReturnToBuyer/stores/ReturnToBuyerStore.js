import { makeObservable, observable, reaction } from 'mobx';
import { toAmountString, toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import ReturnToBuyerActions from './ReturnToBuyerActions';
import { autocomplete } from '../../../../../../../../services/contractService';
import { getSettlementAccounts } from '../../../../../../../../services/newMoney/contractorService';
import { mapForAutocomplete } from '../../../../../../../../helpers/newMoney/contractHelper';
import defaultModel from './ReturnToBuyerModel';
import validationRules from './../validationRules';
import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';
import notTaxableReasonGetter from './../notTaxableReasonGetter';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import { getKontragentSettlements } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import NdsTypesEnum from '../../../../../../../../enums/newMoney/NdsTypesEnum';

class ReturnToBuyerStore extends ReturnToBuyerActions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        NdsSum: ``,
        Kontragent: ``,
        KontragentSettlementAccount: ``,
        KontragentInn: ``,
        KontragentKpp: ``,
        Description: ``,
        TaxationSystemType: ``,
        Patent: ``
    };

    @observable model = { ...defaultModel };

    availableTaxPostingDirection = AvailableTaxPostingDirection.Incoming;

    /* override */
    needAllSumValidation = true;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        this.setActivePatents(options.activePatents || []);

        this.model.TaxPostingsMode = options.operation.TaxPostingsInManualMode
            ? ProvidePostingType.ByHand
            : ProvidePostingType.Auto;

        this.initIncludeNds();
        !this.model.Description && this.handleDescriptionMessage();
        this.initializeKontragentSettlements();

        if (this.model.IsTypeChanged) {
            this.calculateNdsBySumAndType([this.model.Sum, this.model.NdsType, this.model.IncludeNds]);
        }

        this.initAccountingPostings();
        this.initTaxPostings();
        this.initReactions();
    }

    initReactions = () => {
        reaction(() => [this.validationState.KontragentSettlementAccount, this.validationState.KontragentInn, this.validationState.KontragentKpp], this.checkKontragentRequisitesVisibility);

        reaction(() => [this.model.Sum, this.model.NdsType, this.model.IncludeNds], this.calculateNdsBySumAndType);

        reaction(() => [this.model.Sum, this.model.IncludeNds, this.model.NdsSum], this.handleDescriptionMessage);

        reaction(() => [this.model.NdsSum], () => this.validateField(`NdsSum`));

        reaction(() => [this.model.Kontragent], this.loadKontragentRequisites);

        reaction(() => this.model.Date, async Date => {
            if (!this.validationState.Date) {
                this.setTaxationSystem(await taxationSystemService.getTaxSystem(Date));
                this.updateActivePatents();
            }
        });

        reaction(this.getFieldsForTaxPostings, this.loadTaxPostings);
        reaction(this.getFieldsForAccountingPostings, this.loadAccountingPostings);
    };

    initIncludeNds() {
        if (typeof this.model.IncludeNds !== `boolean`) {
            const { isAfter2025, IsUsn, IsOsno } = this.isAfter2025WithTaxation;

            if (isAfter2025 && IsUsn) {
                this.setIncludeNds({ checked: true });
            } else {
                this.setIncludeNds({ checked: IsOsno });
            }
        }
    }

    handleDescriptionMessage = () => {
        if (!this.isNew && !this.isCopy()) {
            return;
        }

        const { Sum, IncludeNds, NdsSum } = this.model;
        let msg = `Возврат денег клиенту. НДС не облагается.`;

        if (Sum && (!IncludeNds || !NdsSum)) {
            msg = `Возврат денег клиенту на сумму ${toAmountString(Sum)} руб. НДС не облагается.`;
        }

        if (Sum && IncludeNds && NdsSum) {
            msg = `Возврат денег клиенту на сумму ${toAmountString(Sum)} руб. В т.ч. НДС ${toAmountString(NdsSum)} руб.`;
        }

        this.setDescription(msg);
    }

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
        }) &&
            taxPostingsValidator.isUsnListValid(this.model.TaxPostings.Postings);
    };

    validateModel() {
        Object.keys(validationRules).forEach(fieldName => {
            this.validateField(fieldName, this.model[fieldName]);
        });

        this.checkKontragentRequisitesVisibility();
        this.validateTaxPostingsList();
    }

    getContractAutocomplete = async ({ query = `` }) => {
        const response = await autocomplete({ query, kontragentId: this.model.Kontragent.KontragentId });

        return mapForAutocomplete(response, query);
    }

    /* override */
    modelForSave = () => {
        const { model, TaxationSystem } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderOutgoingReturnToBuyer;
        const customPostings = model.TaxPostingsMode === ProvidePostingType.ByHand ? model.TaxPostings.Postings : [];
        const TaxPostings = this.getTaxPostingForSave(customPostings, model.TaxPostingsMode, TaxationSystem);

        return {
            OperationType: operationType.value,
            Direction: operationType.Direction,
            DocumentBaseId: model.DocumentBaseId,
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
            Nds: {
                IncludeNds: model.IncludeNds,
                Type: model.NdsType,
                Sum: model.NdsSum || 0
            },
            Sum: model.Sum,
            Description: model.Description,
            IsMainContractor: model.IsMainContractor,
            ProvideInAccounting: model.ProvideInAccounting,
            IsPaid: model.Status === DocumentStatusEnum.Payed,
            TaxationSystemType: model.TaxationSystemType,
            PatentId: this.patentIdForSave,
            TaxPostings
        };
    }

    /* override */
    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.Sum,
            this.model.Kontragent.KontragentId,
            this.model.Status,
            this.model.NdsSum,
            this.model.NdsType,
            this.model.IncludeNds,
            this.TaxationSystem.StartYear,
            this.model.TaxationSystemType
        ]);
    }

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

        if (this.model.IncludeNds && this.model.NdsType !== NdsTypesEnum.Nds0 && this.model.NdsType !== NdsTypesEnum.None && !this.model.NdsSum) {
            return requiredFieldForTaxPostings.ndsSum;
        }

        if (this.model.Status === DocumentStatusEnum.NotPayed) {
            return requiredFieldForTaxPostings.notPaid;
        }

        return null;
    }

    /* override */
    getTaxPostingsExplainingMsg = () => {
        return notTaxableReasonGetter.get({ taxationSystem: this.TaxationSystem, isIp: this.isIp }) || this.getRequiredFieldsForTaxPostingMsg();
    }

    /* override */
    getValidatedUsnPosting = posting => {
        return taxPostingsValidator.getValidatedNegativeUsnPosting(posting);
    }

    /* override */
    getFieldsForAccountingPostings = () => {
        return JSON.stringify([
            this.model.Sum,
            this.model.Date,
            this.model.KontragentAccountCode,
            this.model.Kontragent.KontragentId,
            this.model.SettlementAccountId,
            this.model.Status,
            this.model.ProvideInAccounting
        ]);
    }

    /* override */
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
    }

    /* override */
    getAccountingPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForAccountingPostingMsg();
    }
}

export default ReturnToBuyerStore;
