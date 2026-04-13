$.fn.truncateString = function (options) {
    if (!window.mdNew || !mdNew.TruncateStringHelper) {
        console.warn && console.warn("Error. Can't find TruncateStringHelper.");
        return $(this);
    }

    var defaults = {
        maxRowCount: 3,
        ending: '...'
    };
    var settings = $.extend({}, defaults, options);

    return $(this).each(function(index, el){
        var args = {
            $el: $(el),
            maxRowCount: settings.maxRowCount,
            ending: settings.ending
        };
        mdNew.TruncateStringHelper.truncate(args);
    });
};