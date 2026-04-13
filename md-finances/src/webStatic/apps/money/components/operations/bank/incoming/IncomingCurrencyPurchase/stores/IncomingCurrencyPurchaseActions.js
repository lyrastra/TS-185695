import { action, override } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import IncomingCurrencyPurchaseComputed from './IncomingCurrencyPurchaseComputed';
import validator from '../../../../validation/validator';
import validationRules from '../validationRules';

class IncomingCurrencyPurchaseActions extends IncomingCurrencyPurchaseComputed {
    @action setNumber = ({ value }) => {
        if (this.model.Number !== value) {
            this.model.Number = value;
            this.validateField(`Number`);
        }
    };

    @override async setDate({ value }) {
        if (this.model.Date !== value) {
            this.model.Date = value;
            this.validateField(`Date`);
            const isValidDate = dateHelper(this.initialDate).isSameOrBefore(dateHelper(this.model.Date));
            this.isShowApprove = isValidDate && this.isOutsourceUser;
        }

        if (!this.validationState.Date) {
            this.setTaxationSystem(await taxationSystemService.getTaxSystem(this.model.Date));
        }
    }

    @action setFromSettlementAccount = ({ value }) => {
        if (value !== this.model.FromSettlementAccountId) {
            this.model.FromSettlementAccountId = value;
            this.validateField(`FromSettlementAccountId`);
        }
    };

    @action setSum = ({ value }) => {
        const sum = toFloat(value);

        if (sum === this.model.Sum) {
            return;
        }

        this.model.Sum = sum;
        this.validateField(`Sum`);
    };

    @action setDescription = value => {
        if (this.model.Description !== value) {
            this.model.Description = value;
            this.validateField(`Description`);
        }
    };

    @action validateField(validationField) {
        const rules = validationRules[validationField];

        if (!rules) {
            return;
        }

        const requisites = this.Requisites;
        const { model } = this;

        this.validationState[validationField] = validator({
            model, rules, requisites
        });
    }
}

export default IncomingCurrencyPurchaseActions;
