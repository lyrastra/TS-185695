(function (common) {
    common.Views.Dialogs.ConfirmDialogView = common.Views.BaseApplicationView.extend({
        template: "dialogs/ConfirmDialog",

        events: {
            "click #confirm": 'confirm',
            "click #cancel": "cancel",
            "click .cancel": "cancel"
        },

        initialize: function(options) {
            var defaultOptions = {                
                title: 'Внимание',
                okButton: "Подтвердить",
                cancelButton: "Отмена",
                templateUrl: this.templateUrl
            };
            
            this.options = _.extend({}, defaultOptions, options);
        },

        createContainer: function() {
            var dialogContainer = $("#confirmDialog");
            if (!dialogContainer.length) {
                dialogContainer = $("<div id='confirmDialog'></div>");
                $('body').append(dialogContainer);
            }
            
            dialogContainer.addClass('mdConfirmDialog');
            return dialogContainer;
        },

        setTitle: function() {
            var title = this.options.title;

            if (title === false) {
                this.$('header').hide();
                return;
            }
            
            this.$('header').show();
            this.$('.mdDialogTitle').html(title);
        },
        
        setConfirmButton: function () {
            var button = this.options.okButton;

            if (_.isString(button)) {
                this.$("#confirm .innerText").text(button);
            }
            
            if (_.isArray(button)) {
                this.$("#confirm").remove();

                _.each(button, function (button) {
                    var view = this,
                        el = $('<div class="mdButton rightMargin"><span class="innerText">' + button.name + '</span></div>');
                    el.on('click', function() {
                        _.result(button, 'confirm');
                    });
                    
                    this.$('.mdDialogFooter').prepend(el);
                }, this);
            }
        },

        setCancelButton: function () {
            var button = this.options.cancelButton;

            if (button === false) {
                this.$("#cancel").hide();
                return;
            }
            
            this.$("#cancel .innerText").text(button);
        },

        render: function (dialogOptions) {
            dialogOptions = dialogOptions || {};
            
            var view = this;
            this.setElement(this.createContainer());

            TemplateManager.get(view.template, function (template) {
                view.$el.html(template);
                view.$(".message").html(view.options.message);
                
                view.setCancelButton();
                view.setConfirmButton();
                view.setTitle();
                view.delegateEvents(view.events);
                
            }, view.options.templateUrl);
        },

        closeDialog: function() {
            this.undelegateEvents();
        },

        confirm: function () {
            this.closeDialog();
            _.result(this.options, 'confirm');
        },

        cancel: function () {
            this.closeDialog();
            _.result(this.options, 'cancel');
        }
    });

})(Common);
