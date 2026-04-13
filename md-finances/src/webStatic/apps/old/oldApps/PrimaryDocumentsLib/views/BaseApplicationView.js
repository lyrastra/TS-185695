const templates = require.context(`../templates`, true, /\.html$/);

(function (primaryDocuments) {
    primaryDocuments.Views.BaseApplicationView = Backbone.View.extend({
        templateUrl: templates
    });
})(PrimaryDocuments);
