(function(common) {
    common.Views.BaseView = Backbone.View.extend({
        getDataForRender: function() {
            var data = this.model || this.collection;
            if (data) {
                return data.toJSON();
            }
        },

        rendered: false,

        render: function() {
            var view = this;

            TemplateManager.get(view.template, function (template) {
                view.$el.html(template);

                view.beforeRender();
                view.trigger("renderBegin");
                
                view.fillTemplate();

                view.rendered = true;
                view.onRender();
                view.initializeControls();
                view.trigger("renderComplete");
            }, this.templateUrl);
        },

        fillTemplate: function() {
            var data = this.getDataForRender();
            if (data) {
                this.$el.render(data, this.getDirectives());
            }
        },
        
        setLoadedTemplate: function() {
            this.$el.html(TemplateManager.getFromPage(this.template));
        },

        initializeControls: function() {
            
        },

        onRender: function () { },

        beforeRender: function() { },
        
        error: function (message) {
            Backbone.history.navigate("error/", { trigger: true });
        },
        
        getElementByEvent: function (event) {
            return event.target || event.toElement || event.srcElement;
        },
        
        getDirectives: function () { }
    });
})(Common);
