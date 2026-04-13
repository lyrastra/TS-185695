(function(bank) {

    bank.Views.PurseDocumentHelper = {
        disableForm: function ($form) {
            $form.find('.input_text').replaceWith(replaceWithText);
            $form.find('input, textarea, .mdCustomSelectWrap').each(disable);
            $form.find('.mdAutocomplete-closeIcon, .link.add').remove();
            $form.find('button').attr('disabled', 'disabled');
        }
    };

    function replaceWithText() {
        return $(this).text();
    }

    function disable() {
        var $control = $(this);
        if ($control.is('input')) {
            $control.attr('disabled', 'disabled');
        }

        if ($control.is('textarea')) {
            $control.attr('readonly', 'readonly');
        }

        if ($control.is('.mdCustomSelectWrap')) {
            $control.addClass('disabled').find('select').attr('disabled', 'disabled');
        }

        return $control;
    }

})(Bank);