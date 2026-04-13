(function (bank) {

    bank.Collections.BaseDocumentFillingCollection = Backbone.Collection.extend({
        
        loaded: false,
        isError: false,

        initialize: function(options) {
            this.url = options.url;
        },

        sync: function (method, model, options) {
            var success = options.success;

            options.success = function (resp, status, xhr) {
                model.loaded = true;
                if (success) success(resp, status, xhr);
            };

            options.error = function () {
                model.loaded = true;
                model.isError = true;
            };
            Backbone.Collection.prototype.sync.call(this, method, model, options);
        },

        parse: function (resp) {
            return resp.List;
        },

        fetch: function (options) {
            options = options || {};
            options.type = "POST";
            options.contentType = 'application/json; charset=utf-8';

            return Backbone.Collection.prototype.fetch.call(this, options);
        }
    });

})(Bank);
