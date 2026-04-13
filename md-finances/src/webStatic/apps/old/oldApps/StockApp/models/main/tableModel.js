(function (stockModule) {

    stockModule.Models.TableModel = stockModule.Models.BaseModel.extend({
        url: '',
        getCountUrl: '',
        
        defaults: {
            Status: false,
            StatisticsAction: 0,
            List: []
        },
        
        parse: function(res) {
            var result = {
                Status: res.Status,
                Count: res.Count,
                List: res.List
            };

            if (result.List && result.List.length) {
                for (var i = 0, item = result.List[0]; i < result.List.length; i++, item = result.List[i]) {
                    item.Number = i + 1;
                    item.Amount = item.Count * item.Price;
                }
            }

            return result;
        },

        load: function (options) {
            this.getPostRequest(this.url, options.data, options.success, options.error);
        },

        getCount: function (options) {
            this.getPostRequest(this.getCountUrl, options.data, options.success, options.error);
        },
        
        getPostRequest: function(url, data, successFunc, errorFunc) {
            this.fetch({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: url,
                data: data,
                success: function (res) {
                    successFunc(res);
                },
                error: function (res) {
                    errorFunc(res);
                }
            });
        }
    });

})(Stock);