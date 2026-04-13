/* eslint-disable */
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

(function (common) {

    common.Models.OsnoTaxModel = Backbone.Model.extend({
        initialize: function () {
            common.Mixin.ModelValidationMixin.init(this);
        },

        defaults: function(){
            return {
                PostingDate: dateHelper().format('DD.MM.YYYY'),
                Incoming: '',
                Outgoing: '',
                Type: 0,
                Kind: 0,
                NormalizedCostType: 0
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
            }
        },

        isEmpty: function () {
            var fields = ["Incoming", "Outgoing"],
                isEmpty = true,
                self = this,
                field;

            _.each(fields, function (val) {
                field = self.get(val);

                if (_.isNumber(field) && field != 0) {
                    isEmpty = false;
                } else if (field && field.toString().length) {
                    isEmpty = false;
                }
            });

            return isEmpty;
        }

    });

})(Common);
