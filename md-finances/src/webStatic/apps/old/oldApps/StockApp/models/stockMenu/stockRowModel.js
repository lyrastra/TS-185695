(function (model) {

    var baseModule = Stock.module('Main');
    var stockUrl = StockUrl.module('Main');

    model.Models.StockRowModel = baseModule.Models.BaseModel.extend({
        url: stockUrl.SaveStock,
        defaults: {
            Id: 0,
            Name: '',
            Type: 1,
            IsMain: false,
            StockTypeList: []
        }
    });

})(Stock.module('Stock.Menu'));