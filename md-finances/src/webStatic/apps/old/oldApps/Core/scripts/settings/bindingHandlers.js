(function (stickit) {

    'use strict';

    stickit.addHandler({
        selector: 'select',
        initialize: function ($el, model, options) {
            $el.mdSelectUls();

            model.on('change:' + options.observe, function() {
                $el.change();
            });
        }
    });
    
    stickit.addHandler({
        selector: 'input[data-type=integer]',
        getVal: function ($el) {
            var val = Converter.toInteger($el.val());
            return val === false ? $el.val() : val;
        }
    });

    stickit.addHandler({
        selector: 'input[data-type=float]',
        getVal: function ($el) {
            var val = Converter.toFloat($el.val());
            return val === false ? $el.val() : val;
        }
    });
    
    stickit.addHandler({
        selector: '[data-type=integer]',
        onGet: function (val, options) {
            return Common.Utils.Converter.toAmountString(val, 0);
        }
    });

    stickit.addHandler({
        selector: '[data-type=float]',
        onGet: function (val, options) {
            return Common.Utils.Converter.toAmountString(val);
        }
    });

})(Backbone.Stickit);