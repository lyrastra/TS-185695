import { makeObservable, observable, reaction } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import Actions from './Actions';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import defaultModel from './Model';
import validationRules from './../validationRules';
import OrderType from '../../../../../../../../enums/OrderTypeEnum';
import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';
import notTaxableReasonGetter from './../notTaxableReasonGetter';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';

class TransferToOtherAccountStore extends Actions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Description: ``
    };

    @observable model = { ...defaultModel };

    constructor(options) {
        super(options);
        makeObservable(this);

        !this.model.Description && this.setDescription(`Перевод на другой счет. НДС не облагается.`);
        this.model.TaxPostings.ExplainingMessage = this.getTaxPostingsExplainingMsg();
        this.initAccountingPostings();
        this.initReactions();
    }

    initReactions = () => {
        reaction(() => this.model.Date, async Date => {
            if (!this.validationState.Date) {
                this.setTaxationSystem(await taxationSystemService.getTaxSystem(Date));
            }
        });

        reaction(this.getFieldsForAccountingPostings, this.loadAccountingPostings);
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
    modelForSave = () => {
        const { model } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderOutgoingTransferToAccount;
        let postingsAndTaxMode = model.TaxPostingsMode;

        if (!model.ProvideInAccounting) {
            postingsAndTaxMode = ProvidePostingType.ByHand;
        }

        return {
            Id: model.Id,
            BaseDocumentId: model.BaseDocumentId,
            OrderType: OrderType.PaymentOrder,
            OperationType: operationType.value,
            Direction: operationType.Direction,
            Number: model.Number,
            Date: model.Date,
            SettlementAccountId: model.SettlementAccountId,
            TransferSettlementaccountId: model.TransferSettlementaccountId,
            Sum: model.Sum,
            Status: model.Status,
            Description: model.Description,
            PaymentPriority: model.PaymentPriority,
            ProvideInAccounting: model.ProvideInAccounting,
            PostingsAndTaxMode: postingsAndTaxMode
        };
    }

    /* override */
    getTaxPostingsExplainingMsg = () => {
        return notTaxableReasonGetter.get({ taxationSystem: this.TaxationSystem });
    };

    /* override */
    getFieldsForAccountingPostings = () => {
        return JSON.stringify([
            this.model.Sum,
            this.model.Date,
            this.model.SettlementAccountId,
            this.model.TransferSettlementaccountId,
            this.model.Status,
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

        if (this.model.Status === DocumentStatusEnum.NotPayed) {
            return requiredFieldForAccountingPostings.notPaid;
        }

        return null;
    };

    /* override */
    getAccountingPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForAccountingPostingMsg();
    }
}

export default TransferToOtherAccountStore;
