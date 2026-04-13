import {
    observable, reaction, runInAction, makeObservable
} from 'mobx';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import OrderType from '../../../../../../../../enums/OrderTypeEnum';
import IncomingCurrencySaleActions from './IncomingCurrencySaleActions';
import validationRules from '../validationRules';
import defaultModel from './Model';
import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';

class IncomingCurrencySaleStore extends IncomingCurrencySaleActions {
    @observable validationState = {
        FromSettlementAccountId: ``,
        Number: ``,
        Date: ``,
        Sum: ``,
        Description: ``
    };

    @observable model = { ...defaultModel };

    @observable taxPostingsLoading = false;

    @observable accountingPostingsLoading = false;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        this.model.Description = this.isNew ? defaultModel.Description : this.model.Description;
        this.model.TaxPostingsMode = options.operation.TaxPostingType;
        this.model.PostingsAndTaxMode = options.operation.TaxPostingType;

        this.initTaxPostings();
        this.initAccountingPostings();

        this.initReactions();

        this.validateField(`FromSettlementAccountId`);
    }

    initReactions = () => {
        reaction(this.getFieldsForTaxPostings, this.loadTaxPostings);
        reaction(this.getFieldsForAccountingPostings, this.loadAccountingPostings);
        reaction(() => this.model.SettlementAccountId, this.resetFromSettlementAccountId);
    };

    resetFromSettlementAccountId = () => {
        runInAction(() => {
            this.setFromSettlementAccount({ value: null });
        });
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

    /* override */
    modelForSave() {
        const { model } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderIncomingCurrencySale;

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
            FromSettlementAccountId: model.FromSettlementAccountId,
            Sum: model.Sum || 0,
            Description: model.Description,
            PaymentPriority: model.PaymentPriority,
            ProvideInAccounting: model.ProvideInAccounting,
            PostingsAndTaxMode: ProvidePostingType.Auto,
            IsPaid: model.Status === DocumentStatusEnum.Payed
        };
    }

    /* override */
    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Sum,
            this.model.FromSettlementAccountId
        ]);
    };

    /* override */
    getRequiredFieldsForTaxPostingMsg = () => {
        if (!this.model.FromSettlementAccountId) {
            return requiredFieldForTaxPostings.fromSettlementAccountId;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForTaxPostings.sum;
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
            this.model.SettlementAccountId,
            this.model.FromSettlementAccountId,
            this.model.TaxationSystemType,
            this.model.ProvideInAccounting
        ]);
    };

    /* override */
    getRequiredFieldsForAccountingPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForAccountingPostings.date;
        }

        if (!this.model.FromSettlementAccountId) {
            return requiredFieldForAccountingPostings.fromSettlementAccountId;
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

export default IncomingCurrencySaleStore;
