/*
* 0.1 версия скрипта для разворачивания составной кнопки с списком действий
* работает на готовой верстке
* */

(function ($) {

    $.fn.mdButtonGroup = function (options) {

        options = $.extend({}, options);

        var openDropDown = function (command, $ul) {
                if (command === 'open') {
                    $ul.show();
                } else {
                    $ul.hide();
                }
            },
            activate = function ($toggle, $ul) {
                $toggle.addClass('active');
                $toggle.data('toggle', 'on');
                openDropDown('open', $ul);
            },
            diactivate = function ($toggle, $ul) {
                $toggle.removeClass('active');
                $toggle.data('toggle', 'off');
                openDropDown('close', $ul);
            };

        return this.each(function () {
            var $button = $(this),
                $toggle = $button.find('.dropdown-toggle'),
                $ul = $button.find('.dropdown-menu');

            $toggle.on('click', function () {
                var toggleState = $toggle.data('toggle');

                if (toggleState === 'off') {
                    activate($toggle, $ul);

                    $(document).on('click.mdButtonGroup', function(event) {
                        if ($(event.target).closest(".mdButton-group").length) return;
                        $ul.hide();
                        diactivate($toggle, $ul);
                        event.stopPropagation();
                    });
                } else {
                    diactivate($toggle, $ul);
                    $(document).off('click.mdButtonGroup');
                }
            });
        });

    };

})(jQuery);