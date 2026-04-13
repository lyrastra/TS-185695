(function ($) {

    $.fn.kbkAutocomplete = function (options) {
        var defaultSettings, autocomplete;

        defaultSettings = $.extend({
            addLink: false,
            onlyFromList: false
        }, options);

        autocomplete = new window.mdAutocomplete({
            url: '/Accounting/Subcontos/GetKbkAutocomplateBySubconto',
            el: $(this),
            className: 'projectAutocomplete',
            onSelect: defaultSettings.onSelect,
            onBlur: defaultSettings.onBlur,
            data: defaultSettings.getData,
            settings: defaultSettings
        });

        return autocomplete;
    };

})(jQuery);