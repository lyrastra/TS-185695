(function ($) {
    $.scrollFollow = function (box, option) {
        box = $(box);

        // Начальные условия блока
        var startPosition = box.offset().top;
        var heightBox = box.height();
        var boxId = box.get(0).id;

        var parent = box.parent().get(0);
        var parentStartHeight = parent.offsetHeight;

        // устанавливаем к какой части будет приклеиваться блок
        var sideSticky = option.sticky == 'top' ? true : false;

        function scrollWindow() {
            var ver = parseInt($.browser.version);
            if (!($.browser.msie && ver < 8)) {
                if (sideSticky) topSticky();
                else bottomSticky();
            }
        }

        function topSticky() {
            
            var parentContent = box.parent().offset().top; // отступ сверху от родительского дива
            var windowScroll = jQuery(window).scrollTop() + option.topPosition; // величина прокрутки
            var parentHeight = parent.offsetHeight;
            var stickyHeight = box.height();
            if ((windowScroll > parentContent) && ((parentHeight + parentContent) > (windowScroll + stickyHeight))) {
                box.css({ position: 'fixed', top: option.topPosition });
                if (option.isTableHeader && $("#moulage-sticky-block").length == 0) {
                    box.before("<div id=\"moulage-sticky-block\" style=\"height: " +
                                   (option.moulageStickyBlockHeight !== 0 ? option.moulageStickyBlockHeight : stickyHeight) +
                               "px\">&nbsp</div>");
                }
            } else if (windowScroll < parentContent) {
                if (option.isTableHeader) {
                    $("#moulage-sticky-block").remove();
                }
                box.css({ position: 'relative', top: '0px' });
            } else if ((parentHeight + parentContent) < (windowScroll + stickyHeight)) {
                if (option.isTableHeader) {
                    $("#moulage-sticky-block").remove();
                }
                box.css({ position: 'relative', top: '0px' });
            }
        }

        function bottomSticky() {
            var windowTop = jQuery(window).scrollTop();
            var windowHeight = jQuery(window).height();

            // Начальные условия родительского контейнера
            var parentTop = parent.offsetTop;
            var parentHeight = parent.offsetHeight;

            var windowPosition = windowTop + windowHeight - option.correctionPosition;
            var bottomLineBox = startPosition + heightBox;
            var parentBottomLine = parentTop + parentHeight;

            if (windowPosition > parentTop + heightBox && windowPosition < parentBottomLine) {
                if ($("#moulagebottom-sticky-block").length == 0) {
                    box.before("<div id=\"moulagebottom-sticky-block\" style=\"height: " + heightBox + "px;\">&nbsp</div>");
                }
                box.css({ position: 'fixed', top: 'auto', bottom: option.bottomPosition });
                box.find(".linkDoc").show();
                box.addClass("diaphanous-incriment-three-quarters");
            } else if (windowPosition >= bottomLineBox) {
                if ($("#moulagebottom-sticky-block").length > 0) {
                    $("#moulagebottom-sticky-block").remove();
                }
                box.css({ position: 'relative', top: 'auto', bottom: '0px' });
                box.find(".linkDoc").hide();
                box.removeClass("diaphanous-incriment-three-quarters");
            }
        }

        function setMulageBlock() {
            var mulageId = "mulage-sticky-block-" + boxId;
            var jqMulageId = "#" + mulageId;

            if ($(jqMulageId).length != 0) {
                $(jqMulageId).remove();
            } else {
                box.before("<div id='" + mulageId + "' style='height: " + heightBox + "px;'></div>");
            }
        }

        $(window).bind("scroll.scrollFollow", scrollWindow);

        if (sideSticky) {
            topSticky();
        } else {
            bottomSticky();
        }
    };

    jQuery.fn.mdStickyscroll = function (settings) {
        var option = {
            sticky: 'top',
            topPosition: 0,
            isTableHeader: false,
            bottomPosition: '0px',
            correctionPosition: 0,
            moulageStickyBlockHeight: 0
        };

        this.each(function () {
            if (settings)
                $.extend(option, settings);

            new $.scrollFollow(this, option);
        });

        return this;
    };
})(jQuery);