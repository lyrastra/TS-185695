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

class ReturnFromAccountablePersonStore extends Actions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Worker: ``,
        Description: ``
    };

    @observable model = { ...defaultModel };

    @observable accountingPostingsLoading = false;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        if (options.operation.Employee) {
            this.model.SalaryWorkerId = options.operation.Employee.Id || null;
            this.model.WorkerName = options.operation.Employee.Name || null;
        }

        this.model.TaxPostings.ExplainingMessage = this.getTaxPostingsExplainingMsg() || this.model.TaxPostings.ExplainingMessage;
        this.initAccountingPostings();

        this.initReactions();
    }

    initReactions = () => {
        reaction(() => this.model.Date, async Date => {
            if (!this.validationState.Date) {
                this.setTaxationSystem(await taxationSystemService.getTaxSystem(Date));
            }
        });
        reaction(() => this.TaxationSystem, () => {
            this.model.TaxPostings = { Postings: [], ExplainingMessage: this.getTaxPostingsExplainingMsg() };
        });
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

    modelForSave() {
        const { model } = this;
        const operationType = MoneyOperationTypeResources.PaymentOrderIncomingReturnFromAccountablePerson;
        let postingsAndTaxMode = model.PostingsAndTaxMode;

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
            SalaryWorkerId: model.SalaryWorkerId,
            WorkerName: model.WorkerName,
            Kontragent: {},
            Employee: {
                Id: model.SalaryWorkerId,
                Name: model.WorkerName
            },
            Sum: model.Sum,
            AdvanceStatements: model.AdvanceStatements, // todo: выпилить после перехода на новый бэк
            AdvanceStatement: { DocumentBaseId: model.AdvanceStatement?.DocumentBaseId },
            Description: model.Description,
            PaymentPriority: model.PaymentPriority,
            ProvideInAccounting: model.ProvideInAccounting,
            PostingsAndTaxMode: postingsAndTaxMode
        };
    }

    get isNotTaxable() {
        return true;
    }

    getTaxPostingsExplainingMsg = () => {
        return notTaxableReasonGetter.get({ taxationSystem: this.TaxationSystem });
    };

    getFieldsForAccountingPostings = () => {
        return [
            this.model.Sum,
            this.model.Date,
            this.model.SalaryWorkerId,
            this.model.SettlementAccountId,
            this.model.ProvideInAccounting
        ];
    };

    getRequiredFieldsForAccountingPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForAccountingPostings.date;
        }

        if (!this.model.SalaryWorkerId) {
            return requiredFieldForAccountingPostings.payer;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForAccountingPostings.sum;
        }

        return null;
    };

    getAccountingPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForAccountingPostingMsg();
    };
}

export default ReturnFromAccountablePersonStore;
