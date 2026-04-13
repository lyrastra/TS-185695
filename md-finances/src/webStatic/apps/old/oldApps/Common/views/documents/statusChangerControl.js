(function (common, converter) {
    'use strict';
    common.Controls.StatusChangerControl = common.Views.BaseView.extend({
        defaultOptions: {
            attr: 'Status'
        },

        initialize: function (options) {
            this.options = _.extend({}, this.defaultOptions, options);

            this.model.on('change:' + this.options.attr, this.setStatus, this);
            this.initializeVariables(options);
        },

        initializeVariables: function (options) {
            this.statusTypes = options.statusTypes;
        },

        events: {
            "click .selected": "openStatusList",
            "click .statusList li": "changeStatus"
        },
        
        render: function() {
            this.beforeRender();
            this.onRender();
        },

        onRender: function () {
            this.formatStatusBar();
            this.setDefaultStatusValue();
            this.listenForClickNotOpenStatusList();
            this.setStatus();
        },

        formatStatusBar: function () {
            var statusTypes = this.statusTypes,
                statusItems = this.$(".statusList li"),
                consist;

            if (!statusTypes || !statusTypes.length) {
                this.$el.remove();
                this.model.unset(this.options.attr);
            } else {
                _.each(statusItems, function (val) {
                    consist = false;
                    _.filter(statusTypes, function (item) {
                        if ($(val).attr("data-val") == item) {
                            consist = true;
                        }
                    });

                    if (!consist) {
                        $(val).remove();
                    }
                });
            }
        },

        listenForClickNotOpenStatusList: function () {
            var view = this;
            $(document, "input[type=checkbox]").click(function (event) {
                var element = $(event.target);
                if (!element.closest(".status").length) {
                    view.$(".statusList").hide();
                }
            });
        },

        openStatusList: function (event) {
            if(this.$el.is('.disabled')){
                return;
            }

            var statusBlock = $(event.target).closest(".status"),
                list = statusBlock.find(".statusList");

            if (list.is(":visible")) {
                list.hide();
            }
            else {
                list.show();
            }
        },
        
        setDefaultStatusValue: function() {
            var status = this.model.get(this.options.attr),
                firstItemValue = this.$(".statusList li").first().attr("data-val");
            
            if (status) {
                return;
            }

            this.model.set("firstItemValue", firstItemValue, { silent: true });
        },
        
        changeStatus: function (event) {
            var link = $(event.target),
                status = converter.toInteger(link.attr("data-val"));
          
            this.model.set(this.options.attr, status);
            
            this.setStatus();

            link.closest(".statusList").hide();
        },
      
        setStatus: function () {
            var status = this.model.get(this.options.attr),
                statusText = this.$(".statusList li").filter("[data-val=" + status + "]").text();

            this.$(".selected").attr("data-value", status).text(statusText);
        }
    });

})(Common, Converter);