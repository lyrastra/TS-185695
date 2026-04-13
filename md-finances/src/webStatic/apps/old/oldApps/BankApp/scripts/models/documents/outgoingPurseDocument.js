/* eslint-disable */
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';

(function (bank) {

    bank.Models.OutgoingPurseDocument = Backbone.Model.extend({
        url: '/Accounting/PurseOperation/Save',
        documentName: 'Списание',

        defaults: function() {
            return {
                PurseOperationType: Md.Data.PurseOperationType.Transfer
            };
        },

        initialize: function() {
            this.on('change:Date', this.onChangeDate);
            this.on(`change:NdsSum`, function() {
                this.validate();
            });
        },

        validation: {
            Number: {
                required: true,
                msg: 'Укажите номер'
            },
            Date: [
                {
                    required: true,
                    msg: 'Введите дату'
                },
                {
                    date: true,
                    msg: 'Введите корректную дату'
                },
                {
                    notClosedDate: true
                },
                {
                    notBeforeBalanceDate: true
                }
            ],
            Sum: [
                {
                    required: true,
                    msg: `Укажите сумму`
                },
                {
                    notZero: true,
                    msg: 'Сумма не может быть равна 0'
                }
            ],
            KontragentName: {
                required: function() {
                    return this.get('PurseOperationType') === Md.Data.PurseOperationType.OtherOutgoing;
                },
                msg: 'Укажите получателя'
            },
            NdsSum: {
                fn: `ndsValidation`
            }
        },

        load: function(options){
            var defaults = {
                url: '/Accounting/PurseOperation/GetOutgoingOperation'
            };

            return this.fetch(_.extend(defaults, options));
        },

        parse: function(resp){
            if(!resp.Sum){
                resp = _.omit(resp, 'Sum');
            }

            // в списаниях счета игнорируем

            if(!resp.KontragentId){
                resp = _.omit(resp, 'KontragentId');
            }

            return resp;
        },

        onChangeDate: function(model, val) {
            var dateFormat = 'DD.MM.YYYY';
            var previous = dateHelper(this.previous('Date'), dateFormat).year();
            var current = dateHelper(val, dateFormat).year();

            if (current !== previous) {
                this.trigger('change:Year');
            }
        },

        ndsValidation() {
            const year = dateHelper(this.get('Date')).year();
           
            if (year < 2026) {
                return ``;
            }
            
            if (this.get(`NdsType`) === Common.Data.BankAndCashNdsTypes.Nds0) {
                return ``;
            }
            
            if (+this.get(`NdsSum`) >= +this.get(`Sum`) && this.get(`IncludeNds`)) {
                return `Неверная сумма НДС`;
            }
            
            if ((+this.get(`NdsSum`) <= 0 || this.get(`NdsSum`) === undefined) && this.get(`IncludeNds`)) {
                return `Введите сумму НДС`;
            }

            if (this.get(`TaxationSystemType`) === TaxationSystemType.Patent) {
                return ``;
            }
        }
    });

})(Bank);
