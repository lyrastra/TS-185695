(function (sales) {
    sales.Models.Main.RelatedDocsToolTip = Backbone.Model.extend({

        url: function () {
            switch(parseInt(this.get('documentType'))) {
                case Common.Data.DocumentTypes.PaymentOrder:
                    return WebApp.AccountingPaymentOrder.GetLinkedDocuments;
                case Common.Data.DocumentTypes.IncomingCashOrder:
                case Common.Data.DocumentTypes.OutgoingCashOrder:
                    return WebApp.CashOrder.GetLinkedDocuments;
                case Common.Data.AccountingDocumentType.MiddlemanReport:
                    return String.format(WebApp.MiddlemanReport.GetRelatedDocuments, this.get('DocumentBaseId'));
                case Common.Data.DocumentTypes.RetailReport:
                    var firmRequisites = new Common.FirmRequisites();
                    if (!firmRequisites.get('IsAccounting')) {
                        return WebApp.BizRetailReport.GetLinkedDocuments;
                    }
            }

            return WebApp.ClosingDocumentsOperation.GetRelatedDocuments;
        },

        sync: function (method, model, options) {
            options.type = 'POST';
            options.data = $.toJSON(model.toJSON());
            options.contentType = 'application/json; charset=utf-8';

            return Backbone.Model.prototype.sync.call(this, method, model, options);
        },

        parse: function (response) {
            return {
                relatedList: response.Documents
            };
        }
    });

})(Sales); 