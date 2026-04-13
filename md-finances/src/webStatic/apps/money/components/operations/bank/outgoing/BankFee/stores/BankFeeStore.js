import { observable, reaction, makeObservable } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Actions from './Actions';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import defaultModel from './Model';
import validationRules from './../validationRules';
import OrderType from '../../../../../../../../enums/OrderTypeEnum';
import notTaxableReasonGetter from './../notTaxableReasonGetter';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import {
    osnoTaxPostingsForServerNewBackend,
    usnTaxPostingsForServerNewBackend
} from '../../../../../../../../mappers/taxPostingsMapper';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import TaxPostingTransferType from '../../../../../../../../enums/newMoney/TaxPostingTransferTypeEnum';
import TaxPostingTransferKind from '../../../../../../../../enums/newMoney/TaxPostingTransferKindEnum';
import TaxPostingNormalizedCostType from '../../../../../../../../enums/newMoney/TaxPostingNormalizedCostTypeEnum';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';

class BankFeeStore extends Actions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Description: ``,
        TaxationSystemType: ``,
        Patent: ``
    };

    @observable model = { ...defaultModel };

    availableTaxPostingDirection = AvailableTaxPostingDirection.Outgoing;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        this.model.TaxPostingsMode = options.operation.TaxPostingsInManualMode
            ? ProvidePostingType.ByHand
            : ProvidePostingType.Auto;

        this.model.PostingsAndTaxMode = this.model.TaxPostingsMode;

        this.setActivePatents(options.activePatents || []);

        this.initTaxPostings();
        this.initAccountingPostings();
        this.initReactions();
    }

    initReactions = () => {
        reaction(() => this.model.Date, async Date => {
            if (!this.validationState.Date) {
                this.setTaxationSystem(await taxationSystemService.getTaxSystem(Date));
                this.updateActivePatents();
            }
        });

        reaction(this.getFieldsForTaxPostings, this.loadTaxPostings);
        reaction(this.getFieldsForAccountingPostings, this.loadAccountingPostings);
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
    }

    /* override */
    modelForSave = () => {
        const { model, TaxationSystem } = this;
        const operationType = MoneyOperationTypeResources.BankFee;
        let postingsAndTaxMode = model.TaxPostingsMode;

        if (!model.ProvideInAccounting) {
            postingsAndTaxMode = ProvidePostingType.ByHand;
        }

        const customPostings = postingsAndTaxMode === ProvidePostingType.ByHand ? model.TaxPostings.Postings : [];
        const TaxPostings = this.getTaxPostingForSave(customPostings, postingsAndTaxMode, TaxationSystem);

        return {
            Id: model.Id,
            DocumentBaseId: model.DocumentBaseId,
            OrderType: OrderType.MemorialWarrant,
            OperationType: operationType.value,
            Direction: operationType.Direction,
            Number: model.Number,
            Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
            SettlementAccountId: model.SettlementAccountId,
            TaxationSystemType: model.TaxationSystemType,
            Sum: model.Sum,
            Description: model.Description,
            PaymentPriority: model.PaymentPriority,
            ProvideInAccounting: model.ProvideInAccounting,
            PostingsAndTaxMode: postingsAndTaxMode,
            TaxPostings,
            PatentId: this.patentIdForSave
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

    /* override */
    getTaxPostingsExplainingMsg = () => {
        const { TaxationSystemType } = this.model;

        return notTaxableReasonGetter.get({
            taxationSystem: this.TaxationSystem,
            TaxationSystemType
        }) || this.getRequiredFieldsForTaxPostingMsg();
    };

    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.Sum,
            this.model.TaxationSystemType,
            this.TaxationSystem.StartYear
        ]);
    };

    getRequiredFieldsForTaxPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForTaxPostings.date;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForTaxPostings.sum;
        }

        return null;
    };

    getTransferType = () => {
        return [
            TaxPostingTransferType.Direct,
            TaxPostingTransferType.Indirect,
            TaxPostingTransferType.NonOperating
        ];
    };

    getTransferKind = ({ Type }) => {
        switch (Type) {
            case TaxPostingTransferType.NonOperating:
                return [TaxPostingTransferKind.None];
            case TaxPostingTransferType.Direct:
                return [TaxPostingTransferKind.Material];
            case TaxPostingTransferType.Indirect:
                return [TaxPostingTransferKind.Material, TaxPostingTransferKind.Salary, TaxPostingTransferKind.Amortization, TaxPostingTransferKind.OtherOutgo];
            default:
                return [TaxPostingTransferKind.None];
        }
    };

    getNormalizedCostType = () => {
        return [...Object.values(TaxPostingNormalizedCostType)];
    };

    /* override */
    getFieldsForAccountingPostings = () => {
        return JSON.stringify([
            this.model.Sum,
            this.model.Date,
            this.model.SettlementAccountId,
            this.model.TaxationSystemType,
            this.model.ProvideInAccounting
        ]);
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
}

export default BankFeeStore;
