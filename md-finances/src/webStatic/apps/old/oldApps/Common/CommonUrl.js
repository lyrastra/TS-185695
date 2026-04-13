const templates = require.context(`./templates`, true, /\.html$/);

(function(url) {
    // eslint-disable-next-line no-param-reassign
    url.BaseTemplate = templates;
})(Common.Urls);