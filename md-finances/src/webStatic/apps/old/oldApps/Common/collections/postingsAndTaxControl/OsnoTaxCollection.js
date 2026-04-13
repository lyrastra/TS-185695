(function (common) {

    common.Collections.OsnoTaxCollection = Backbone.Collection.extend({

        model: common.Models.OsnoTaxModel,
        initialize: function () {
            common.Mixin.AddCollectionValidation.init(this, "PostingDate", "Incoming", "Outgoing");
        }
    });

})(Common);