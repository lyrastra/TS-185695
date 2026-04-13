/* eslint-disable */
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';

(function(cash) {
    cash.Views.outgoingLoanRepayment = Marionette.ItemView.extend({
        template: '#OutgoingLoanRepayment',

        initialize() {
            _.extend(this, cash.Views.initKontragentAutocompleteMixin);
        },

        onRender() {
            this.bind();
            this.initializeControls();
            this._showOrHideLoanInterestSum();
            this._bindEvents();

            return this;
        },
        
        initializeControls() {
            const model = this.model;
            this.initKontragentAutocomplete();

            this.$('[data-type=float]').decimalMask();
            this.$('select').change();
            this.controls = {};

            this.$('[data-bind=ProjectNumber]').projectAutocomplete(model, {
                isReceivedOperation: true,
                withMainContract: false,
                direction: Direction.Incoming,
                kind: [mdNew.ContractKind.ReceivedCredit, mdNew.ContractKind.ReceivedLoan].toString()
            });

            this._initLongTermQtip();
        },

        _bindEvents() {
            this.listenTo(this.model, 'change:Sum change:LoanInterestSum change:KontragentName', this._updateDescription);
            this.listenTo(this.model, 'change:Date', this._updateDescription);
        },
        
        _showOrHideLoanInterestSum() {
            const $el = this.$('.js-loanInterestSumBlock');
            const date = this.model.get('Date');
            const taxationSystems = Common.Utils.CommonDataLoader.TaxationSystems;

            if (date) {
                const formatedDate = Converter.toDate(date);
                const current = taxationSystems.Current(formatedDate);
                $el.removeClass('hidden');
            }
        },

        _updateDescription() {
            const model = this.model;
            const sum = model.get('Sum');
            const loanInterestSum = model.get('LoanInterestSum');
            const kontragent = model.get('KontragentName');
            let description = '';

            const formatedSum = Converter.toAmountString(sum);
            const formatedLoanInterestSum = Converter.toAmountString(loanInterestSum);

            if (kontragent) {
                if (sum && !loanInterestSum) {
                    description = `Погашение займа на сумму ${formatedSum} контрагенту ${kontragent}.` +
                        ' НДС не облагается.';
                } else if (sum && loanInterestSum) {
                    description = `Погашение займа на сумму ${formatedSum} и процентов ` +
                        `на сумму ${formatedLoanInterestSum} контрагенту ${kontragent}. НДС не облагается.`;
                }
            }

            model.set('Destination', description);
        },

        _initQtip($el, text) {
            $el.qtip({
                style: { classes: 'qtip-yellow newWave', width: 280 },
                position: { my: 'left center', at: 'right center' },
                content: { text }
            });
        },

        _initLongTermQtip() {
            const text = 'Кредит или займ является долгосрочным, если получен на срок более года.';
            this._initQtip(this.$('.js-longTermQtip'), text);
        },

        initKontragentAutocomplete() {
            const model = this.model;

            this.$('[data-bind=KontragentName]').saleKontragentWaybillAutocomplete({
                onSelect(selected) {
                    model.set({
                        KontragentId: selected.object.Id,
                        KontragentName: selected.object.Name
                    });
                },
                clean() {
                    model.unset('KontragentId');
                    model.unset('KontragentName');
                },

                onBlur() {
                    if (!model.get('KontragentName')) {
                        model.unset('KontragentId');
                    }
                }
            });
        }

    });
}(Cash, Md));
