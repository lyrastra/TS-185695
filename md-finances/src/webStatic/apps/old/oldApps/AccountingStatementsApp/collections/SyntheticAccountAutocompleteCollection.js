(function(module) {

    module.Collections.SyntheticAccountAutocompleteCollection = Backbone.Collection.extend({
        url: WebApp.SyntheticAccount.GetSyntheticAccountAutocomplete,
        models: module.Models.SyntheticAccountAutocompleteModel,
        
        parse: function (res) {
            this.EmptyAccount = this.getEmptyAccount(res.List);
            return res.List;
        },
        
        getEmptyAccount: function(collection) {
            return _.find(collection, function (item) {
                return item.BalanceType == -1;
            });

        }
    });

})(AccountingStatements);