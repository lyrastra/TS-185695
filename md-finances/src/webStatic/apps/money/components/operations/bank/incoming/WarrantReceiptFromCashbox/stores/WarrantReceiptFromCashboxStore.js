import { observable, reaction, makeObservable } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Actions from './Actions';
import { rkoAutocomplete } from '../../../../../../../../services/rkoService';
import { mapCashOrderToAutocomplete } from '../../../../../../../../mappers/mappers';
import { getNumberFromDocumentName } from '../../../../../../../../helpers/newMoney/utils';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import defaultModel from './Model';
import MoneyOperationService from '../../../../../../../../services/newMoney/moneyOperationService';
import validationRules from './../validationRules';
import OrderType from '../../../../../../../../enums/OrderTypeEnum';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

class WarrantReceiptFromCashboxStore extends Actions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Description: ``
    };

    @observable model = { ...defaultModel };

    @observable accountingPostingsLoading = false;

    /* override */
    messageNoAccountingObjects = ``;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        this.model.TaxPostings.ExplainingMessage = notTaxableMessages.simple;
        this.initAccountingPostings();
        this.initReactions();
    }

    initReactions = () => {
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

    getCashOrderAutocomplete = async ({ query = `` }) => {
        if (!query.length && this.model.CashOrder.DocumentName) {
            this.setCashOrder({});
        }

        const numberFromQuery = getNumberFromDocumentName(query);
        const { List } = await rkoAutocomplete({ query: numberFromQuery, baseId: this.model.BaseDocumentId });

        return mapCashOrderToAutocomplete(List, query);
    };

    /* override */
    modelForSave() {
        const { model } = this;
        const operationType = MoneyOperationTypeResources.MemorialWarrantReceiptFromCash;
        let postingsAndTaxMode = model.TaxPostingsMode;

        if (!model.ProvideInAccounting) {
            postingsAndTaxMode = ProvidePostingType.ByHand;
        }

        return {
            Id: model.Id,
            BaseDocumentId: model.BaseDocumentId,
            DocumentBaseId: model.DocumentBaseId,
            OrderType: OrderType.PaymentOrder,
            OperationType: operationType.value,
            Direction: operationType.Direction,
            Number: model.Number,
            Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
            CashOrder: {
                DocumentBaseId: model.CashOrder.DocumentId ? model.CashOrder.DocumentId : null,
                ...model.CashOrder
            },
            SettlementAccountId: model.SettlementAccountId,
            Sum: model.Sum,
            Description: model.Description,
            PaymentPriority: model.PaymentPriority,
            ProvideInAccounting: model.ProvideInAccounting,
            PostingsAndTaxMode: postingsAndTaxMode
        };
    }

    /* override */
    get isNotTaxable() {
        return true;
    }

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

    remove = () => {
        return MoneyOperationService.deleteOperation([this.model.BaseDocumentId]);
    };
}

export default WarrantReceiptFromCashboxStore;
