(function ($) {

    $.fn.cashOrderAutocomplete = function (options) {
        var defaultSettings, autocomplete;

        defaultSettings = $.extend({}, options);

        autocomplete = new mdSaleAutocomplete({
            url: options.isIncomeCashOrder ? WebApp.CashOrder.GetCashOrderIncomeFromCashAutocomplete : WebApp.CashOrder.GetCashOrderRemoveFromSettlementAccountAutocomplete,
            el: $(this),
            className: 'cashOrderAutocomplete',
            onSelect: defaultSettings.onSelect,
            onBlur: defaultSettings.onBlur,
            data: defaultSettings.getData,
            settings: defaultSettings
        });

        autocomplete.parse = function (data) {
            return $.map(data, function (item) {
                var number = item.Number,
                    name = "№ " + number,
                    label = name + " от " + item.Date;

                return { label: label, value: item.Id, object: item };
            });
        };

        return autocomplete;
    };

})(jQuery);