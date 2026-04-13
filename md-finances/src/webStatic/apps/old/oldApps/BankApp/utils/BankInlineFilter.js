(function ($) {

    $.fn.bankInlineFilter = function (options) {
        options = options || {};

        var filter = new mdFilter({
            el: $(this),
            settings: {
                shortFilterList: _.map(options.list, mapItem)
            },
            onSearch: options.onSearch,
            ShowExtendedFilter: options.ShowExtendedFilter
        });
    };

    function mapItem(description, id){
        var item = { Id: id };
        if(typeof description === 'string'){
            item.Name = description;
            item.Value = '';
        }
        else{
            _.extend(item, description);
        }
        return item;
    }

}(jQuery));
