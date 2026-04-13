/* eslint-disable */
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import SyntheticAccountCodesEnum from '../../../../../../enums/SyntheticAccountCodesEnum';

(function(common) {

    common.Models.PostingModel = Backbone.Model.extend({

        defaults: function() {
            return {
                Date: dateHelper().format('DD.MM.YYYY'),
                DebitNumber: '',
                CreditNumber: '',
                Sum: '',
                SubcontoCredit: [],
                SubcontoDebit: []
            };
        },

        initialize: function () {
            this.validationRules = {
                DebitNumber: {
                    notNullOrEmptyDebit: { msg: 'Укажите дебет', otherAccountBalanceType: 'CreditBalanceType' }
                },
                CreditNumber: {
                    notNullOrEmptyDebit: { msg: 'Укажите кредит', otherAccountBalanceType: 'DebitBalanceType' }
                },
                Sum: {
                    isDigitMask: { msg: 'Укажите число' },
                    notNullOrEmpty: { msg: 'Укажите сумму' },
                    notZero: { msg: 'Сумма не может быть равна 0'}
                },
                SubcontoDebit: {
                    requiredDebitSubconto: { msg: 'Укажите субконто' }
                },
                SubcontoCredit: {
                    requiredCreditSubconto: { msg: 'Укажите субконто' }
                }
            };

            common.Mixin.ModelValidationMixin.init(this);
        },

        initValidation: function() {
            common.Mixin.ModelValidationMixin.init(this);
        },

        removeValidation:function() {
            common.Mixin.ModelValidationMixin.remove(this);
        },

        validator: _.extend({},
            common.Mixin.FunctionForValidation,
            {
                notNullOrEmptyDebit: common.Mixin.FunctionForPostingsAndTaxValidation.notNullOrEmptyDebit,
                requiredDebitSubconto: function (data, args) {
                    return validateSubcontoByAccountCode(this.get('Debit')).apply(this, arguments);
                },
                requiredCreditSubconto: function (data, args) {
                    return validateSubcontoByAccountCode(this.get('Credit')).apply(this, arguments);
                }
            }
        ),

        setByAccount: function (fieldName, account) {
            this.set(fieldName, account.get('Code'));
            this.set(fieldName + 'TypeId', account.get('TypeId'));
            this.set(fieldName + 'Number', account.get('Number'));
            this.set(fieldName + 'BalanceType', account.get('BalanceType'));
        },

        addNoMoreSumValidation: function (sum) {
            this.validationRules.Sum.noMoreThen = { msg: "Сумма проводки не должна быть больше суммы операции", noMoreValue: sum };
        }
    });

    function validateSubcontoByAccountCode(accountCode) {
        return function (subcontoCollection, args) {
            if (!this.subcontoValidation) {
                return { valid: true, options: { field: args.field } };
            }

            var validations = this.subcontoValidation[accountCode] || [];
            var fields = _.pluck(validations, 'field');

            for (var i = 0; i < validations.length; i++) {
                var msg = validateSubconto(subcontoCollection, validations[i], accountCode);
                if(msg){
                    args.field = fields;
                    return { message: msg, options: args };
                }
            }

            return { valid: true, options: { field: fields } };
        };
    }

    function validateSubconto(subcontoCollection, validation, accountCode){
        var subconto = findSubcontoByType(subcontoCollection, validation.type);

        if (validation?.cashList?.length === 1 && accountCode === SyntheticAccountCodesEnum._50_02 && isEmptySubconto(subconto)) {
            return 'Добавьте кассу в Реквизитах'
        }

        if (validation?.required && isEmptySubconto(subconto)) {
            return 'Укажите субконто "' + validation.label + '"';
        }

        if (isSpecialSettlementAccount(accountCode)) {
            var regexp = /^\d{20}$/;
            if(!regexp.test(subconto.Name)){
                return 'Ожидается 20 цифр';
            }
        }
    }

    function isSpecialSettlementAccount(code){
        const { _53_01, _55_03, _55_04} = SyntheticAccountCodesEnum;

        return [ _53_01, _55_03, _55_04].includes(code);
    }

    function isEmptySubconto(subconto){
        return !subconto || !subconto.Name;
    }

    function findSubcontoByType(collection, subcontoType){
        return _.find(collection, function(subconto){
            if (!subconto){
                return false;
            }

            if (_.isUndefined(subconto.Type)) {
                return subconto.SubcontoType === subcontoType;
            }
            return subconto.Type == subcontoType;
        });
    }

})(Common);
