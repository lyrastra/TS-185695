(function ($) {

    $.fn.primaryDocumentsFilter = function (options) {
        var dropDownList = [
            {
                Name: "Искать везде"
            },
            {
                Name: "В приходных кассовых ордерах",
                Id: "DirectionType",
                Value: '2'
            },
            {
                Name: "В расходных кассовых ордерах",
                Id: "DirectionType",
                Value: '1'
            },
            {
                Name: 'Все непроведенные',
                Id: 'ProvideInAccounting',
                Value: false
            }
        ];

        options = options || {};

        var filter = new mdFilter({
            el: $(this),
            settings: {
                shortFilterList: dropDownList
            },
            onSearch: options.onSearch,
            ShowExtendedFilter: options.ShowExtendedFilter
        });
    };
    
}(jQuery));
