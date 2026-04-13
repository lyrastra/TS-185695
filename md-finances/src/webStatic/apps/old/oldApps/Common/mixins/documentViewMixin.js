(function(common) {

    common.Mixin.DocumentViewMixin = {
        Events: {
            clickToTitleSpan: function (event) {
                var span = $(event.target || event.currentTarget);
                span.hide();
                span.next().css("display", "inline-block");

                if (event.originalEvent) {
                    var input = span.next().find("input");
                    input.focus();
                }
            },
            
            blurNumberInput: function (event) {
                var input = $(event.target || event.currentTarget),
                    span = input.parent().prev(), text = input.val();

                if (input.hasClass("input-validation-error")) {
                    return;
                }

                if ($.trim(text).length == 0) {
                    this.clickToTitleSpan({
                        target: span
                    });

                    return;
                }

                input.parent().hide();

                span.text(text);
                span.show();
            },

            blurDateInput: function (datepicker, value) {
                var input = $(datepicker),
                    wrapper = input.closest('.input'),
                    span = wrapper.prev(), text;

                if (input.hasClass("input-validation-error")) {
                    return;
                }

                text = common.Utils.Converter.getDateForDocument(value);

                wrapper.hide();
                span.text(text);
                span.show();
            },
            
            goToPreviosPage: function() {
                if (_.isUndefined(HistoryManager.goToPreviosPage())) {
                    window.location = _.result(this, 'backUrl');
                }
            }
        },
        Binder: {
            initBinderToView: function(view) {
                view.collectionBinder = new Backbone.MdCollectionBinder();
            },
            initializeCollectionBinder: function () {
                if (Backbone.MdCollectionBinder) {
                    this.collectionBinder = new Backbone.MdCollectionBinder();
                }
            },
            bindCollectionToView: function(options) {
                this.collectionBinder.bind(options.collection, { el: options.regionTable, $el: options.regionTable }, options.parentLineEl);
            },
            unbindCollectionView: function() {
                this.collectionBinder.unbind();
            }
        },
        DocumentPage: {
            actionSaveButton: function (action) {
                switch (action) {
                    case 'lock':
                        this.isSaved = true;
                        this.$(".mdButton").addClass("disabled");
                        break;
                    case 'unlock':
                        this.isSaved = false;
                        this.$(".mdButton").removeClass("disabled");
                        break;
                }
            }
        }
    };

})(Common);