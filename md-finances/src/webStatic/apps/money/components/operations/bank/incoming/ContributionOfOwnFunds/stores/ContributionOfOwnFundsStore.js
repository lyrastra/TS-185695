import { observable, makeObservable } from 'mobx';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import ContributionOfOwnFundsActions from './Actions';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import defaultModel from './Model';
import validationRules from './../validationRules';
import OrderType from '../../../../../../../../enums/OrderTypeEnum';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import notTaxableMessages from '../../../../../../../../resources/newMoney/notTaxableMessages';

class ContributionOfOwnFundsStore extends ContributionOfOwnFundsActions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Description: ``
    };

    @observable model = { ...defaultModel };

    @observable accountingPostingsLoading = false;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        this.model.Description = this.isNew ? defaultModel.Description : this.model.Description;
        this.model.TaxPostings.ExplainingMessage = notTaxableMessages.simple;
    }

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
        const operationType = MoneyOperationTypeResources.PaymentOrderIncomingContributionOfOwnFunds;
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
            SettlementAccountId: model.SettlementAccountId,
            Sum: model.Sum,
            Description: model.Description,
            PaymentPriority: model.PaymentPriority,
            ProvideInAccounting: model.ProvideInAccounting,
            PostingsAndTaxMode: postingsAndTaxMode
        };
    }
}

export default ContributionOfOwnFundsStore;
