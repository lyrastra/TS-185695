(function (money) {

    money.Models.Common.Kontragent = Backbone.Model.extend({
        url: WebApp.Kontragents.GetKontragentOrWorkerName,

        fetch: function (options) {
            options = _.extend(options, {
                data: {
                    id: this.get('id'),
                    kontragentType: this.get('kontragentType')
                }
            });

            Backbone.Model.prototype.fetch.call(this, options);
        }

    });

})(Money);
