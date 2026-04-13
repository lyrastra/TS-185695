(function (common) {
    
    common.Utils.AjaxTools = {
        sendToServiceJsonSync: function(url, parameters) {
            var responseText = $.ajax(
                {
                    type: "POST",
                    url: url,
                    contentType: "application/json; charset=utf-8;",
                    dataType: "json",
                    data: $.toJSON(parameters),
                    async: false,
                    traditional: false
                }).responseText;

            var result = $.parseJSON(responseText);
            if (result.hasOwnProperty("d")) {
                return result.d;
            } else {
                return result;
            }
        },
        
        send: function (options) {
            options = extendOptions(options);
            options.type = 'POST';
            options.dataType = "json";
            options.contentType = "application/json; charset=utf-8;";

            options.data = $.toJSON(options.data);

            return $.ajax(options);
        },
        
        load: function (options) {
            options = extendOptions(options);
            return $.ajax(options);
        },

        save: function (options) {
            options.contentType = 'application/json; charset=utf-8;';
            options.dataType = 'json';
            options.type = 'POST',
            options.data = $.toJSON(options.data);
            options = extendOptions(options);

            return $.ajax(options);
        }
    };
    
    function extendOptions(options) {
        var emptyAction = function () { };

        options = options || {};
        options.error = options.error || emptyAction;
        options.success = options.success || emptyAction;

        var success = options.success;
        options.success = function (response) {
            if (!response || response.Status === false) {
                options.error();
                return;
            }

            success.call(this, response);
        };

        return options;
    }
    
})(Common);