(function (bank) {

    bank.Views.BaseView = Backbone.View.extend({
        isTemplateLoaded: false,
        isModelLoaded: false,

        rootUrl: BankUrl.BaseTemplate,
        templateUrl: BankUrl.BaseTemplate,

        templateHtml: '',

        setHtml: function (view, html) {
            view.templateHtml = html;
        },
     
        set: function (id, view, func) {
            var set = function (tmpl) {
                func(view, tmpl);
                view.isTemplateLoaded = true;
                view.trigger('LoadTemplateComplete');
                view.baseRender();
            };

            TemplateManager.get(id, set, this.rootUrl);
        },

        baseRender: function () {
            if (this.isTemplateLoaded && this.isModelLoaded) {
                this.$el.html(this.getJqTransparencyTemplate(this.templateHtml, this.model.toJSON(), this.getDirectives()));
                this.trigger("BaseRenderViewComplete");
            }
        },

        getJqTransparencyTemplate: function (html, object, directives) {
            return $(html).render(object, directives);
        }
    });

})(Bank);
