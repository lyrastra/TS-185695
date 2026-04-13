(function (buy, common) {
    buy.Collections.PostingsAndTax.AccountingStatementPostingsCollection = common.Collections.BuhOperationCollection.extend({
        url: WebApp.AccountingStatements.GetAllPostings,
        
        onlyPostingsAndTaxMode: common.Data.ProvidePostingType.ByHand,

        allowNegativeSum: true,

        checkSourceData: function () {
            return true;
        },
        
        listenSource: function () {
            this.sourceDocument.on('change:Description', this.setDescription, this);
        },

        setDescription: function() {
            var description = this.sourceDocument.get('Description');
            this.each(function (row) {
                row.get('ManualPostings').each(function(posting) {
                    posting.set('Description', description);
                });
            });
        },

        getDocumentSpecialProperties: function () {
            var data = {};
            if (this.sourceDocument.has('AccountCodes')) {
                _.extend(data, this.getAccountCodeData());
            }

            return data;
        },
        
        getAccountCodeData: function() {
            var codes = this.sourceDocument.get('AccountCodes');

            if (this.sourceDocument.get("FixedAssetBaseId") > 0) {
                return {
                    Credit: mapAccountCodeList(codes.CreditCodes),
                    Debit: mapAccountCodeList(codes.DebitCodes),
                    SubcontoDebit: { disabled: true },
                    DelateFirstRow: false
                };
            }

            return {
                Debit: { excludeCodes: codes.ExcludeDebitCodes }
            };
        }
    });
    
    function mapAccountCodeList(codes) {
        if (codes.length === 1) {
            return { defaultAccountCode: codes[0], disabled: true };
        }

        return { accountsFilter: codes };
    }
    
})(Buy, Common);