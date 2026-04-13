(function(main) {

    main.BuyStore = {};
    main.Buy = {
        Routers: {},
        Views: {
            Documents: {},
            Filters: {},
            Table: {},
            Controls: {}
        },
        Models: {
            Documents: {},
            Filters: {}
        },
        Collections: {
            Documents: {},
            PostingsAndTax: {}
        },
        Data: {},
        Utils: {},
        Mixins: {},
        
        module: function() {
            return function() {
                return main.Buy;
            };
        }()
    };

})(window);