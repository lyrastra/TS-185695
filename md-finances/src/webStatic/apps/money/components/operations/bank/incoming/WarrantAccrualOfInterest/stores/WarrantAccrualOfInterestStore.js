import { observable, reaction, makeObservable } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import WarrantAccrualOfInterestActions from './Actions';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import defaultModel from './Model';
import validationRules from './../validationRules';
import OrderType from '../../../../../../../../enums/OrderTypeEnum';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import notTaxableReasonGetter from '../notTaxableReasonGetter';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import TaxPostingTransferType from '../../../../../../../../enums/newMoney/TaxPostingTransferTypeEnum';
import TaxPostingTransferKind from '../../../../../../../../enums/newMoney/TaxPostingTransferKindEnum';
import TaxPostingNormalizedCostType from '../../../../../../../../enums/newMoney/TaxPostingNormalizedCostTypeEnum';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import { getTaxationSystemType, isValidTaxationSystem } from '../../../../../../../../mappers/taxationSystemMapper';

class WarrantAccrualOfInterestStore extends WarrantAccrualOfInterestActions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Description: ``,
        TaxPostings: ``
    };

    @observable model = { ...defaultModel };

    @observable taxPostingsLoading = false;

    @observable accountingPostingsLoading = false;

    availableTaxPostingDirection = AvailableTaxPostingDirection.Incoming;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        this.initAccountingPostings();
        this.initTaxPostings();
        this.initReactions();
        this.initTaxationSystem();
    }

    initTaxationSystem = () => {
        const currentTaxationSystem = getTaxationSystemType(this.TaxationSystem);

        if (this.model.TaxationSystemType && !isValidTaxationSystem(currentTaxationSystem, this.model.TaxationSystemType)) {
            this.setTaxationSystemType({ value: currentTaxationSystem });
        }
    };

    initReactions = () => {
        reaction(this.getFieldsForTaxPostings, this.loadTaxPostings);
        reaction(this.getFieldsForAccountingPostings, this.loadAccountingPostings);
        reaction(() => this.model.Date, async Date => {
            if (!this.validationState.Date) {
                this.setTaxationSystem(await taxationSystemService.getTaxSystem(Date));
            }
        });
    };

    isValid = () => {
        return !Object.keys(this.validationState).find(key => {
            return this.validationState[key] !== ``;
        }) && this.isValidTaxPostings();
    };

    isValidTaxPostings = () => {
        return taxPostingsValidator.isValid(this.model.TaxPostings.Postings, { Sum: this.sumOperation, isOsno: this.isOsno });
    };

    validateModel() {
        Object.keys(validationRules).forEach(fieldName => {
            this.validateField(fieldName, this.model[fieldName]);
        });
    }

    /* override */
    modelForSave() {
        const { model, TaxationSystem } = this;
        const operationType = MoneyOperationTypeResources.MemorialWarrantAccrualOfInterest;
        let postingsAndTaxMode = model.TaxPostingsMode;

        if (!model.ProvideInAccounting || this.model.TaxPostingsInManualMode) {
            postingsAndTaxMode = ProvidePostingType.ByHand;
        }

        const customPostings = postingsAndTaxMode === ProvidePostingType.ByHand ? model.TaxPostings.Postings : [];
        const TaxPostings = this.getTaxPostingForSave(customPostings, postingsAndTaxMode, TaxationSystem);

        return {
            Id: model.Id,
            Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
            DocumentBaseId: model.DocumentBaseId,
            OrderType: OrderType.MemorialWarrant,
            OperationType: operationType.value,
            Direction: operationType.Direction,
            Number: model.Number,
            SettlementAccountId: model.SettlementAccountId,
            Sum: model.Sum,
            Description: model.Description,
            PaymentPriority: model.PaymentPriority,
            ProvideInAccounting: model.ProvideInAccounting,
            PostingsAndTaxMode: postingsAndTaxMode,
            TaxationSystemType: model.TaxationSystemType,
            TaxPostings
        };
    }

    /* override */
    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.Sum,
            this.model.TaxationSystemType,
            this.model.Description,
            this.model.SettlementAccountId
        ]);
    };

    /* override */
    getTaxPostingsExplainingMsg = () => {
        return notTaxableReasonGetter.get(this.model.TaxationSystemType, { taxationSystem: this.TaxationSystem }) || this.getRequiredFieldsForTaxPostingMsg();
    };

    /* override */
    getRequiredFieldsForTaxPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForTaxPostings.date;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForTaxPostings.sum;
        }

        return null;
    };

    /* override */
    getFieldsForAccountingPostings = () => {
        return [
            this.model.Sum,
            this.model.Date,
            this.model.SettlementAccountId,
            this.model.ProvideInAccounting
        ];
    };

    /* override */
    getRequiredFieldsForAccountingPostingMsg = () => {
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
}

export default WarrantAccrualOfInterestStore;
