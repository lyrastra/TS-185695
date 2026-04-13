import {
    makeObservable, observable, reaction, runInAction
} from 'mobx';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import OrderType from '../../../../../../../../enums/OrderTypeEnum';
import OutgoingCurrencyPurchaseActions from './OutgoingCurrencyPurchaseActions';
import validationRules from '../validationRules';
import defaultModel from './Model';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';

class OutgoingCurrencyPurchaseStore extends OutgoingCurrencyPurchaseActions {
    @observable validationState = {
        ToSettlementAccountId: ``,
        Number: ``,
        Date: ``,
        Sum: ``,
        ExchangeRate: ``,
        Description: ``
    };

    @observable model = { ...defaultModel };

    needAllSumValidation = false;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        this.model.TaxPostingsMode = options.operation.TaxPostingsInManualMode
            ? ProvidePostingType.ByHand
            : ProvidePostingType.Auto;
        // родительский store ожидает поле PostingsAndTaxMode
        this.model.PostingsAndTaxMode = this.model.TaxPostingsMode;

        if (this.model.ToSettlementAccountId) {
            this.updateCurrencyRate();
        } else {
            this.isNew && this.setToSettlementAccount({ value: this.getDefaultToSettlementAccountId() });
            this.validateField(`ToSettlementAccountId`);
        }

        if (!this.model.DocumentBaseId) {
            this.model.ExchangeRate = defaultModel.ExchangeRate;
            this.model.ExchangeRateDiff = defaultModel.ExchangeRateDiff;
            this.model.TotalSum = defaultModel.TotalSum;
            this.model.Description = defaultModel.Description;
            this.model.TaxPostings = defaultModel.TaxPostings;
        }

        this.initTaxPostings();
        this.initAccountingPostings();

        reaction(this.getFieldsForTaxPostings, this.loadTaxPostings);
        reaction(this.getFieldsForAccountingPostings, this.loadAccountingPostings);
        reaction(() => this.model.SettlementAccountId, this.resetToSettlementAccountId);
    }

    resetToSettlementAccountId = () => {
        runInAction(() => {
            this.setToSettlementAccount({ value: this.getDefaultToSettlementAccountId() });
        });
    };

    getDefaultToSettlementAccountId = () => {
        const possibleValues = this.toSettlementAccounts;

        if (possibleValues && possibleValues.length >= 1) {
            return possibleValues[0].value;
        }

        return null;
    }

    isValid = () => {
        const isValidTaxPostings = taxPostingsValidator.isValid(
            this.model.TaxPostings.Postings,
            {
                isOsno: this.isOsno,
                needAllSumValidation: this.needAllSumValidation,
                ExchangeRateDiff: this.model.ExchangeRateDiff
            }
        );

        const isValidOperation = Object.keys(this.validationState)
            .every(key => this.validationState[key] === ``);

        return isValidTaxPostings && isValidOperation;
    };

    validateModel() {
        Object.keys(validationRules).forEach(fieldName => {
            this.validateField(fieldName, this.model[fieldName]);
        });
    }

    /* override */
    modelForSave() {
        const { model, TaxationSystem } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderOutgoingCurrencyPurchase;
        const postingsAndTaxMode = model.TaxPostingsMode;

        const customPostings = postingsAndTaxMode === ProvidePostingType.ByHand ? model.TaxPostings.Postings : [];
        const TaxPostings = this.getTaxPostingForSave(customPostings, postingsAndTaxMode, TaxationSystem);

        const result = {
            Id: model.Id,
            BaseDocumentId: model.BaseDocumentId,
            DocumentBaseId: model.DocumentBaseId,
            OrderType: OrderType.PaymentOrder,
            OperationType: operationType.value,
            Direction: operationType.Direction,
            Number: model.Number,
            Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
            SettlementAccountId: model.SettlementAccountId,
            ToSettlementAccountId: model.ToSettlementAccountId,
            Sum: model.Sum,
            TotalSum: model.TotalSum,
            ExchangeRate: model.ExchangeRate,
            ExchangeRateDiff: model.ExchangeRateDiff,
            Description: model.Description,
            PaymentPriority: model.PaymentPriority,
            ProvideInAccounting: model.ProvideInAccounting,
            PostingsAndTaxMode: postingsAndTaxMode,
            IsPaid: true,
            TaxPostings
        };

        return result;
    }

    /* override */
    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Number,
            this.model.Sum,
            this.model.ExchangeRate,
            this.model.ExchangeRateDiff,
            this.model.Status,
            this.model.SettlementAccountId,
            this.model.ToSettlementAccountId
        ]);
    }

    /* override */
    getRequiredFieldsForTaxPostingMsg = () => {
        if (!this.model.ToSettlementAccountId) {
            return requiredFieldForTaxPostings.toSettlementAccountId;
        }

        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForTaxPostings.date;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForTaxPostings.sum;
        }

        if (!toFloat(this.model.ExchangeRate)) {
            return requiredFieldForTaxPostings.exchangeRate;
        }

        return null;
    }

    /* override */
    getTaxPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForTaxPostingMsg();
    }

    /* override */
    getFieldsForAccountingPostings = () => {
        return JSON.stringify([
            this.model.Sum,
            this.model.Date,
            this.model.ExchangeRate,
            this.model.ExchangeRateDiff,
            this.model.SettlementAccountId,
            this.model.TaxationSystemType,
            this.model.ProvideInAccounting,
            this.model.ToSettlementAccountId
        ]);
    };

    /* override */
    getRequiredFieldsForAccountingPostingMsg = () => {
        if (!this.model.ToSettlementAccountId) {
            return requiredFieldForAccountingPostings.toSettlementAccountId;
        }

        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForAccountingPostings.date;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForAccountingPostings.sum;
        }

        if (!toFloat(this.model.ExchangeRate)) {
            return requiredFieldForAccountingPostings.exchangeRate;
        }

        return null;
    };

    /* override */
    getAccountingPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForAccountingPostingMsg();
    };
}

export default OutgoingCurrencyPurchaseStore;
