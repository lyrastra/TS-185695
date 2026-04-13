(function (accountingPosting) {
    accountingPosting.Collections.AccountingDocumentPostingCollection = Backbone.Collection.extend({
        getByBaseIdUrl: "/Accounting/ClosingDocumentPosting/GetByBaseId",
        getByOperationId: "/Accounting/ClosingDocumentPosting/Get",
        
        url: this.getByOperationId,
        
        load: function (id, isBaseId, options) {
            this.url = this.getByBaseIdUrl;

            if (this.loaded[id]) {
                this.reset(this.loaded[id].List);
                options.success();
                return;
            }

            this.loadingCollectionId = id;

            _.extend(options, {
                data: {
                    'id': id
                }
            });

            this.fetch(options);
        },
        
        parse: function(resp) {
            if (resp) {
                this.loaded[this.loadingCollectionId] = resp;

                return resp.List;
            }
        },
        
        loaded: {}
    });
})(AccountingPosting);