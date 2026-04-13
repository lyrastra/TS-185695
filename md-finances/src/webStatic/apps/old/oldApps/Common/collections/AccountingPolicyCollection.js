(function (common) {
    common.Models.AccountingPolicyModel = Backbone.Model.extend({
        url: WebApp.AccountingPolicy.GetAccountingPolicy,

        loaded: false,

        parse: function (response) {
            if (!this.yearStack) {
                this.yearStack = {};
            }
            
            if (!this.yearStack[response.Year]) {
                this.yearStack[response.Year] = response;
            }
            return response;
        }
    });

})(Common);
