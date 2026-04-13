(function(validation) {

    'use strict';

    _.extend(validation.validators, {
        positive: function(value, attr, customValue, model) {
            if (Converter.toFloat(value) < 0) {
                return 'Значение должно быть положительным числом';
            }
        },

        notZero: function(value, attr, useValidation, model) {
            var validate = _.isFunction(useValidation) ? useValidation.call(model, value, attr) : useValidation;

            if (validate === false) {
                return;
            }

            if (Converter.toFloat(value) === 0) {
                return 'Значение не может быть равно 0';
            }
        },

        lessThan: function(value, attr, field, model) {
            var val;
            if (_.isString(field)) {
                val = model.get(field);
            }

            if (_.isFunction(field)) {
                val = field.call(model, value, attr);
            }

            if (Converter.toFloat(value) > val) {
                return 'Значение не может быть больше ' + val;
            }
        },

        lessOrEqualThan: function(value, attr, field, model) {
            var val;
            if (_.isString(field)) {
                val = model.get(field);
            }

            if (_.isFunction(field)) {
                val = field.call(model, value, attr);
            }

            if (Converter.toFloat(value) >= val && !!value) {
                return 'Значение не может быть больше или равно ' + val;
            }
        },

        fractionalPartValidation: function(value) {
            if (value) {
                value = Converter.toFloat(value);
                var regExp = new RegExp('^-?([\\d ]?)*([.,]\\d{1,2})?$');

                if (!regExp.test(value)) {
                    return 'Укажите 2 знака после запятой';
                }
            }
        }
    });

})(Backbone.Validation);