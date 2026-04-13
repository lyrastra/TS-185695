import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

/* eslint-disable */
(function (cash) {

    cash.Models.DownloadReportModel = cash.Models.BaseApplicationModel.extend({
        url: Cash.Data.HasCashOperationInPeriod,

        initialize: function (options) {
        },

        defaults: {
            StartDate: dateHelper().format('DD.MM.YYYY'),
            EndDate: dateHelper().format('DD.MM.YYYY')
        },

        checkCashOperation: function (data, callback) {
            this.fetch({
                error: function (url, response) {
                    callback(response);
                },
                success: function (url, response) {
                    callback(response);
                },
                data: data
            });
        }
    });
})(Cash);

