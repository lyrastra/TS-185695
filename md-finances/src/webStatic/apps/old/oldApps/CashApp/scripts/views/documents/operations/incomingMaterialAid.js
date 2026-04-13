(function(cash) {
    'use strict';

    cash.Views.incomingMaterialAid = Marionette.ItemView.extend({
        template: '#IncomingMaterialAid',

        initialize: function() {
            _.extend(this, cash.Views.initKontragentAutocompleteMixin);
        },

        onRender: function() {
            this.bind();
            this.initializeControls();
            return this;
        },

        initializeControls: function() {
            this.initKontragentAutocomplete();

            this.$('[data-type=float]').decimalMask();
            this.$('select').change();

            this._initKontragentQtip();
            this._initSumQtip();
            !this.model.get('Id') && this._updateDescription();

            this._bindEvents();
        },

        _bindEvents: function() {
            this.listenTo(this.model, 'change:KontragentName', this._updateDescription);
        },

        _initKontragentQtip: function() {
            var companyId = Md.Core.Engines.CompanyId;
            var link = companyId.getLinkWithParams('/Estate/Home#/authorizedCapital/');
            var text = 'Если учредитель еще не заведен в системе, то создайте его в разделе' +
                ' <a target="_blank" href="' + link + '">Уставный капитал</a>';
            this.$('.js-kontragentQtip').qtip({
                hide: {
                    fixed: true,
                    delay: 500
                },
                style: { classes: 'qtip-yellow newWave', width: 280 },
                position: { my: 'left center', at: 'right center' },
                content: { text: text }
            });
        },

        _initSumQtip: function() {
            var text = 'Предельный размер расчетов наличными денежными средствами ' +
                'в рамках предпринимательской деятельности по одному договору не должен превышать 100 тыс. руб.';
            this.$('.js-sumQtip').qtip({
                style: { classes: 'qtip-yellow newWave', width: 280 },
                position: { my: 'left center', at: 'right center' },
                content: { text: text }
            });
        },

        initKontragentAutocomplete: function() {
            var model = this.model;

            this.$('[data-bind=KontragentName]').saleKontragentWaybillAutocomplete({
                url: '/Kontragents/Autocomplete/GetFounderAutocomplete',
                addLink: false,
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

        _updateDescription: function() {
            this.model.set('Destination', this.getDescription());
        },

        getDescription: function() {
            var model = this.model;
            var kontragentName = model.get('KontragentName');
            var text = 'Финансовая помощь';

            if (kontragentName) {
                text += ' от ' + kontragentName;
            }

            return text;
        },

        destroy: function() {
            this.stopListening();
            this.unstickit(this.model, this.bindings);
            this.undelegateEvents();
            this.$el.empty();
        },

        bindings: {}
    });

})(Cash, Md);
