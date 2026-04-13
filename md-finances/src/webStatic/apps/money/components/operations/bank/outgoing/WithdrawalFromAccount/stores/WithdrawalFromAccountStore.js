import { makeObservable, observable, reaction } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import Actions from './Actions';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';
import defaultModel from './Model';
import validationRules from './../validationRules';
import requiredFieldForAccountingPostings from '../../../../../../../../resources/newMoney/requiredFieldForAccountingPostings';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';
import { getNumberFromDocumentName } from '../../../../../../../../helpers/newMoney/utils';
import { PKOAutocomplete } from '../../../../../../../../services/rkoService';
import { mapCashOrderToAutocomplete } from '../../../../../../../../mappers/mappers';
import notTaxableReasonGetter from '../notTaxableReasonGetter';
import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';

class WithdrawalFromAccountStore extends Actions {
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

        this._initModel(defaultModel, options.operation);

        this.onSave = options.onSave;

        this.model.TaxPostings.ExplainingMessage = notTaxableReasonGetter.get({ taxationSystem: this.TaxationSystem });
        !this.model.Description && this.setDescription(`Снятие с расчетного счета. НДС не облагается.`);

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
        const { model } = this;
        const operationType = MoneyOperationTypeResources.WithdrawalFromAccount;

        return {
            Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
            Number: model.Number,
            SettlementAccountId: model.SettlementAccountId,
            Sum: model.Sum,
            Description: model.Description,
            DocumentBaseId: model.DocumentBaseId,
            CashOrder: {
                DocumentBaseId: model?.CashOrder?.DocumentId || null
            },
            IsPaid: model.Status === DocumentStatusEnum.Payed,
            ProvideInAccounting: model.ProvideInAccounting,
            OperationType: operationType.value
        };
    };

    /* override */
    getTaxPostingsExplainingMsg = () => {
        return null;
    };

    getFieldsForTaxPostings = () => {
        return null;
    };

    getRequiredFieldsForTaxPostingMsg = () => {
        return null;
    };

    getCashOrderAutocomplete = async ({ query = `` }) => {
        if (!query.length && this.model.CashOrder.DocumentName) {
            this.setCashOrder({});
        }

        const numberFromQuery = getNumberFromDocumentName(query);
        const { List } = await PKOAutocomplete({ query: numberFromQuery });

        return mapCashOrderToAutocomplete(List, query);
    };

    /* override */
    getFieldsForAccountingPostings = () => {
        return JSON.stringify([
            this.model.Sum,
            this.model.Date,
            this.model.SettlementAccountId,
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

export default WithdrawalFromAccountStore;
