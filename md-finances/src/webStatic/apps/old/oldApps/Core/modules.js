(function(window) {
    'use strict';
    var md = {
        Converters: {
            Nds: {}
        },
        Core: {
            Options: {},
            Engines: {},
            Templates: {},
            Page: {},
            Helpers: {},
            Components: {}
        },

        Components: {},
        Controls: {},
        Functions: {},

        Extensions: {},
        Page: {},

        Behaviors: {},

        /**
         * data for core & applications
         */
        Data: {
            Enums: {},
            Preloading: {
                Requisites: {},
                TaxationSystems: {}
            }
        },
        Applications: {},

        /**
         * private office workspace user and data to work correctly account
         */
        WorkSpace: {},
        Account: {},
        Utils: {}
    };

    if (window.Md) {
        window.Md = $.extend(true, window.Md, md);
    } else {
        window.Md = md;
    }

    window.Core = window.Md.Core;

    window.Md.Main = window.Main = {
        Views: {
            Main: {},
            Masters: {
                MoneyBalances: {}
            }
        },
        Models: {
            Main: {},
            Masters: {
                MoneyBalances: {}
            }
        },
        Collections: {},
        Routers: {},

        module: function() {
            return function() {
                return Main;
            };
        }()
    };

    window.Md.Money = window.Money = {
        Views: {
            Dialogs: {
                Incoming: {},
                Movement: {},
                Outgoing: {},
                Sales: {},
                Documents: {}
            },
            Main: {
                Discharge: {},
                Operations: {}
            }
        },
        Mixins: {
            Table: {},
            Dialogs: {}
        },
        Services: {},
        Helpers: {},
        Controls: {},
        Components: {},
        Models: {
            Dialogs: {
                Incoming: {},
                Movement: {},
                Outgoing: {},
                Documents: {}
            },
            Main: {
                Discharge: {},
                Operations: {}
            },
            Common: {}
        },
        Collections: {
            Common: {},
            Dialogs: {
                Documents: {}
            }
        },
        Routers: {},
        Tests: {},
        Data: {},
        Enums: {},
        module: function() {
            return function() {
                return Money;
            };
        }()
    };

    window.Md.Contract = window.Contract = {
        Views: {
            Main: {}
        },
        Models: {

            Main: {},
            Common: {}
        },

        module: function() {
            return function() {
                return Contract;
            };
        }()
    };

    window.Md.Bills = window.Bills = {
        Views: {
            Main: {}
        },
        Models: {
            Main: {},
            Common: {}
        },
        module: function() {
            return function() {
                return Bills;
            };
        }()
    };
})(window);