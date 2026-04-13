(function (cash) {

    cash.Models.TotalInformationModel = Backbone.Model.extend({
        url: cash.Data.GetCashOperationSummaryInfo,

        initialize: function (options) {},

        Page: {
            PageNum: 1,
            PageSize: 20
        },

        sync: function (method, model, options) {
            if (model.currentRequest) {
                model.currentRequest.abort();
            }
            if (method == "read") {
                var data = [];

                if (options.data && _.isObject(options.data)) {
                    $.each(options.data, function (key, value) {
                        data.push({ Key: key, Value: value });
                    });
                }

                    data = data.concat([{ Key: "Page", Value: JSON.stringify(this.Page) }]);

                _.extend(options, {
                    type: 'POST',
                    contentType: "application/json; charset=utf-8;",
                    data: $.toJSON(data)
                });
            }

            var xhr = Backbone.Model.prototype.sync.call(this, method, model, options);
            model.currentRequest = xhr;
            return xhr;
        },


        parse: function (response) {
            var tags = response;
            tags.isEmpty = true;
            
            $.each(response, function(ind, val) {
                if (parseInt(val, 10)) {
                    tags.isEmpty = false;
                    return;
                }
            });

            return tags;
        },

        firstPage: 1
    });

})(Cash);
