(function (stockModule) {
    stockModule.Helpers.BaseValidationHelper = {
        notNullorEmpty: function (data, args) {
            var errorMsg = args.msg || 'обязательно для заполнения';

            if (data === undefined || data === null || !data.toString().match(/\S/)) {
                return errorMsg;
            }
            
            var d = data.toString().replace(new RegExp(' ', 'g'), '');
            if (d === '') {
                return errorMsg;
            }
            
            return true;
        },

        isDigit: function (data, args) {
            var digit = data.toString().replace(',', '.');

            return (stockModule.Helpers.BaseValidationHelper.notNullorEmpty(digit, {}) !== true
                || isNaN(digit) ? args.msg || 'требуется число' : true);
        },

        notNegative: function (data, args) {
            return (stockModule.Helpers.BaseValidationHelper.isDigit(data, {}) !== true
                || data < 0 ? args.msg || 'требуется число >= 0' : true);
        },
        
        isPositive: function (data, args) {
            data = data.toString().replace(new RegExp(' ', 'g'), '');
            return (stockModule.Helpers.BaseValidationHelper.isDigit(data, {}) !== true
                || data <= 0 ? args.msg || 'требуется число > 0' : true);
        },

        length: function (data, args) {
            var valid = true;
            var len = (data || '').toString().length;
            var msg = '';
            if (args.min) {
                valid = len >= args.min;
                msg = 'длина должна быть больше ' + (args.min - 1);
            }
            if (args.max) {
                valid = len <= args.max;
                msg = 'длина должна быть меньше ' + (args.max + 1);
            }
            if (args.eq) {
                valid = len == args.eq;
                msg = 'длина должна быть равна ' + args.eq;
            }
            return valid || args.msg || msg;
        },
        
        notEqual: function(data, args) {
            var eqVal = args.val;
            
            if (eqVal === undefined || eqVal === null || eqVal === '') {
                throw 'BaseValidationHelper function notEqual: need args.val data';
            }

            if (typeof eqVal === 'function') {
                eqVal = eqVal();
            }

            return data !== eqVal || args.msg || 'значение не может быть равно ' + eqVal;
        },
        
        noMoreField: function (data, args) {
            var vField = parseFloat(data.toString().replace(',', '.')),
                rField = parseFloat(this.get([args.relationField]));

            return vField <= rField ? true : args.msg;
        },

        isDigitMask: function (data, args) {
            if (_.isNumber(data)) {
                return true;
            }

            var number = parseInt(data.toString().replace(new RegExp(' ', 'g'), ''), 10);

            return _.isNumber(number);
        },
        
        callFunc: function(func) {
            return func();
        },
        
        checkFlagClosedPeriod: function (data, args) {
            return data ? args.msg : true;
        }
    };
})(Stock);