(function (cash) {
    'use strict';

    cash.Views.toUnderAgency = Marionette.ItemView.extend({
        template: '#ToAgencyContractTemplate',

        initialize: function() {
            _.extend(this, cash.Views.initKontragentAutocompleteMixin);
            _.extend(this, cash.Views.setMaxSumTooltipMixin);
        },

        onRender: function() {
            this.bind();
            this.initializeControls();
            this._updateDescription();
            return this;
        },

        initializeControls: function() {
            var model = this.model;
            this.initKontragentAutocomplete();
            this.setMaxSumTooltip();
            this.$('[data-type=float]').decimalMask();
            this.$('select').change();
            this.controls = {};


            this.$('[data-bind=ProjectNumber]').projectAutocomplete(model, {
                direction: model.get('Direction'),
                isMediationOperation: true,
                withMainContract: false,
                kind: [mdNew.ContractKind.Mediation].toString()
            });
        },

        _updateDescription: function() {
            var destination = this.model.get('Destination');
            var id = this.model.get("Id");
            var description = '';

            if(!id){
                description = destination + '. НДС не облагается.';
            }
            else if(destination){
                description = destination;
            }
            this.model.set('Destination', description);
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
                url: WebApp.KontragentClosingDocs.GetAutocompleteForWaybill
            });
        },

    });

})(Cash, Md);
