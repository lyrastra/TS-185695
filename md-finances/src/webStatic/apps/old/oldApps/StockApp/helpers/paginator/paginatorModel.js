(function (stockModule) {
    var pageSizes = [20, 50, 100, 300, 500, 'Все'];
    stockModule.Models.PaginatorModel = Backbone.Model.extend({
        defaults: {
            currentPage: 1,
            totalPage: 0,
            totalSize: 0,
            hasNext: false,
            hasPrevious: false,
            pageSizes: pageSizes,
            pageSize: pageSizes[0]
        }
    });
})(Stock);