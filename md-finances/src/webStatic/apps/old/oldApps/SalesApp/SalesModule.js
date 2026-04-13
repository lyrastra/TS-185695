window.SalesStore = {};

window.Sales = {
    Views: {
        Dialogs: {},
        Main: {},
        Table: {
            Operations: {}
        },
        Documents: {
            Kontragents: {},
            Actions: {}
        },
        Filters: {},
        Mixins: {}
    },
    Models: {
        Dialogs: {},
        Main: {},
        Documents: {
            Kontragents: {}
        },
        Filters: {}
    },
    Collections: {
        Dialogs: {}
    },
    Routers: {},
    Data: {},
    Mixins: {},
    module: function () {
        return function () {
            return Sales;
        };
    }()
};

Md.Components = _.extend(new Backbone.Marionette.Application(), Md.Components);