import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import { downloadPost } from '@moedelo/frontend-core-v2/helpers/httpClient';

const {
    TemplateManager, $, Converter, Cash, _
} = window;

(function salary(cash) {
    // eslint-disable-next-line no-param-reassign
    cash.Views.salary = Backbone.View.extend({
        template: `SalaryPaymentTemplate`,

        events: {
            'click .downloadPaybill li': `downloadPaybill`
        },

        initialize() {
            this.model.on(`change:PayToWorkers`, this.setSum, this);
            this.model.on(`change:PayToWorkers change:PaybillNumber change:PaybillDate`, this.setComments, this);
            this.model.on(`change:PayToWorkers`, this.showPaybillRow, this);
        },

        render() {
            const template = TemplateManager.getFromPage(this.template);
            this.$el.html(template);
            this.bind();
            this.initializeControls();
            this.showPaybillRow();
            this.setSum();

            return this;
        },

        initializeControls() {
            const view = this;

            this.$(`input[data-type=float]`).decimalMask();

            this.$(`[data-type=date]`).mdDatepicker({
                minDate: dateHelper(this.model.getMinDate(), `DD.MM.YYYY`).format(`YYYY-MM-DD`)
            }).change();

            this.$(`[data-collapsible]`).not(`[data-type=date]`).collapsible();

            this.$(`[data-collapsible]`).filter(`[data-type=date]`).collapsible({
                validate(val, $el) {
                    return !view.model.preValidate($el.attr(`data-bind`), val);
                }
            });

            this.controls = {
                workers: new cash.Components.WorkerPaymentList({
                    el: this.$(`[data-control=workerPayments]`),
                    model: this.model,
                    name: `PayToWorkers`
                }).render()
            };
        },

        setSum() {
            const sum = this.model.get(`PayToWorkers`).reduce((memo, item) => {
                return (item.Sum || 0) + memo;
            }, 0) || 0;
            this.model.set(`Sum`, sum);
            this._showTotalSum();
        },

        isSingleWorkerOnPayBillPaymentMethod() {
            const workers = this.model.get(`PayToWorkers`);

            if (workers.length !== 1) {
                return false;
            }

            const payBillPaymentMethod = 0;

            return workers[0]?.PaymentMethod === payBillPaymentMethod;
        },

        showPaybillRow() {
            const paybilEl = this.$(`.mdRow.paybill`);

            if (this.model.get(`PayToWorkers`).length > 1 || this.isSingleWorkerOnPayBillPaymentMethod()) {
                paybilEl.show();
            } else {
                paybilEl.hide();
            }
        },

        setComments() {
            if (this.model.get(`PayToWorkers`).length > 1) {
                const text = `Платежная ведомость №${this.model.get(`PaybillNumber`)} от ${this.model.get(`PaybillDate`)}`;
                this.model.set({ Comments: text });
            }
        },

        downloadPaybill(event) {
            const fileType = $(event.target).attr(`data-type`);
            const params = {
                Date: this.model.get(`PaybillDate`),
                Number: this.model.get(`PaybillNumber`),
                WorkerSumList: this.model.get(`PayToWorkers`).map(payment => {
                    return _.pick(payment, `WorkerId`, `Sum`);
                }),
                CashOrderNumber: this.model.get(`Number`),
                CashOrderDate: this.model.get(`Date`),
                FileExtension: fileType
            };

            downloadPost(cash.Urls.LoadPaybill, params);
        },

        _showTotalSum() {
            const $el = this.$(`.js-totalSum`);
            const sum = this.model.get(`Sum`);

            if (sum) {
                $el.text(`${Converter.toAmountString(sum)} (итого)`);
            } else {
                $el.empty();
            }
        },

        destroy() {
            const { workers } = this.controls;
            workers.destroy && workers.destroy();
        }
    });
}(Cash));
