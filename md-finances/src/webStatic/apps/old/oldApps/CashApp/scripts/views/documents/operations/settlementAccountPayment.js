(function(cash) {

    'use strict';

    var settlementAccountPayment = Backbone.View.extend({
        render: function() {
            var template = TemplateManager.getFromPage(this.template);
            this.$el.html(template);

            this.bind();

            this.initializeControls();
            return this;
        },

        initializeControls: function() {
            this.$('[data-type=float]').decimalMask();
        }
    });

    cash.Views.toSettlementAccount = settlementAccountPayment.extend({
        template: 'ToSettlementAccountTemplate'
    });

    cash.Views.fromSettlementAccount = settlementAccountPayment.extend({
        template: 'FromSettlementAccountTemplate'
    });

})(Cash);