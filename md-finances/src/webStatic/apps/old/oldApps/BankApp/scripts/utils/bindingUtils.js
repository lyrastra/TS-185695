(function (stickit) {

    'use strict';

    stickit.addHandler({
        selector: '[data-validation=true]',
        setOptions: {
            validate: true
        }
    });

    stickit.addHandler({
        selector: '[data-number=float]',
        getVal: function ($el) {
            var val = Converter.toFloat($el.val());
            return val === false ? $el.val() : val;
        }
    });

    stickit.addHandler({
        selector: 'input[data-number=float]',
        onGet: function (val, options) {
            return Common.Utils.Converter.toAmountString(val);
        }
    });
    
    stickit.addHandler({
        selector: '[data-type=float]',
        onGet: function (val, options) {
            return Common.Utils.Converter.toAmountString(val);
        }
    });

})(Backbone.Stickit);