/* eslint-disable */
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import TaxationSystemType from '@moedelo/frontend-enums/mdEnums/TaxationSystemType';

const { Common } = window;

(function(bank) {
    bank.Models.IncomingPurseDocument = Backbone.Model.extend({
        url: `/Accounting/PurseOperation/Save`,

        documentName: `Поступление`,

        defaults: function() {
            return {
                PurseOperationType: Md.Data.PurseOperationType.Income
            };
        },

        validation: {
            Number: {
                required: true,
                msg: `Укажите номер`
            },
            Date: [
                {
                    required: true,
                    msg: `Введите дату`
                },
                {
                    date: true,
                    msg: `Введите корректную дату`
                },
                {
                    notClosedDate: true
                },
                {
                    notBeforeBalanceDate: true
                }
            ],
            DocumentsSum: {
                lessThan: `Sum`,
                msg: `Сумма оплаты по документам не может превышать сумму поступления`
            },
            KontragentName: {
                required: true,
                msg: `Укажите плательщика`
            },
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
            NdsSum: {
                fn: `ndsValidation`
            }
        },

        initialize: function() {
            this.on(`change:Sum`, function() {
                this.validate(this.pick(`DocumentsSum`));
            });
            this.on(`change:NdsSum`, function() {
                this.validate();
            });
            this.on(`change:Date`, this.onChangeDate);
        },

        load: function(options) {
            var defaults = {
                url: `/Accounting/PurseOperation/GetIncomingOperation`
            };

            return this.fetch(_.extend(defaults, options));
        },

        parse: function(resp) {
            if (!resp.Sum) {
                resp = _.omit(resp, `Sum`);
            }

            if (resp.BillDocumentBaseId && resp.Bill) {
                resp.BillNumber = `№ ${resp.Bill.Number} от ${resp.Bill.Date}`;
            }

            resp.PurseOperationType = Md.Data.PurseOperationType.Income;

            return resp;
        },

        onChangeDate: function(model, val) {
            const dateFormat = `DD.MM.YYYY`;
            const previous = dateHelper(this.previous(`Date`), dateFormat).year();
            const current = dateHelper(val, dateFormat).year();

            if (current !== previous) {
                this.trigger(`change:Year`);
            }
        },

        ndsValidation() {
            const usnNds2025Date = dateHelper(`2025-01-01`);
            const documentDate = dateHelper(this.get(`Date`), `DD.MM.YYYY`);

            const ts = new window.Money.Collections.Common.TaxationSystemCollection(window._preloading.TaxationSystems);
            const taxSystem = ts.Current();
            const isUsn = taxSystem.isUsn();

            if (this.get(`TaxationSystemType`) === TaxationSystemType.Patent) {
                return ``;
            }

            if (documentDate.isBefore(usnNds2025Date) || !isUsn) {
                return ``;
            }

            if (this.get(`NdsType`) === Common.Data.BankAndCashNdsTypes.None || this.get(`NdsType`) === Common.Data.BankAndCashNdsTypes.Nds0) {
                return ``;
            }

            if ((+this.get(`NdsSum`) <= 0 || this.get(`NdsSum`) === undefined) && this.get(`IncludeNds`)) {
                return `Введите сумму НДС`;
            }

            if (+this.get(`NdsSum`) >= +this.get(`Sum`) && this.get(`IncludeNds`)) {
                return `Неверная сумма НДС`;
            }
        }
    });
})(Bank);
