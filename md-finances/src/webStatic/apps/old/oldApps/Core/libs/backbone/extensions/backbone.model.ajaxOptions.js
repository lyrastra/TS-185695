(function (backbone) {
    Backbone.emulateHTTP = true;

    var baseSaveFunc = backbone.Model.prototype.save;
    var baseFetchFunc = backbone.Model.prototype.fetch;
    var baseDestroyFunc = backbone.Model.prototype.destroy;

    backbone.Model.prototype.save = _.wrap(baseSaveFunc, function (func, data, options) {
        options = getDefaultOptions(options, 'save');
        return func.call(this, data, options);
    });

    backbone.Model.prototype.fetch = _.wrap(baseFetchFunc, function (func, options) {
        options = getDefaultOptions(options);
        return func.call(this, options);
    });
    
    backbone.Model.prototype.destroy = _.wrap(baseDestroyFunc, function (func, options) {
        options = getDefaultOptions(options);
        
        if (options.data) {
            options.contentType = 'application/json';
            options.data = JSON.stringify(options.data);
        }
        
        return func.call(this, options);
    });
    
    function getDefaultOptions(options, event) {
        var emptyAction = function () { };

        options = options || {};
        options.error = options.error || emptyAction;
        options.success = options.success || emptyAction;
        options.emulateHTTP = true;
        
        var success = options.success;
        options.success = function (model, response, opts) {
            if (!response || response.Status === false) {
                options.error();
                return;
            }

            if (event) {
                model.trigger(event);
            }
            success.call(this, model, response, opts);
        };

        return options;
    }
    
})(Backbone);
