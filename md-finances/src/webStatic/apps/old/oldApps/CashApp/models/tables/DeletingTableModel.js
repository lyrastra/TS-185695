(function (cash) {

    cash.Models.DeletingTableModel = cash.Models.BaseApplicationModel.extend({
        
        url: cash.Data.DeleteCashOrders,
        
        sync: function (method, model, options) {
            options = _.extend(options, { type: "POST" });
            options.data = JSON.stringify(model.toJSON());
            options.contentType = 'application/json; charset=utf-8';
            return Backbone.Model.prototype.sync.call(this, method, model, options);
        }

    });
    
})(Cash);