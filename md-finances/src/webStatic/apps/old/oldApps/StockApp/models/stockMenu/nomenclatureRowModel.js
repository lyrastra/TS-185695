(function (model) {

    var baseModule = Stock.module('Main');
    var stockUrl = StockUrl.module('Main');

    model.Models.NomenclatureRowModel = baseModule.Models.BaseModel.extend({
        url: stockUrl.SaveStockNomenclature,
        defaults: {
            Id: 0,
            Name: '',
            ChildNomenclatures: [],
            ParentId: 0
        }
    });

})(Stock.module('Stock.Menu'));