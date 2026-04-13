(function (base) {

    base.Views.BaseView = Backbone.View.extend({

        isTemplateLoaded: false,
        isModelLoaded: false,

        rootUrl: Cash.Data.RootPathPage,
        templateUrl: Cash.Data.BaseTemplate,

        templateHtml: "",
        paginator: {},
        
        modelBinded: false,

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

        loadTemplate: function (tmplId) {
            var self = this;
            TemplateManager.get(tmplId, function (tmpl) {
                self.isTemplateLoaded = true;
                self.templateHtml = tmpl;
                self.trigger('LoadTemplateComplete');
            }, self.templateUrl);
        },

        loadModel: function (data) {
            var self = this;
            self.model.fetch({
                data: data,
                success: function () {
                    self.isModelLoaded = true;
                    self.trigger('LoadModelComplete');
                }
            });
        },

        setTemplate: function () {
            this.set(this.template, this, this.setHtml);
        },
        
        baseRender: function () {
            if (this.isTemplateLoaded && this.isModelLoaded) {
                var directives = null;
                if (this.getDirectives) {
                    directives = this.getDirectives();  
                }
                this.$el.html(this.getJqTransparencyTemplate(this.templateHtml, this.model.toJSON(), directives));
                this.trigger("BaseRenderViewComplete");
            }
        },

        getJqTransparencyTemplate: function (html, object, directives) {
            return $(html).render(object, directives);
        },

        errorLoadDialog: function() {
            ToolTip.globalMessage(1, false, AjaxErrorsResource.AjaxError, true);
        },
        
        unificationValidationFields: function () {
            var view = this,
                fields = view.$('[name]'),
                validationMessages = view.$('[data-valmsg-for]');

            $.each(fields, function (ind, val) {
                var newName = $(val).attr("name") + view.serialNumber;
                $(val).attr("name", newName);
            });

            $.each(validationMessages, function (ind, val) {
                var newName = $(val).attr("data-valmsg-for") + view.serialNumber;
                $(val).attr("data-valmsg-for", newName);
            });
        },
        
        getFilters: function (filter, items) {
            _.each(filter, function (fieldValue, key) {
                if (!_.isUndefined(fieldValue)) {
                    var item = { Key: key };
                    if (_.isObject(fieldValue)) {
                        var obj = "{";
                        _.each(fieldValue, function(val, valKey) {
                            obj += valKey + ":" +  val + ",";
                        });
                        item.Value = obj + "}";
                    } else if (fieldValue) {
                        item.Value = fieldValue.toString();
                    }
                    items.push(item);
                }
            });

            return items;
        },
        
        showLoadingMessage: function() {
            ToolTip.globalMessage(1, true, "Подождите, идет загрузка...", "endless");
        },
        
        hideLoadingMessage: function() {
            ToolTip.globalMessageClose();
        },
        
        goBack: function () {
            if (window.history.length == 1) {
                this.gotoMainPage();
            } else {
                window.history.back();
            }
        },

        gotoMainPage: function() {
            base.CashRouter.navigate("", { replace: true, trigger: true });
        },

        onError: function () {
            this.hideLoadingMessage();
            base.CashRouter.navigate("error/", { replace: true, trigger: true });
        },
        
        bindModelToView: function () {
            if (!this.binder) {
                this.binder = new Backbone.MdModelBinder();
            }

            if (this.modelBinded) {
                this.binder.unbind();
            }
            this.binder.bind(this.model, this);
            this.modelBinded = true;
        }
    });
})(Cash);
