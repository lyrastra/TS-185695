import { action } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import Computed from './Computed';
import validationRules from './../validationRules';
import validator from './../../../../validation/validator';

class Actions extends Computed {
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
    };

    @action setWorker = ({ value }) => {
        const { Id, Name } = value || {};

        if (Id !== this.model.SalaryWorkerId) {
            this.setAdvanceStatement({});
        }

        this.model.SalaryWorkerId = Id;
        this.model.WorkerName = Name;

        if (this.validationState.Worker) {
            this.validateField(`Worker`);
        }
    };

    @action setAdvanceStatement = ({ value }) => {
        const { DocumentBaseId, Name } = value || {};
        this.model.AdvanceStatement = { DocumentBaseId, Name };
    };

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

export default Actions;
