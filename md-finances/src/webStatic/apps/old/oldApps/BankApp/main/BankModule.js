window.Bank = {
    Views: {
        Controls: {},
        Dialogs: {},
        Main: {},
        Tools: {},
        Table: {
            Operations:{}
        },
        Documents: {
            Operations: {
                Incoming: {},
                Outgoing: {},
                Memorial: {}
            }
        },
        Filters: {}
    },
    Utils: {},
    Helpers: {},
    Models: {
        Main: {},
        Documents: {
            Operations: {
                Incoming: {},
                Outgoing: {},
                Memorial: {}
            }
        },
        Tools: {},
        Table: {
            Operations:{}
        },
        Filters: {},
        ImportWizard: {},
        PostingsAndTax: {}
    },
    Collections: {
        PostingsAndTax: {}
    },
    Routers: {},
    Mixins: {},
    Behaviors: {},
    PurseImportWizard: {},
    module: function () {
        return function () {
            return Bank;
        };
    }()
};
window.BankStore = {};
window.BankEnums = {};
window.BankUrl = {
    module: function () {
        return function () {
            return BankUrl;
        };
    }()
};

window.BankApp = {
    Views: {},
    Collections: {},
    Models: {},
    Utils: {},
    Controls: {},
    Components: {},

    start: function() {}
};