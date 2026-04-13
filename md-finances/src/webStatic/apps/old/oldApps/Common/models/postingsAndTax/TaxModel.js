/* eslint-disable */
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

(function(common) {

    common.Models.TaxModel = Backbone.Model.extend({
        initialize: function() {
            common.Mixin.ModelValidationMixin.init(this);
        },

        defaults: function() {
            return {
                PostingDate: dateHelper().format('DD.MM.YYYY'),
                Incoming: '',
                Outgoing: '',
                Destination: ''
            };
        },

        validator: {
            notNullOrEmpty: common.Mixin.FunctionForValidation.notNullOrEmpty,
            oneOfWithRelatedFields: common.Mixin.FunctionForValidation.oneOfWithRelatedFields,
            noMore: common.Mixin.FunctionForValidation.noMore,
            noLess: common.Mixin.FunctionForValidation.noLess,
            isDigit: common.Mixin.FunctionForValidation.isDigit,
            isDigitMask: common.Mixin.FunctionForValidation.isDigitMask,
            isDate: common.Mixin.FunctionForValidation.isDate,
            notNullOrEmptyIncoming: common.Mixin.FunctionForPostingsAndTaxValidation.notNullOrEmptyIncoming,
            notNullOrEmptyOutgoing: common.Mixin.FunctionForPostingsAndTaxValidation.notNullOrEmptyOutgoing
        },

        validationRules: {
            PostingDate: {
                notNullOrEmpty: { msg: "Укажите дату" },
                isDate: { msg: "Неверный формат даты" }
            },
            Incoming: {
                notNullOrEmptyIncoming: { msg: "Укажите сумму" },
                isDigitMask: { msg: "Укажите число" }
            },
            Outgoing: {
                notNullOrEmptyOutgoing: { msg: "Укажите сумму" },
                isDigitMask: { msg: "Укажите число" }
            },
            Destination: {
                notNullOrEmpty: { msg: "Укажите описание" }
            }
        },

        isEmpty: function () {
            var fields = ["Incoming", "Outgoing", "Destination"],
                isEmpty = true,
                self = this,
                field;

            _.each(fields, function(val) {
                field = self.get(val);
                if (field && field.toString().length) {
                    isEmpty = false;
                }
            });

            return isEmpty;
        }

    });

})(Common);
