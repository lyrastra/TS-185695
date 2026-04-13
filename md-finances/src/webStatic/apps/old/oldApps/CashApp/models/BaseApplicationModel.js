(function (cash) {
    cash.Models.BaseApplicationModel = Backbone.Model.extend({
        postfetch: function (opt) {
            opt.type = 'POST';
            opt.dataType = "json";
            opt.contentType = "application/json; charset=utf-8;";
            Backbone.Model.prototype.fetch.call(this, opt);
        }
    });
})(Cash);