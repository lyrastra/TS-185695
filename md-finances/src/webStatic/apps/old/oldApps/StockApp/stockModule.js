(function (main) {
    
    main.Stock = {
        Routers: {},
        Views: {},
        Models: {
            Filters: {}
        },
        Collections: {},
        Components: {},
        Data: {},
        Helpers: {},
        Events: {},
        Mixin: {},
        Product: {
            Models: {},
            Collections: {},
            Views: {},
            Data: {}
        },
        Operation: {
            Models: {},
            Collections: {},
            Views: {},
            Data: {}
        },
        Templates: {},

        module: function () {
            return function () {
                return main.Stock;
            };
        }()
    };

    main.StockStore = {};

    main.StockUrl = {
        module: function () {
            return function () {
                return main.StockUrl;
            };
        }()
    };
    
})(window);