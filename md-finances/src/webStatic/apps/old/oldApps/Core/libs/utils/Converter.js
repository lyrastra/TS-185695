import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

/* eslint-disable */
(function($, md) {
    'use strict';

    md.Utils.Converter = window.Converter = {
        /// return false if cannot be parsed
        toFloat: function(str, dec) {
            var result;

            if (_.isNumber(str)) {
                return str;
            }

            if (_.isString(str)) {
                str = str.replace(/\s/g, '')
                    .replace(',', '.');

                if (!str.length) {
                    return false;
                }
                var regexp = /^-?(\d)*([.,]\d*)?$/;
                if (regexp.test(str)) {
                    result = (dec) ? +parseFloat(str).toFixed(dec) : parseFloat(str);
                    return _.isNaN(result) ? false : result;
                }
                return false;
            }
            return false;
        },

        toAmountString: function(number, rightOfDecimalPoint) {
            var _number = window.Converter.toFloat(number);

            if (_.isUndefined(rightOfDecimalPoint)) {
                rightOfDecimalPoint = 2;
            }

            if (_.isNumber(_number) && !_.isNaN(_number)) {
                number = _number.toFixed(rightOfDecimalPoint);
                return $().number_format(number, {
                    decimalSeparator: ',',
                    thousandSeparator: ' ',
                    numberOfDecimals: rightOfDecimalPoint
                });
            }
            return number;
        },

        /// return false if cannot be parsed
        toInteger: function(str) {
            if (_.isNumber(str)) {
                if (parseInt(str) == str) {
                    return str;
                }
                return false;
            }
            if (_.isString(str)) {
                str = str.replace(/\s/g, '');
                if (!str.length) {
                    return false;
                }
                var intRegexp = /^-?(\d*)$/;
                if (intRegexp.test(str)) {
                    return parseInt(str, 10);
                }
                return false;
            }
            return false;
        },

        /// return null if cannot be parsed
        toDate: function(str) {
            if (_.isDate(str)) {
                return str;
            }

            if (!_.isString(str)) {
                return null;
            }

            try {
                if (/\/Date\((-?\d+)\)\//.test(str)) {
                    var newDate = parseInt(str.replace(/[^0-9]/g, ''), 10);
                    return new Date(newDate);
                } else {
                    var date = dateHelper(str, 'DD.MM.YYYY').toDate();
                    return date;
                }
            } catch(e) {
                return null;
            }
        },

        toFixed: function(number, precision) {
            var multiplier = Math.pow(10, precision);
            return Math.round(number * multiplier) / multiplier;
        },

        MoneyToString: function(val, fixedCount) {
            var fixed = fixedCount || 2;
            if (_.isNumber(val)) {
                if (val % 1 !== 0) {
                    return val.toFixed(fixed).replace('.', ',');
                } else {
                    return val.toString();
                }
            }
            return val;
        },

        FormatUrl: function(url) {
            if (url) {
                return url.replace('~', '/biz');
            }
            return '';
        }
    };

})(window.jQuery, Md);
