const templates = require.context(`./templates`, true, /\.html$/);

(function(url) {
    var prefix = '/Accounting/';

    // eslint-disable-next-line no-param-reassign
    url.BaseTemplate = templates;
})(PrimaryDocuments.Urls);