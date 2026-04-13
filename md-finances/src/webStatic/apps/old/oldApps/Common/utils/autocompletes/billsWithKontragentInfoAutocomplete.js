/* eslint-disable */
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

(function ($) {

    $.fn.billsAndKontragenteIngoAutocomplete = function (options) {
        var defaultSettings, autocomplete;

        defaultSettings = $.extend({
            addLink: false
        }, options);

        autocomplete = new mdSaleAutocomplete({
            url: WebApp.Bills.GetBillWithKontragentAutocomplete,
            el: $(this),
            className: 'billAndKontragentAutocomplete',
            onSelect: defaultSettings.onSelect,
            onBlur: defaultSettings.onBlur,
            data: defaultSettings.getData,
            settings: defaultSettings
        });

        autocomplete.parse = function (data) {
            return $.map(data, function (item) {
                var number = item.Number.length ? "№ " + item.Number : "б/н",
                    date = Converter.toDate(item.Date),
                    label;

                if (date) {
                    date = dateHelper(date).format('DD.MM.YYYY');
                    date = date.length ? "от " + date : "";
                }

                label = [number, date].join(" ");

                return { label: label, value: label, object: item };
            });
        };

        return autocomplete;
    };

})(jQuery);
