(function (money) {

    money.Models.Common.BillSettlement = Backbone.Model.extend({
        url: WebApp.SettlementAccounts.GetSettlementAccountByBillId,
        
        defaults: {
            "BillId": 0
        },

        fetch: function (options) {
            options = _.extend(options, {
                data: {
                    BillId: this.get("BillId")
                }
            });

            Backbone.Model.prototype.fetch.call(this, options);
        }
    });

})(Money.module("Models.Common"));
