import { action, override } from 'mobx';
import { toFloat } from '@moedelo/frontend-core-v2/helpers/converter';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';
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

    @override setDate({ value }) {
        if (this.model.Date !== value) {
            this.model.Date = value;
            this.validateField(`Date`);

            const isValidDate = dateHelper(this.initialDate).isSameOrBefore(dateHelper(this.model.Date));
            this.isShowApprove = isValidDate && this.isOutsourceUser;

            this.setAcquiringCommissionDate({ value });
        }
    }

    @action setSum = ({ value }) => {
        const sum = toFloat(value) || 0;

        if (sum === toFloat(this.model.Sum)) {
            return;
        }

        this.model.Sum = sum;
        this.validateField(`Sum`);
    };

    @action setAcquiringCommission = ({ value }) => {
        const sum = toFloat(value) || 0;

        if (sum === toFloat(this.model.AcquiringCommission)) {
            return;
        }

        if (sum > 0 && this.isCommissionDateAfter2026 && this.model.TaxationSystemType !== TaxationSystemType.Patent) {
            this.setIncludeNds({ checked: true });
        } else {
            this.setIncludeNds({ checked: false });
        }

        this.model.AcquiringCommission = sum;
        this.validateField(`AcquiringCommission`);

        if (sum > 0 && this.validationState.Sum.length) {
            this.validationState.Sum = ``;
        }

        if (!this.model.AcquiringCommissionDate) {
            this.setAcquiringCommissionDate({ value: this.model.Date });
        }
    };

    @action setAcquiringCommissionDate = ({ value }) => {
        const isOldDateAfter2026 = dateHelper(this.model.AcquiringCommissionDate).isAfter(`31.12.2025`);
        
        this.model.AcquiringCommissionDate = value;

        if (this.isCommissionDateAfter2026 && !isOldDateAfter2026 && this.model.AcquiringCommission > 0 && this.model.TaxationSystemType !== TaxationSystemType.Patent) {
            this.setIncludeNds({ checked: true });
        }

        if (!this.isCommissionDateAfter2026) {
            this.setIncludeNds({ checked: false });
        }

        this.validateField(`AcquiringCommissionDate`);
    };

    @action setSaleDate = ({ value }) => {
        this.model.SaleDate = value;
        this.validateField(`SaleDate`);
    };

    @action setDescription = value => {
        this.model.Description = value;
        this.validateField(`Description`);
    };

    @action setIsMediation = ({ checked }) => {
        this.model.IsMediation = checked;
    }

    @action setIncludeNds = ({ checked }) => {
        if (checked) {
            if (this.isCommissionDateAfter2026) {
                this.setNdsType({ value: this.ndsTypes[0].value });
            }
        } else {
            this.setNdsType({ value: null });
            this.setNdsSum({ value: null });
        }
    
        this.model.IncludeNds = checked;
        this.validateField(`NdsSum`);
    };

    @action setNdsType = ({ value }) => {
        this.model.NdsType = value === ` ` ? null : value;
       
        this.validateField(`NdsSum`);
    };

    @action setNdsSum = ({ value }) => {
        this.model.NdsSum = toFloat(value) || ``;
        this.validateField(`NdsSum`);
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

    @action setTaxationSystemType = ({ value }) => {
        this.validationState.TaxationSystemType = ``;
        this.model.TaxationSystemType = value;

        if (value === TaxationSystemType.Patent) {
            this.setIncludeNds({ checked: false });
        } else if (this.isCommissionDateAfter2026 && !!this.model.AcquiringCommission) {
            this.setIncludeNds({ checked: true });
        }
    };
}

export default Actions;
