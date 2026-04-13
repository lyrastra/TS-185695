(function (mainModule){

    'use strict';

    mainModule.Moedelo = mainModule.Moedelo || {
        Core: Core || {},
        Components: (Core && Core.Components) || {},
        Data: {},
        Applications: {}
    };

})(window);