(function (money) {

    money.Models.Common.Bank = Backbone.Model.extend({
        url: WebApp.Banks.GetBankName,

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
