(function (money) {

    money.Models.Common.BillSumForIncoming = Backbone.Model.extend({
        url: WebApp.MoneyTransferOperation.GetBillsSum,
        
        defaults: {
            "Sum": 0,
            "BillId": 0
        },

        fetch: function (options) {
            options = _.extend(options, {
                data: {
                    Sum: this.get("Sum"),
                    BillId: this.get("BillId")
                }
            });

            Backbone.Model.prototype.fetch.call(this, options);
        }
    });

})(Money.module("Models.Common"));
