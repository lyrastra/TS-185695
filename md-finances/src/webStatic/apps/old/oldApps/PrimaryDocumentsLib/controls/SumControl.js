(function (primaryDocuments, common) {
    primaryDocuments.Controls.SumControl = common.Controls.BaseControl.extend({

//        template: "controls/SumControl",

        initialize: function (options) {
            common.Controls.BaseControl.prototype.initialize.call(this);
            common.Helpers.Mixer.addMixin(this, common.Mixin.OnChangeFieldMixin);
            this.initializeEvents();
        },
        
        initializeEvents: function() {
            this.model.on("change:InvoiceType", this.onChangeDocType, this);
        },

        onRender: function () {
            this.onChangeDocType();
        },
        
        onChangeDocType: function() {
            var model = this.model,
                invoiceType = parseInt(model.get("InvoiceType"), 10);

            if (invoiceType !== common.Data.InvoiceType.Advance) {
                this.hide();
            } else {
                this.show();
            }
        }
    });

})(PrimaryDocuments, Common);