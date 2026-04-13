(function (money) {

    money.Models.Common.RelatedOutgoingOperation = Backbone.Model.extend({
        url: WebApp.FinancialOperations.GetOutgoingMoneyTransferOperation,

        fetch: function (options) {
            options = _.extend(options, {
                data: {
                    id: this.get("id")
                }
            });

            Backbone.Model.prototype.fetch.call(this, options);
        }

    });

})(Money);
