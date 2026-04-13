(function (common) {
    common.Models.Dialogs.Kontragent = Backbone.Model.extend({
        url: WebApp.Kontragents.Save,
        defaults: {
            Type: Enums.Kontragents.KontragentTypes.Kontragent
        }
    });

})(Common);