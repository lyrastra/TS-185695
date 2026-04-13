(function () {
    'use strict';

    var selectors = {
        wrapper: '.mdInput-group, .mdCustomSelect, .datepickerWrapper'
    };

    Backbone.configureValidation = _.once(function(){
        Backbone.Validation.configure({
            forceUpdate: true,
            selector: 'data-bind'
        });

        _.extend(Backbone.Validation.callbacks, {
            valid: function (view, attr, selector) {
                var prefix = this.prefix || '';

                var viewAttr = prefix + attr.replace(/\./g, '_');
                var $el = view.$('[' + selector + '=' + viewAttr + ']');
                var wrapper = $el.closest(selectors.wrapper);

                wrapper.length && wrapper.removeClass('input-validation-error');

                $el.removeClass('input-validation-error');
                if ($el.is('[data-collapsible]')) {
                    $el.closest('.collapsible-container').find('span.collapsible').removeClass('input-validation-error');
                }

                $el.removeClass('input-validation-error');
                var $errorMessageContainer = view.$('[data-valmsg-for=' + attr + ']');
                $errorMessageContainer.removeClass('field-validation-error').addClass('field-validation-valid').empty();
            },

            invalid: function (view, attr, error, selector) {
                var prefix = this.prefix || '';

                var viewAttr = prefix + attr.replace(/\./g, '_');
                var $el = view.$('[' + selector + '=' + viewAttr + ']');
                var wrapper = $el.closest(selectors.wrapper);

                $el.addClass('input-validation-error');
                var $errorMessageContainer = view.$("[data-valmsg-for=" + attr + "]");
                if ($errorMessageContainer.length == 0) {
                    $errorMessageContainer = $("<span data-valmsg-for='" + attr + "'></span>");

                    if(wrapper.length){
                        wrapper.addClass("input-validation-error");
                        wrapper.after($errorMessageContainer);
                    }
                    else{
                        $el.after($errorMessageContainer);
                    }
                }

                $errorMessageContainer.empty().removeClass('field-validation-valid').addClass('field-validation-error');
                $errorMessageContainer.html("<span>" + error + "</span>");
            }
        });
    });

    $(function () {
        Backbone.configureValidation();
    });
})();