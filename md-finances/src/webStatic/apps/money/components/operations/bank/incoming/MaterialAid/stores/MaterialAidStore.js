import { observable, reaction, makeObservable } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import {
    osnoTaxPostingsForServerNewBackend,
    usnTaxPostingsForServerNewBackend
} from '../../../../../../../../mappers/taxPostingsMapper';
import Actions from './Actions';
import { getSettlementAccounts, getFoundersAutocomplete } from '../../../../../../../../services/newMoney/contractorService';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import defaultModel from './Model';
import validationRules from './../validationRules';
import OrderType from '../../../../../../../../enums/OrderTypeEnum';
import notTaxableReasonGetter from './../notTaxableReasonGetter';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import { getKontragentSettlements } from '../../../../../../../../helpers/newMoney/kontragentHelper';
import TaxPostingTransferType from '../../../../../../../../enums/newMoney/TaxPostingTransferTypeEnum';
import TaxPostingTransferKind from '../../../../../../../../enums/newMoney/TaxPostingTransferKindEnum';
import TaxPostingNormalizedCostType from '../../../../../../../../enums/newMoney/TaxPostingNormalizedCostTypeEnum';

class MaterialAidStore extends Actions {
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

    /* ui state */
    @observable isContractorRequisitesShown = false;
    /* ui state END */

    @observable model = { ...defaultModel };

    @observable kontragentSettlements = [];

    @observable taxPostingsLoading = false;

    @observable accountingPostingsLoading = false;

    @observable kontragentLoading = false;

    availableTaxPostingDirection = AvailableTaxPostingDirection.Incoming;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        this.initializeKontragentSettlements();

        this.initAccountingPostings();
        this.initTaxPostings();

        this.initReactions();
    }

    initReactions = () => {
        reaction(() => [this.validationState.KontragentSettlementAccount, this.validationState.KontragentInn, this.validationState.KontragentKpp], this.checkKontragentRequisitesVisibility);

        reaction(() => this.model.Kontragent.KontragentId, this.loadKontragentRequisites);

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
            isOsno: this.isOsno,
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

    modelForSave = () => {
        const { model, TaxationSystem } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderIncomingMaterialAid;

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
            // Contractor: model.Kontragent, /* todo: хз зачем это вообще, может отвалится старый бэк */
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
            KontragentId: model.Kontragent.KontragentId,
            Sum: model.Sum,
            Description: model.Description,
            PaymentPriority: model.PaymentPriority,
            ProvideInAccounting: model.ProvideInAccounting,
            PostingsAndTaxMode: model.TaxPostingsMode,
            TaxPostings
        };
    };

    getTaxPostingForSave(posting = [], postingsAndTaxMode, TaxationSystem) {
        if (postingsAndTaxMode !== ProvidePostingType.ByHand) {
            return { IsManual: false };
        }

        if (TaxationSystem.IsOsno) {
            return {
                IsManual: true,
                Postings: osnoTaxPostingsForServerNewBackend(posting, { Date: this.model.Date })
            };
        }

        return {
            IsManual: true,
            Postings: usnTaxPostingsForServerNewBackend(posting, { Date: this.model.Date })
        };
    }

    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.Sum,
            this.model.Kontragent.KontragentId,
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

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForTaxPostings.sum;
        }

        return null;
    };

    /* override */
    getTaxPostingsExplainingMsg = () => {
        return notTaxableReasonGetter.get({ taxationSystem: this.TaxationSystem }) || this.getRequiredFieldsForTaxPostingMsg();
    };

    /* override */
    getFieldsForAccountingPostings = () => {
        return JSON.stringify([
            this.model.Sum,
            this.model.Date,
            this.model.Kontragent.KontragentId,
            this.model.SettlementAccountId,
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

    getTransferType = () => {
        return [
            TaxPostingTransferType.NonOperating,
            TaxPostingTransferType.OperationIncome
        ];
    };

    getTransferKind = ({ Type }) => {
        if (Type === TaxPostingTransferType.NonOperating) {
            return [TaxPostingTransferKind.None];
        }

        return [
            TaxPostingTransferKind.Service,
            TaxPostingTransferKind.ProductSale,
            TaxPostingTransferKind.PropertyRight,
            TaxPostingTransferKind.OtherPropertySale
        ];
    };

    getNormalizedCostType = () => {
        return [TaxPostingNormalizedCostType.None];
    };

    getContractorAutocomplete = async ({ query = `` }) => {
        const { SettlementAccountId, Date } = this.model;

        const data = {
            count: 5,
            SettlementAccountId,
            Date,
            query
        };

        return getFoundersAutocomplete(data);
    };
}

export default MaterialAidStore;
