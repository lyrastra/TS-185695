import {
    observable, reaction, toJS, makeObservable
} from 'mobx';
import { toFloat, toDate } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import { getSettlementAccounts } from '../../../../../../../../services/newMoney/contractorService';
import { getKontragentSettlements } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import validationRules from '../validationRules';
import { autocomplete } from '../../../../../../../../services/contractService';
import { mapForAutocomplete } from '../../../../../../../../helpers/newMoney/contractHelper';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import defaultModel from './IncomingCurrencyPaymentFromBuyerModel';
import IncomingCurrencyPaymentFromBuyerActions from './IncomingCurrencyPaymentFromBuyerActions';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import { mapDocumentsToNewBackend } from '../../../../../../../../helpers/newMoney/linkedDocumentsHelper';
import { getTaxationSystemType } from '../../../../../../../../mappers/taxationSystemMapper';
import { isDifferenceAvailableInTax } from '../../../../../../../../helpers/MoneyOperationHelper';
import { calculateNdsBySumAndType } from '../../../../../../../../helpers/newMoney/ndsCalculationHelper';
import NdsTypesEnum from '../../../../../../../../enums/newMoney/NdsTypesEnum';

class IncomingCurrencyPaymentFromBuyerStore extends IncomingCurrencyPaymentFromBuyerActions {
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
        DocumentsSum: ``,
        TaxationSystemType: ``,
        Patent: ``,
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

    needAllSumValidation = false;

    needAllTotalSumValidation = true;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        this.updatePatents();

        this.model.TaxPostingsMode = options.operation.TaxPostingsInManualMode
            ? ProvidePostingType.ByHand
            : ProvidePostingType.Auto;
        // родительский store ожидает поле PostingsAndTaxMode
        this.model.PostingsAndTaxMode = this.model.TaxPostingsMode;

        if (typeof this.model.IncludeNds !== `boolean`) {
            const { isAfter2025, IsUsn } = this.isAfter2025WithTaxation;

            if (isAfter2025 && IsUsn) {
                this.setIncludeNds({ checked: true });
            }
        }

        this.updateCurrencyRate();
        this.setDocuments(this.model.Documents || []);
        this.initializeKontragentSettlements();

        if (this.model.IsTypeChanged) {
            this.calculateNdsBySumAndType([this.model.TotalSum, this.model.NdsType, this.model.IncludeNds]);
        }

        this.initTaxPostings();
        this.initAccountingPostings();
        this.initReactions();
    }

    initReactions = () => {
        reaction(() => [this.validationState.KontragentSettlementAccount, this.validationState.KontragentInn, this.validationState.KontragentKpp], this.checkKontragentRequisitesVisibility);

        reaction(() => this.model.Kontragent, this.loadKontragentRequisites);

        reaction(() => this.model.SettlementAccountId, this.updateCurrencyRate);

        reaction(() => [this.model.TotalSum, this.model.NdsType, this.model.IncludeNds], this.calculateNdsBySumAndType);

        reaction(() => [this.currentNdsRateFromAccPolicy], this.checkNdsFromAccPol);

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

    /**
     * Запрашивает активные патенты и в зависимоси от их наличия и текущего TaxationSystemType сбрасывает СНО
     */
    updatePatents = async () => {
        const date = toDate(this.model.Date);
        const activePatents = await taxationSystemService.getActivePatents(date);

        this.setActivePatents(activePatents || []);

        if (this.model.PatentId && !this.isCurrentPatentActive) {
            this.setPatentId({ value: null });
        }

        if (this.taxationSystemTypeValueIsPatent && !this.hasActivePatents) {
            const newTaxationSystemType = getTaxationSystemType(await taxationSystemService.getTaxSystem(date));
            this.setTaxationSystemType({ value: newTaxationSystemType });
        }
    }

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
        const {
            model, documents, TaxationSystem
        } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderIncomingCurrencyPaymentFromBuyer;
        let postingsAndTaxMode = model.TaxPostingsMode;

        if (!model.ProvideInAccounting) {
            postingsAndTaxMode = ProvidePostingType.ByHand;
        }

        const customPostings = postingsAndTaxMode === ProvidePostingType.ByHand ? model.TaxPostings.Postings : [];
        const TaxPostings = this.getTaxPostingForSave(customPostings, postingsAndTaxMode, TaxationSystem);

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
            Description: model.Description,
            Contract: {
                DocumentBaseId: model.Contract.ContractBaseId
            },
            Documents: mapDocumentsToNewBackend(documents),
            ProvideInAccounting: model.ProvideInAccounting,
            TaxationSystemType: model.TaxationSystemType,
            PatentId: this.patentIdForSave,
            TaxPostings
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
            this.model.TotalSum,
            this.model.IncludeNds,
            this.model.NdsSum,
            this.model.NdsType,
            this.model.Kontragent.KontragentId,
            this.TaxationSystem.StartYear,
            this.model.SettlementAccountId,
            this.model.TaxationSystemType,
            mapDocumentsToSave(this.model.Documents)
        ]);
    };

    getRequiredFieldsForTaxPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForTaxPostings.date;
        }

        if (!this.model.Kontragent.KontragentId) {
            return requiredFieldForAccountingPostings.payer;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForTaxPostings.sum;
        }

        if (this.model.IncludeNds && this.model.NdsType !== NdsTypesEnum.Nds0 && this.model.NdsType !== NdsTypesEnum.None && !this.model.NdsSum) {
            return requiredFieldForTaxPostings.ndsSum;
        }

        return null;
    };

    getTaxPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForTaxPostingMsg();
    };

    /* override */
    getFieldsForAccountingPostings = () => {
        return JSON.stringify([
            this.model.Sum,
            this.model.Date,
            this.model.TotalSum,
            this.model.SettlementAccountId,
            this.model.Kontragent.KontragentId,
            this.model.ProvideInAccounting,
            this.model.TaxationSystemType,
            mapDocumentsToSave(this.model.Documents)
        ]);
    };

    /* override */
    getRequiredFieldsForAccountingPostingMsg = () => {
        if (!this.model.Kontragent.KontragentId) {
            return requiredFieldForAccountingPostings.payer;
        }

        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForAccountingPostings.date;
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

export default IncomingCurrencyPaymentFromBuyerStore;
