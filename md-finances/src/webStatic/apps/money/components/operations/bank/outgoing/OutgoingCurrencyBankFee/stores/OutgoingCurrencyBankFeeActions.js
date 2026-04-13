import { action, override } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import taxationSystemService from '@moedelo/frontend-common-v2/apps/requisites/services/taxationSystemService';
import validationRules from '../validationRules';
import validator from '../../../../validation/validator';
import OutgoingCurrencyBankFeeComputed from './OutgoingCurrencyBankFeeComputed';
import { getCurrencyRate } from '../../../../../../../../services/newMoney/currencyService';
import { round } from '../../../../../../../../helpers/numberConverter';
import taxPostingsValidator from '../../../../validation/taxPostingsValidator';

class OutgoingCurrencyBankFeeActions extends OutgoingCurrencyBankFeeComputed {
    @override async setDate({ value }) {
        if (this.model.Date !== value) {
            this.model.Date = value;
            this.validateField(`Date`);
            const isValidDate = dateHelper(this.initialDate).isSameOrBefore(dateHelper(this.model.Date));
            this.isShowApprove = isValidDate && this.isOutsourceUser;
        }

        if (!this.validationState.Date) {
            this.setTaxationSystem(await taxationSystemService.getTaxSystem(this.model.Date));
            this.updateCurrencyRate();
        }
    }

    @action setSum = ({ value }) => {
        const sum = toFloat(value);

        if (sum === toFloat(this.model.Sum)) {
            return;
        }

        this.model.Sum = sum;

        this.validateField(`Sum`);
        this.calculateTotalSum();
    };

    @action updateCurrencyRate = async () => {
        this.model.CentralBankRate = await getCurrencyRate({
            date: this.model.Date,
            currencyCode: this.currencyCode
        });
        this.calculateTotalSum();
    };

    @action calculateTotalSum = () => {
        this.model.TotalSum = round(this.model.Sum * this.model.CentralBankRate);
    };

    @action setDescription = value => {
        if (this.model.Description !== value) {
            this.model.Description = value;
            this.validateField(`Description`);
        }
    };

    @action validateField = validationField => {
        const rules = validationRules[validationField];

        if (!rules) {
            return;
        }

        const requisites = this.Requisites;
        const { model } = this;

        this.validationState[validationField] = validator({
            model, rules, requisites
        });
    };

    @action validateTaxPostingsList = () => {
        this.model.TaxPostings.Postings = taxPostingsValidator.getValidatedList(this.model.TaxPostings.Postings, { isOsno: this.isOsno, isIp: this.isIp });
    };
}

export default OutgoingCurrencyBankFeeActions;
