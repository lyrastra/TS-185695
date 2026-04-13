(function (bank, common) {
    bank.Views.Controls.BaseControl = bank.Views.BaseView.extend({
        events: {},

        initialize: function (options) {
            _.extend(this, common.Mixin.documentStateMixin);
            this.renderComplite = false;
        },
        
        render: function () {
            var view = this;
            
            TemplateManager.get(view.template, function (template) {
                var content = $(template).render(view.model.toJSON(), view.getDirectives());
                view.$el.html(content);
                view.delegateEvents(view.events);
                view.globalCloserInitialize();
                $.validator.unobtrusive.parseDynamicContent(view.$el);

                if (view.model.get("readOnly") == true) {
                    view.documentStateMixin.makeReadOnly();
                }
                view.onRender();
                view.initializeControls();
                view.trigger("renderComplite");
            }, this.templateUrl);
        },
        
        initializeControls: function () {
            var selects = this.$("select");
            selects.each(function () {
                $(this).mdSelect();
            });

            this.$("input[data-format]").mdNumberInputMask({ needAddition: true });

            this.$(".mdDatepicker")
                .removeClass("hasDatepicker")
                .val(this.model.get("DocumentDate")).change()
                .mdDatepicker({ minDate: Converter.toDate(common.Utils.CommonDataLoader.FirmRequisites.getRegistrationDate()) });
        },

        onChangeField: function (event) {
            var element = $(event.target || event.currentTarget);
            var fieldName = element.attr("name");
            var value;
            if (element.is("input[type=checkbox]")) {
                value = element.is(":checked");
            }
            else {
                value = element.val();
            }
            this.model.set(fieldName, value);
        },

        onRender: function () {
            var view = this;
        },

        globalCloserInitialize: function () {
            var view = this;
            $(document).bind("click.operation", function (e) {
                if ($(e.target).closest(".transparentSelect .innerText, .transparentSelect .direction").length) {
                    return;
                }
                view.closeOperationsList();
            });
        },
        
        closeOperationsList: function () {
            this.$(".transparentSelect .mdDropDownList").slideUp("fast");
        },

        clearSignsOfExistence: function () {
            this.model.destroy();
            delete this.model;
            this.undelegateEvents();
            this.$el.removeData().unbind();
            $(document).unbind("click.operation");
        },

        validate: function () {
            return true;
        },

        tableValid: function () {
            var errors = this.model.validateModel();
            var error = _.find(errors, function (error) {
                return error.message && error.message.length;
            });
            if (error) {
                this.showErrors(error.message);
                return false;
            }
            this.removeErrorMesage();

            return true;
        },

        showErrors: function (message) {
            var container = this.$(".field-validation-error.lastError");
            if (!container.length) {
                container = $("<span></span>");
                container.addClass("field-validation-error lastError");
                this.$el.append(container.append($("<span></span>")));
            }
            container.find("span").html(message);
        },

        removeErrorMesage: function () {
            var container = this.$(".field-validation-error.lastError");
            container.remove();
        },

        determiningVisibilityOfParentEl: function () {
            var view = this,
                parentForm = view.$el.closest("form"),
                isParentVisible = parentForm.length ? parentForm.is(":visible") : false;
            return isParentVisible;
        },

        show: function () {
            if (this.determiningVisibilityOfParentEl()) {
                this.$el.slideDown("fast");
            } else {
                this.$el.show();
            }

            this.returnTracesOfTheExistence();
        },

        hide: function () {
            if (this.determiningVisibilityOfParentEl()) {
                this.$el.slideUp("fast");
            } else {
                this.$el.hide();
            }
            this.clearTracesOfTheExistence();
        },

        returnTracesOfTheExistence: function() {
            var view = this,
                inputs = view.$("input").not("[type=checkbox]");

            $.each(inputs, function() {
                $(this).change();
            });
        },

        clearTracesOfTheExistence: function () {
            var view = this,
                changedValues = view.localChangedValues.length ? view.localChangedValues : [];

            if (changedValues.length) {
                _.each(changedValues, function (val) {
                    view.model.unset(val);
                });
            }
            changedValues = null;
        },

        //на будущее
        getDirectives: function () {
            var view = this,
                directives = {
                    
                };
            return directives;
        }
    });

    bank.Views.Controls.BaseControl.extend = function (protoProps, staticProps) {
        if (protoProps.events) {
            protoProps.events = _.extend(_.clone(this.prototype.events), protoProps.events);
        }
        return Backbone.View.extend.call(this, protoProps, staticProps);
    };

})(Bank, Common);
