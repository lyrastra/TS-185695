(function ($){
    var defaultSettings = {
        parse: function(val) {
            return val;
        },
        format: function(val) {
            return val;
        },
        validate: function() {
            return true;
        },
        defaultValue: '',
        ignoreAttribute: 'data-collapsible-ignore'
    };

    $.fn.collapsible = function(options) {
        globalEvents();

        return $(this).each(function(index, el) {
            if ($(el)[0].tagName.toLowerCase() === 'input') {
                collapsibleInput($(el), _.extend({}, defaultSettings, options));
            }
        });
    };

    var ui = {
        controls: 'input, textarea, button, select'
    };

    var listenGlobal = false;
    function globalEvents(){
        if(listenGlobal){
            return;
        }

        listenGlobal = true;
        $('body')
            .off('keydown.collapsible')
            .on('keydown.collapsible', ui.controls, onKeydown);
    }

    function onKeydown(event) {
        var key = event.keyCode,
            input = $(event.target);

        if (key === 9) {
            tabNext(input, event);
        }

        if (key === 13) {
            input.focusout().change();
        }
    }

    function tabNext(el, event) {
        var $controls = $(ui.controls),
            current = $controls.index(el),
            next = $controls.filter(':eq(' + (current + 1) + ')');

        var container = next.closest('.collapsible-container');
        if (container.length > 0 && container.is(':visible')) {
            container.find('.collapsible.link').click();
            event.preventDefault();
        }
    }

    function createWrapper($input){
        var container = $input.closest('.collapsible-container');
        if (container.length) {
            return container;
        }
        $input.wrap($('<div class="collapsible-container" />'));
        return $input.closest('.collapsible-container');
    }

    function collapsibleInput($input, inputOptions) {
        var container = createWrapper($input);

        var span = $('<span class="collapsible link" />').click(edit);
        container.append(span);

        $input.on('blur change', updateSpan);
        updateSpan();

        function updateSpan() {
            var val = $.trim($input.val());
            if (val !== inputOptions.parse(val)) {
                $input.val(inputOptions.parse(val)).change();
                return;
            }

            if (!val.length && val !== inputOptions.defaultValue) {
                $input.val(inputOptions.defaultValue).change();
                return;
            }

            if (!val.length || !inputOptions.validate(val, $input)) {
                edit();
                return;
            }

            var text = inputOptions.format($input.val());
            span.text(text).show();
            span.siblings().hide();
        }

        function edit(event) {
            if ($input.attr(inputOptions.ignoreAttribute)) {
                return;
            }
            $input.show();
            span.hide().siblings().show();

            if (event) {
                $input.focus().select();
            }
        }
    }

})(jQuery);