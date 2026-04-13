import { makeObservable, observable, reaction } from 'mobx';
import { toAmountString, toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import Actions from './Actions';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import defaultModel from './Model';
import validationRules from './../validationRules';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import { mapAdvanceStatementsToBackendModel } from '../../../../../../../../mappers/advanceStatementMapper';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import AvailableTaxPostingDirection from '../../../../../../../../enums/newMoney/AvailableTaxPostingDirectionEnum';
import DocumentStatuses from '../../../../../../../../enums/DocumentStatusEnum';
import requiredFieldForTaxPostings from '../../../../../../../../resources/newMoney/requiredFieldForTaxPostings';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';

class PaymentToAccountablePersonStore extends Actions {
    @observable validationState = {
        Number: ``,
        Date: ``,
        Sum: ``,
        Worker: ``,
        Description: ``
    };

    @observable model = { ...defaultModel };

    availableTaxPostingDirection = AvailableTaxPostingDirection.Outgoing;

    constructor(options) {
        super(options);
        makeObservable(this);

        this._initModel(defaultModel, options.operation);

        !this.model.Description && this.handleDescriptionMessage();

        this.model.TaxPostingsMode = options.operation.TaxPostingsInManualMode
            ? ProvidePostingType.ByHand
            : ProvidePostingType.Auto;

        this.model.PostingsAndTaxMode = this.model.TaxPostingsMode;
        this.model.WithIpAsWorker = !options.requisites.IsOoo;

        this.initAccountingPostings();
        this.initTaxPostings();
        this.initReactions();
    }

    /* override */
    initTaxPostings() {
        if (!this.isNew && Object.keys(this.model.TaxPostings).length) {
            this.model.TaxPostings.ExplainingMessage = this.getTaxPostingsExplainingMsg() || this.model.TaxPostings.ExplainingMessage;
            this.setTaxPostingList(this.model.TaxPostings);
        } else {
            this.loadTaxPostings();
        }
    }

    initReactions = () => {
        reaction(() => [this.model.Sum, this.model.SalaryWorkerId], this.handleDescriptionMessage);

        reaction(() => this.model.Date, async Date => {
            if (!this.validationState.Date) {
                this.setTaxationSystem(await taxationSystemService.getTaxSystem(Date));
            }
        });

        reaction(this.getFieldsForTaxPostings, this.loadTaxPostings);
        reaction(this.getFieldsForAccountingPostings, this.loadAccountingPostings);
    };

    handleDescriptionMessage = () => {
        if (!this.isNew) {
            return;
        }

        const { Sum, WorkerName } = this.model;
        let sumMsg = ``;
        let workerMsg = ``;

        if (Sum) {
            sumMsg = ` на сумму ${toAmountString(Sum)} руб`;
        }

        if (WorkerName) {
            workerMsg = ` ${WorkerName}`;
        }

        const msg = `Выплата подотчетному лицу${workerMsg}${sumMsg}. НДС не облагается.`;

        this.setDescription(msg);
    };

    /* override */
    getFieldsForTaxPostings = () => {
        return JSON.stringify([
            this.model.Date,
            this.model.Sum,
            this.model.SalaryWorkerId,
            this.model.Status
        ]);
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
        const operationType = MoneyOperationTypeResources.PaymentToAccountablePerson;

        const customPostings = model.TaxPostingsMode === ProvidePostingType.ByHand ? model.TaxPostings.Postings : [];
        const TaxPostings = this.getTaxPostingForSave(customPostings, model.TaxPostingsMode, TaxationSystem);

        return {
            Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
            Number: model.Number,
            SettlementAccountId: model.SettlementAccountId,
            Employee: {
                Id: model.SalaryWorkerId,
                Name: model.WorkerName
            },
            Sum: model.Sum,
            Description: model.Description,
            ProvideInAccounting: model.ProvideInAccounting,
            AdvanceStatements: mapAdvanceStatementsToBackendModel(model.AdvanceStatements),
            TaxPostings,
            IsPaid: model.Status === DocumentStatuses.Payed,
            DocumentBaseId: model.DocumentBaseId,
            OperationType: operationType.value,
            Direction: operationType.Direction
        };
    };

    /* override */
    getRequiredFieldsForTaxPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForTaxPostings.date;
        }

        if (!this.model.SalaryWorkerId) {
            return requiredFieldForTaxPostings.outgoingPayer;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForTaxPostings.sum;
        }

        if (this.model.Status === DocumentStatuses.NotPayed) {
            return requiredFieldForTaxPostings.notPaid;
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
            this.model.Date,
            this.model.SettlementAccountId,
            this.model.Status,
            this.model.SalaryWorkerId,
            this.model.ProvideInAccounting
        ]);
    };

    /* override */
    getRequiredFieldsForAccountingPostingMsg = () => {
        if (!this.model.Date || this.validationState.Date) {
            return requiredFieldForAccountingPostings.date;
        }

        if (!this.model.SalaryWorkerId) {
            return requiredFieldForAccountingPostings.outgoingPayer;
        }

        if (!toFloat(this.model.Sum)) {
            return requiredFieldForAccountingPostings.sum;
        }

        if (this.model.Status === DocumentStatuses.NotPayed) {
            return requiredFieldForAccountingPostings.notPaid;
        }

        return null;
    };

    /* override */
    getAccountingPostingsExplainingMsg = () => {
        return this.getRequiredFieldsForAccountingPostingMsg();
    };
}

export default PaymentToAccountablePersonStore;
