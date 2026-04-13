/* eslint-disable */
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

(function(root) {

    var request;
    var defaultDocDate = '01.01.2013';

    var firmRequisites = Backbone.Model.extend({
        url: WebApp.Requisites.Get,

        loaded: false,
        isError: false,

        getFromAccount: function() {
            if (window.Account) {
                return window.Account.requisites;
            }
        },

        initialize: function() {
            request = request || this.getFromAccount();

            if (request) {
                if (request.responseText) {
                    this.set(this.parse(JSON.parse(request.responseText)));
                    this.loaded = true;

                    var firstDayYear = dateHelper(this.get("BalanceDate"), "DD.MM.YYYY").startOf('year').format("DD.MM.YYYY");
                    this.set("BalanceDate", firstDayYear);

                    return;
                }

                var model = this;
                $.when(request).done(function(resp) {
                    model.set(model.parse(resp));
                    model.loaded = true;
                });
            }

            var preloaded = this.getPreloadedData();
            if (preloaded) {
                this.set(preloaded);
            }
        },

        getRegistrationDate: function() {
            var value = this.get('RegistrationDate');

            if (!value) {
                value = '01.01.2012';
            }

            return ValueCrusher.convertToDate(value);
        },

        getRegistrationYear: function() {
            return this.getRegistrationDate().getFullYear();
        },

        getMinDocumentDate: function () {
            var isAccounting = this.get('IsAccounting');

            if (isAccounting) {
                return this.getMinDocumentDateForAccounting();
            } else {
                return this.getRegistrationDate();
            }
        },

        getMinDocumentDateForAccounting: function () {
            var defaultDate = ValueCrusher.convertToDate(defaultDocDate);
            var regDate = this.getRegistrationDate();
            var result = regDate.getTime() > defaultDate.getTime() ? regDate : defaultDate;
            return result;
        },

        fetch: function(options) {
            options = options || {};
            var success = options.success;
            var model = this;

            options.success = function(resp, status, xhr) {
                model.loaded = true;
            if (success) {
                    success(resp, status, xhr);
                }
            };

            options.error = function(xhr, ajaxOptions, thrownError) {
                model.loaded = true;
                model.isError = true;
            };

            return Backbone.Model.prototype.fetch.call(this, options);
        },

        load: function(options) {
            options = options || {};

            if (!request) {
                request = this.fetch.apply(this, arguments);
                return request;
            }

            return request.done(options.success);
        },

        reload: function(){
            request = null;
            return this.load();
        },

        inClosedPeriod: function(date) {
            date = Converter.toDate(date);
            if (!date) {
                return false;
            }

            return date <= this.getLastClosedPeriod();
        },

        inClosedPeriodWithBalanceDate: function(date) {
            date = Converter.toDate(date);
            if (!date) {
                return false;
            }

            return date <= this.getLastClosedPeriodWithBalance();
        },

        getFirstOpenPeriodDate: function() {
            var closed = dateHelper(this.getLastClosedPeriodWithBalance());
            return closed.add(1, 'days').format('DD.MM.YYYY');
        },

        getLastClosedPeriod: function() {
            var date = this.get('FinancialResultLastClosedPeriod');
            return Converter.toDate(date);
        },

        getLastClosedPeriodWithBalance: function() {
            var date = this.get('FinancialResultLastClosedPeriod') || this.get('BalanceDate');
            return Converter.toDate(date);
        },

        getFirstOpenDate: function() {
            var closedDate = this.getLastClosedPeriod();
            return new Date(closedDate.getFullYear(), closedDate.getMonth(), closedDate.getDate() + 1);
        },

        getPreloadedData: function() {
            var data = getData('Md.Data.Preloading.Requisites');
            if (data && !isEmptyObject(data)) {
                return data;
            }
            return null;
        }
    });

    if (root.Money) {
        root.Money.Models.Common.FirmRequisites = firmRequisites;
    }

    if (root.Common) {
        root.Common.FirmRequisites = firmRequisites;
    }

    function getData(module) {
        module = module.split('.');

        var data = window;
        while (module.length) {
            data = data[module.shift()];
            if (!data) {
                return null;
            }
        }

        return data;
    }

    function isEmptyObject(obj) {
        for (var prop in obj) {
            if (Object.prototype.hasOwnProperty.call(obj, prop)) {
                return false;
            }
        }
        return true;
    }

})(window);
