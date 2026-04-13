(function ($) {
    $.fn.loading = function () {
        $(this).activity({ segments: 11, steps: 3, opacity: 0.3, width: 5, space: 3, length: 10, color: '#0b0b0b', speed: 1.5 });
    };
    
    $.fn.buttonLoading = function () {
        $(this).activity({ segments: 11, steps: 3, opacity: 0.3, width: 2, space: 0, length: 4, color: '#0b0b0b', speed: 1.5, padding: 5, valign: 'center', align: 'left' });
    };
    
    $.fn.disable = function () {
        var overlay = $('<div/>');
        overlay.addClass('disableOverlay');
        
        var offset = $(this).position();
        overlay.css({
            position: 'absolute',
            top: offset.top,
            left: offset.left,
            height: $(this).height(),
            width: $(this).width(),
            'background-color': '#FFFFFF',
            opacity: '0.5',
            'z-index': '900'
        });

        $(this).append(overlay);
    };
    
    $.fn.enable = function () {
        $(this).find('.disableOverlay').remove();
    };
})(jQuery);