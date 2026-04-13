(function (sales) {

    sales.Collections.SalesBaseCollection = Backbone.BasePaginator.BasePager.extend({
        initialize: function (models, options) {
            if (options) {
                if (options.filterObject) {
                    this.filterObject = options.filterObject;
                }
                if (options.filterDateObject) {
                    this.filterDateObject = options.filterDateObject;
                }
            } 
        },
        
        deleteOperation: function (ids, success, error) {
            $.ajax({
                type: "POST",
                url: _.result(this, 'deleteUrl'),
                contentType: "application/json; charset=utf-8;",
                dataType: "json",
                data: $.toJSON(ids),
                success: function () {
                    success();
                },
                error: function () {
                    error();
                }
            });
        },

        fetch: function (options) {
            var obj = this;
            this.loaded = false;
            options = options || {};
            var old = options.success;

            options.success = function (collection, response) {
                obj.loaded = true;
                if (_.isFunction(old)) {
                    old(collection, response);
                }
            };

            Backbone.BasePaginator.BasePager.prototype.fetch.call(this, options);
        },

        getModels: function (ids) {
            var modelArray = new Array();

            this.each(function (value, index) {
                if (ids.indexOf(value.get('Id').toString()) > -1) {
                    modelArray.push(value);
                }
            });

            return modelArray;
        },

        firstPage: 1
    });

})(Sales);
