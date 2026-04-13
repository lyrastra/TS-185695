(function (common) {

    common.Models.Dialogs.Settlement = Backbone.Model.extend({
        url: '/Accounting/SettlementAccounts/SaveSettlementAccount',

        defaults: {
            Id: 0,
            Bank: "",
            BankName: "",
            Number: "",
            IsPrimary: true
        },

        initialize: function () {
            common.Mixin.ModelValidationMixin.init(this);
        },

        validateSettlement: function () {
            this.validationRules.Number.uniqueNumber = { msg: "Счет с таким номером уже существует" };

            var baseValidation = common.Mixin.ModelValidationMixin.validateModel;
            var isValid = baseValidation.call(this);

            delete this.validationRules.Number.uniqueNumber;

            return isValid;
        },

        validator: function () {
            var validator = {};
            _.extend(validator, common.Mixin.FunctionForValidation);
            _.extend(validator, settlementValidator);
            
            return validator;
        },

        validationRules: {
            Number: {
                notNullOrEmpty: { msg: 'Укажите расчетный счет ' },
                isDigit: { msg: "Ожидаются только цифры" },
                length: { msg: "Требуется значение длиной 20 символов", size: 20 }
            },
            Bank: {
                notNullOrEmpty: { msg: 'Укажите банк' }
            }
        }
    });

    var settlementValidator = {
        uniqueNumber: function (number, args) {
            var errorObj = { message: args.msg, options: args };

            var response = common.Utils.AjaxTools.sendToServiceJsonSync(RequisitesApp.Services.IsNumberExist, { number: number });

            if (response == "true") {
                return errorObj;
            }

            return { valid: true, options: { field: args.field } };
        }
    };

})(Common);
