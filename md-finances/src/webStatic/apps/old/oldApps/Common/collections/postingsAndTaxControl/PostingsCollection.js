(function (common) {

    common.Collections.PostingsCollection = Backbone.Collection.extend({

        model: common.Models.PostingModel,

        initialize: function () {
            this.initValidation();
        },
        
        initValidation: function() {
            common.Mixin.AddCollectionValidation.init(this, "DebitNumber", "CreditNumber", "Sum");
        },
        
        removeValidation: function() {
            common.Mixin.removeCollectionValidation(this, "DebitNumber", "CreditNumber", "Sum");
        }

    });

})(Common);