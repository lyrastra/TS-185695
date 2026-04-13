(function(cash) {
    cash.Views.toOtherCash = Backbone.View.extend({
        template: `ToOtherCashTemplate`,

        render: function() {
            var template = TemplateManager.getFromPage(this.template);
            this.$el.html(template);

            this.bind();
            this.initializeControls();

            return this;
        },

        initializeControls: function() {
            this.$('[data-type=float]').decimalMask();
            this.$('select').change();
        },
        
        bindings: {
            'select[data-bind=DestinationCashId]': {
                observe: 'DestinationCashId',
                selectOptions: {
                    collection: function() {
                        var cashList = new cash.Collections.CashCollection().filter(function(item) {
                            return item.get(`Id`) != this.model.get(`CashId`);
                        }, this);

                        return _.map(cashList, function(item) {
                            return {
                                value: item.get(`Id`),
                                label: item.get(`Name`)
                            };
                        });
                    }
                }
            }
        }
    });
}(Cash));
