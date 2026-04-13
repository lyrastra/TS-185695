(function (sales) {
    sales.Models.Main.StatusSaving = Backbone.Model.extend({
        url: WebApp.ClosingDocumentsOperation.SaveStatus
    });

})(Sales);