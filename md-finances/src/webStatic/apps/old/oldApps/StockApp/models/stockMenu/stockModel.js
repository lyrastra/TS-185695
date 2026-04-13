(function (model) {

    var baseModule = Stock.module('Main');
    var stockUrl = StockUrl.module('Main');

    model.Models.StockModel = baseModule.Models.BaseModel.extend({
        url: stockUrl.GetStock,
        defaults: {
            Status: false,
            StatisticsAction: 0,
            List: new model.Collections.StockMenuCollection
        },
        parse: function (resp) {
            resp = baseModule.Models.BaseModel.prototype.parse.call(this, resp);

            if (resp !== null && resp.List) {
                resp.List = new model.Collections.StockMenuCollection(resp.List);
            }

            return resp;
        },
        getMainStock: function() {
            var result = _.find(this.get('List').models, function(item) {
                return item.get('IsMain') == true;
            });
            
            return result;
        },
        getStockNotMain: function() {
            var result = _.filter(this.get('List').models, function (item) {
                return item.get('IsMain') == false;
            });

            return result;
        },
        
        getWholesaleStocks: function () {
            return this.getStocksByType(Common.Data.StockTypeEnum.Wholesale);
        },
        
        getRetailStocks: function () {
            return this.getStocksByType(Common.Data.StockTypeEnum.Retail);
        },
        
        getStocksByType: function(type) {
            var result = this.get('List').filter(function (item) {
                return item.get('Type') == type;
            });

            return _.map(result, function (item) { return item.toJSON(); });
        }

    });

})(Stock.module('Stock.Menu'));