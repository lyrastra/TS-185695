/* eslint-disable */
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

(function ($) {

    $.fn.advanceStatementAutocomplete = function (options) {
        var autocomplete = new mdSaleAutocomplete({
            url: WebApp.AccountingAdvanceStatement.GetAccountingAdvanceStatementAutocomplete,
            el: $(this),
            onSelect: options.onSelect,
            data: options.getData,
            settings: $.extend({
                createEventIfEmptyList: false,
                onlyFromList: false
            }, options)
        });

        autocomplete.parse = function (data) {
            return $.map(data, function (item) {
                var number = item.Number.length ? "№ " + item.Number : "б/н",
                    date = Converter.toDate(item.Date),
                    typeName = "Авансовый отчет",
                    label;

                if (date) {
                    date = dateHelper(date).format('D MMM YYYY');
                    date = date.length ? "от " + date : "";
                }

                label = [typeName, number, date].join(" ");

                return { label: label, value: label, object: item };
            });
        };

        autocomplete.onBlur = options.onBlur;
    };

    $.fn.workerWithAdvanceAutocomplete = function (options) {
        var defaultSettings, autocomplete;

        defaultSettings = $.extend({
            addLink: false
        }, options);

        autocomplete = new mdSaleAutocomplete({
            url: WebApp.AccountingAdvanceStatement.GetWorkerWithAdvanceAutocomplete,
            el: $(this),
            className: 'workerAutocomplete',
            onSelect: defaultSettings.onSelect,
            onBlur: defaultSettings.onBlur,
            data: defaultSettings.getData,
            settings: defaultSettings
        });

        return autocomplete;
    };

})(jQuery);
