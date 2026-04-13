const templates = require.context(`../../../../templates`, true, /\.html$/);

(function (sales) {
    sales.Views.Table.Operations.StatusChanger = Backbone.View.extend({
        title: '',

        template: 'table/operations/StatusChanger',
        templateUrl: templates,

        initialize: function (options) {
        },

        events: {
            "click .cancel": "withoutPayment",
            "click .create": "makePayment"
        },

        dialogWidth: 500,
   
        render: function (options) {
            var view = this;
            var templateUsed = view.template;

            TemplateManager.get(templateUsed, function (template) {

                view.$el.html(template);
                
                view.model = options.model;
                    
                if (view.model.get("statusPaymentsPopup") && view.model.get("statusPaymentsPopup").length) {
                    view.$el.render(view.model.toJSON(), view.getDirectives());
                        
                    view.element = view.$el.find(".statusPaymentsPopup").unwrap();
                }
                view.trigger("render");

                
            }, this.templateUrl);

            return view;
        },
        
        makePayment: function () {
            this.closeDialog();
            this.yesFunction();
        },

        centeringPopupWindow: function () {
            var view = this;
            if (view.element) {
                var leftShift = "-" + (view.element.outerWidth() / 2 - 10) + "px";
                view.element.css("left", leftShift);
            }
        },

        closeDialog: function () {
            this.$el.dialog("close");
        },

        withoutPayment: function () {
            this.closeDialog();
            this.noFunction();
        },

        getDirectives: function () {
            var directives = {
                
                statusPaymentsPopup: {
                  summ: {
                      text: function () {
                          return ValueCrusher.parseStr(this.Sum);
                      }
                  },
                  
                  date: {
                      text: function() {
                          return this.Date;
                      }
                    }
                }
            };
            return directives;
        }
    });
})(Sales.module("Views.Table.Operations"));