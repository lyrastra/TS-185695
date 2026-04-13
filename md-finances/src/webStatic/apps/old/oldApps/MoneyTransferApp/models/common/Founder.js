(function (money) {

    money.Models.Common.Founder = Backbone.Model.extend({
        urlRoot: WebApp.Kontragents.GetFounderName
    });

})(Money.module("Models.Common"));
