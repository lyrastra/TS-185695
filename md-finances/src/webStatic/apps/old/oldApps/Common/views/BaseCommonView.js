const templates = require.context(`../templates`, true, /\.html$/);

(function (common) {
    common.Views.BaseApplicationView = common.Views.BaseView.extend({
        templateUrl: templates
    });
})(Common);
