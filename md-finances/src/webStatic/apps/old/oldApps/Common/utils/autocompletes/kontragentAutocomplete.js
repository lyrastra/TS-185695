(function ($) {

    $.fn.kontragentAutocomplete = function (options) {
        var defaultSettings, autocomplete;

        defaultSettings = $.extend({
            addLink: true
        }, options);

        autocomplete = new mdSaleAutocomplete({
            url: KontragentsApp.Autocomplete.KontragentWithTypeAutocomplete,
            el: $(this),
            className: 'kontragentAutocomplete',
            onSelect: defaultSettings.onSelect,
            onBlur: defaultSettings.onBlur,
            data: defaultSettings.getData,
            settings: defaultSettings
        });

        return autocomplete;
    };

})(jQuery);