(function (common) {

    common.Collections.TaxCollection = Backbone.Collection.extend({

        model: common.Models.TaxModel,
        initialize: function () {
            common.Mixin.AddCollectionValidation.init(this, "PostingDate", "Incoming", "Outgoing", "Destination");
        }
    });

})(Common);