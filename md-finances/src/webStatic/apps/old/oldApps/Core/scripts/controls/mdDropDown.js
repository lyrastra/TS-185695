$(function () {
   
    $(document, "input[type=checkbox]").click(function (event) {
        var element = $(event.target);

        var currentDropDown = element.closest('[data-mdDropDown]'),
            current = $('[data-mdDropDown]').index(currentDropDown);

        if (currentDropDown.find('[data-mdDropDown-link]').length) {
            event.preventDefault();
            currentDropDown.find('[data-mdDropDown-list]').toggle();
            $('[data-mdDropDown-list]').filter(':not(:eq(' + current + '))').hide();
        } else {
            $("[data-mdDropDown-list]").hide();
        }
    });
    
});