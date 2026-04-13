(function (stockModule) {

    stockModule.Models.Filters.StockFilterModel = Backbone.FilterObject.BaseFilterObject.extend({        
        loaclStorage: new Store('StockFilterModel'),
        
        defaults: {
            Sorter: {
                Sort: 'name',
                SortDirection: 1
            },
            Filter: {}
        }
    });

})(Stock);