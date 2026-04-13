(function (cash) {
    'use strict';

    cash.Views.incomingCashContributing = Marionette.ItemView.extend({
        template: '#IncomingCashContributing',

        initialize: function() {
            _.extend(this, cash.Views.initKontragentAutocompleteMixin);
        },

        onRender: function() {
            this.bind();
            this.initializeControls();
            this._bindEvents();
            return this;
        },

        initializeControls: function() {
            var model = this.model;
            this.initKontragentAutocomplete();
            this.$('[data-type=float]').decimalMask();
            this.$('select').change();
            this.controls = {};

            this.$('[data-bind=ProjectNumber]').projectAutocomplete(model, {
                direction: model.get('Direction')
            });

            this._initQtip();
        },

        _bindEvents: function() {
            this.listenTo(this.model, 'change:Sum change:KontragentName', this._updateDescription);
        },

        _initQtip: function() {
            var url = Md.Core.Engines.CompanyId.getLinkWithParams('/Estate/Home#/authorizedCapital/');
            var text = 'Если учредитель еще не заведен в системе, то создайте его в разделе <a target="_blank" href="' + url + '">Уставный капитал</a>';
            this.$('.js-qtip').qtip({
                style: { classes: 'qtip-yellow newWave', width: 280 },
                position: { my: 'left center', at: 'right center' },
                content: { text: text },
                hide: {fixed: true, delay: 300}
            });
        },

        _updateDescription: function() {
            var model = this.model;
            var sum = model.get('Sum');
            var kontragent = model.get('KontragentName');
            var description = '';

            var formatedSum = Converter.toAmountString(sum);

            if (kontragent) {
                description = 'Взнос в уставный капитал от ' + kontragent;
                if (sum) {
                    description = 'Взнос в уставный капитал от ' + kontragent + ' на сумму ' + formatedSum + ' р. НДС не облагается.';
                }
            }

            model.set('Destination', description);
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
                },
                url: KontragentsApp.Autocomplete.GetKontragentFoundersAutocomplete
            });
        },

    });

})(Cash, Md);
