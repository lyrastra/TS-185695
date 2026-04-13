(function (primaryDocuments) {
    primaryDocuments.Views.Documents.Actions.DeleteDialogView = primaryDocuments.Views.BaseApplicationView.extend({
        template: 'Documents/Actions/DeletingDialog',

        events: {
            "click .simplemodal-close": function (event) {
                this.close();
                return false;
            },
            "hover .simplemodal-close": function (event) {
                $(event.target).toggleClass('ui-state-hover');
            },
            "click .mdButton": "success"
        },

        initialize: function(options = {}) {
            this.options = options;
            this.dialogId = "PrimaryDocumentDeleteDialog";
            _.bindAll(this, 'close');
        },

        getDeleteDialogRenderOptions: function(anchor){
            var offset = anchor.offset(),
                dialogWidth = 320;

            return {
                    top: offset.top + anchor.outerHeight() - 1,
                    left: offset.left - dialogWidth/2 + anchor.outerWidth()/2
                };
        },

        render: function(options) {
            options = options || { };
            if(!options.css && options.anchor) {
                options.css = this.getDeleteDialogRenderOptions(options.anchor);
            }

            var view = this;
            _.extend(view.options, options);

            var container = $("#" + view.dialogId);
            if (container.length == 0) {
                container = $("<div></div>", {
                    'class': "documentDeleteDialog",
                    'id':view.dialogId
                });
                container.hide();
                $('body').append(container);
            }

            this.$el = container;
            
            TemplateManager.get(this.template, function(template) {
                view.$el.html(template);

                if(options.message) {
                    view.$el.find(".dialogContent").find("span").html(options.message);
                }

                view.$el.css(options.css);
                view.delegateEvents(view.events);

                view.open();
            }, this.templateUrl);

            return view;
        },

        listenClick: function() {
            var view = this;
            
            $("body").on("click.primaryDocumentDeleteDialog", function (event) {
                var elem = $(event.target);
                if (elem.closest("#" + view.dialogId).length == 0) {
                    if (view.$el.is(":visible")) {
                        view.close();
                    }
                }
            });
        },

        onHashChange: function () {
            var view = this;

            if ("onhashchange" in window) {
                window.removeEventListener("hashchange", view.close, false);
                window.addEventListener("hashchange", view.close, false);
            } else {
                Backbone.history.off("route", view.close);
                Backbone.history.on("route", view.close);
            }
        },

        close: function (event) {
            var view = this;
            view.$el.slideUp("fast", 'linear', function () {
                view.trigger("close");
                $("body").off("click.primaryDocumentDeleteDialog");
                
                if ("onhashchange" in window) {
                    window.removeEventListener("hashchange", view.close, false);
                } else {
                    Backbone.history.off("route", view.close);
                }
            });
        },

        open: function () {
            var view = this;
            view.canDeleteMessage();
            this.$el.slideDown("fast", 'linear', function() {
                view.trigger("open");
                view.listenClick();
                view.onHashChange();
            });
        },
        
        success: function (event) {
            event.stopPropagation();
            this.trigger("success");
            this.close();
        },
        
        canDeleteMessage: function() {
            if (this.options.deleteMessage) {
                this.$('.deleteMessage').html(this.options.deleteMessage).show();
            } else {
                this.$('.deleteMessage').hide();
            }
            
            if (this.options.notDelete) {
                this.$(".dialog_buttons .button").attr("disabled", "disabled");
                this.$(".dialog_buttons .mdButton").addClass("disabled");
            }
        }
    });

})(PrimaryDocuments);
