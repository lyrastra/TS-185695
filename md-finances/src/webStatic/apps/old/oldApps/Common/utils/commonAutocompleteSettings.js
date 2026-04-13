(function($) {

    $.fn.mdSaleAutocomplete = function (action, options) {
        var autocomplete = $(this).data("mdSaleAutocomplete");
        if (!autocomplete) {
            return;
        }

        switch (action) {
            case "destroy":
                autocomplete.destroy();
                break;
            case "search":
                autocomplete.search(options);
                break;
        }
    };
    
})(jQuery);