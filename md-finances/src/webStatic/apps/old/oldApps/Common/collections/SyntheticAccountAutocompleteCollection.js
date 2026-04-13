(function (module) {

    module.Collections.SyntheticAccountAutocompleteCollection = Backbone.Collection.extend({
        url: WebApp.SyntheticAccount.GetSyntheticAccountAutocomplete,

        parse: function (res) {
            this.EmptyAccount = this.getEmptyAccount(res.List);
            return res.List;
        },

        getEmptyAccount: function (collection) {
            return _.find(collection, function (item) {
                return item.BalanceType == -1;
            });

        },
        
        getItemByCode: function(code) {
            var items = this.where({ Code: code });
            return items && items.length > 0 ? items[0] : null;
        }
    });

})(Common);