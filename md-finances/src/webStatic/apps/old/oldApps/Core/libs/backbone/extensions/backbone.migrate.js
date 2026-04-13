(function () {
    'use strict';

    Backbone.Collection.prototype.getByCid = function () {
        return Backbone.Collection.prototype.get.apply(this, arguments);
    };

})();