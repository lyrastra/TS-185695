import { observable, reaction, makeObservable } from 'mobx';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import IncomingCurrencyFromAnotherAccountActions from './IncomingCurrencyFromAnotherAccountActions';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import defaultModel from './IncomingCurrencyFromAnotherAccountModel';
import validationRules from './../validationRules';
import OrderType from '../../../../../../../../enums/OrderTypeEnum';
import notTaxableReasonGetter from '../notTaxableReasonGetter';
import requiredFieldForAccountingPostings
    from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';

class IncomingCurrencyFromAnotherAccountStore extends IncomingCurrencyFromAnotherAccountActions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Description: ``,
        SettlementAccountId: ``,
        FromSettlementAccountId: ``
    };

    @observable model = { ...defaultModel };

    @observable accountingPostingsLoading = false;

    @observable taxPostingsLoading = false;

    /* override */
    messageNoAccountingObjects = ``;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        this.moneySourceStore = options.moneySourceStore;
        this.model.TaxPostings.ExplainingMessage = notTaxableReasonGetter.get(this.TaxationSystem);
        this.initTaxPostings();
        this.initAccountingPostings();
        this.initReactions();
    }

    initReactions = () => {
        reaction(this.getFieldsForTaxPostings, this.loadTaxPostings);

        reaction(this.getFieldsForAccountingPostings, this.loadAccountingPostings);
    };

    isValid = () => {
        return !Object.keys(this.validationState).find(key => {
            return this.validationState[key] !== ``;
        });
    };

    validateModel() {
        Object.keys(validationRules).forEach(fieldName => {
            this.validateField(fieldName, this.model[fieldName]);
        });
    }

    getFieldsForAccountingPostings = () => {
        return [
            this.model.Date,
            this.model.ProvideInAccounting,
            this.model.Sum,
            this.model.SettlementAccountId,
            this.model.FromSettlementAccountId
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

        if (!this.model.FromSettlementAccountId) {
            return requiredFieldForAccountingPostings.fromSettlementAccountId;
        }

        return null;
    };

    /* override */
    getAccountingPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForAccountingPostingMsg();
    };

    getFieldsForTaxPostings = () => {
        return [
            this.model.Sum,
            this.model.SettlementAccountId,
            this.model.FromSettlementAccountId
        ];
    };

    getRequiredFieldsForTaxPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForTaxPostings.date;
        }

        if (!this.model.FromSettlementAccountId) {
            return requiredFieldForTaxPostings.fromSettlementAccountId;
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
    modelForSave() {
        const { model } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderIncomingCurrencyFromAnotherAccount;

        return {
            DocumentBaseId: model.DocumentBaseId,
            OrderType: OrderType.PaymentOrder,
            OperationType: operationType.value,
            Direction: operationType.Direction,
            Number: model.Number,
            Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
            SettlementAccountId: model.SettlementAccountId,
            FromSettlementAccountId: model.FromSettlementAccountId,
            Sum: model.Sum,
            Description: model.Description,
            ProvideInAccounting: model.ProvideInAccounting
        };
    }
}

export default IncomingCurrencyFromAnotherAccountStore;
