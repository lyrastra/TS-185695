import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

/* eslint-disable */

(function ($) {
    $.fn.fieldValueConverter = function (settings) {
        settings = $.extend({
        }, settings);

        this.each(function () {
            $(this).on("change.valueConverter", convertValue);
            $(this).on("remove", function () {
                $(this).off(".valueConverter remove");
            });
        });
    };


    function convertValue() {
        var $el = $(this),
                format = $el.data("format"),
                value = $el.val(),
                convertedValue = typeDefinition(format, value);

        $el.val(convertedValue);
    }

    function typeDefinition(format, value) {
        switch (format) {
            case 'int':
                return parseInt(value, 10);
            case 'float':
                return toFloat(value);
            case 'date':
                return toDate(value);
            default:
                return value;
        }
    }

    function toFloat(str) {
        var regexp = /^-?(\d)*([.,]\d*)?$/;
        if (_.isNumber(str)) { return str; }

        if (_.isString(str)) {
            str = str.replace(/\s/g, "").replace(",", ".");
            if (str.length == 0) {
                return "";
            }

            if (regexp.test(str)) {
                var result = parseFloat(str);
                return _.isNaN(result) ? "" : result.toFixed(2).replace(".", ",");
            }
            return "";
        }
        return "";
    }

    /// return false if cannot be parsed
    function toInteger(str) {
        if (_.isNumber(str)) {
            if (parseInt(str) == str) {
                return str;
            }
            return false;
        }
        if (_.isString(str)) {
            str = str.replace(/\s/g, "");
            if (str.length == 0) {
                return false;
            }
            var intRegexp = /^-?(\d*)$/;
            if (intRegexp.test(str)) {
                return parseInt(str, 10);
            }
            return false;
        }
        return false;
    }

    /// return null if cannot be parsed
    function toDate(str) {
        if (_.isDate(str)) {
            return str;
        }

        if (!_.isString(str)) {
            return null;
        }

        try {
            if (/\/Date\((-?\d+)\)\//.test(str)) {
                var newDate = parseInt(str.replace(/[^0-9]/g, ""));
                return new Date(newDate);
            } else {
                var date = dateHelper(str, 'DD.MM.YYYY').toDate();
                return date;
            }
        } catch (e) {
            return null;
        }
    }
})(jQuery);
