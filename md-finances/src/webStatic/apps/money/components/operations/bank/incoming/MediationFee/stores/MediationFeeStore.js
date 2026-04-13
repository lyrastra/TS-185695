import {
    observable, reaction, toJS, makeObservable
} from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Actions from './Actions';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import defaultModel from './Model';
import validationRules from './../validationRules';
import OrderType from '../../../../../../../../enums/OrderTypeEnum';
import notTaxableReasonGetter from './../notTaxableReasonGetter';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import { calculateNdsBySumAndType } from '../../../../../../../../helpers/newMoney/ndsCalculationHelper';
import { mapLinkedBillToBackend } from '../../../../../../../../mappers/linkedBills/linkedBillsMapper';
import { getSettlementAccounts } from '../../../../../../../../services/newMoney/contractorService';
import { getKontragentSettlements } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import { round } from '../../../../../../../../helpers/numberConverter';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';
import { mapDocumentsToNewBackend } from '../../../../../../../../helpers/newMoney/linkedDocumentsHelper';
import NdsTypesEnum from '../../../../../../../../enums/newMoney/NdsTypesEnum';

class MediationFeeStore extends Actions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        NdsSum: ``,
        Kontragent: ``,
        KontragentSettlementAccount: ``,
        KontragentInn: ``,
        KontragentKpp: ``,
        MiddlemanContract: ``,
        DocumentsSum: ``,
        Description: ``,
        BillsSum: ``
    };

    @observable model = { ...defaultModel };

    @observable isContractorRequisitesShown = false;

    @observable kontragentSettlements = [];

    @observable taxPostingsLoading = false;

    @observable accountingPostingsLoading = false;

    availableTaxPostingDirection = AvailableTaxPostingDirection.Incoming;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        if (typeof this.model.IncludeNds !== `boolean`) {
            const { isAfter2025, IsUsn, IsOsno } = this.isAfter2025WithTaxation;

            if (isAfter2025 && IsUsn) {
                this.setIncludeNds({ checked: true });
            } else {
                this.setIncludeNds({ checked: IsOsno });
            }
        }

        this.setDocuments(this.model.Documents || []);
        this.setBills(this.model.Bills || []);
        this.initializeKontragentSettlements();

        if (this.model.IsTypeChanged) {
            this.calculateNdsBySumAndType([this.model.Sum, this.model.NdsType, this.model.IncludeNds]);
        }

        this.initTaxPostings();
        this.initAccountingPostings();
        this.initReactions();
    }

    initReactions = () => {
        reaction(() => [this.validationState.KontragentSettlementAccount, this.validationState.KontragentInn, this.validationState.KontragentKpp], this.checkKontragentRequisitesVisibility);
        reaction(() => [this.model.Sum, this.model.NdsType, this.model.IncludeNds], this.calculateNdsBySumAndType);
        reaction(() => [this.model.Kontragent], this.loadKontragentRequisites);
        reaction(() => [this.currentNdsRateFromAccPolicy], this.checkNdsFromAccPol);
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

    calculateNdsBySumAndType = ([Sum, NdsType, IncludeNds, NdsSum]) => {
        if (NdsSum || NdsSum === 0) {
            return;
        }

        if (IncludeNds) {
            const ndsSum = NdsType === null ? `` : calculateNdsBySumAndType(Sum, NdsType);

            this.setNdsSum({ value: ndsSum });
        }
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

    modelForSave() {
        const { model, TaxationSystem } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderIncomingMediationFee;

        let postingsAndTaxMode = model.TaxPostingsMode;

        if (!model.ProvideInAccounting) {
            postingsAndTaxMode = ProvidePostingType.ByHand;
        }

        const customPostings = model.TaxPostingsMode === ProvidePostingType.ByHand ? model.TaxPostings.Postings : [];
        const TaxPostings = this.getTaxPostingForSave(customPostings, model.TaxPostingsMode, TaxationSystem);

        return {
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
                DocumentBaseId: model?.MiddlemanContract?.DocumentBaseId || 0
            },
            MiddlemanContract: model.MiddlemanContract,
            Sum: model.Sum,
            IncludeNds: model.IncludeNds,
            NdsSum: model.NdsSum,
            NdsType: model.NdsType,
            Nds: {
                IncludeNds: model.IncludeNds,
                Type: model.NdsType,
                Sum: model.NdsSum || 0
            },
            Mediation: {
                IsMediation: !!model.IsMediation,
                CommissionSum: model.MediationCommission || null
            },
            Description: model.Description,
            Bills: toJS(model.Bills).filter(doc => doc.DocumentBaseId > 0 && toFloat(doc.Sum) > 0).map(mapLinkedBillToBackend),
            Documents: mapDocumentsToNewBackend(model.Documents),
            PaymentPriority: model.PaymentPriority,
            ProvideInAccounting: model.ProvideInAccounting,
            PostingsAndTaxMode: postingsAndTaxMode,
            TaxPostings
        };
    }

    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.Sum,
            this.model.IncludeNds,
            this.model.NdsSum,
            this.model.NdsType,
            this.model.Kontragent.KontragentId,
            this.model.MiddlemanContract,
            this.documentsSumExceptMiddlemanReport,
            this.TaxationSystem.StartYear
        ]);
    };

    getRequiredFieldsForTaxPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForTaxPostings.date;
        }

        if (!this.model.Kontragent.KontragentId) {
            return requiredFieldForTaxPostings.payer;
        }

        if (!this.model?.MiddlemanContract?.DocumentBaseId) {
            return requiredFieldForTaxPostings.middlemanContract;
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
        if (this.documentsSumExceptMiddlemanReport === round(toFloat(this.model.Sum))) {
            return notTaxableMessages.simple;
        }

        const operationData = {
            taxationSystem: this.TaxationSystem,
            operation: this.modelForSave()
        };

        return notTaxableReasonGetter.get(operationData) || this.getRequiredFieldsForTaxPostingMsg();
    };

    getFieldsForAccountingPostings = () => {
        return [
            this.model.Sum,
            this.model.Date,
            this.model.Kontragent.KontragentId,
            this.model.MiddlemanContract,
            this.model.SettlementAccountId,
            this.model.ProvideInAccounting
        ];
    };

    getRequiredFieldsForAccountingPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForAccountingPostings.date;
        }

        if (!this.model.Kontragent.KontragentId) {
            return requiredFieldForAccountingPostings.payer;
        }

        if (!this.model?.MiddlemanContract?.DocumentBaseId) {
            return requiredFieldForTaxPostings.middlemanContract;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForAccountingPostings.sum;
        }

        return null;
    };

    getAccountingPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForAccountingPostingMsg();
    };
}

export default MediationFeeStore;
