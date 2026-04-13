import { makeObservable, observable, reaction } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import validationRules from '../validationRules';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import defaultModel from './OutgoingCurrencyBankFeeModel';
import OutgoingCurrencyBankFeeActions from './OutgoingCurrencyBankFeeActions';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import requiredFieldForAccountingPostings
    from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';

class OutgoingCurrencyBankFeeStore extends OutgoingCurrencyBankFeeActions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Description: ``
    };

    @observable model = { ...defaultModel };

    needAllSumValidation = false;

    needAllTotalSumValidation = true;

    availableTaxPostingDirection = AvailableTaxPostingDirection.Outgoing;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        this.model.TaxPostingsMode = options.operation.TaxPostingsInManualMode
            ? ProvidePostingType.ByHand
            : ProvidePostingType.Auto;

        this.model.PostingsAndTaxMode = this.model.TaxPostingsMode;

        this.updateCurrencyRate();
        this.initTaxPostings();
        this.initAccountingPostings();
        this.initReactions();
    }

    initReactions = () => {
        reaction(() => this.model.SettlementAccountId, this.updateCurrencyRate);
        reaction(this.getFieldsForTaxPostings, this.loadTaxPostings);
        reaction(this.getFieldsForAccountingPostings, this.loadAccountingPostings);
    };

    isValid = () => {
        const options = {
            Sum: this.model.Sum,
            TotalSum: this.model.TotalSum,
            needAllSumValidation: this.needAllSumValidation,
            needAllTotalSumValidation: this.needAllTotalSumValidation
        };

        const taxPostingsIsValid = taxPostingsValidator.isValid(this.model.TaxPostings.Postings, options);
        const modelIsValid = !Object.keys(this.validationState).find(key => this.validationState[key] !== ``);

        return modelIsValid && (taxPostingsIsValid || this.isNotTaxable);
    };

    validateModel() {
        Object.keys(validationRules).forEach(fieldName => {
            this.validateField(fieldName, this.model[fieldName]);
        });

        this.validateTaxPostingsList();
    }

    modelForSave = () => {
        const { model, TaxationSystem } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderOutgoingCurrencyBankFee;
        let postingsAndTaxMode = model.TaxPostingsMode;

        if (!model.ProvideInAccounting) {
            postingsAndTaxMode = ProvidePostingType.ByHand;
        }

        const customPostings = postingsAndTaxMode === ProvidePostingType.ByHand ? model.TaxPostings.Postings : [];
        const TaxPostings = this.getTaxPostingForSave(customPostings, postingsAndTaxMode, TaxationSystem);

        return {
            DocumentBaseId: model.DocumentBaseId,
            OperationType: operationType.value,
            Direction: operationType.Direction,
            Number: model.Number,
            Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
            SettlementAccountId: model.SettlementAccountId,
            Sum: model.Sum,
            TotalSum: model.TotalSum,
            Description: model.Description,
            IsPaid: true,
            ProvideInAccounting: model.ProvideInAccounting,
            PostingsAndTaxMode: postingsAndTaxMode,
            TaxPostings
        };
    };

    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Sum,
            this.model.TotalSum,
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

    getTaxPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForTaxPostingMsg();
    };

    /* override */
    getFieldsForAccountingPostings = () => {
        return JSON.stringify([
            this.model.Sum,
            this.model.TotalSum,
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

export default OutgoingCurrencyBankFeeStore;
