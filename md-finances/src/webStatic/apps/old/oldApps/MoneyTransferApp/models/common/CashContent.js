(function (money) {

    money.Models.Common.CashContent = Backbone.Model.extend({
        url: WebApp.MoneyBalanceMaster.IsExistCashOperationsBeforeYear,

        loaded: false,
        isError: false,
        
        sync: function (method, model, options) {
            var success = options.success;
            
            options.success = function (resp, status, xhr) {
                model.loaded = true;
                if (success) success(resp, status, xhr);
            };
            
            options.error = function(xhr, ajaxOptions, thrownError) {
                model.loaded = true;
                model.isError = true;
            };
            Backbone.Model.prototype.sync.call(this, method, model, options);
        },
        
        fetch: function (options) {
        	options = options || {};
        	options.data = JSON.stringify({ year: this.get('Year') });
        	options.type = "POST";
        	options.dataType = "json";
        	options.contentType = 'application/json; charset=utf-8';
        	Backbone.Model.prototype.fetch.call(this, options);
        }
    });

})(Money.module("Models.Common"));
