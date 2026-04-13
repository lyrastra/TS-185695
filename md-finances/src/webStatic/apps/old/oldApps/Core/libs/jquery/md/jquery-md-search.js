(function ($) {

    var settings = {
        text: 'поиск',
        searchCallback: function () { },
        canselCallback: function () { }
    };

    var ENTERKEYCODE = 13;

    $.fn.mdSearchLine = function (options) {
        $.extend(settings, options);
        return new searchObject(this);
    };

    var searchObject = function (parent) {

        $(parent).append(getHtml());

        var self = this,
            parentId = $($(parent).get(0)).attr('id'),
            input = $('#' + parentId + ' #MdSearchLine #searchInput'),
            icon = $('#' + parentId + ' #MdSearchLine .searchIcon');

        bindEvent();

        function bindEvent() {
            icon.on('click.MdSearch', faidInInput);
            input.on('keypress.MdSearch', searchEvent);
            input.on('click.MdSearch', inputFocus);
            input.on('blur.MdSearch', outFocus);
            input.on('keyup.MdSearch', checkForEmptyLineAndSearch);
        }

        function getHtml() {
            return '<div id="MdSearchLine" class="searchLineInput"><input type="text" id="searchInput" class="searchInput defaultText" value="' + settings.text + '"/><div class="searchIcon searchIcon-search"></div></div>';
        }

        function faidInInput(e) {
            var element = e.target || e.toElement || e.srcElement;
            var $el = $(element);
            var flag = $el.hasClass('searchIcon-search');

            if (flag) {
                setSearchInput();
            } else {
                setDefaultSearchInput();
                canselTrigger(getSearchInputVal());
            }
        }

        function inputFocus() {
            if (!input.hasClass('defaultText')) {
                return;
            }

            input.removeClass('defaultText');
            clearSearchInput();
            input.focus();
        }
        
        function checkForEmptyLineAndSearch() {
            var val = getSearchInputVal();
            if (isEmptyString(val) && isCancelSearchIcon()) {
                setCanselSearch();
                canselTrigger(val);
            }
        }
        
        function isEmptyString(value) {
            var trimmedValue = value.replace(/^\s+|\s+$/g, '');
            return trimmedValue === '';
        }
        
        function isCancelSearchIcon() {
            return input.next().hasClass('searchIcon-cansel');
        }

        function outFocus() {
            var val = getSearchInputVal();
            if (icon.hasClass('searchIcon-search') && (val == '' || val == ' ')) {
                setDefaultSearchInput();
            }
        }

        function setSearchInput() {
            if (input.hasClass('defaultText')) {
                return;
            }

            setSearchCansel();
            searchTrigger(getSearchInputVal());
        }

        function setDefaultSearchInput() {
            clearSearchInput();
            setCanselSearch();
            defaultInput();
        }

        function searchEvent(e) {
            var key = e.keyCode || e.which;            
            if (key == ENTERKEYCODE) {
                if (input.hasClass('defaultText')) {
                    return;
                }

                setSearchCansel();
                searchTrigger(getSearchInputVal());
            }
        }

        function searchTrigger(val) {
            settings.searchCallback(val);
        }

        function canselTrigger(val) {
            settings.canselCallback(val);
        }

        function getSearchInputVal() {
            if (input.hasClass('defaultText')) {
                return null;
            }

            return input.val();
        }

        function defaultInput() {
            input.addClass('defaultText');
            input.val(settings.text);
        }

        function clearSearchInput() {
            input.val('');
        }

        function setSearchCansel() {
            icon.removeClass('searchIcon-search');
            icon.addClass('searchIcon-cansel');
        }

        function setCanselSearch() {
            icon.removeClass('searchIcon-cansel');
            icon.addClass('searchIcon-search');
        }

        self.setWatermarkText = function(text) {
            settings.text = text;
            defaultInput();
        };

        return self;

    };

})(jQuery);

(function ($) {

    $.fn.mdSearchString = function (options) {

        var option = $.extend(settings, options);

        return this.each(function () {
            return new searchString($(this), option);
        });

    };

    var KEY = {
        ENTER: 13,
        ESC: 27
    };

    var settings = {
        text: '',
        searchCallback: function () { },
        canselCallback: function () { }
    };

    var html = {
        wrap: '<div class="mdSearchStringInputWrap searchLineInput"></div>',
        icon: '<div class="searchIcon searchIcon-search"></div>'
    };

    var searchString = function ($el, opt) {

        var $wrap = $(html.wrap),
            icon, input = $el;

        function init() {
            setInput(opt.text);

            $wrap.css({ width: 'auto' });
            $el.wrap($wrap);
            $el.after(html.icon);

            icon = $el.next();

            bindEvents();
        }

        function setInput(text) {

            input.addClass('searchInput');
            input.addClass('defaultText');

            input.val(text);
            input.css({ 'z-index': 0 });

        }

        function bindEvents() {
            icon.on('click.MdSearch', faidInInput);
            input.on('keypress.MdSearch', searchEvent);
            input.on('click.MdSearch', inputFocus);
            input.on('blur.MdSearch', outFocus);
        }

        function faidInInput(e) {
            var element = e.target || e.toElement || e.srcElement;
            var $currentEl = $(element);
            var flag = $currentEl.hasClass('searchIcon-search');

            if (flag) {
                setSearchInput();
            } else {
                setDefaultSearchInput();
                canselTrigger(getSearchInputVal());
            }
        }

        function searchEvent(e) {
            var key = e.keyCode || e.which;

            if (key == KEY.ENTER) {
                if (input.hasClass('defaultText')) {
                    return;
                }

                setSearchCansel();
                searchTrigger(getSearchInputVal());
            }
        }

        function inputFocus() {
            if (!input.hasClass('defaultText')) {
                return;
            }

            input.removeClass('defaultText');
            clearSearchInput();
            input.focus();
        }

        function outFocus() {
            var val = getSearchInputVal();
            if (icon.hasClass('searchIcon-search') && (val == '' || val == ' ')) {
                setDefaultSearchInput();
            }
        }

        function setSearchInput() {
            if (input.hasClass('defaultText')) {
                return;
            }

            setSearchCansel();
            searchTrigger(getSearchInputVal());
        }

        function setDefaultSearchInput() {
            clearSearchInput();
            setCanselSearch();
            defaultInput();
        }

        function searchTrigger(val) {
            settings.searchCallback(val);
        }

        function canselTrigger(val) {
            settings.canselCallback(val);
        }

        function getSearchInputVal() {
            if (input.hasClass('defaultText')) {
                return null;
            }

            return input.val();
        }

        function defaultInput() {
            input.addClass('defaultText');
            input.val(settings.text);
        }

        function clearSearchInput() {
            input.val('');
        }

        function setSearchCansel() {
            icon.removeClass('searchIcon-search');
            icon.addClass('searchIcon-cansel');
        }

        function setCanselSearch() {
            icon.removeClass('searchIcon-cansel');
            icon.addClass('searchIcon-search');
        }

        init();
    };
    
})(jQuery);