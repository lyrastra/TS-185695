/* eslint-disable func-names, no-param-reassign */
/* global _, Common */
(function(common) {
    common.Data.Applications = {
        Money: {
            Url: `/Business`,
            Code: 1
        },

        Sales: {
            Url: `/AccDocuments/Sales`,
            Code: 4,

            Pages: {
                Closing: 2,
                Office: 3
            }
        },

        Buy: {
            Url: `/AccDocuments/Buy`,
            Code: 5
        },

        Bank: {
            Url: `/App/Bank`,
            Code: 6
        },

        Analytics: {
            Url: `/Rpt/AnalyticsTurnover`,
            Code: 7
        },

        Cash: {
            Url: `/App/Cash`,
            Code: 8
        }
    };

    _.each(common.Data.Applications, (app, key) => {
        app.toString = function() {
            return key;
        };
    });
}(Common));
