import {
    observable, reaction, makeObservable
} from 'mobx';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import CurrencyTransferToAccountActions from './CurrencyTransferToAccountActions';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import defaultModel from './CurrencyTransferToAccountModel';
import validationRules from './../validationRules';
import OrderType from '../../../../../../../../enums/OrderTypeEnum';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import requiredFieldForAccountingPostings
    from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';

class CurrencyTransferToAccountStore extends CurrencyTransferToAccountActions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Description: ``,
        ToSettlementAccountId: ``
    };

    @observable model = { ...defaultModel };

    /* override */
    messageNoAccountingObjects = ``;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        !this.model.Description && this.setDescription(`Перевод на другой счет. НДС не облагается.`);

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
            this.model.ToSettlementAccountId
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

    getFieldsForTaxPostings = () => {
        return [
            this.model.Sum
        ];
    };

    getRequiredFieldsForTaxPostingMsg = () => {
        if (!toFloat(this.model.Sum)) {
            return requiredFieldForTaxPostings.sum;
        }

        return null;
    };

    getTaxPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForTaxPostingMsg();
    };

    /* override */
    modelForSave = () => {
        const { model } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderOutgoingCurrencyTransferToAccount;

        return {
            DocumentBaseId: model.DocumentBaseId,
            OrderType: OrderType.PaymentOrder,
            OperationType: operationType.value,
            Direction: operationType.Direction,
            Number: model.Number,
            Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
            SettlementAccountId: model.SettlementAccountId,
            ToSettlementAccountId: model.ToSettlementAccountId,
            Sum: model.Sum,
            Description: model.Description,
            IsPaid: true,
            ProvideInAccounting: model.ProvideInAccounting
        };
    };
}

export default CurrencyTransferToAccountStore;
