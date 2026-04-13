import { Model } from 'backbone';
import validateHelper from '@moedelo/md-frontendcore/mdCommon/helpers/validateHelper';

export default Model.extend({
    initialize() {
        this.constraints = this._getConstraints();
    },

    validate(attributes) {
        return validateHelper.validate(attributes, this.constraints);
    },

    getValidateErrorMessage(attrName) {
        return validateHelper.getFirstMessageResult(this.validationError, attrName);
    },

    _getConstraints() {
        const model = this;
        return {
            Sum: (val) => {
                let result;
                if (!val && model.workerId) {
                    result = {
                        presence: {
                            message: 'Укажите сумму'
                        },
                        numericality: {
                            greaterThan: 0,
                            notGreaterThan: 'Укажите сумму'
                        }
                    };
                }

                return result;
            }
        };
    }
});

