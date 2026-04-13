import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

/* eslint-disable */
(function($, md) {
    'use strict';

    $.fn.saleBillAutocomplete = function(options) {
        options || (options = {});
        options.settings || (options.settings = {});

        var defaultSettings = {};
        var autocomplete = new mdSaleAutocomplete({
            url: options.url || WebApp.Bills.GetBillsForIncomingAutocomplete,
            el: $(this),
            clasName: 'billSaleAutocomplete',
            onSelect: options.onSelect,
            data: options.getData,
            clean: options.clean,
            settings: _.extend(defaultSettings, options, options.settings)
        });

        autocomplete.parse = function(data) {
            if (data.length == 0 && !options.settings.addLink) {
                return [{ label: 'Счет не найден', value: '', object: { Id: 0, Number: '', New: true}}];
            }
            return $.map(data, function(item) {
                var number = item.Number.length ? '№ ' + item.Number : 'б/н';

                var projectDate = item.Date;
                var date = Converter.toDate(item.Date);
                if (date) {
                    var currentYear = (new Date()).getFullYear();
                    var dateFormat = (currentYear != date.getFullYear()) ? 'D MMMM YYYY' : 'D MMMM';
                    projectDate = dateHelper(date).format(dateFormat);
                }

                var val = projectDate.length ? number + ' от ' + projectDate : number;
                return { label: val, value: item.Number, object: item };
            });
        };

        autocomplete.onCreate = options.onCreate;
    };

}(jQuery, Md));
