(function (common) {

    common.Mixin.FunctionForValidation = {
      
        notNullOrEmpty: function (data, args) {
            var errorObj = createErrorObject(args);

            if(typeof args === 'function'){
                var fundResult = args.call(this);
                if(!fundResult){
                    return returnValidObject(args.field);;
                } else{
                    errorObj = fundResult;
                }
            }


            if (data === undefined || data === null || !data.toString().match(/\S/)) {
                return errorObj;
            }

            var d = data.toString().replace(new RegExp(' ', 'g'), '');
            if (d === '') {
                return errorObj;
            }

            return returnValidObject(args.field);
        },
        
        notZero: function(data, args) {
            var errorObj = createErrorObject(args);

            if (!data) {
                return errorObj;
            }

            var number = data;
            if (typeof data === 'string'){
                number = parseFloat(data.replace(',', '.').replace(' ', ''));
            }

            if (isNaN(number) || number === 0) {
                return errorObj;
            }

            return returnValidObject(args.field);
        },
        
        oneOfWithRelatedFields: function(data, args) {
            var errorObj = createErrorObject(args);

            if (data === null || data === 0) {
                if (args.relatedFields.length) {
                    for (var i = 0, item = args.relatedFields[0]; i < args.relatedFields.length; i++, item = args.relatedFields[i]) {
                        var id = this.get(item);
                        if (id !== 0 && id !== null && id !== '') {
                            return returnValidObject(args.field);
                        } 
                    }
                }

                return errorObj;
            }

            return returnValidObject(args.field);
        },
        
        noMore: function (data, args) {
            var value = common.Utils.Converter.maskStringToFloat(data),
                errorObj = createErrorObject(args),
                noMoreField = common.Utils.Converter.maskStringToFloat(this.get(args.noMoreField));
            
            if (value > noMoreField) {
                return errorObj;
            }

            return returnValidObject(args.field);
        },
        
        noMoreThen: function (data, args) {
            var value = common.Utils.Converter.maskStringToFloat(data);
            var errorObj = createErrorObject(args);
            var noMoreField = common.Utils.Converter.maskStringToFloat(args.noMoreValue);

            if (value > noMoreField) {
                return errorObj;
            }

            return returnValidObject(args.field);
        },
        
        noLess: function (data, args) {
            var value = common.Utils.Converter.maskStringToFloat(data),
                errorObj = createErrorObject(args),
                noLessField = common.Utils.Converter.maskStringToFloat(this.get(args.noLessField));

            if (value < noLessField) {
                return errorObj;
            }

            return returnValidObject(args.field);
        },
        
        selectIsSet: function (data, args) {
            var errorObj = createErrorObject(args);
            
            if (data === undefined || data === '' || data === null) {
                return errorObj;
            }
                
            return returnValidObject(args.field);
        },
        
        isDigit: function (data, args) {
            var errorObj = createErrorObject(args),
                digit = data.toString().replace(',', '.');

            if (isNaN(digit)) {
                return errorObj;
            }

            return returnValidObject(args.field);
        },
        
        isDigitMask: function (data, args) {
            var errorObj = createErrorObject(args),
                digit = data.toString().replace(new RegExp(' ', 'g'), '').replace(',', '.');

            if (isNaN(digit)) {
                return errorObj;
            }

            return returnValidObject(args.field);
        },
        
        length: function (data, args) {
            if (!args || _.isUndefined(args.size)) {
                throw new Error("Parameter size must be defined.");
            }
            
            if ($.trim(data).length != args.size) {
                return createErrorObject(args);
            }

            return returnValidObject(args.field);
        },
        
        number: function (data, args) {
            var val = Converter.toFloat(data);

            if ($.trim(data).length > 0 && !_.isNumber(val)) {
                return { message: args.msg || '', options: args };
            }

            return { valid: true, options: { field: args.field } };
        },
        
        positive: function (data, args) {
            var val = Converter.toFloat(data);

            if (_.isNumber(val) && val < 0) {
                return { message: args.msg || '', options: args };
            }

            return { valid: true, options: { field: args.field } };
        },
        
        isDate: function (data, args) {
            if ($.trim(data).length > 0) {
                var date = Converter.toDate(data);
                if (!_.isDate(date)) {
                    return { message: args.msg || '', options: args };
                }
            }

            return { valid: true, options: { field: args.field } };
        },
        
        firmRegistration: function (data, args) {
            var fn = validateRequisitesDate('RegistrationDate', less);
            return fn.call(this, data, args);
        },

        balanceDate: function (data, args) {
            var fn = validateRequisitesDate('BalanceDate', less);
            return fn.call(this, data, args);
        },

        closedPeriodDate: function (data, args) {
            var fn = validateRequisitesDate('FinancialResultLastClosedPeriod', lessOrEqual);
            return fn.call(this, data, args);
        },
        
        utils: {
            createErrorObject: createErrorObject,
            returnValidObject: returnValidObject
        }
    };

    function validateRequisitesDate(attr, comparator) {
        return function (data, args) {
            if(typeof args === 'function'){
                args = args.call(this, data);
            }

            if(args !== false){
                var date = Converter.toDate(data);

                if (_.isDate(date)) {
                    var registrationDate = Converter.toDate(new Common.FirmRequisites().get(attr));
                    if (comparator(date, registrationDate)) {
                        return {message: args.msg || '', options: args};
                    }
                }
            }

            return { valid: true, options: { field: args.field }};
        };
    }

    function less(a, b){
        return a < b;
    }

    function lessOrEqual(a, b){
        return a <= b;
    }

    function createErrorObject(args) {
        var errorMsg = args.msg || '';
        return { message: errorMsg, options: args };
    }
    
    function returnValidObject(field) {
        return { valid: true, options: { field: field }};
    }

})(Common);