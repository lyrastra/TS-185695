const templates = require.context(`../../../../templates`, true, /\.html$/);

(function (sales) {
    sales.Views.Table.Operations.Commenting = Backbone.View.extend({
        
        templateUrl: templates,
        template: 'table/operations/Commenting',

        events: {
            "click .commentWindow .saveComment": "saveComment",
            "click .close": "windowReduction"
        },

        model: new sales.Models.Main.Commenting(),
        
        initialize: function (options) {
            var view = this;
            view.windowClose();
        },

        render: function () {
            var view = this,
                commentElement = view.$(".commentWindow"),
                commentText = commentElement.find("p").text();

            TemplateManager.get(view.template, function (template) {
                commentElement.hide();

                if (commentElement.hasClass("small")) {
                    commentElement.addClass("overall").removeClass("small")
                              .fadeIn("fast")
                              .prepend($(template).find(".close"), $(template).find("textarea"), $(template).find("footer"))
                              .find("p").hide();
                }
                else {
                    commentElement.removeClass("active");
                }
                
                view.$(".commentWindow").find("textarea").text(commentText);
                
                view.trigger("renderComplite");
                view.onRender();
            }, this.templateUrl);

            return view;
        },

        onRender: function () {
            this.delegateEvents();
            this.setWatermark();
        },

        windowClose: function () {
            var view = this;
            $(document).off(".comment");
            $(document).on("click.comment", function (e) {

                if ($(e.target).closest(view.$el.find(".ttAbout")).length) {
                    return;
                }
                
                view.windowReduction();
                
            });
        },

        saveComment: function () {
            var view = this,
                id = view.$el.data("id"),
                doctype = view.$el.find(".ttInfo").data("doctype"),
                status = view.$el.find('input[type=radio]:checked').data("status"),
                commentText = view.$el.find("textarea").val(),
                oldCommentText = view.$el.find("p").text();

            var params = {
                "Id": id,
                "DocumentType": doctype,
                "Comment": commentText
            };

            if (!status) {
                status = view.$el.find(".ttInfo").data("status");
            }

            if ( oldCommentText !== commentText){

                view.model.set(params);
                view.model.save({},{
                        success: function(e) {

                            if (e.get("Status")) {
                                view.$el.find(".commentWindow").find("p").text(commentText)
                                    .closest("li").find(".ttInfo").attr('data-status', status);

                                if (commentText.length) {
                                    view.$el.find(".ttAbout a").addClass("commented");
                                } else {
                                    view.$el.find(".ttAbout a").removeClass("commented");
                                }

                            
                                ToolTip.globalMessage(1, true, view.model.get("successText"));
                            } else {
                                ToolTip.globalMessage(1, false, view.model.get("errorText"));
                            }
                        },
                        error: function() {
                            ToolTip.globalMessage(1, false, view.model.get("errorText"));
                        }                
                    });
            }
        
            view.windowReduction();
        },

        setWatermark: function () {

            this.$('textarea[watermark]').each(function (index, value) {
                var text = $(this).attr('watermark');
                $(this).watermark(text);
            });
        },

        windowReduction: function () {
            this.$(".commentWindow.overall").fadeOut("fast", function () {
                $(this).removeClass("overall")
                    .addClass("small")
                    .removeAttr("style")
                    .find("textarea").remove().end()
                    .find(".close").remove().end()
                    .find("label").remove().end()
                    .find("footer").remove().end()
                    .find("p").show().end();
            });
            
            this.$('.ttAbout').removeClass("active");
            this.$('.ttAbout a').removeClass("blocked");
        }
       
    });

})(Sales);
