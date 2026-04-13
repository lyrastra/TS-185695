import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
/* eslint-disable */
(function(validation) {

    'use strict';

    _.extend(validation.validators, {
        number: function(value, attr, customValue, model) {
            if (Converter.toFloat(value) === false) {
                return 'Значение должно быть числом';
            }
        },

        integer: function(value, attr, customValue, model) {
            if (Converter.toInteger(value) === false) {
                return 'Значение должно быть целым числом';
            }
        },

        date: function(value, attr, use, model) {
            var useValidation = _.isFunction(use) ? use.call(model, value, attr) : use;
            if (!useValidation) {
                return;
            }

            var date;
            if ($.trim(value).length == 0) {
                return;
            }

            if (!dateHelper(value, 'DD.MM.YYYY', true).isValid()) {
                return 'Некорректная дата';
            }
        },

        month: function(value, attr, customValue, model) {
            var date;
            if ($.trim(value).length == 0) {
                return;
            }

            if (!dateHelper(value, 'DD.MM.YYYY', true).isValid()) {
                return 'Некорректная дата';
            }
        },

        inn: function(value, attr, customValue, model) {
            value = $.trim(value);
            var message = 'ИНН не прошел форматный контроль';

            if (value.length == 10) {
                var controlSumm10 = ((2 * value.charAt(0) + 4 * value.charAt(1) + 10 * value.charAt(2) + 3 * value.charAt(3)
                    + 5 * value.charAt(4) + 9 * value.charAt(5) + 4 * value.charAt(6) + 6 * value.charAt(7) + 8 * value.charAt(8)) % 11) % 10;
                if (value[9] != controlSumm10) {
                    return message;
                }
            }
            if (value.length == 12) {
                var controlSumm11 = ((7 * value.charAt(0) + 2 * value.charAt(1) + 4 * value.charAt(2) + 10 * value.charAt(3) + 3 * value.charAt(4)
                    + 5 * value.charAt(5) + 9 * value.charAt(6) + 4 * value.charAt(7) + 6 * value.charAt(8) + 8 * value.charAt(9)) % 11) % 10;
                var controlSumm12 = ((3 * value.charAt(0) + 7 * value.charAt(1) + 2 * value.charAt(2)
                    + 4 * value.charAt(3) + 10 * value.charAt(4) + 3 * value.charAt(5) + 5 * value.charAt(6)
                    + 9 * value.charAt(7) + 4 * value.charAt(8) + 6 * value.charAt(9) + 8 * value.charAt(10)) % 11) % 10;
                if ((value[10] != controlSumm11) || (value[11] != controlSumm12)) {
                    return message;
                }
            }
        }
    });

})(Backbone.Validation);
