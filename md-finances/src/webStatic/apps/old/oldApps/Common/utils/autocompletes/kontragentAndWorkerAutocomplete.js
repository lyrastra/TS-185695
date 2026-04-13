(function ($) {
    
    $.fn.kontragentAndWorkerAutocomplete = function (options) {
        var defaultSettings, autocomplete;

        defaultSettings = $.extend({
            addLink: false
        }, options);

        autocomplete = new mdSaleAutocomplete({
            url: WebApp.Kontragents.GetKontragentsAndWorkersAutocomplete,
            el: $(this),
            className: 'kontragentAndWorkerAutocomplete',
            onSelect: defaultSettings.onSelect,
            onBlur: defaultSettings.onBlur,
            data: defaultSettings.getData,
            settings: defaultSettings
        });

        return autocomplete;
    };

})(jQuery);