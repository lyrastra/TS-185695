(function (common) {
    common.Views.Dialogs.KontragentDialogView = common.Views.BaseApplicationView.extend({
        template: `dialogs/KontragentDialog`,

        messages: {
            process: "Сохранение контрагента",
            success: "Контрагент сохранен"
        },

        initialize: function (options) {
            this.saved = false;
            this.createDialogContainer();

            _.bindAll(this, "close", 'removeDialog');

            if (options.templateRootUrl) {
                this.templateUrl = options.templateRootUrl;
            }
        },

        createDialogContainer: function () {
            var dialogContainer = $("#AddKontragentDialog");
            if (dialogContainer.length == 0) {
                dialogContainer = $("<div></div>", {
                    'class': 'AddKontragentDialog',
                    'id': 'AddKontragentDialog'
                })
                .appendTo($('body'));
            }
            this.setElement(dialogContainer);
        },

        events: {
            "click #save": "save",
            "click #cancelAddKontragent": "close",
            "click .kontragentTypes .link": "changeType",

            "change input": "onChangeField",
            "change textarea": "onChangeField",

            "change #Inn": "changeInn"
        },

        onChangeField: function (event) {
            var element = $(event.currentTarget || event.target),
                fieldName = element.attr("name"),
                value = (element.is("input[type=checkbox]")) ? element.is(":checked") : element.val();

            this.model.set(fieldName, value);
        },

        changeInn: function () {
            var inn = this.$("#Inn").val();
            if ($.trim(inn).length == 10) {
                this.$("#Kpp").closest(".field").show();
            }
            else {
                this.$("#Kpp").val("").change().closest(".field").hide();
            }

        },

        initializeDialog: function () {
            if (this.$el.data('dialog')) {
                this.$el.dialog("destroy", this);
            }

            this.$el.dialog(
                {
                    dialogClass: "kontragentAddDialog",
                    draggable: false,
                    autoOpen: false,
                    modal: true,
                    resizable: false,
                    width: 640,
                    title: 'Новый контрагент',
                    close: _.bind(this.onCloseDialog, this),
                    position: 'center'
                });
        },

        onCloseDialog: function () {
            if (!this.saved) {
                this.model.trigger("cancel");
            }
            this.undelegateEvents();
        },

        render: function () {
            var view = this;

            TemplateManager.get(view.template, function (template) {
                view.$el.html(template);
                view.$("form").render(view.model.toJSON());

                $.validator.unobtrusive.parse(view.$el);
                view.initializeDialog();

                view.onRender();
                view.initializeControls();

                ToolTip.globalMessageClose();

                view.$el.dialog('open');

                view.changeInn();

            }, view.templateUrl);
        },

        onRender: function () {
            this.onHashChange();
            if ($.fn.checkKontragentByInn) {
                this.$el.checkKontragentByInn();
            }
        },

        onHashChange: function () {
            var view = this;

            if ("onhashchange" in window) {
                common.Utils.EventHelper.removeHandler(window, 'hashchange', view.removeDialog);
                common.Utils.EventHelper.addHandler(window, 'hashchange', view.removeDialog);
            } else {
                Backbone.history.off("route", view.removeDialog);
                Backbone.history.on("route", view.removeDialog);
            }
        },

        initializeControls: function () {
            var view = this,
                bankName = view.$("#BankName"),
                selectedValue,
                text;

            view.$('[watermark]').each(function () {
                text = $(this).attr('watermark');
                $(this).watermark(text);
            });

            bankName.saleBankAutocomplete({
                onSelect: function (item) {
                    selectedValue = item.object;

                    bankName.val(selectedValue.Name).change();
                    view.model.set({
                        Bik: selectedValue.Bik,
                        BankId: selectedValue.Id,
                        BankCorrespondentAccount: selectedValue.BankCorrespondentAccount
                    });

                    view.$("#SettlementAccountNumber").focus();
                }
            });
        },

        changeType: function (event) {
            var link = $(event.target),
                type = Converter.toInteger(link.attr("data-value"));

            this.model.set({ Type: type });

            this.$(".kontragentTypes").find(".selected").removeClass("selected").addClass("link");
            link.removeClass("link").addClass("selected");
        },

        save: function () {
            var view = this;
            var form = this.$('form');
            form.validate();
            if (form.valid()) {
                view.saved = true;
                view.$el.dialog('close');
                ToolTip.globalMessage(1, true, view.messages.process);
                view.model.save({}, {
                    success: function (model, response) {
                        if (response.Status) {
                            model.set({
                                Id: model.get("SavedId")
                            });
                            model.trigger("save");
                            ToolTip.globalMessage(1, true, view.messages.success);
                        }
                        else {
                            Backbone.history.navigate("error/", { trigger: true });
                        }
                    },
                    error: function () {
                        Backbone.history.navigate("error/", { trigger: true });
                    }
                });
            }
        },

        close: function () {
            this.$el.dialog("close");
        },

        removeDialog: function () {
            var view = this;
            if (view.$el.data("dialog")) {
                view.$el.dialog("destroy");
            }

            view.undelegateEvents();
            view.remove();

            if ("onhashchange" in window) {
                common.Utils.EventHelper.removeHandler(window, 'hashchange', view.removeDialog);
            } else {
                Backbone.history.off("route", view.removeDialog);
            }
        }
    });

})(Common);
