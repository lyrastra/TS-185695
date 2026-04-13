(function (money) {

    money.Models.Common.InventoryNumber = Backbone.Model.extend({
        url: WebApp.PurchaseOfFixedAssetsOutgoing.GetNextInventoryNumber
    });

})(Money);
