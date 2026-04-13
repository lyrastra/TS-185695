(function (base) {
    base.Helpers.TemplateHelper = {

        rootUrl: BankUrl.module('Main').BaseTemplate,

        setHtml: function (view, html) {
            view.templateHtml = html;
        },

        set: function (id, view, func) {
            var set = function(tmpl) {
                func(view, tmpl);
                view.isTemplateLoaded = true;
                view.trigger('LoadTemplateComplete');
                view.baseRender();
            };

            TemplateManager.get(id, set, this.rootUrl);
        },

        setElWithHtml: function (id, view) {
            this.set(id, view, this.setHtml);
        }
    };

})(Bank.module('Main'));