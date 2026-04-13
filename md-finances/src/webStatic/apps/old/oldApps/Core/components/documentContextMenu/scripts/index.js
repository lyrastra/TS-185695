(function (components) {

    'use strict';

    components.DocumentContextMenu = function (options) {

        if (options && !options.$box) {
            throw 'parent box to be defined';
        }

        this.options = options || {};

    };

    components.DocumentContextMenu.prototype.render = function () {
        this.options.$box.html(this.options.template);
    };

})(Core.Components);