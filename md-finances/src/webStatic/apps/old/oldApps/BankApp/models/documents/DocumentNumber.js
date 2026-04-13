(function (bank) {
    bank.Models.Documents.DocumentNumber = Backbone.Model.extend({
        url: WebApp.AccountingPaymentOrder.GetPaymentNumber,

        load: function (success) {
            var model = this;
            this.fetch({
                success: function (documentNumberModel, response, options) {
                    if (response.Status) {
                        success();
                    }
                },
                data: model.toJSON()
            });
        },

        isBusy: function (isBusy, notBusy, checking) {
            if (checking) {
                var model = this;
                this.fetch({
                    success: function(documentNumberModel, response, options) {
                        if (response.Status) {
                            notBusy();
                        } else {
                            isBusy();
                        }
                    },
                    error: isBusy,
                    data: model.toJSON(),
                    url: WebApp.AccountingPaymentOrder.DocumentNumberNotBusy
                });
            } else {
                notBusy();
            }
            
        }
    });

})(Bank);
