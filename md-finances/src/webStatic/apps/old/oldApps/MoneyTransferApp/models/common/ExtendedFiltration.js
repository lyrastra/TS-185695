(function(money) {
    'use strict';

    money.Models.Common.ExtendedFiltration = Backbone.Model.extend({
        url: WebApp.SettlementAccounts.GetMoneyBayFilter,

        loaded: false,
        isError: false,

        sync: function(method, model, options) {
            var success = options.success;

            options.success = function(resp, status, xhr) {
                model.loaded = true;
                if (success) {
                    success(resp, status, xhr);
                }
            };

            options.error = function(xhr, ajaxOptions, thrownError) {
                model.loaded = true;
                model.isError = true;
            };
            Backbone.Model.prototype.sync.call(this, method, model, options);
        }
    });

})(Money);
