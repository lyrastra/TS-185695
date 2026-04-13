(function (accountingPosting) {
    accountingPosting.Collections.TaxPostingCollection = Backbone.Collection.extend({
        url: "/Accounting/ClosingDocumentPosting/GetTaxPostings",
        
        load: function (id, options) {
            if (this.loaded[id]) {
                this.reset(this.loaded[id].List);
                options.success();
                return;
            }

            this.loadingCollectionId = id;

            var ids = {};
            if (_.isArray(id)) {
                for (var i = 0 ; i < id.length; i++) {
                    ids[i] = id[i];
                }
            } else {
                ids = id;
            }

            _.extend(options, {
                data: {
                    'id': ids
                }
            });

            this.fetch(options);
        },
        
        parse: function(resp) {
            if (resp) {
                this.loaded[this.loadingCollectionId] = resp;

                var data = resp.List;
                resp.List = $.map(data, function (item) {
                    return {
                        MainPostings: item.Postings,
                        LinkedDocuments: item.LinkedDocuments
                    };
                });
                return resp.List;
            }
        },
        
        loaded: {}
    });
})(AccountingPosting);