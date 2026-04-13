(function(money) {
    'use strict';

    money.Models.Common.Project = Backbone.Model.extend({
        url: WebApp.Projects.GetProjectNumber,

        fetch: function(options) {
            options = _.extend(options, {
                data: {
                    id: this.get('id')
                }
            });

            Backbone.Model.prototype.fetch.call(this, options);
        }

    });

})(Money.module('Models.Common'));
