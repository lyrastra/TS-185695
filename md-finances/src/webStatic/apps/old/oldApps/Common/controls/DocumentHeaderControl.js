import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';

/* eslint-disable */
(function (common) {
    'use strict';

    common.Controls.DocumentHeaderControl = common.Controls.BaseControl.extend({
        template: "DocumentHeaderControl",

        defaultOptions: {
            attrs: {
                status: 'Status'
            }
        },

        initialize: function (options) {
            this.options = _.extend({}, this.defaultOptions, options);

            common.Controls.BaseControl.prototype.initialize.call(this, this.options);
            common.Helpers.Mixer.addMixin(this, common.Mixin.OnChangeFieldMixin);
            if (this.options.template) {
                this.template = this.options.template;
            }
        },

        initializeVariables: function (options) {
            this.statusTypes = options.statusTypes;
            this.Controls = {};
        },

        events: {
            "blur #NumberDocument": "onBlurNumberInput",
            "change #DateDocument": "onChangeDateInput",
            "click .h1 span.input_text": 'onClickTitleSpan'
        },

        onRender: function () {
            if (this.$el.closest("form").length > 0 && this.options.validate !== false) {
                $.validator.unobtrusive.parseDynamicContent(this.$el);
            }

            this.useStatusChangerControl();
            this.setMinDate();
            this.setDocTitle();
            this.setMinDate();
            this.onBlurNumberInput();
            this.onChangeDateInput();
            setReadOnlyFields.call(this);
            showAccountingReadOnlyMessage.call(this);
        },

        setMinDate: function () {
            if (!this.model.get('MinDate')) {
                return;
            }

            var elem = this.$('#DateDocument');

            elem.attr("MinDateValue", this.model.get('MinDate'));
            elem.attr("data-val-minDate", 'Дата не может быть меньше ' + this.model.get('MinDate'));
            elem.val(this.model.get('Date'));
        },

        setDocTitle: function () {
            var name = _.result(this.model, "documentName");
            this.$(".docTitle").text(name);
        },

        onClickTitleSpan: function (event) {
            var $span = $(event.target || event.currentTarget);

            if($span.attr('readonly') || $span.attr('disabled')){
                return;
            }

            $span.hide();
            $span.next().css('display', 'inline-block');

            if (event.originalEvent) {
                var input = $span.next().find('input');
                input.focus().trigger('focus-date-input');
            }
        },

        onBlurNumberInput: function () {
            var input = this.$('#NumberDocument');
            var span = input.parent().prev();

            if (input.hasClass('input-validation-error')) {
                return;
            }

            var text = input.val();

            if (!$.trim(text).length) {
                this.onClickTitleSpan({
                    target: span
                });

                return;
            }

            input.parent().hide();

            span.text(text);
            span.show();
        },

        useStatusChangerControl: function () {
            if (!this.statusTypes || !this.statusTypes.length) {
                this.$(".status").remove();
                return;
            }

            this.statusChangerControl = new common.Controls.StatusChangerControl({
                model: this.model,
                el: this.$(".status"),
                statusTypes: this.statusTypes,
                attr: this.options.attrs.status
            });

            this.statusChangerControl.render();
        },

        onChangeDateInput: function (event) {
            var input = this.$(".h1 [name=Date]"),
                inputWrapper = input.closest(".input"),
                span = inputWrapper.prev(),
                text, date, format = "D MMMM",
                today = new Date();

            if (event && (this.options.validate !== false && !input.valid())) {
                this.onClickTitleSpan({
                    target: span
                });
                return;
            }

            if (input.hasClass("input-validation-error")) {
                this.onClickTitleSpan({
                    target: span
                });
                return;
            }

            text = input.val();
            date = window.Converter.toDate(text);

            if (!_.isDate(date) || isNaN(date.valueOf())) {
                return;
            }

            if (today.getFullYear() != date.getFullYear()) {
                format = 'D MMMM YYYY [г.]';
            }
            text = dateHelper(date).format(format);

            inputWrapper.hide();
            span.text(text);
            span.show();
        }
    });

    /** @access private */
    function setReadOnlyFields() {
        var readOnlyFields = this.model.readOnlyFields;
        if(_.contains(readOnlyFields, 'Date')){
            setReadOnlyFieldText.call(this, 'Date');
        }

        if(_.contains(readOnlyFields, 'Number')){
            setReadOnlyFieldText.call(this, 'Number');
        }
    }

    /** @access private */
    function setReadOnlyFieldText(field) {
        var $text = this.$('[data-text-for=' + field + ']');
        if($text.text().length){
            $text.attr('readonly', 'readonly');
        }
    }

    function showAccountingReadOnlyMessage() {
        var isDisplay = this.model.get('AccountingReadOnly');
        if (isDisplay){
            this.$('.h1').append('<span class="disabled-link field-validation-error" >Документ проведен бухгалтером и недоступен для редактирования.</span>');
        }
    }

})(Common);
