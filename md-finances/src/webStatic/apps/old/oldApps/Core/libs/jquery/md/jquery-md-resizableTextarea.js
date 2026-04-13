(function($) {

    $.fn.resizableTextarea = function(options) {
        if (!$(this).length) {
            return this;
        }

        defaultRowsSet.call(this);

        this.on("keydown focusin blur", resizeTextarea);
        resize(this);

        function defaultRowsSet() {
            var rows = 1;

            if (options && options.rows) {
                rows = parseInt(options.rows, 10);
            }
            this.attr("rows", rows);
        }

        function resizeTextarea(event) {
            _.defer(function() {
                resize($(event.target));
            });
        }

        function resize($control) {
            var lineHeight = parseInt($control.css("line-height"));
            if (_.isNaN(lineHeight)) {
                lineHeight = 20;
            }
            $control.css("height", "");
            $control.attr("rows", 1);
            var rowCount = Math.floor(($control[0].scrollHeight) / lineHeight);
            var height = rowCount * lineHeight > 0 ? rowCount * lineHeight : lineHeight;

            $control.attr("rows", rowCount);
            // IE не расчитывает сам высоту по line-height, к тому же в реквизитах нужен !important
            $control.attr("style", $control.attr("style") + "; height: " + height + "px !important");
        }

        return this;
    };

})(jQuery);
