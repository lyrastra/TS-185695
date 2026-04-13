import { action } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import IncomingFromPurseComputed from './Computed';
import validationRules from './../validationRules';
import validator from './../../../../validation/validator';

class IncomingFromPurseActions extends IncomingFromPurseComputed {
    @action setNumber = ({ value }) => {
        if (this.model.Number !== value) {
            this.model.Number = value;
            this.validateField(`Number`);
        }
    };

    @action setSum = ({ value }) => {
        const sum = toFloat(value);

        if (sum === toFloat(this.model.Sum)) {
            return;
        }

        this.model.Sum = sum;

        this.validateField(`Sum`);
    }

    @action setDescription = value => {
        this.model.Description = value;
        this.validateField(`Description`);
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

export default IncomingFromPurseActions;
