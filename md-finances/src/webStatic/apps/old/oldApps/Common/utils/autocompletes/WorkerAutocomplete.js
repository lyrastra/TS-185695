(function ($) {
    
    $.fn.workerAutocomplete = function (options) {
        var defaultSettings, autocomplete;
        defaultSettings = $.extend({
            addLink: false
        }, options);

        autocomplete = new mdSaleAutocomplete({
            url: options.url || '/Payroll/Workers/GetWorkersAutocomplete',
            el: $(this),
            className: 'workerAutocomplete',
            onSelect: defaultSettings.onSelect,
            onBlur: defaultSettings.onBlur,
            data: defaultSettings.getData,
            settings: defaultSettings
        });

        if (options.parse) {
            autocomplete.parse = options.parse;
        }

        return autocomplete;
    };

})(jQuery);