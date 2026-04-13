(function (cash) {
    'use strict';

    cash.Utils.Mapper = function () {
        return {
            toFormData: function (params) {
                return params = _.map(params, function (param, name) {
                    var val = param;
                    if (_.isArray(val)) {
                        return _.map(val, function (arrayVal, index) {
                            var paramName = name + '[' + index + ']';

                            if (_.isObject(arrayVal)) {
                                return _.map(arrayVal, function (objVal, objName) {
                                    return paramName + '.' + objName + '=' + objVal;
                                }).join('&');
                            }

                            return paramName + '=' + arrayVal;
                        }).join('&');
                    }
                    return name + '=' + val;
                }).join('&');
            }
        };
    }();

}(Cash));