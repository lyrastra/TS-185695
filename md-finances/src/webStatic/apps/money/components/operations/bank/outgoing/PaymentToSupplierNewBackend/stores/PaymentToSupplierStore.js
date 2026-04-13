import {
    makeObservable, observable, reaction, toJS
} from 'mobx';
import { toAmountString, toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import AccountingDocumentType from '@moedelo/frontend-enums/mdEnums/AccountingDocumentType';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import PaymentToSupplierActions from './PaymentToSupplierActions';
import { autocomplete } from '../../../../../../../../services/contractService';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import defaultModel from './PaymentToSupplierModel';
import validationRules from './../validationRules';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import { calculateNdsBySumAndType } from '../../../../../../../../helpers/newMoney/ndsCalculationHelper';
import { getSettlementAccounts } from '../../../../../../../../services/newMoney/contractorService';
import { getKontragentSettlements } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import { mapForAutocomplete } from '../../../../../../../../helpers/newMoney/contractHelper';
import linkedDocumentService from '../../../../../../../../services/newMoney/linkedDocumentService';
import DocumentStatuses from '../../../../../../../../enums/DocumentStatusEnum';
import { mapDocumentsToNewBackend } from '../../../../../../../../helpers/newMoney/linkedDocumentsHelper';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import { mapLinkedBillToBackend } from '../../../../../../../../mappers/linkedBills/linkedBillsMapper';
import NdsTypesEnum from '../../../../../../../../enums/newMoney/NdsTypesEnum';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import DocumentTypeEnum from '../../../../../../../../enums/DocumentTypeEnum';
import SyntheticAccountCodesEnum from '../../../../../../../../enums/SyntheticAccountCodesEnum';

class PaymentToSupplierStore extends PaymentToSupplierActions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Kontragent: ``,
        KontragentSettlementAccount: ``,
        KontragentInn: ``,
        KontragentKpp: ``,
        DocumentsSum: ``,
        DocumentsCount: ``,
        Description: ``,
        NdsSum: ``,
        ReserveSum: ``
    };
    @observable isKontragentAccountCodeDisabled = false;
    @observable model = { ...defaultModel };
    @observable reserve = { saved: true, opened: false };

    availableTaxPostingDirection = AvailableTaxPostingDirection.Outgoing;

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

        this.model.TaxPostingsMode = options.operation.TaxPostingsInManualMode
            ? ProvidePostingType.ByHand
            : ProvidePostingType.Auto;

        this.model.PostingsAndTaxMode = this.model.TaxPostingsMode;

        !this.model.Description && this.handleDescriptionMessage();
        this.setDocuments(this.model.Documents || []);
        this.handleReceiptStatementDocument();
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

        reaction(() => [this.model.Kontragent], this.loadKontragentRequisites);

        reaction(() => this.model.Date, async Date => {
            if (!this.validationState.Date) {
                this.setTaxationSystem(await taxationSystemService.getTaxSystem(Date));
            }
        });

        reaction(() => this.model.Documents, this.handleReceiptStatementDocument);

        reaction(this.getFieldsForTaxPostings, this.loadTaxPostings);

        reaction(this.getFieldsForAccountingPostings, this.loadAccountingPostings);
    };

    handleReceiptStatementDocument = () => {
        let value = this.model.IsMainContractor ? SyntheticAccountCodesEnum._60_02 : SyntheticAccountCodesEnum._76_05;
        let isDisabled = false;

        if (toJS(this.model.Documents).some(document => document.DocumentType === DocumentTypeEnum.ReceiptStatement)) {
            value = SyntheticAccountCodesEnum._76_05;
            isDisabled = true;
        }

        this.setKontragentAccountCode({ value });
        this.setKontragentAccountCodeState({ isDisabled });
    }

    /* tax postings */
    initTaxPostings() {
        if ((!this.isNew && !this.isCopy()) && !this.shouldRegenerateTaxPostings()) {
            this.model.TaxPostings.ExplainingMessage = this.getTaxPostingsExplainingMsg() || this.model.TaxPostings.ExplainingMessage;
            this.model.TaxPostingsMode = this.model.PostingsAndTaxMode;
            this.setTaxPostingList(this.model.TaxPostings);
        } else {
            this.loadTaxPostings();
        }
    }

    calculateNdsBySumAndType = ([Sum, NdsType, IncludeNds, NdsSum]) => {
        if (NdsSum || NdsSum === 0) {
            return;
        }

        if (IncludeNds) {
            const ndsSum = NdsType === null ? `` : calculateNdsBySumAndType(Sum, NdsType);

            this.setNdsSum({ value: ndsSum });
        }
    };

    handleDescriptionMessage = () => {
        if (!this.isNew) {
            return;
        }

        const { Sum, IncludeNds, NdsSum } = this.model;
        let msg = `Оплата контрагенту. НДС не облагается.`;

        if (Sum && (!IncludeNds || !NdsSum)) {
            msg = `Оплата контрагенту на сумму ${toAmountString(Sum)} руб. НДС не облагается.`;
        }

        if (Sum && IncludeNds && NdsSum) {
            msg = `Оплата контрагенту на сумму ${toAmountString(Sum)} руб. В т.ч. НДС ${toAmountString(NdsSum)} руб.`;
        }

        this.setDescription(msg);
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

    getContractAutocomplete = async ({ query = `` }) => {
        const response = await autocomplete({ query, kontragentId: this.model.Kontragent.KontragentId });

        return mapForAutocomplete(response, query);
    };

    getDocumentAutocomplete = data => {
        const documentTypes = [
            AccountingDocumentType.Waybill,
            AccountingDocumentType.Statement
        ];

        if (this.isOsno || this.isUsn) {
            documentTypes.push(AccountingDocumentType.Upd);
        }

        const isIpOsno = !this.Requisites.IsOoo && this.TaxationSystem.IsOsno;

        if (this.isUsn15 || isIpOsno) {
            documentTypes.push(AccountingDocumentType.InventoryCard);
        }

        documentTypes.push(AccountingDocumentType.ReceiptStatement);

        return linkedDocumentService.outgoingAutocomplete(data, documentTypes);
    };

    /* override */
    modelForSave = () => {
        const {
            model, documents, bills, TaxationSystem
        } = this;

        const operationType = MoneyOperationTypeResources.PaymentOrderPaymentToSupplier;

        const customPostings = model.TaxPostingsMode === ProvidePostingType.ByHand ? model.TaxPostings.Postings : [];
        const TaxPostings = this.getTaxPostingForSave(customPostings, model.TaxPostingsMode, TaxationSystem);

        return {
            Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
            DocumentBaseId: model.DocumentBaseId,
            IsPaid: model.Status === DocumentStatuses.Payed,
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
            ProvideInAccounting: model.ProvideInAccounting,
            Bills: toJS(bills).filter(doc => doc.DocumentBaseId > 0 && toFloat(doc.Sum) > 0).map(mapLinkedBillToBackend),
            Documents: mapDocumentsToNewBackend(documents),
            ReserveSum: model.ReserveSum,
            TaxPostings,
            TaxationSystemType: model.TaxationSystemType,
            PatentId: this.patentIdForSave,
            OperationType: operationType.value,
            Direction: operationType.Direction
        };
    };

    /* override */
    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.Sum,
            this.model.Kontragent.KontragentId,
            this.model.IncludeNds,
            this.model.NdsSum,
            this.model.Status,
            mapDocumentsToSave(this.model.Documents),
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

        if (this.model.Status === DocumentStatuses.NotPayed) {
            return requiredFieldForTaxPostings.notPaid;
        }

        if (this.model.IncludeNds && this.model.NdsType !== NdsTypesEnum.Nds0 && this.model.NdsType !== NdsTypesEnum.None && !this.model.NdsSum) {
            return requiredFieldForTaxPostings.ndsSum;
        }

        if (this.isOsno && this.isIp && !this.model.Documents.length) {
            return requiredFieldForTaxPostings.documents;
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
            this.model.IsMainContractor,
            this.model.Contract,
            this.model.Kontragent.KontragentId,
            this.model.SettlementAccountId,
            this.model.Status,
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
            return requiredFieldForAccountingPostings.outgoingPayer;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForAccountingPostings.sum;
        }

        if (this.model.Status === DocumentStatuses.NotPayed) {
            return requiredFieldForAccountingPostings.notPaid;
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

export default PaymentToSupplierStore;
