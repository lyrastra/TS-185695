(function (stockModule) {
    stockModule.Helpers.TemplateHelper = {
        templates: {},

        rootUrl: StockUrl.module('Main').BaseTemplate,

        setTemplate: function (view, tmpl) {
            $(view.el).html(_.template(tmpl, view.model.toJSON()));
        },

        setHtml: function (view, html) {
            var $el = $(view.el);
            $el.html(html);
        },

        set: function (id, view, func, rootUrl) {
            var set = function (tmpl) { func(view, tmpl); view.isTemplateLoad = true; view.trigger('LoadTemplateComplete'); };
            
            if (this.templates[id]) {
                set(this.templates[id]);
            } else {
                TemplateManager.get(id, set, rootUrl || this.rootUrl);
            }            
        },

        setElWithTemplate: function (id, view, rootUrl) {
            this.set(id, view, this.setTemplate, rootUrl);
        },

        setElWithHtml: function (id, view, rootUrl) {
            this.set(id, view, this.setHtml, rootUrl);
        }
    };

})(Stock);