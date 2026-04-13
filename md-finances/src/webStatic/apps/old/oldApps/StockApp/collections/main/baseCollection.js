(function (stockModule) {

    stockModule.Collections.BaseCollection = Backbone.Collection.extend({
        validation: function () {
            var result = true;
            
            this.each(function (item) {
                result = result && item.validateModel();
            });

            return result;
        },
        
        getData: function (parameters) {
            var options = {
                url: this.url,
                success: parameters.success,
                data: parameters.data
            };

            this.postfetch(options);
        },
        
        postfetch: function (opt) {
            opt.type = 'POST';
            opt.dataType = "json";
            opt.contentType = "application/json; charset=utf-8;";
            Backbone.Collection.prototype.fetch.call(this, opt);
        }
    });

})(Stock);