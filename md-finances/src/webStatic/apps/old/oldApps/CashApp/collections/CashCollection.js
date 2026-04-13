(function (cash) {

    var request;
    var fullList;

    cash.Collections.CashCollection = Backbone.Collection.extend({
        initialize: function() {
            if (request) {
                this.reset(this.parse(JSON.parse(request.responseText)));
            }
        },

        url: cash.Data.GetCashList,
        
        load: function (options) {
            options = options || {};

            if (!request) {
                request = this.fetch.apply(this, arguments);
                return request;
            }

            return request.done(options.success);
        },

        reload: function (options) {
            request = this.fetch.apply(this, arguments);
            return request;
        },

        getAll: function(){
            return fullList;
        },
        
        parse: function(resp) {
            fullList = resp.List;
            return _.where(resp.List, { Enable : true });
        }
    });

})(Cash);
