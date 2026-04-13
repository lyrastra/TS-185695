import { action } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import Computed from './Computed';
import validationRules from './../validationRules';
import validator from './../../../../validation/validator';
import { mapKontragentToModel } from '../../../../../../../../helpers/newMoney/kontragentHelper';

class Actions extends Computed {
    @action setNumber = ({ value }) => {
        if (this.model.Number !== value) {
            this.model.Number = value;
            this.validateField(`Number`);
        }
    }

    @action setSum = ({ value }) => {
        const sum = toFloat(value);

        if (sum === toFloat(this.model.Sum)) {
            return;
        }

        this.model.Sum = sum;

        this.validateField(`Sum`);
    }

    @action setLongTermLoan = ({ checked }) => {
        this.model.IsLongTermLoan = checked;
    }

    @action setContractorName = ({ value }) => {
        if (!value) {
            this.setContractor({});

            return;
        }

        if (value !== this.model.Kontragent.KontragentName) {
            this.model.Kontragent.KontragentName = value;
        }
    }

    @action setContractor = async ({ original }) => {
        const previous = this.model.Kontragent;
        this.model.Kontragent = mapKontragentToModel(original);

        if (this.model.Kontragent.KontragentId && (previous.KontragentId !== this.model.Kontragent.KontragentId)) {
            await this.hanldeKontragentRequisites();
        }

        if (this.validationState.Kontragent) {
            this.validateField(`Kontragent`);
        }
    }

    @action setDescription = value => {
        this.model.Description = value;
        this.validateField(`Description`);
    }

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

    @action setContractorSettlementAccount = ({ value }) => {
        this.model.Kontragent.KontragentSettlementAccount = value;
        this.validateField(`KontragentSettlementAccount`);
    }

    @action setContractorINN = ({ value }) => {
        this.model.Kontragent.KontragentINN = value;
        this.validateField(`KontragentInn`);
    }

    @action setContractorKPP = ({ value }) => {
        this.model.Kontragent.KontragentKPP = value;
        this.validateField(`KontragentKpp`);
    }
}

export default Actions;
