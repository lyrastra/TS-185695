(function($) {
    'use strict';

    $.fn.customselect = function(callback) {
        return new CustomSelect(this, callback);
    };

    function CustomSelect(elem, callback) {
        this.element = $(elem);
        this.callback = callback;
        this.isFirst = true;
        this.__construct();
    }

    CustomSelect.prototype.__construct = function() {
        var context = this;

        this.select = this.element.find('select');
        this.value = this.element.find('.custom-select__value');
        this.select.bind('change keydown keypress keyup', function() {
            context.update.call(context);
        });

        this.update();
    };

    CustomSelect.prototype.update = function() {
        var selectedValue = this.select.val(),
            selectedOptionText = this.select.find('option[value="' + selectedValue + '"]').text();

        this.value.html(selectedOptionText || selectedValue);

        if (this.callback && !this.isFirst) {
            this.callback.call();
        }
        this.isFirst = false;
    };
})(jQuery);

