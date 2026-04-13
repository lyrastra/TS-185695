(function (components) {

    'use strict';

    var loader;
    
    components.PageLoader = function (options) {
        var defaults = {
            el: $('#page_content')
        };
        options = _.extend(defaults, options);

        this.show = function (message) {
            var el = $(options.el);
            ToolTip.globalMessage(1, true, message || 'Подождите, идет загрузка...', 'endless');
            loader = ToolTip.busyShowing(el, 'mainPageLoader busyShowing');
            loader.siblings().hide();

            return this;
        };
        
        this.hide = function () {
            if (loader) {
                loader.siblings().show();
                loader.remove();
            } 
            ToolTip.globalMessageClose();
        };

        return this;
    };

})(Core.Components);