import _ from 'underscore';

const validation = Backbone.Validation;

validation.configure({
    forceUpdate: true,
    selector: 'data-bind'
});

_.extend(validation.callbacks, {
    valid(view, attr, selector) {
        const prefix = this.prefix || '';

        const viewAttr = prefix + attr.replace(/\./g, '_');
        const $el = view.$(`[${selector}=${viewAttr}]`);

        $el.removeClass('input-validation-error');
        if ($el.is('[data-collapsible]')) {
            $el.closest('.collapsible-container').find('span.collapsible').removeClass('input-validation-error');
        }

        const $errorMessageContainer = view.$(`[data-valmsg-for=${viewAttr}]`);
        $errorMessageContainer.removeClass('field-validation-error').addClass('field-validation-valid').empty();
    },

    invalid(view, attr, error, selector) {
        const prefix = this.prefix || '';

        const viewAttr = prefix + attr.replace(/\./g, '_');
        let $el = view.$(`[${selector}=${viewAttr}]`),
            $errorMessageContainer = view.$(`[data-valmsg-for=${viewAttr}]`);

        $el.addClass('input-validation-error');
        if ($el.is('[data-collapsible]')) {
            const container = $el.closest('.collapsible-container');
            container.find('span.collapsible').addClass('input-validation-error');

            if (!$errorMessageContainer.length) {
                $errorMessageContainer = $(`<span data-valmsg-for='${viewAttr}'></span>`);
                container.append($errorMessageContainer);
            }
        } else if ($errorMessageContainer.length == 0) {
            $errorMessageContainer = $(`<span data-valmsg-for='${viewAttr}'></span>`);
            $el.after($errorMessageContainer);
        }

        $errorMessageContainer.empty().removeClass('field-validation-valid').addClass('field-validation-error');
        $errorMessageContainer.html(`<span>${error}</span>`);
    }
});
