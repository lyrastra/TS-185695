/* eslint-disable */
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

(function (common) {

    var newDate = function(year, month, day) {
        return new Date(Date.UTC(year, month, day));
    };

    common.Filters.DocumentsDateFilterModel = Backbone.FilterObject.BaseFilterObject.extend({
        localStorage: new Store('DocumentsDateFilterModel'),

        defaults: {
            Filter: {},
            State: {}
        },

        initialize: function (options) {
            this.setDefaultValue();
        },

        setDefaultValue: function() {
            var today = new Date();

            this.todayYear = today.getFullYear();
            this.quarter = this.getQuarter(today);

            this.set({
                "Filter": { YearFilter: this.todayYear },
                "State": { Type: Enums.TimeFilterTypes.Year }
            });

            this.filterDefaults = { YearFilter: this.todayYear };
            this.stateDefaults = { Type: Enums.TimeFilterTypes.Year };
        },

        getQuarter: function (date) {
            var todayMonth = date.getMonth() + 1;

            if (todayMonth <= 3) {
                return 1;
            }
            else if (todayMonth > 3 && todayMonth <= 6) {
                return 2;
            }
            else if (todayMonth > 6 && todayMonth <= 9) {
                return 3;
            }
            else if (todayMonth > 9) {
                return 4;
            }
        },

        getStartDate: function () {
            var type = this.get("State").Type,
                filter = this.get("Filter");

            switch (type) {
                case Enums.TimeFilterTypes.Year:
                    return newDate(filter.YearFilter, 0, 1);

                case Enums.TimeFilterTypes.Month:
                    return newDate(filter.MonthFilter.Year, filter.MonthFilter.Month - 1, 1);

                case Enums.TimeFilterTypes.Quarter:
                    var month = 3 * (filter.QuarterFilter.Quarter - 1);
                    return newDate(filter.QuarterFilter.Year, month, 1);

                case Enums.TimeFilterTypes.Custom:
                    return filter.StartDate ? Converter.toDate(filter.StartDate) : newDate(1900, 0, 1);

                default:
                    return newDate(1900, 0, 1);
            }
        },

        getFinalDate: function () {
            var type = this.get("State").Type;
            var filter = this.get("Filter");

            switch (type) {
                case Enums.TimeFilterTypes.Year:
                    return newDate(filter.YearFilter, 11, 31);

                case Enums.TimeFilterTypes.Month:
                    return newDate(filter.MonthFilter.Year, filter.MonthFilter.Month, 0);

                case Enums.TimeFilterTypes.Quarter:
                    var month = 3 * (filter.QuarterFilter.Quarter - 1) + 2;
                    return newDate(filter.QuarterFilter.Year, month + 1, 0);

                case Enums.TimeFilterTypes.Custom:
                    return filter.FinalDate ? Converter.toDate(filter.FinalDate) : newDate(3000, 0, 1);

                default:
                    return newDate(3000, 0, 1);
            }
        },

        getDateMomentObject: function (dateString, format) {
            return dateHelper(dateString, format);
        }
    });

})(Common.module("Filters"));
