import {
    makeObservable, observable, reaction, toJS
} from 'mobx';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toAmountString, toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import { getSettlementAccounts } from '../../../../../../../../services/newMoney/contractorService';
import CurrencyPaymentToSupplierActions from './CurrencyPaymentToSupplierActions';
import { autocomplete } from '../../../../../../../../services/contractService';
import { mapForAutocomplete } from '../../../../../../../../helpers/newMoney/contractHelper';
import defaultModel from './CurrencyPaymentToSupplierModel';
import validationRules from './../validationRules';
import { getKontragentSettlements } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import { mapCurrencyInvoicesDocumentsToSave } from '../../../../../../../../mappers/documentsMapper';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import requiredFieldForAccountingPostings
    from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import { isDifferenceAvailableInTax } from '../../../../../../../../helpers/MoneyOperationHelper';
import { calculateNdsBySumAndType } from '../../../../../../../../helpers/newMoney/ndsCalculationHelper';

class CurrencyPaymentToSupplierStore extends CurrencyPaymentToSupplierActions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Kontragent: ``,
        KontragentSettlementAccount: ``,
        KontragentInn: ``,
        KontragentKpp: ``,
        Description: ``,
        DocumentsSum: ``,
        NdsSum: ``
    };

    @observable model = { ...defaultModel };

    availableTaxPostingDirection = AvailableTaxPostingDirection.Outgoing;

    needAllSumValidation = false;

    needAllTotalSumValidation = true;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        if (this.isCopy()) {
            this.model.TaxPostings = {};
        }

        !this.model.Description && this.handleDescriptionMessage();
        this.setDocuments(this.model.Documents || []);
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
        this.updateCurrencyRate();
    }

    initReactions = () => {
        reaction(() => [
            this.validationState.KontragentSettlementAccount,
            this.validationState.KontragentInn,
            this.validationState.KontragentKpp], this.checkKontragentRequisitesVisibility);
        reaction(() => [this.model.Sum], this.handleDescriptionMessage);
        reaction(() => [this.model.Kontragent], this.loadKontragentRequisites);
        reaction(() => this.model.Date, this.loadTaxSystem);
        reaction(() => [this.model.SettlementAccountId, this.model.Date], this.updateCurrencyRate);
        reaction(() => [this.model.Sum, this.model.CentralBankRate], this.calculate);
        reaction(() => [this.model.TotalSum, this.model.NdsType, this.model.IncludeNds], this.calculateNdsBySumAndType);
        reaction(this.getFieldsForTaxPostings, this.loadTaxPostings);
        reaction(this.getFieldsForAccountingPostings, this.loadAccountingPostings);
    };

    handleDescriptionMessage = () => {
        if (!this.isNew && !this.isCopy()) {
            return;
        }

        const { Sum } = this.model;

        if (Sum) {
            this.setDescription(`Оплата контрагенту на сумму ${toAmountString(Sum)} ${this.sumCurrencyLetters}. НДС не облагается.`);
        } else {
            this.setDescription(`Оплата контрагенту. НДС не облагается.`);
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

    loadTaxSystem = async Date => {
        if (!this.validationState.Date) {
            this.setTaxationSystem(await taxationSystemService.getTaxSystem(Date));
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
        const isDifferenceAllowable = isDifferenceAvailableInTax(this.model.OperationType);

        const options = {
            Sum: this.model.Sum,
            TotalSum: this.model.TotalSum,
            needAllSumValidation: this.needAllSumValidation,
            needAllTotalSumValidation: this.needAllTotalSumValidation,
            isDifferenceAllowable
        };

        const taxPostingsIsValid = taxPostingsValidator.isValid(this.model.TaxPostings.Postings, options);
        const modelIsValid = !Object.keys(this.validationState).find(key => {
            return this.validationState[key] !== ``;
        });

        return taxPostingsIsValid && modelIsValid;
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
    };

    /* override */
    modelForSave = () => {
        const { model, TaxationSystem, Requisites } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderOutgoingCurrencyPaymentSuppliersForGoods;
        const Documents = Requisites.IsOoo ? [] : mapCurrencyInvoicesDocumentsToSave(toJS(model.Documents));
        const customPostings = model.TaxPostingsMode === ProvidePostingType.ByHand ? model.TaxPostings.Postings : [];
        const TaxPostings = this.getTaxPostingForSave(customPostings, model.TaxPostingsMode, TaxationSystem);

        const baseModel = {
            DocumentBaseId: model.DocumentBaseId,
            OperationType: operationType.value,
            Direction: operationType.Direction,
            Number: model.Number,
            Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
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
            TotalSum: model.TotalSum,
            IsPaid: true,
            Description: model.Description,
            Documents,
            Contract: {
                DocumentBaseId: model.Contract.ContractBaseId
            },
            TaxPostings,
            ProvideInAccounting: model.ProvideInAccounting,
            PostingsAndTaxMode: model.TaxPostingsMode
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

    /* override */
    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.Kontragent.KontragentId,
            this.model.Sum,
            mapDocumentsToSave(this.model.Documents)
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

        return null;
    };

    /* override */
    getTaxPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForTaxPostingMsg();
    }

    getFieldsForAccountingPostings = () => {
        return JSON.stringify([
            this.model.Sum,
            this.model.TotalSum,
            this.model.Date,
            this.model.Kontragent.KontragentId,
            this.model.ProvideInAccounting,
            this.model.SettlementAccountId
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

        return null;
    };

    /* override */
    getAccountingPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForAccountingPostingMsg();
    };
}

function mapDocumentsToSave(documents) {
    return toJS(documents).filter(doc => doc.DocumentBaseId > 0);
}

export default CurrencyPaymentToSupplierStore;
