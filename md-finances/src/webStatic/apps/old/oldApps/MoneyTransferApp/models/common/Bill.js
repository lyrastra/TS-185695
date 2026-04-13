(function (money) {

    money.Models.Common.Bill = Backbone.Model.extend({
        url: WebApp.Bills.GetBillNumber,

        fetch: function (options) {
            options = _.extend(options, {
                data: {
                    id: this.get("id")
                }
            });

            Backbone.Model.prototype.fetch.call(this, options);
        }

    });

})(Money.module("Models.Common"));
