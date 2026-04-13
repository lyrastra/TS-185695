Backbone.FilterObject = (function (Backbone, _, $) {

    var FilterObject = { };

    FilterObject.BaseFilterObject = Backbone.Model.extend({
        
        sync: function(method, model, options) {
            try {
                var resp = { };
                var store = model.localStorage;

                switch (method) {
                case "read":
                    resp = store.find(model);
                    break;
                case "create":
                    resp = store.create(model);
                    break;
                case "update":
                    resp = store.update(model);
                    break;
                case "delete":
                    resp = store.destroy(model);
                    break;
                }
                options.success(resp);
            } catch(e) {
                options.error(e);
            }

            return this;
        },
        
        parse: function (resp, options) {
            if (resp.attributes) {
                return resp.attributes;
            }
            return resp;
        },

        setFilter: function (key, value) {
            var model = this,
                filter = model.get("Filter"),
                obj = {};
            if (!_.isObject(key)) {
                if (value) {
                    obj[key] = value;
                    model.set("Filter", _.extend(filter, obj));
                } else {
                    model.set("Filter", _.omit(filter, key));
                }
            } else {
                model.set("Filter", _.extend(filter, key));
            }
            
        },

        getAttr: function(name) {
            var model = this,
                filterObject = model.get("Filter"),
                result;

            $.each(filterObject, function(ind, val) {
                if (ind == name) {
                    result = val;
                } else if (_.isObject(val)) {
                    $.each(val, function(index, value) {
                        if (index == name) {
                            result = value;
                        }
                    });
                }

            });

            return result;
        },

        setSort: function (key, value) {
            var model = this,
               filter = model.get("Sorter"),
               obj = {
                   Sort: key,
                   SortDirection: value
               };

                model.set("Sorter", _.extend(filter, obj));
        },

        save: function (attributes, options) {
            Backbone.Model.prototype.save.call(this, attributes, options);
            this.trigger("SaveModel");
        },

        clearFilter: function () {
            this.set({
                Filter: _.clone(this.defaults.Filter)
            });
        },

        clearDateFilter: function() {
            this.set({
                Filter: this.filterDefaults,
                State: this.stateDefaults
            });
        },
        
        description: {},

        getDescription: function () {
            var items = [],
                self = this;

            var filter = this.get("Filter");
            _.each(self.description, function (fieldDescription, key) {
                var filterItem = filter[key];
                if (!_.isUndefined(filterItem)) {
                    var item = {};

                    item.Key = fieldDescription.Name;
                    item.Value = fieldDescription.getValue ? fieldDescription.getValue(filterItem) : filterItem;

                    items.push(item);
                }
            });

            return items;
        }
    });

    return FilterObject;

}(Backbone, _, $));