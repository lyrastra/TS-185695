(function (mainModule) {

    'use strict';

    mainModule.Template = function () {

        return {
            get: function (templateId) {
                return $('#' + templateId).html();
            }
        };

    }();

})(Moedelo.Core);