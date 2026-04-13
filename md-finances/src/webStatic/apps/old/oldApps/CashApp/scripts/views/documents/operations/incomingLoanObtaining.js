(function(cash) {
    'use strict';

    cash.Views.incomingLoanObtaining = Marionette.ItemView.extend({
        template: '#IncomingLoanObtaining',

        initialize: function() {
            _.extend(this, cash.Views.initKontragentAutocompleteMixin);
        },

        onRender: function() {
            this.bind();
            this.initializeControls();
            return this;
        },

        initializeControls: function() {
            var model = this.model;
            this.initKontragentAutocomplete();

            this.$('[data-type=float]').decimalMask();
            this.$('select').change();
            this.controls = {};

            this.$('[data-bind=ProjectNumber]').projectAutocomplete(model, {
                isReceivedOperation: true,
                withMainContract: false,
                direction: model.get('Direction'),
                kind: [mdNew.ContractKind.ReceivedCredit, mdNew.ContractKind.ReceivedLoan].toString()
            });

            this._initQtip();
        },

        _initQtip: function() {
            var text = 'Кредит или займ является долгосрочным, если получен на срок более года.';
            this.$('.js-qtip').qtip({
                style: { classes: 'qtip-yellow newWave', width: 280 },
                position: { my: 'left center', at: 'right center' },
                content: { text: text }
            });
        },

        initKontragentAutocomplete: function() {
            var model = this.model;

            this.$('[data-bind=KontragentName]').saleKontragentWaybillAutocomplete({
                onSelect: function(selected) {
                    model.set({
                        KontragentId: selected.object.Id,
                        KontragentName: selected.object.Name
                    });
                },
                clean: function() {
                    model.unset('KontragentId');
                    model.unset('KontragentName');
                },
                onBlur: function() {
                    if(!model.get('KontragentName')) {
                        model.unset('KontragentId');
                    }
                }
            });
        },

    });

})(Cash, Md);
