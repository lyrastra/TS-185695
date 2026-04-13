/* eslint-disable */

window.Common = (function (common, window) {
    if (!common) {
        common = {
            Utils: {}
        };
    }

    common.Utils.Converter = {
        toAmountString: function(number, rightOfDecimalPoint) {
            var _number = window.Converter.toFloat(number);

            if (_.isUndefined(rightOfDecimalPoint)) {
                rightOfDecimalPoint = 2;
            }

            if (_.isNumber(_number) && !_.isNaN(_number)) {
                number = _number.toFixed(rightOfDecimalPoint);
                return $().number_format(number, { decimalSeparator: ',', thousandSeparator: ' ', numberOfDecimals: rightOfDecimalPoint });
            }
            return number;
        },

        roundFloat: function(floatNumber, rightOfDecimalPoint) {
            if (!floatNumber) {
                return 0;
            }


            if (floatNumber % 1 === 0) {
                return floatNumber;
            }

            return $().number_format(floatNumber, { decimalSeparator: ',', thousandSeparator: '', numberOfDecimals: rightOfDecimalPoint });
        },

        pfrNumber: function (number) {
            if (!number) {
                return "";
            }
            
            number = "" + number;
            number = number.replace(/\D/g, "");
            
            var resultNumber = [
                number.substring(0, 3),
                number.substring(3, 6),
                number.substring(6)
            ];

            return resultNumber.join("-");
        },

        capitaliseFirstLetter: function(string) {
            return string.charAt(0).toUpperCase() + string.slice(1);
        },

        getDateForDocument: function(value) {
            var date = window.Converter.toDate(value),
                format = 'D MMMM', today = new Date();

            if (!_.isDate(date)) {
                return null;
            }

            if (today.getFullYear() != date.getFullYear()) {
                format = 'D MMMM YYYYг.';
            }
            return dateHelper(date).format(format);
        },

        maskStringToFloat: function(value) {
            if (typeof value === 'string') {
                return parseFloat(value.replace(new RegExp(' ', 'g'), '').replace(',', '.'));
            }

            return value;
        },

        convertArabicToRoman: function(number) {
            var result = "";
            var i = ["", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"];
            var x = ["", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC"];
            var c = ["", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM"];
            var m = ["", "M", "MM", "MMM", "MMMM"];
            if (number >= 1 && number <= 4999) {
                result += m[Math.floor(number / 1000)];
                number %= 1000;
                result += c[Math.floor(number / 100)];
                number %= 100;
                result += x[Math.floor(number / 10)];
                number %= 10;
                result += i[number];
            }
            return result;
        },

        round: function(val, decimalPart){
            decimalPart = decimalPart || 2;
            var factor = Math.pow(10, decimalPart);
            return Math.round(val * factor)/factor;
        }
    };


    common.Utils.NdsConverter = {
        toPercent: function(options) {
            var value = window.Converter.toFloat(options.value),
                type = parseInt(options.type, 10),
                typesEnum = options.typeEnum || common.Data.NdsTypes;

            if (!value && !type) { return false; }

            switch (type) {
                case typesEnum.Nds10:
                case typesEnum.Nds110:
                    value = value * 10 / 110;
                    break;
                case typesEnum.Nds18:
                case typesEnum.Nds118:
                    value = value * 18 / 118;
                    break;
                case typesEnum.Nds20:
                case typesEnum.Nds120:
                    value = value * 20 / 120;
                    break;
                case typesEnum.Nds22:
                case typesEnum.Nds22To122:
                    value = value * 22 / 122;
                    break;
                case typesEnum.Nds5:
                case typesEnum.Nds5To105:
                    value = value * 5 / 105;
                    break;
                case typesEnum.Nds7:
                case typesEnum.Nds7To107:
                    value = value * 7 / 107;
                    break;
                case typesEnum.Nds0:
                    value = 0;
                    break;
                case typesEnum.None:
                    value = null;
                    break;
            }

            return _.isNumber(value) ? value.toFixed(2) : value;
        },

        ndsToAccountNds: function (ndsType) {
            var ndsTypes = common.Data.NdsTypes,
                accountNdsType = common.Data.AccountNdsType;

            switch (ndsType) {
                case ndsTypes.None:
                    return accountNdsType.WithoutNds;
                case ndsTypes.Nds0:
                    return accountNdsType.Nds0;
                case ndsTypes.Nds10:
                case ndsTypes.Nds110:
                    return accountNdsType.Nds10;
                case ndsTypes.Nds18:
                case ndsTypes.Nds118:
                    return accountNdsType.Nds18;
            }
        }
    };

    return common;
})(window.Common, window);
