import validate from 'validate.js';

class Validator {
    errors = {};

    constructor(model, constraints) {
        this.model = model;
        this.constraints = constraints;
        this.forceValidation = false;
    }

    validate(attrs) {
        const dataForValidation = this.model.toJSON();
        let changedAttrs;

        if (attrs) {
            changedAttrs = attrs;
        } else {
            changedAttrs = Object.keys(this.constraints);
        }

        const errors = validate.validate(dataForValidation, this.constraints, { fullMessages: false }) || {};

        const validAttrs = {};

        Object.keys(this.constraints).forEach(key => {
            if (Object.hasOwnProperty.call(errors, key)) {
                return;
            }

            validAttrs[key] = undefined;
        });

        const invalidAttrs = {};

        Object.keys(errors).forEach(attr => {
            if (changedAttrs.some(key => key === attr)) {
                invalidAttrs[attr] = errors[attr];
            }
        });

        Object.assign(this.errors, validAttrs, invalidAttrs);

        return Object.values(this.errors).every(m => !m?.length);
    }

    getError(attr) {
        return this.errors[attr]?.[0];
    }

    isValid(attr) {
        if (!attr) {
            return this.validate();
        }

        return !this.getError(attr);
    }
}

export default Validator;
