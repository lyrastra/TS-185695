(function (common) {
    common.Controls.BaseControl = common.Views.BaseView.extend({
        events: {},

        initialize: function(options) {
            this.options = options || {};
            this.minDate = this.options.minDate;

            _.extend(this, common.Mixin.documentStateMixin);
            this.initializeVariables(options);
            this.initializeEvents();
            this.renderComplite = false;
        },

        validate: function() {
            return true;
        },

        initializeEvents: function() { },

        initializeVariables: function (options) { },

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
           this.$(".field-validation-error.lastError").remove();
        },

        render: function () {
            var template = TemplateManager.getFromPage(this.template);
            this.$el.html(template);
            this.fillTemplate();
            this.trigger('render');
            this.onRender();
            this.initializeControls();
            return this;
        },

        initializeControls: function() {
            var minDate = this.minDate || Converter.toDate(common.Utils.CommonDataLoader.FirmRequisites.getMinDocumentDate());

            this.$("input[data-format]").fieldValueConverter();

            this.$(".mdDatepicker")
                .mdDatepicker({ minDate: minDate });
        },

        determiningVisibilityOfParentEl: function () {
            var view = this,
                parentForm = view.$el.closest("form");
            return parentForm.length ? parentForm.is(":visible") : false;
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
                $(this).triggerHandler('change');
            });
        },

        clearTracesOfTheExistence: function () {
            if (this.localChangedValues) {
                var changedValues = this.localChangedValues.length ? this.localChangedValues : [];

                if (changedValues.length) {
                    _.each(changedValues, function (val) {
                        this.model.unset(val);
                    }, this);
                }
            }
        }
    });

})(Common);
