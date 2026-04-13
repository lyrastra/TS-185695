(function (sales) {
    sales.Models.Main.Commenting = Backbone.Model.extend({

        url: WebApp.ClosingDocumentsOperation.SaveComment,

        initialize: function (options) {
            if (options && options.url) {
                this.url = options.url;
            }
        },

        defaults: {
            "successText": "Комментарий сохранен",
            "errorText": "Ошибка сохранения комментария"
        }
    });

})(Sales);