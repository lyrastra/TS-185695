(function (collection) {

    var baseModule = Stock.module('Main');
    
    collection.Collections.StockMenuCollection = baseModule.Collections.BaseCollection.extend({
        model: collection.Models.StockRowModel
    });

})(Stock.module('Stock.Menu'));