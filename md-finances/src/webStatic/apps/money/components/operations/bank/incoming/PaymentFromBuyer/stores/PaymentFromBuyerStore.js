import {
    observable, reaction, toJS, makeObservable, override
} from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
import PaymentFromBuyerActions from './PaymentFromBuyerActions';
import { autocomplete } from '../../../../../../../../services/contractService';
import { getSettlementAccounts } from '../../../../../../../../services/newMoney/contractorService';
import { mapForAutocomplete } from '../../../../../../../../helpers/newMoney/contractHelper';
import { mapLinkedBillToBackend } from '../../../../../../../../mappers/linkedBills/linkedBillsMapper';
import { mapDocumentsToNewBackend } from '../../../../../../../../helpers/newMoney/linkedDocumentsHelper';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import defaultModel from './PaymentFromBuyerModel';
import validationRules from './../validationRules';
import OrderType from '../../../../../../../../enums/OrderTypeEnum';
import notTaxableReasonGetter from './../notTaxableReasonGetter';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import { getKontragentSettlements } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import NdsTypesEnum from '../../../../../../../../enums/newMoney/NdsTypesEnum';
import { calculateNdsBySumAndType } from '../../../../../../../../helpers/newMoney/ndsCalculationHelper';

class PaymentFromBuyerStore extends PaymentFromBuyerActions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        NdsSum: ``,
        Kontragent: ``,
        MediationCommission: ``,
        KontragentSettlementAccount: ``,
        KontragentInn: ``,
        KontragentKpp: ``,
        DocumentsSum: ``,
        DocumentsAccountCode: ``,
        Description: ``,
        BillsSum: ``,
        TaxationSystemType: ``,
        Patent: ``,
        ReserveSum: ``,
        MediationCommissionNdsSum: ``
    };

    /* ui state */
    @observable isContractorRequisitesShown = false;
    /* ui state END */

    @observable model = { ...defaultModel };

    @observable kontragentSettlements = [];

    @observable taxPostingsLoading = false;

    @observable accountingPostingsLoading = false;

    @observable kontragentLoading = false;

    @observable reserve = { saved: true, opened: false };

    availableTaxPostingDirection = AvailableTaxPostingDirection.Incoming;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        const { isAfter2025, IsUsn, IsOsno } = this.isAfter2025WithTaxation;

        if (typeof this.model.IncludeNds !== `boolean`) {
            if (isAfter2025 && IsUsn) {
                this.setIncludeNds({ checked: true });
            } else {
                this.setIncludeNds({ checked: IsOsno });
            }
        }

        if (typeof this.model.IncludeMediationCommissionNds !== `boolean` && isAfter2025 && IsUsn) {
            this.setIncludeMediationCommissionNds({ checked: true });
        }

        if (isAfter2025 && IsUsn && this.model.TaxationSystemType === TaxationSystemType.Patent && this.model.NdsType === NdsTypesEnum.None && this.model.IncludeNds) {
            this.setIncludeNds({ checked: false });
            this.setNdsType({ value: null });
            this.setNdsSum({ value: null });
        }

        // eslint-disable-next-line max-len
        if (isAfter2025 && IsUsn && this.model.TaxationSystemType === TaxationSystemType.Patent && this.model.MediationCommissionNdsType === NdsTypesEnum.None && this.model.IncludeMediationCommissionNds) {
            this.setIncludeMediationCommissionNds({ checked: false });
            this.setMediationCommissionNdsSum({ value: null });
            this.setMediationCommissionNdsType({ value: null });
        }

        this.setDocuments(this.model.Documents || []);
        this.setBills(this.model.Bills || []);
        this.initializeKontragentSettlements();
        this.setActivePatents(options.activePatents || []);

        if (this.model.IsTypeChanged) {
            this.calculateNdsBySumAndType([this.model.Sum, this.model.NdsType, this.model.IncludeNds]);
        }

        this.initAccountingPostings();
        this.initTaxPostings();
        this.initReactions();
    }

    initReactions = () => {
        // reaction(() => [this.model.IsMainContractor], this.validateDocumentsByAccountCode);  вторая часть ТС, будет тестироваться позже
        reaction(() => [this.validationState.KontragentSettlementAccount, this.validationState.KontragentInn, this.validationState.KontragentKpp], this.checkKontragentRequisitesVisibility);
        reaction(() => [this.model.Sum, this.model.NdsType, this.model.IncludeNds], this.calculateNdsBySumAndType);
        reaction(() => [this.model.MediationCommission, this.model.MediationCommissionNdsType, this.model.IncludeMediationCommissionNds], this.calculateMediationCommissionNdsBySumAndType);
        reaction(() => [this.model.Kontragent], this.loadKontragentRequisites);
        reaction(() => [this.currentNdsRateFromAccPolicy], this.checkNdsFromAccPolicy);
        reaction(() => this.model.Date, async Date => {
            if (!this.validationState.Date) {
                this.setTaxationSystem(await taxationSystemService.getTaxSystem(Date));
                this.updateActivePatents();
            }
        });
        reaction(this.getFieldsForTaxPostings, this.loadTaxPostings);
        reaction(this.getFieldsForAccountingPostings, this.loadAccountingPostings);
    };

    calculateNdsBySumAndType = ([Sum, NdsType, IncludeNds, NdsSum]) => {
        if (NdsSum || NdsSum === 0) {
            return;
        }

        if (IncludeNds) {
            const ndsSum = NdsType === null ? `` : calculateNdsBySumAndType(Sum, NdsType);

            this.setNdsSum({ value: ndsSum });
        }
    };

    calculateMediationCommissionNdsBySumAndType = ([MediationCommission, MediationCommissionNdsType, IncludeMediationCommissionNds]) => {
        if (IncludeMediationCommissionNds) {
            const ndsSum = MediationCommissionNdsType === null ? `` : calculateNdsBySumAndType(MediationCommission, MediationCommissionNdsType);

            this.setMediationCommissionNdsSum({ value: ndsSum });
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
        }) &&
            taxPostingsValidator.isValid(this.model.TaxPostings.Postings, options);
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
        const {
            model, documents, bills, TaxationSystem
        } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderIncomingPaymentForGoods;
        const customPostings = model.TaxPostingsMode === ProvidePostingType.ByHand ? model.TaxPostings.Postings : [];
        const TaxPostings = this.getTaxPostingForSave(customPostings, model.TaxPostingsMode, TaxationSystem);
        let postingsAndTaxMode = model.TaxPostingsMode;

        if (!model.ProvideInAccounting) {
            postingsAndTaxMode = ProvidePostingType.ByHand;
        }

        const baseModel = {
            Id: model.Id,
            BaseDocumentId: model.BaseDocumentId,
            DocumentBaseId: model.DocumentBaseId,
            OrderType: OrderType.PaymentOrder,
            OperationType: operationType.value,
            Direction: operationType.Direction,
            Number: model.Number,
            Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
            SettlementAccountId: model.SettlementAccountId,
            Kontragent: model.Kontragent,
            KontragentId: model.Kontragent.KontragentId,
            KontragentAccountCode: model.KontragentAccountCode,
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
            MediationCommission: model.MediationCommission,
            Sum: model.Sum,
            IncludeNds: model.IncludeNds,
            NdsSum: model.NdsSum,
            NdsType: model.NdsType,
            Description: model.Description,
            Documents: mapDocumentsToNewBackend(documents),
            ReserveSum: model.ReserveSum,
            ContractBaseId: model.Contract.ContractBaseId,
            Contract: {
                DocumentBaseId: model.Contract.ContractBaseId
            },
            Nds: {
                IncludeNds: model.IncludeNds,
                Type: model.NdsType,
                Sum: model.NdsSum || 0
            },
            Mediation: {
                IsMediation: !!model.IsMediation,
                CommissionSum: model.MediationCommission || null
            },

            IsMainContractor: model.IsMainContractor,
            Bills: toJS(bills).filter(doc => doc.DocumentBaseId > 0 && toFloat(doc.Sum) > 0).map(mapLinkedBillToBackend),
            IsMediation: model.IsMediation,
            PaymentPriority: model.PaymentPriority,
            ProvideInAccounting: model.ProvideInAccounting,
            PostingsAndTaxMode: postingsAndTaxMode,
            TaxPostings,
            TaxationSystemType: model.TaxationSystemType,
            PatentId: this.patentIdForSave
        };

        const getMediationNds = () => {
            return model.IsMediation ? {
                IncludeNds: model.IncludeMediationCommissionNds,
                Type: model.MediationCommissionNdsType,
                Sum: model.MediationCommissionNdsSum || 0
            } : null;
        };

        const usnAfter2025Model = {
            MediationNds: getMediationNds(),
            Nds: {
                IncludeNds: model.IncludeNds,
                Type: model.NdsType,
                Sum: model.NdsSum || 0
            }
        };
        const { isAfter2025, IsUsn } = this.isAfter2025WithTaxation;

        return IsUsn && isAfter2025 ? { ...baseModel, ...usnAfter2025Model } : baseModel;
    };

    /* override */
    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.Sum,
            this.model.Kontragent.KontragentId,
            this.model.IsMediation,
            this.model.MediationCommission,
            this.model.IncludeNds,
            this.model.NdsType,
            this.model.NdsSum,
            this.model.IncludeMediationCommissionNds,
            this.model.MediationCommissionNdsSum,
            this.model.MediationCommissionNdsType,
            mapDocumentsToSave(this.model.Documents),
            this.TaxationSystem.StartYear,
            this.model.TaxationSystemType
        ]);
    };

    /* override */
    getRequiredFieldsForTaxPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForTaxPostings.date;
        }

        if (!this.model.Kontragent.KontragentId) {
            return requiredFieldForTaxPostings.payer;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForTaxPostings.sum;
        }

        if (this.model.IncludeNds && this.model.NdsType !== NdsTypesEnum.Nds0 && this.model.NdsType !== NdsTypesEnum.None && !this.model.NdsSum) {
            return requiredFieldForTaxPostings.ndsSum;
        }

        // eslint-disable-next-line max-len
        if (this.model.IsMediation && this.model.IncludeMediationCommissionNds && this.model.MediationCommissionNdsType !== NdsTypesEnum.Nds0 && this.model.MediationCommissionNdsType !== NdsTypesEnum.None && !this.model.MediationCommissionNdsSum) {
            return requiredFieldForTaxPostings.ndsSum;
        }

        return null;
    };

    /* override */
    getTaxPostingsExplainingMsg = () => {
        const notTaxableReasonRequest = {
            taxationSystem: this.TaxationSystem,
            taxationSystemType: this.model.TaxationSystemType,
            isOoo: this.isOoo
        };

        return notTaxableReasonGetter.get(notTaxableReasonRequest) || this.getRequiredFieldsForTaxPostingMsg();
    };

    /* override */
    getFieldsForAccountingPostings = () => {
        return JSON.stringify([
            this.model.Sum,
            this.model.Date,
            this.model.Contract,
            this.model.KontragentAccountCode,
            this.model.Kontragent.KontragentId,
            this.model.SettlementAccountId,
            mapDocumentsToSave(this.model.Documents),
            this.model.ProvideInAccounting
        ]);
    };

    /* override */
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
}

function mapDocumentsToSave(documents) {
    return toJS(documents).filter(doc => doc.DocumentBaseId > 0);
}

export default PaymentFromBuyerStore;
