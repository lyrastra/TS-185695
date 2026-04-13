/* eslint-disable */
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

(function(utils, converter) {
    utils.DirectiveHelper = {
        textMoneyDirective(propertyName) {
            return {
                text() {
                    return converter.toAmountString(Converter.toFloat(this[propertyName]));
                }
            };
        },

        textMoneyDirectiveWithZero(propertyName) {
            return {
                text() {
                    const sum = Converter.toFloat(this[propertyName]);
                    if (sum < 0 || sum > 0) {
                        return converter.toAmountString(sum);
                    }
                    return ``;
                }
            };
        },

        inputDateDirective(propertyName) {
            return {
                value() {
                    const date = Converter.toDate(this[propertyName]);
                    return dateHelper(date).format(`DD.MM.YYYY`);
                }
            };
        },

        textDateDirective(propertyName) {
            return {
                text() {
                    const date = Converter.toDate(this[propertyName]);
                    return dateHelper(date).format(`DD.MM.YYYY`);
                }
            };
        }
    };
}(Common.Utils, Common.Utils.Converter));
