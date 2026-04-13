(function (stockModule) {

    stockModule.Collections.NomenclatureMenuCollection = stockModule.Collections.BaseCollection.extend({
        model: stockModule.Models.NomenclatureRowModel
    });

})(Stock);