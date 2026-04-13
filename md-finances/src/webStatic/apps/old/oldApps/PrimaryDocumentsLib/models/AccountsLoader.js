(function (primaryDocuments) {
    var supplierAccounts = new Backbone.Model();
    supplierAccounts.url = WebApp.ChartOfAccount.GetSupplierAccounts;

    var clientAccounts = new Backbone.Model();
    clientAccounts.url = WebApp.ChartOfAccount.GetClientAccounts;

    /**
	 * Создает модель и прокидывает в неё урл
	 *
	 * @param custom url.
	 * @return модель.
	 */
    var customModelCreate = function(url) {
        var model = new Backbone.Model();
        model.url = url;
        return model;
    };
   
    primaryDocuments.Models.AccountsLoader = {
        _loadAccounts: function (model, callback, context) {
            if (model.loaded === true) {
                callback.call(context, model.toJSON().List);
            } else {
                model.fetch({
                    success: function () {
                        model.loaded = true;
                        callback.call(context, model.toJSON().List);
                    }
                });
            }
        },

        loadSupplierAccounts: function (callback, context) {
            this._loadAccounts(supplierAccounts, callback, context);
        },
        
        loadClientAccounts: function (callback, context) {
            this._loadAccounts(clientAccounts, callback, context);
        },
        
        customLoad: function (callback, context, url) {
            this._loadAccounts(customModelCreate(url), callback, context);
        }
    };
    
})(PrimaryDocuments);